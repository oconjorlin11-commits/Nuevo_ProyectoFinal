using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services.Interfaz_service
{
    public interface IEmpleadoRepository
    {
        IReadOnlyList<EmpleadoDto> GetActivos();
        IReadOnlyList<EmpleadoDto> GetPorCargos(params string[] cargos);
        IReadOnlyList<EmpleadoDto> BuscarPorCodigo(string prefijoCodigo);
        IReadOnlyList<string> GetCargos();
        string GetSiguienteCodigo();
        bool CodigoExiste(string codigo);
        void Crear(EmpleadoDto empleado);
        bool Actualizar(string codigo, string nombre, string? cargo, string cedula, string? telefono, decimal salario);
        bool Desactivar(string codigo);
        bool Reactivar(string codigo);
    }
}
