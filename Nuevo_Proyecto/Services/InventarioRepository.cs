using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Catalogos;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Services
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;

        public InventarioRepository(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
        }

        public IReadOnlyList<ProductoInventarioDto> GetTodos()
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking().OrderBy(p => p.Nombre)
                .Select(Proyecciones.ProductoInventario).ToList();
        }

        public IReadOnlyList<ProductoInventarioDto> GetActivos()
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking().Where(p => p.Activo == true).OrderBy(p => p.Nombre)
                .Select(Proyecciones.ProductoInventario).ToList();
        }

        public IReadOnlyList<ProductoInventarioDto> GetActivosPorCategoria(int categoriaId)
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking()
                .Where(p => p.Activo == true && p.CategoriaId == categoriaId).OrderBy(p => p.Nombre)
                .Select(Proyecciones.ProductoInventario).ToList();
        }

        public IReadOnlyList<ProductoInventarioDto> Buscar(string texto)
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking()
                .Where(p => p.Codigo.Contains(texto) || p.Nombre.Contains(texto)).OrderBy(p => p.Nombre)
                .Select(Proyecciones.ProductoInventario).ToList();
        }

        public IReadOnlyList<MovimientoInventarioDto> GetMovimientos(DateTime? desde, DateTime? hasta)
        {
            using var db = _factory.CreateDbContext();
            var query = db.MovimientoInventarios.AsNoTracking().AsQueryable();

            if (desde.HasValue) query = query.Where(m => m.Fecha >= desde.Value);
            if (hasta.HasValue)
            {
                var fin = Proyecciones.FinDeDia(hasta.Value);
                query = query.Where(m => m.Fecha <= fin);
            }

            return query
                .OrderByDescending(m => m.Fecha).ThenByDescending(m => m.MovimientoId)
                .Select(m => new MovimientoInventarioDto
                {
                    MovimientoId = m.MovimientoId,
                    Fecha = m.Fecha,
                    Tipo = m.TipoMovimiento,
                    Cantidad = m.Cantidad,
                    StockAnterior = m.StockAnterior,
                    StockNuevo = m.StockNuevo,
                    Observacion = m.Observacion,
                    NombreProducto = m.Producto.Nombre,
                    NombreEmpleado = m.Empleado.Nombre,
                    PrecioVenta = m.Producto.PrecioVenta,
                    ValorInventario = m.Producto.PrecioVenta * m.StockNuevo
                })
                .ToList();
        }

        public int Reactivar(string codigo, int stock, int minimo, int empleadoId)
        {
            using var db = _factory.CreateDbContext();
            var prod = db.Productos.FirstOrDefault(p => p.Codigo == codigo);
            if (prod == null) return 0;

            var inv = db.Inventario.FirstOrDefault(i => i.ProductoId == prod.ProductoId);
            int stockAnterior = inv?.Stock ?? 0;

            prod.Activo = true;
            if (inv == null)
            {
                db.Inventario.Add(new Models.Entities.Inventario { ProductoId = prod.ProductoId, Stock = stock, StockMinimo = minimo });
            }
            else
            {
                inv.Stock = stock;
                inv.StockMinimo = minimo;
            }

            db.MovimientoInventarios.Add(MovimientosHelper.Crear(prod.ProductoId,
                TipoMovimientoInventario.ReactivacionProducto, Math.Abs(stock - stockAnterior),
                stockAnterior, stock, empleadoId, "Producto reactivado"));

            return db.SaveChanges();
        }

        public int Actualizar(int productoId, string nombre, int categoriaId, int unidadId, string? descripcion,
                              decimal precioVenta, bool activo, int stock, int minimo, int empleadoId, string observacion)
        {
            using var db = _factory.CreateDbContext();
            var prod = db.Productos.FirstOrDefault(p => p.ProductoId == productoId);
            if (prod == null) return 0;

            var inv = db.Inventario.FirstOrDefault(i => i.ProductoId == productoId);
            int stockAnterior = inv?.Stock ?? 0;
            int minimoAnterior = inv?.StockMinimo ?? 0;

            // El tipo de movimiento se deduce de lo que realmente cambió
            var tipo = TipoMovimientoHelper.DetectarTipoCambio(
                nombre, categoriaId, unidadId, precioVenta, activo, stock, minimo,
                prod.Nombre, prod.CategoriaId, prod.UnidadId, prod.PrecioVenta, prod.Activo ?? false,
                stockAnterior, minimoAnterior);

            var antes = new { prod.Nombre, prod.CategoriaId, prod.UnidadId, prod.PrecioVenta, Activo = prod.Activo ?? false, Stock = stockAnterior, StockMinimo = minimoAnterior };

            prod.Nombre = nombre;
            prod.CategoriaId = categoriaId;
            prod.UnidadId = unidadId;
            if (descripcion != null) prod.Descripcion = descripcion;   // null = conservar la actual
            prod.PrecioVenta = precioVenta;
            prod.Activo = activo;

            if (inv == null)
            {
                db.Inventario.Add(new Models.Entities.Inventario { ProductoId = productoId, Stock = stock, StockMinimo = minimo });
            }
            else
            {
                inv.Stock = stock;
                inv.StockMinimo = minimo;
            }

            db.MovimientoInventarios.Add(MovimientosHelper.Crear(productoId, tipo,
                Math.Abs(stock - stockAnterior), stockAnterior, stock, empleadoId, observacion));

            return db.SaveChanges();
        }

        public int Inhabilitar(int productoId, int empleadoId, string motivo)
        {
            using var db = _factory.CreateDbContext();
            var prod = db.Productos.FirstOrDefault(p => p.ProductoId == productoId);
            if (prod == null) return 0;

            var inv = db.Inventario.AsNoTracking().FirstOrDefault(i => i.ProductoId == productoId);
            int stock = inv?.Stock ?? 0;

            prod.Activo = false;

            db.MovimientoInventarios.Add(MovimientosHelper.Crear(productoId,
                TipoMovimientoInventario.InhabilitacionProducto, 0, stock, stock, empleadoId, motivo));

            return db.SaveChanges();
        }
    }
}
