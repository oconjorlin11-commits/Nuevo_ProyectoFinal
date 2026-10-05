using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services.Interfaz_service
{
    public interface IAuthService
    {
        ResultadoLoginDto Autenticar(string usuario, string contrasena);
    }
}
