using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services.Interfaz_service
{
    public interface IInventarioRepository
    {
        IReadOnlyList<ProductoInventarioDto> GetTodos();
        IReadOnlyList<ProductoInventarioDto> GetActivos();
        IReadOnlyList<ProductoInventarioDto> GetActivosPorCategoria(int categoriaId);
        IReadOnlyList<ProductoInventarioDto> Buscar(string texto);
        IReadOnlyList<ProductoInventarioDto> BuscarPorCodigoExacto(string codigo);
        IReadOnlyList<MovimientoInventarioDto> GetMovimientos(DateTime? desde, DateTime? hasta);

        int Reactivar(string codigo, int stock, int minimo, int empleadoId);

        /// <param name="descripcion">null = conservar la descripción actual.</param>
        int Actualizar(int productoId, string nombre, int categoriaId, int unidadId, string? descripcion,
                       decimal precioVenta, bool activo, int stock, int minimo, int empleadoId, string observacion);

        int Inhabilitar(int productoId, int empleadoId, string motivo);
    }
}
