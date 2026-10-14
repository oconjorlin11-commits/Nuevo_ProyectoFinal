using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Services.Helpers;

namespace Nuevo_Proyecto.Services
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;

        public AuditoriaService(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
        }

        /// <summary>
        /// Registra un evento en la tabla de auditoría llamando al stored procedure usp_RegistrarAuditoria
        /// El UsuarioAutorizador se obtiene automáticamente de la sesión si no se proporciona
        /// </summary>
        public void RegistrarAccion(
            string seccion,
            string accion,
            int? registroId = null,
            string? usuario = null,
            string? rolUsuario = null,
            string? usuarioAutorizador = null,
            string? detalle = null,
            string? ipEquipo = null,
            string? descripcion = null)
        {
            try
            {
                // Si no se proporciona usuario, usar el actual
                usuario ??= SesionActual.NombreUsuario;
                rolUsuario ??= SesionActual.Rol;
                ipEquipo ??= ObtenerIPEquipo();

                using var db = _factory.CreateDbContext();

                // Si no se proporciona UsuarioAutorizador, obtenerlo de la sesión actual
                if (string.IsNullOrEmpty(usuarioAutorizador))
                {
                    usuarioAutorizador = SesionActual.UsuarioAutorizador;
                }

                // Llamar al stored procedure usp_RegistrarAuditoria
                db.Database.ExecuteSqlInterpolated(
                    $@"EXEC dbo.usp_RegistrarAuditoria
                        @Seccion = {seccion},
                        @Accion = {accion},
                        @RegistroID = {registroId},
                        @Usuario = {usuario},
                        @RolUsuario = {rolUsuario},
                        @UsuarioAutorizador = {usuarioAutorizador},
                        @Detalle = {detalle},
                        @IP_Equipo = {ipEquipo},
                        @Descripcion = {descripcion}"
                );
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                System.Diagnostics.Debug.WriteLine($"Error registrando auditoría: {ex.GetBaseException().Message}");
            }
        }

        /// <summary>
        /// Registra un evento en la tabla de auditoría llamando al stored procedure usp_RegistrarAuditoria
        /// El UsuarioAutorizador se obtiene automáticamente de la sesión si no se proporciona
        /// </summary>
        public bool RegistrarAuditoria(
            string seccion,
            string accion,
            string usuario,
            string rolUsuario,
            string? usuarioAutorizador = null,
            int? registroID = null,
            string? detalle = null,
            string? ipEquipo = null,
            string? descripcion = null)
        {
            try
            {
                using var db = _factory.CreateDbContext();

                // Obtener IP del equipo si no se proporciona
                if (string.IsNullOrEmpty(ipEquipo))
                {
                    ipEquipo = ObtenerIPEquipo();
                }

                // Si no se proporciona UsuarioAutorizador, obtenerlo de la sesión actual
                if (string.IsNullOrEmpty(usuarioAutorizador))
                {
                    // Usar el UsuarioAutorizador de la sesión (admin que autorizó al cajero)
                    usuarioAutorizador = SesionActual.UsuarioAutorizador;
                }

                // Llamar al stored procedure usp_RegistrarAuditoria
                db.Database.ExecuteSqlInterpolated(
                    $@"EXEC dbo.usp_RegistrarAuditoria
                        @Seccion = {seccion},
                        @Accion = {accion},
                        @RegistroID = {registroID},
                        @Usuario = {usuario},
                        @RolUsuario = {rolUsuario},
                        @UsuarioAutorizador = {usuarioAutorizador},
                        @Detalle = {detalle},
                        @IP_Equipo = {ipEquipo},
                        @Descripcion = {descripcion}"
                );

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                System.Diagnostics.Debug.WriteLine($"Error registrando auditoría: {ex.GetBaseException().Message}");
                return false;
            }
        }

        /// <summary>
        /// Obtiene la dirección IP del equipo local
        /// </summary>
        private string ObtenerIPEquipo()
        {
            try
            {
                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                var ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                return ipAddress?.ToString() ?? "Desconocida";
            }
            catch
            {
                return "Desconocida";
            }
        }
    }
}
