using Nuevo_Proyecto.Services.Helpers;

namespace Nuevo_Proyecto.Services
{
    public sealed class PermisoSeccionService
    {
        private readonly AuthService _authService;

        public PermisoSeccionService(AuthService? authService = null)
        {
            _authService = authService ?? new AuthService();
        }

        public ResultadoPermisoDto ValidarAccesoTemporal(string seccion, string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return ResultadoPermisoDto.Error("Debe indicar el usuario administrador.");

            if (string.IsNullOrWhiteSpace(clave))
                return ResultadoPermisoDto.Error("Debe indicar la contraseña del administrador.");

            var resultado = _authService.Autenticar(usuario, clave);
            if (!resultado.Exitoso || resultado.Sesion == null)
                return ResultadoPermisoDto.Error(resultado.Mensaje ?? "Las credenciales no son válidas.");

            bool esAdmin = resultado.Sesion.Rol?.Contains("admin", System.StringComparison.OrdinalIgnoreCase) == true ||
                           SesionActual.AdministradoresSistema.Contains(resultado.Sesion.NombreUsuario ?? string.Empty, System.StringComparer.OrdinalIgnoreCase);

            if (!esAdmin)
                return ResultadoPermisoDto.Error("El usuario no tiene permisos de administrador para autorizar esta sección.");

            var nombreAutorizador = resultado.Sesion.NombreEmpleado ?? resultado.Sesion.NombreUsuario ?? "Administrador";

            return ResultadoPermisoDto.Ok(nombreAutorizador);
        }
    }

    public sealed class ResultadoPermisoDto
    {
        public bool Permitido { get; private set; }
        public string? Mensaje { get; private set; }
        public string? NombreAutorizador { get; private set; }

        public static ResultadoPermisoDto Ok(string nombreAutorizador) => new()
        {
            Permitido = true,
            NombreAutorizador = nombreAutorizador,
            Mensaje = "Autorización válida."
        };

        public static ResultadoPermisoDto Error(string mensaje) => new()
        {
            Permitido = false,
            Mensaje = mensaje,
            NombreAutorizador = null
        };
    }
}
