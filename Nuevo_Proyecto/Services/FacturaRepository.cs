using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Catalogos;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Services
{
    public class FacturaRepository : IFacturaRepository
    {
        public const string EstadoActiva = "ACTIVA";
        public const string EstadoAnulada = "ANULADA";

        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;

        public FacturaRepository(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
        }

        // ------------------------------------------------------------------ consultas

        public string GetSiguienteNumero()
        {
            using var db = _factory.CreateDbContext();
            return CalcularSiguienteNumero(db);
        }

        private static string CalcularSiguienteNumero(Dev_ComideriaDbContext db)
        {
            var numeros = db.Facturas.AsNoTracking().Select(f => f.Numero).ToList();
            // Formato incremental: FACT-001, FACT-002... FACT-999 → FACT-000-001 → FACT-999-999 → FACT-000-000-001
            return CodigoGenerator.SiguienteAgrupado("FACT", numeros, "FACT-001");
        }

        public IReadOnlyList<FacturaResumenDto> GetTodas()
        {
            using var db = _factory.CreateDbContext();
            return Resumen(db.Facturas.AsNoTracking());
        }

        public IReadOnlyList<FacturaResumenDto> BuscarPorNumero(string prefijoNumero)
        {
            using var db = _factory.CreateDbContext();
            return Resumen(db.Facturas.AsNoTracking().Where(f => f.Numero != null && f.Numero.ToUpper() == prefijoNumero.ToUpper()));
        }

        public IReadOnlyList<FacturaResumenDto> BuscarPorFechaEspecifica(DateTime fecha)
        {
            using var db = _factory.CreateDbContext();
            // Crear el inicio y fin del día específico
            var inicio = fecha.Date;  // Comienza a las 00:00:00
            var fin = Proyecciones.FinDeDia(fecha);  // Termina a las 23:59:59
            return Resumen(db.Facturas.AsNoTracking().Where(f => f.Fecha >= inicio && f.Fecha <= fin).OrderByDescending(f => f.Fecha));
        }

        public IReadOnlyList<FacturaResumenDto> FiltrarPorFecha(DateTime desde, DateTime hasta)
        {
            using var db = _factory.CreateDbContext();
            var fin = Proyecciones.FinDeDia(hasta);   // antes se perdía todo el último día
            return Resumen(db.Facturas.AsNoTracking().Where(f => f.Fecha >= desde && f.Fecha <= fin));
        }

        public ResumenVentasDto GetResumenVentas(DateTime desde, DateTime hasta)
        {
            using var db = _factory.CreateDbContext();
            var fin = Proyecciones.FinDeDia(hasta);

            var vigentes = db.Facturas.AsNoTracking()
                .Where(f => f.Fecha >= desde && f.Fecha <= fin && f.Estado != EstadoAnulada);

            return new ResumenVentasDto
            {
                Total = vigentes.Sum(f => (decimal?)f.Total) ?? 0m,
                CantidadFacturas = vigentes.Count()
            };
        }

        private static List<FacturaResumenDto> Resumen(IQueryable<Facturas> query) =>
            query.OrderByDescending(f => f.Fecha)
                 .Select(f => new FacturaResumenDto
                 {
                     Numero = f.Numero ?? string.Empty,
                     Fecha = f.Fecha,
                     Total = f.Total,
                     Empleado = f.Empleado.Nombre,
                     FormaPago = f.FormaPago.Nombre,
                     Estado = f.Estado
                 })
                 .ToList();

        public IReadOnlyList<FacturaDetalleDto> GetDetallePorNumero(string numero)
        {
            using var db = _factory.CreateDbContext();
            return db.DetalleFactura.AsNoTracking()
                .Where(d => d.Factura.Numero == numero)
                .OrderBy(d => d.DetalleId)
                .Select(d => new FacturaDetalleDto
                {
                    Numero = d.Factura.Numero ?? string.Empty,
                    Fecha = d.Factura.Fecha,
                    Cliente = d.Factura.Cliente != null ? d.Factura.Cliente.Nombre : ClienteRepository.NombreConsumidorFinal,
                    Empleado = d.Factura.Empleado.Nombre,
                    FormaPago = d.Factura.FormaPago.Nombre,
                    Estado = d.Factura.Estado,
                    Producto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    Precio = d.PrecioUnitario,
                    Subtotal = d.subtotal
                })
                .ToList();
        }

        // ------------------------------------------------------------------ crear

        public ResultadoFacturaDto Crear(NuevaFacturaDto dto)
        {
            Validar(dto);

            // El número se calcula dentro de la transacción; si dos cajas chocan en el mismo número
            // el índice único lo rechaza y se reintenta con el siguiente.
            const int maxIntentos = 3;
            for (int intento = 1; ; intento++)
            {
                try
                {
                    return IntentarCrear(dto);
                }
                catch (DbUpdateException ex) when (EsNumeroDuplicado(ex) && intento < maxIntentos)
                {
                    // reintentar con un contexto nuevo
                }
            }
        }

        private static void Validar(NuevaFacturaDto dto)
        {
            if (dto.Lineas == null || dto.Lineas.Count == 0)
                throw new InvalidOperationException("La factura no tiene productos.");
            if (dto.EmpleadoId <= 0)
                throw new InvalidOperationException("Debe indicar el empleado que atiende.");
            if (dto.FormaPagoId <= 0)
                throw new InvalidOperationException("Debe indicar la forma de pago.");
            if (dto.Lineas.Any(l => l.Cantidad <= 0))
                throw new InvalidOperationException("Todas las cantidades deben ser mayores a cero.");
        }

        private static bool EsNumeroDuplicado(DbUpdateException ex) =>
            ex.InnerException is SqlException sql && (sql.Number == 2627 || sql.Number == 2601);

        private ResultadoFacturaDto IntentarCrear(NuevaFacturaDto dto)
        {
            using var db = _factory.CreateDbContext();
            using var tran = db.Database.BeginTransaction();

            // --- productos con precio vigente (el precio NUNCA viene de la pantalla)
            var ids = dto.Lineas.Select(l => l.ProductoId).Distinct().ToList();
            var productos = db.Productos.AsNoTracking()
                .Where(p => ids.Contains(p.ProductoId))
                .ToDictionary(p => p.ProductoId);

            foreach (var id in ids)
            {
                if (!productos.TryGetValue(id, out var p))
                    throw new InvalidOperationException($"El producto con id {id} no existe.");
                if (p.Activo != true)
                    throw new InvalidOperationException($"El producto '{p.Nombre}' está inhabilitado y no se puede facturar.");
            }

            // --- cliente (consumidor final si no se eligió ninguno)
            Cliente? cliente = null;
            if (dto.ClienteId.HasValue && dto.ClienteId.Value > 0)
                cliente = db.Clientes.FirstOrDefault(c => c.ClienteId == dto.ClienteId.Value);
            cliente ??= ClienteRepository.BuscarOCrearConsumidorFinal(db);

            // --- encabezado y detalle
            var estadoId = db.Estados.AsNoTracking()
                .Where(e => e.NombreEstado == EstadoActiva)
                .Select(e => (int?)e.EstadoId)
                .FirstOrDefault();

            var factura = new Facturas
            {
                Numero = CalcularSiguienteNumero(db),
                Cliente = cliente,
                EmpleadoId = dto.EmpleadoId,
                FormaPagoId = dto.FormaPagoId,
                Fecha = DateTime.Now,
                Observacion = Proyecciones.Recortar(dto.Observacion, 200),
                Estado = EstadoActiva,
                EstadoId = estadoId
            };

            decimal total = 0m;
            foreach (var linea in dto.Lineas)
            {
                var precio = productos[linea.ProductoId].PrecioVenta;
                var sub = precio * linea.Cantidad;
                total += sub;

                factura.DetalleFacturas.Add(new DetalleFactura
                {
                    ProductoId = linea.ProductoId,
                    Cantidad = linea.Cantidad,
                    PrecioUnitario = precio,
                    subtotal = sub
                });
            }
            factura.Subtotal = total;
            factura.Total = total;

            db.Facturas.Add(factura);
            db.SaveChanges();   // aquí salta el índice único si el número ya fue tomado

            // --- stock: UPDATE atómico con condición, así dos cajas no pueden vender la última unidad a la vez
            if (AppConfig.DescontarStockEnApp)
            {
                var porProducto = dto.Lineas.GroupBy(l => l.ProductoId)
                                            .Select(g => new { ProductoId = g.Key, Cantidad = g.Sum(x => x.Cantidad) });

                foreach (var item in porProducto)
                {
                    int filas = db.Database.ExecuteSqlInterpolated(
                        $"UPDATE Inventario SET Stock = Stock - {item.Cantidad} WHERE ProductoID = {item.ProductoId} AND Stock >= {item.Cantidad}");

                    if (filas == 0)
                    {
                        var disponible = db.Inventario.AsNoTracking()
                            .Where(i => i.ProductoId == item.ProductoId).Select(i => (int?)i.Stock).FirstOrDefault() ?? 0;
                        throw new InvalidOperationException(
                            $"Stock insuficiente de '{productos[item.ProductoId].Nombre}': disponible {disponible}, solicitado {item.Cantidad}.");
                    }

                    var stockNuevo = db.Inventario.AsNoTracking()
                        .Where(i => i.ProductoId == item.ProductoId).Select(i => i.Stock).First();

                    db.MovimientoInventarios.Add(MovimientosHelper.Crear(item.ProductoId,
                        TipoMovimientoInventario.VentaFactura, item.Cantidad,
                        stockNuevo + item.Cantidad, stockNuevo, dto.EmpleadoId, $"Factura {factura.Numero}"));
                }
            }

            db.SaveChanges();
            tran.Commit();

            return new ResultadoFacturaDto
            {
                FacturaId = factura.FacturaId,
                Numero = factura.Numero!,
                Subtotal = factura.Subtotal,
                Total = factura.Total
            };
        }

        // ------------------------------------------------------------------ anular

        public void Anular(string numero, int empleadoId, string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new InvalidOperationException("Debe indicar el motivo de la anulación.");

            using var db = _factory.CreateDbContext();
            using var tran = db.Database.BeginTransaction();

            var factura = db.Facturas.Include(f => f.DetalleFacturas).FirstOrDefault(f => f.Numero == numero)
                ?? throw new InvalidOperationException($"No existe la factura {numero}.");

            if (string.Equals(factura.Estado, EstadoAnulada, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"La factura {numero} ya está anulada.");

            var estadoAnuladaId = db.Estados.AsNoTracking()
                .Where(e => e.NombreEstado == EstadoAnulada)
                .Select(e => (int?)e.EstadoId)
                .FirstOrDefault();

            factura.Estado = EstadoAnulada;
            if (estadoAnuladaId.HasValue) factura.EstadoId = estadoAnuladaId;
            factura.Observacion = Proyecciones.Recortar(
                $"{(string.IsNullOrWhiteSpace(factura.Observacion) ? "" : factura.Observacion + " | ")}ANULADA: {motivo}", 200);
            db.SaveChanges();

            if (AppConfig.DescontarStockEnApp)
            {
                foreach (var g in factura.DetalleFacturas.GroupBy(d => d.ProductoId))
                {
                    int cantidad = g.Sum(d => d.Cantidad);
                    db.Database.ExecuteSqlInterpolated(
                        $"UPDATE Inventario SET Stock = Stock + {cantidad} WHERE ProductoID = {g.Key}");

                    var stockNuevo = db.Inventario.AsNoTracking()
                        .Where(i => i.ProductoId == g.Key).Select(i => (int?)i.Stock).FirstOrDefault() ?? 0;

                    db.MovimientoInventarios.Add(MovimientosHelper.Crear(g.Key,
                        TipoMovimientoInventario.AnulacionFactura, cantidad,
                        stockNuevo - cantidad, stockNuevo, empleadoId, $"Anulación factura {numero}: {motivo}"));
                }
            }

            db.SaveChanges();
            tran.Commit();
        }
    }
}
