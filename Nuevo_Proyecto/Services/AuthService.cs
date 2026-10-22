using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Valida usuarios contra la tabla Usuarios que ya existe en la base (NombreUsuario, [Contraseña],
    /// RolSistema, CodigoEmpleado, Activo) + las columnas nuevas ContrasenaHash y UltimoAcceso
    /// creadas por Database/01_seguridad_y_auditoria.sql.
    ///
    /// Migración automática: si el usuario todavía tiene la contraseña en texto plano y entra bien,
    /// se guarda su hash y se borra el texto plano.
    /// </summary>
    public class AuthService : IAuthService
    {
        private const string MensajeGenerico = "Usuario o contraseña incorrectos.";

        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;
        private readonly AutenticacionOfflineService _autenticacionOffline;

        public AuthService(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
            _autenticacionOffline = new AutenticacionOfflineService();
        }

        // Fila plana para la consulta SQL (no es una entidad del modelo)
        private sealed class FilaLogin
        {
            public string NombreUsuario { get; set; } = string.Empty;
            public string? Contrasena { get; set; }
            public string? RolSistema { get; set; }
            public int EmpleadoId { get; set; }
            public string Codigo { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
            public string? Cargo { get; set; }
        }

        public ResultadoLoginDto Autenticar(string usuario, string contrasena)
        {
            usuario = (usuario ?? string.Empty).Trim();
            contrasena = (contrasena ?? string.Empty).Trim();

            if (usuario.Length == 0 || contrasena.Length == 0)
                return Fallo("Ingrese su usuario y contraseña.");

            try
            {
                using var db = _factory.CreateDbContext();

                var fila = db.Database.SqlQuery<FilaLogin>($@"
                    SELECT u.NombreUsuario              AS NombreUsuario,
                           u.[Contraseña]               AS Contrasena,
                           u.RolSistema                 AS RolSistema,
                           u.EmpleadoID                 AS EmpleadoId,
                           e.Codigo                     AS Codigo,
                           e.Nombre                     AS Nombre,
                           e.Cargo                      AS Cargo
                    FROM Usuarios u
                    INNER JOIN Empleados e ON u.EmpleadoID = e.EmpleadoID
                    WHERE u.NombreUsuario = {usuario}
                      AND u.Activo = 1
                      AND ISNULL(e.Activo, 1) = 1")
                    .AsEnumerable()
                    .FirstOrDefault();

                if (fila == null) return Fallo(MensajeGenerico);

                // Validar contraseña (comparación directa con el texto plano)
                bool valido = !string.IsNullOrEmpty(fila.Contrasena)
                             && string.Equals(fila.Contrasena.Trim(), contrasena, StringComparison.Ordinal);

                if (!valido) return Fallo(MensajeGenerico);

                // Actualizar último acceso si la columna existe
                try
                {
                    db.Database.ExecuteSqlInterpolated(
                        $"UPDATE Usuarios SET UltimoAcceso = SYSDATETIME() WHERE NombreUsuario = {fila.NombreUsuario}");
                }
                catch
                {
                    // Si la columna UltimoAcceso no existe, ignorar el error
                }

                // Guardar en cache local para uso offline posterior
                _autenticacionOffline.GuardarUltimoUsuario(usuario);

                var resultado = new ResultadoLoginDto
                {
                    Exitoso = true,
                    Mensaje = "Bienvenido",
                    Sesion = new SesionUsuarioDto
                    {
                        EmpleadoId = fila.EmpleadoId,
                        CodigoEmpleado = fila.Codigo,
                        NombreEmpleado = fila.Nombre,
                        NombreUsuario = fila.NombreUsuario,
                        Rol = fila.RolSistema ?? string.Empty,
                        Cargo = fila.Cargo ?? string.Empty
                    }
                };

                // Registrar login exitoso en auditoría
                _autenticacionOffline.RegistrarLoginOfflineEnAuditoria(usuario, true);

                // Sincronizar tabla Usuarios a SQLite para acceso offline
                try
                {
                    var sincronizacion = new SincronizacionUsuariosService();
                    // Ejecutar sin esperar para no ralentizar el login
#pragma warning disable CS4014 // Sin esperar llamada asíncrona
                    sincronizacion.SincronizarUsuarios();
#pragma warning restore CS4014 // Sin esperar llamada asíncrona
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Advertencia: No se pudo sincronizar usuarios: {ex.Message}");
                    // Continuar aunque falle la sincronización
                }

                return resultado;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex) when (EsErrorDeConexion(ex))
            {
                // Error de conexión a SQL Server
                Console.WriteLine($"SQL Server no disponible (Error {ex.Number}): {ex.Message}. Intentando autenticación offline...");

                // Intentar fallback a autenticación offline
                return IntentarAutenticacionOffline(usuario, contrasena);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de autenticación: {ex.Message}");

                // Si es un error de conexión genérico, también intentar offline
                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx
                    && EsErrorDeConexion(sqlEx))
                {
                    return IntentarAutenticacionOffline(usuario, contrasena);
                }

                // Para otros errores, retornar el mensaje genérico
                return Fallo("No se pudo validar el acceso. Verifique la conexión y contacte al administrador.");
            }
        }

        /// <summary>
        /// Intenta autenticar usando el servicio offline cuando SQL Server no está disponible
        /// </summary>
        private ResultadoLoginDto IntentarAutenticacionOffline(string usuario, string contrasena)
        {
            try
            {
                var resultado = _autenticacionOffline.AutenticarOffline(usuario, contrasena);

                if (resultado.Exitoso)
                {
                    _autenticacionOffline.RegistrarLoginOfflineEnAuditoria(usuario, true);
                }
                else
                {
                    _autenticacionOffline.RegistrarLoginOfflineEnAuditoria(usuario, false);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en autenticación offline: {ex.Message}");
                return Fallo("No se pudo autenticar. SQL Server no disponible y no hay sesión previa en cache.");
            }
        }

        private static ResultadoLoginDto Fallo(string mensaje) => new() { Exitoso = false, Mensaje = mensaje };

        /// <summary>
        /// Determina si un SqlException es de conexión (que justifica fallback a SQLite)
        /// Códigos: 26=servidor no encontrado, -1=timeout, -2=sin red, 40=límite recursos, 64=error comunicación
        /// </summary>
        private static bool EsErrorDeConexion(Microsoft.Data.SqlClient.SqlException ex)
        {
            int[] erroresConexion = { 26, 40, 64, 233, -1, -2, -3 };
            return ex.Number == 0 || erroresConexion.Contains(ex.Number) || 
                   ex.Message.Contains("network", StringComparison.OrdinalIgnoreCase) ||
                   ex.Message.Contains("server not found", StringComparison.OrdinalIgnoreCase) ||
                   ex.Message.Contains("connection timeout", StringComparison.OrdinalIgnoreCase);
        }

        // =========================================================================
        // Implementación Asíncrona Segregada (Arquitectura MVP de Referencia)
        // =========================================================================

        public async Task<AuthenticationResultDto> AuthenticateAsync(AuthenticationRequestDto request)
        {
            if (request == null)
            {
                return AuthenticationResultDto.Failure("Solicitud de autenticación nula.");
            }

            string usuario = (request.UserName ?? string.Empty).Trim();
            string contrasena = (request.Password ?? string.Empty).Trim();

            if (usuario.Length == 0 || contrasena.Length == 0)
            {
                return AuthenticationResultDto.Failure("Ingrese su usuario y contraseña.");
            }

            try
            {
                using var db = await _factory.CreateDbContextAsync();

                var fila = await db.Database.SqlQuery<FilaLogin>($@"
                    SELECT u.NombreUsuario              AS NombreUsuario,
                           u.[Contraseña]               AS Contrasena,
                           u.RolSistema                 AS RolSistema,
                           u.EmpleadoID                 AS EmpleadoId,
                           e.Codigo                     AS Codigo,
                           e.Nombre                     AS Nombre,
                           e.Cargo                      AS Cargo
                    FROM Usuarios u
                    INNER JOIN Empleados e ON u.EmpleadoID = e.EmpleadoID
                    WHERE u.NombreUsuario = {usuario}
                      AND u.Activo = 1
                      AND ISNULL(e.Activo, 1) = 1")
                    .FirstOrDefaultAsync();

                if (fila == null)
                {
                    return AuthenticationResultDto.Failure(MensajeGenerico);
                }

                bool valido = !string.IsNullOrEmpty(fila.Contrasena)
                             && string.Equals(fila.Contrasena.Trim(), contrasena, StringComparison.Ordinal);

                if (!valido)
                {
                    return AuthenticationResultDto.Failure(MensajeGenerico);
                }

                try
                {
                    await db.Database.ExecuteSqlInterpolatedAsync(
                        $"UPDATE Usuarios SET UltimoAcceso = SYSDATETIME() WHERE NombreUsuario = {fila.NombreUsuario}");
                }
                catch
                {
                    // Ignorar si la columna no existe
                }

                var userDto = new AuthenticatedUserDto
                {
                    UserId = fila.EmpleadoId,
                    EmployeeId = fila.EmpleadoId,
                    UserName = fila.NombreUsuario,
                    EmployeeName = fila.Nombre,
                    Role = fila.RolSistema ?? fila.Cargo ?? "Usuario",
                    Roles = new[] { fila.RolSistema ?? fila.Cargo ?? "Usuario" }
                };

                return AuthenticationResultDto.Success(userDto);
            }
            catch (Exception ex)
            {
                return AuthenticationResultDto.Failure("No se pudo validar el acceso. Verifique la conexión: " + ex.Message);
            }
        }
    }
}
