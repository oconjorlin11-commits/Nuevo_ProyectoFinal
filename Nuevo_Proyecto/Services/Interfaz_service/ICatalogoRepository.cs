using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services.Interfaz_service
{
    public interface ICatalogoRepository
    {
        IReadOnlyList<LookupDto> GetCategorias();
        IReadOnlyList<LookupDto> GetUnidades();
        IReadOnlyList<LookupDto> GetFormasPago();
        IReadOnlyList<LookupDto> GetClientesActivos();
        IReadOnlyList<LookupDto> GetEmpleadosActivos();
    }
}
