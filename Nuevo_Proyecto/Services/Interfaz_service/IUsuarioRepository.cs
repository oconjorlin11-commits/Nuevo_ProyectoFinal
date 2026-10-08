using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services.Interfaz_service
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Crea un empleado y su usuario asociado usando el procedimiento almacenado usp_InsertarEmpleadoYUsuario
        /// </summary>
        /// <param name="dto">Datos del empleado a crear</param>
        bool CrearUsuarioEmpleado(EmpleadoDto dto);
    }
}
