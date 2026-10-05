using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services.Interfaz_service
{
    public interface IProductoRepository
    {
        string GetSiguienteCodigo();
        bool CodigoExiste(string codigo);

        /// <summary>Crea el producto, su inventario inicial y el movimiento de ingreso (todo en una transacción).</summary>
        void Crear(ProductoNuevoDto producto, int empleadoId);

        decimal GetPrecio(int productoId);
        IReadOnlyList<ProductoInventarioDto> GetActivosPorCategoria(int categoriaId);

        /// <summary>Obtiene productos activos de una categoría. Si categoriaId es null, retorna lista vacía (regla de negocio).</summary>
        IReadOnlyList<ProductoInventarioDto> ObtenerProductosPorCategoriaSeguro(int? categoriaId);
    }
}
