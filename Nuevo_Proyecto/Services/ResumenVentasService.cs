using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Servicio para mostrar resumen de ventas del día
    /// Muestra solo facturas CONFIRMADAS del día (las actuales en cola no se muestran)
    /// Funciona tanto online como offline
    /// </summary>
    public class ResumenVentasService
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _sqlFactory;
        private readonly ComideriaSqliteContextFactory _sqliteFactory;

        public ResumenVentasService(
            IDbContextFactory<Dev_ComideriaDbContext>? sqlFactory = null,
            ComideriaSqliteContextFactory? sqliteFactory = null)
        {
            _sqlFactory = sqlFactory ?? new ComideriaContextFactory();
            _sqliteFactory = sqliteFactory ?? new ComideriaSqliteContextFactory();
        }

        /// <summary>
        /// Obtiene resumen de ventas del día actual
        /// En modo online usa SQL Server, en offline usa SQLite
        /// Solo muestra facturas CONFIRMADAS (las en cola se ignoran)
        /// </summary>
        public ResumenVentasDiaDto ObtenerResumenVentasDelDia()
        {
            try
            {
                if (ConnectionStateManager.UsandoSqlite)
                {
                    return ObtenerResumenDesdeOffline();
                }
                else
                {
                    return ObtenerResumenDesdeOnline();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo resumen: {ex.Message}");
                // Fallback a offline si hay error
                try
                {
                    return ObtenerResumenDesdeOffline();
                }
                catch
                {
                    return new ResumenVentasDiaDto();
                }
            }
        }

        /// <summary>
        /// Obtiene resumen desde SQL Server (modo online)
        /// </summary>
        private ResumenVentasDiaDto ObtenerResumenDesdeOnline()
        {
            using (var db = _sqlFactory.CreateDbContext())
            {
                var hoy = DateTime.Today;
                var manana = hoy.AddDays(1);

                // Obtener todas las facturas del día (confirmadas, no en cola)
                var facturas = db.Facturas
                    .Where(f => f.Fecha >= hoy && f.Fecha < manana)
                    .Include(f => f.DetalleFacturas)
                    .ToList();

                return GenerarResumen(facturas, hoy);
            }
        }

        /// <summary>
        /// Obtiene resumen desde SQLite (modo offline)
        /// </summary>
        private ResumenVentasDiaDto ObtenerResumenDesdeOffline()
        {
            using (var sqlite = _sqliteFactory.CreateDbContext())
            {
                var hoy = DateTime.Today;
                var manana = hoy.AddDays(1);

                // Obtener facturas del SQLite del día
                var facturas = sqlite.Facturas
                    .Where(f => f.Fecha >= hoy && f.Fecha < manana)
                    .Include(f => f.DetalleFacturas)
                    .ToList();

                return GenerarResumen(facturas, hoy);
            }
        }

        /// <summary>
        /// Calcula totales y estadísticas del resumen
        /// </summary>
        private ResumenVentasDiaDto GenerarResumen(List<Models.Entities.Facturas> facturas, DateTime fecha)
        {
            var resumen = new ResumenVentasDiaDto
            {
                FechaResumen = fecha,
                TotalFacturas = facturas.Count,
                TotalVentas = facturas.Sum(f => f.Total),
                Facturas = new List<FacturaVentaDiaDto>()
            };

            // Agregar detalles de cada factura
            foreach (var factura in facturas)
            {
                resumen.Facturas.Add(new FacturaVentaDiaDto
                {
                    FacturaId = factura.FacturaId,
                    Numero = factura.Numero ?? "SN",
                    Fecha = factura.Fecha,
                    Cliente = factura.Cliente?.Nombre ?? "Desconocido",
                    Total = factura.Total,
                    Cantidad = factura.DetalleFacturas?.Sum(d => d.Cantidad) ?? 0,
                    Estado = factura.Estado ?? "Pendiente"
                });
            }

            // Calcular promedio
            if (resumen.TotalFacturas > 0)
            {
                resumen.PromedioFactura = resumen.TotalVentas / resumen.TotalFacturas;
            }

            return resumen;
        }

        /// <summary>
        /// Obtiene estadísticas detalladas por cliente
        /// </summary>
        public List<EstadisticasClienteDto> ObtenerDetallesPorCliente()
        {
            try
            {
                if (ConnectionStateManager.UsandoSqlite)
                {
                    return ObtenerDetallesPorClienteOffline();
                }
                else
                {
                    return ObtenerDetallesPorClienteOnline();
                }
            }
            catch
            {
                return new List<EstadisticasClienteDto>();
            }
        }

        private List<EstadisticasClienteDto> ObtenerDetallesPorClienteOnline()
        {
            using (var db = _sqlFactory.CreateDbContext())
            {
                var hoy = DateTime.Today;
                var manana = hoy.AddDays(1);

                return db.Facturas
                    .Where(f => f.Fecha >= hoy && f.Fecha < manana && f.Cliente != null)
                    .GroupBy(f => f.Cliente!.ClienteId)
                    .Select(g => new EstadisticasClienteDto
                    {
                        NombreCliente = g.First().Cliente!.Nombre,
                        TotalCompras = g.Count(),
                        MontoTotal = g.Sum(f => f.Total),
                        PromedioPorCompra = g.Count() > 0 ? g.Sum(f => f.Total) / g.Count() : 0
                    })
                    .OrderByDescending(x => x.MontoTotal)
                    .ToList();
            }
        }

        private List<EstadisticasClienteDto> ObtenerDetallesPorClienteOffline()
        {
            using (var sqlite = _sqliteFactory.CreateDbContext())
            {
                var hoy = DateTime.Today;
                var manana = hoy.AddDays(1);

                return sqlite.Facturas
                    .Where(f => f.Fecha >= hoy && f.Fecha < manana && f.Cliente != null)
                    .GroupBy(f => f.Cliente!.ClienteId)
                    .Select(g => new EstadisticasClienteDto
                    {
                        NombreCliente = g.First().Cliente!.Nombre,
                        TotalCompras = g.Count(),
                        MontoTotal = g.Sum(f => f.Total),
                        PromedioPorCompra = g.Count() > 0 ? g.Sum(f => f.Total) / g.Count() : 0
                    })
                    .OrderByDescending(x => x.MontoTotal)
                    .ToList();
            }
        }

        /// <summary>
        /// Obtiene productos más vendidos del día
        /// </summary>
        public List<ProductoVendidoDto> ObtenerProductosMasVendidos()
        {
            try
            {
                if (ConnectionStateManager.UsandoSqlite)
                {
                    return ObtenerProductosMasVendidosOffline();
                }
                else
                {
                    return ObtenerProductosMasVendidosOnline();
                }
            }
            catch
            {
                return new List<ProductoVendidoDto>();
            }
        }

        private List<ProductoVendidoDto> ObtenerProductosMasVendidosOnline()
        {
            using (var db = _sqlFactory.CreateDbContext())
            {
                var hoy = DateTime.Today;
                var manana = hoy.AddDays(1);

                return db.DetalleFactura
                    .Where(d => d.Factura!.Fecha >= hoy && d.Factura!.Fecha < manana)
                    .GroupBy(d => d.Producto!.ProductoId)
                    .Select(g => new ProductoVendidoDto
                    {
                        NombreProducto = g.First().Producto!.Nombre,
                        Cantidad = g.Sum(x => x.Cantidad),
                        MontoTotal = g.Sum(x => x.subtotal),
                        PrecioPromedio = g.Count() > 0 ? g.Average(x => x.PrecioUnitario) : 0
                    })
                    .OrderByDescending(x => x.Cantidad)
                    .Take(10)
                    .ToList();
            }
        }

        private List<ProductoVendidoDto> ObtenerProductosMasVendidosOffline()
        {
            using (var sqlite = _sqliteFactory.CreateDbContext())
            {
                var hoy = DateTime.Today;
                var manana = hoy.AddDays(1);

                return sqlite.DetalleFacturas
                    .Where(d => d.Factura!.Fecha >= hoy && d.Factura!.Fecha < manana)
                    .GroupBy(d => d.Producto!.ProductoId)
                    .Select(g => new ProductoVendidoDto
                    {
                        NombreProducto = g.First().Producto!.Nombre,
                        Cantidad = g.Sum(x => x.Cantidad),
                        MontoTotal = g.Sum(x => x.subtotal),
                        PrecioPromedio = g.Count() > 0 ? g.Average(x => x.PrecioUnitario) : 0
                    })
                    .OrderByDescending(x => x.Cantidad)
                    .Take(10)
                    .ToList();
            }
        }
    }

    // DTOs para el resumen - locales a este servicio
    public class ResumenVentasDiaDto
    {
        public DateTime FechaResumen { get; set; }
        public int TotalFacturas { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal PromedioFactura { get; set; }
        public List<FacturaVentaDiaDto> Facturas { get; set; } = new();
    }

    public class FacturaVentaDiaDto
    {
        public int FacturaId { get; set; }
        public string Numero { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = "";
        public decimal Total { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; } = "";
    }

    public class EstadisticasClienteDto
    {
        public string NombreCliente { get; set; } = "";
        public int TotalCompras { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal PromedioPorCompra { get; set; }
    }

    public class ProductoVendidoDto
    {
        public string NombreProducto { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal PrecioPromedio { get; set; }
    }
}
