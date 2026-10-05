using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Catalogos;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Services
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;

        public ProductoRepository(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
        }

        public string GetSiguienteCodigo()
        {
            using var db = _factory.CreateDbContext();
            var codigos = db.Productos.AsNoTracking().Select(p => p.Codigo).ToList();
            return CodigoGenerator.SiguienteSecuencial("P", codigos, 4);
        }

        public bool CodigoExiste(string codigo)
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking().Any(p => p.Codigo == codigo);
        }

        public void Crear(ProductoNuevoDto dto, int empleadoId)
        {
            using var db = _factory.CreateDbContext();

            // Un solo SaveChanges = una sola transacción: producto + inventario + movimiento
            var producto = new Productos
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                CategoriaId = dto.CategoriaId,
                UnidadId = dto.UnidadId,
                Descripcion = dto.Descripcion ?? string.Empty,   // la entidad no admite null
                PrecioVenta = dto.PrecioVenta,
                Activo = dto.Activo
            };
            producto.Inventarios.Add(new Inventario
            {
                Stock = dto.StockInicial,
                StockMinimo = dto.StockMinimo
                // ValorInventario es columna calculada en la BD: no se escribe
            });
            db.Productos.Add(producto);

            var mov = MovimientosHelper.Crear(0, TipoMovimientoInventario.IngresoProducto,
                dto.StockInicial, 0, dto.StockInicial, empleadoId, "Ingreso inicial del producto");
            mov.Producto = producto;          // el Id se resuelve al guardar
            db.MovimientoInventarios.Add(mov);

            db.SaveChanges();
        }

        public decimal GetPrecio(int productoId)
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking()
                .Where(p => p.ProductoId == productoId)
                .Select(p => p.PrecioVenta)
                .FirstOrDefault();
        }

        public IReadOnlyList<ProductoInventarioDto> GetActivosPorCategoria(int categoriaId)
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking()
                .Where(p => p.Activo == true && p.CategoriaId == categoriaId)
                .OrderBy(p => p.Nombre)
                .Select(Proyecciones.ProductoInventario)
                .ToList();
        }

        /// <summary>
        /// Regla de negocio: Retorna productos activos de una categoría.
        /// Si categoriaId es null o menor/igual a 0, retorna lista vacía.
        /// </summary>
        public IReadOnlyList<ProductoInventarioDto> ObtenerProductosPorCategoriaSeguro(int? categoriaId)
        {
            // Validación de regla de negocio: sin categoría, sin productos
            if (!categoriaId.HasValue || categoriaId.Value <= 0)
                return new List<ProductoInventarioDto>();

            return GetActivosPorCategoria(categoriaId.Value);
        }
    }
}
