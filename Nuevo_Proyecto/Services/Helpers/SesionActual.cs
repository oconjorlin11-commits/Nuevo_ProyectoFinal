using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services.Helpers
{
    /// <summary>Usuario que inició sesión. Reemplaza el EmpleadoId = 1 fijo que había en los movimientos.</summary>
    public static class SesionActual
    {
        public static SesionUsuarioDto? Usuario { get; private set; }
        public static string? UsuarioAutorizador { get; set; }
        public static HashSet<string> AdministradoresSistema { get; private set; } = new(StringComparer.OrdinalIgnoreCase);

        public static bool HaySesion => Usuario != null;
        public static int EmpleadoId => Usuario?.EmpleadoId ?? 0;
        public static string NombreUsuario => Usuario?.NombreUsuario ?? "sistema";
        public static string NombreEmpleado => Usuario?.NombreEmpleado ?? string.Empty;
        public static string Rol => Usuario?.Rol ?? "Desconocido";

        public static bool EsAdministrador =>
            Usuario != null &&
            (
                Contiene(Usuario.Rol, "admin") ||
                Contiene(Usuario.Cargo, "admin") ||
                AdministradoresSistema.Contains(Usuario.NombreUsuario)
            );

        public static void Iniciar(SesionUsuarioDto usuario)
        {
            Usuario = usuario;
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
            {
                return;
            }

            if ((usuario.Rol.Contains("admin", StringComparison.OrdinalIgnoreCase) || usuario.Cargo.Contains("admin", StringComparison.OrdinalIgnoreCase))
                && !AdministradoresSistema.Contains(usuario.NombreUsuario))
            {
                AdministradoresSistema.Add(usuario.NombreUsuario);
            }
        }

        public static void ActualizarAdministradores(IEnumerable<string>? usuarios)
        {
            AdministradoresSistema.Clear();
            if (usuarios == null) return;

            foreach (var usuario in usuarios)
            {
                if (!string.IsNullOrWhiteSpace(usuario))
                {
                    AdministradoresSistema.Add(usuario.Trim());
                }
            }
        }

        public static void Cerrar()
        {
            Usuario = null;
            UsuarioAutorizador = null;
        }

        /// <summary>Empleado a registrar en movimientos: el de la sesión o el indicado como respaldo.</summary>
        public static int EmpleadoORespaldo(int empleadoIdIndicado) =>
            empleadoIdIndicado > 0 ? empleadoIdIndicado : (EmpleadoId > 0 ? EmpleadoId : 1);

        private static bool Contiene(string? texto, string valor) =>
            !string.IsNullOrEmpty(texto) && texto.Contains(valor, StringComparison.OrdinalIgnoreCase);
    }
}
