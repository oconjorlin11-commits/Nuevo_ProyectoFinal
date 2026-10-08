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
            return CodigoGenerator.SiguienteAgrupado("PRD", codigos, "PRD-001");
        }

        public bool CodigoExiste(string codigo)
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking().Any(p => p.Codigo == codigo);
        }

        public bool NombreExiste(string nombre)
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking().Any(p => p.Nombre.ToLower() == nombre.ToLower());
        }

        public bool NombreEsSimilar(string nombre)
        {
            using var db = _factory.CreateDbContext();
            var nombresExistentes = db.Productos.AsNoTracking()
                .Select(p => p.Nombre.ToLower())
                .ToList();

            string nombreNormalizado = nombre.ToLower().Trim();

            foreach (var existente in nombresExistentes)
            {
                // Calcular similitud usando Levenshtein distance
                int distancia = CalcularDistanciaLevenshtein(nombreNormalizado, existente);
                int maxLengthComparison = Math.Max(nombreNormalizado.Length, existente.Length);

                // Si la distancia es menor o igual al 30% de la longitud máxima, considerar similar
                double similitud = 1.0 - ((double)distancia / maxLengthComparison);

                if (similitud >= 0.70)  // 70% de similitud
                {
                    return true;
                }
            }

            return false;
        }

        private static int CalcularDistanciaLevenshtein(string s1, string s2)
        {
            int len1 = s1.Length;
            int len2 = s2.Length;
            int[,] d = new int[len1 + 1, len2 + 1];

            for (int i = 0; i <= len1; i++) d[i, 0] = i;
            for (int j = 0; j <= len2; j++) d[0, j] = j;

            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[len1, len2];
        }

        public void Crear(ProductoNuevoDto dto, int empleadoId)
        {
            using var db = _factory.CreateDbContext();

            // Crear el producto
            var producto = new Productos
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                CategoriaId = dto.CategoriaId,
                UnidadId = dto.UnidadId,
                Descripcion = dto.Descripcion ?? string.Empty,
                PrecioVenta = dto.PrecioVenta,
                Activo = dto.Activo
            };

            db.Productos.Add(producto);
            db.SaveChanges();  // Guardar producto

            // Insertar inventario usando SQL directo para evitar conflicto con triggers
            db.Database.ExecuteSqlInterpolated($@"
                INSERT INTO Inventario (ProductoId, Stock, StockMinimo)
                VALUES ({producto.ProductoId}, {dto.StockInicial}, {dto.StockMinimo})
            ");

            // Ahora agregar el movimiento de inventario
            var mov = MovimientosHelper.Crear(producto.ProductoId, TipoMovimientoInventario.IngresoProducto,
                dto.StockInicial, 0, dto.StockInicial, empleadoId, "Ingreso inicial del producto");
            db.MovimientoInventarios.Add(mov);
            db.SaveChanges();  // Guardar movimiento
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
