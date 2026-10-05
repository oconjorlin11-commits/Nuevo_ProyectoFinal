using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services.Helpers
{
    /// <summary>Usuario que inició sesión. Reemplaza el EmpleadoId = 1 fijo que había en los movimientos.</summary>
    public static class SesionActual
    {
        public static SesionUsuarioDto? Usuario { get; private set; }

        public static bool HaySesion => Usuario != null;
        public static int EmpleadoId => Usuario?.EmpleadoId ?? 0;
        public static string NombreUsuario => Usuario?.NombreUsuario ?? "sistema";
        public static string NombreEmpleado => Usuario?.NombreEmpleado ?? string.Empty;

        public static bool EsAdministrador =>
            Usuario != null &&
            (Contiene(Usuario.Rol, "admin") || Contiene(Usuario.Cargo, "admin"));

        public static void Iniciar(SesionUsuarioDto usuario) => Usuario = usuario;
        public static void Cerrar() => Usuario = null;

        /// <summary>Empleado a registrar en movimientos: el de la sesión o el indicado como respaldo.</summary>
        public static int EmpleadoORespaldo(int empleadoIdIndicado) =>
            empleadoIdIndicado > 0 ? empleadoIdIndicado : (EmpleadoId > 0 ? EmpleadoId : 1);

        private static bool Contiene(string? texto, string valor) =>
            !string.IsNullOrEmpty(texto) && texto.Contains(valor, StringComparison.OrdinalIgnoreCase);
    }
}
