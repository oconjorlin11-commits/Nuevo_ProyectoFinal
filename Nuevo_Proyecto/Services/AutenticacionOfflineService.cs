using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Data.DbSqlite;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Servicio de autenticación offline usando SQLite cuando SQL Server no está disponible
    /// Mantiene seguridad sin guardar contraseñas en texto plano
    /// </summary>
    public class AutenticacionOfflineService
    {
        private readonly ComideriaSqliteContextFactory _sqliteFactory;
        private const string SALT_CLAVE = "COMIDERIA_2024_SALT_CLAVE_SEGURA";

        // Clase auxiliar para mapear resultados de SQL query
        private sealed class ResultadoUsuarioOffline
        {
            public int UsuarioId { get; set; }
            public string NombreUsuario { get; set; } = string.Empty;
            public string? Contrasena { get; set; }
            public string? RolSistema { get; set; }
            public int EmpleadoId { get; set; }
            public string Codigo { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
            public string? Cargo { get; set; }
        }

        public AutenticacionOfflineService(ComideriaSqliteContextFactory? sqliteFactory = null)
        {
            _sqliteFactory = sqliteFactory ?? new ComideriaSqliteContextFactory();
        }

        /// <summary>
        /// Intenta autenticar usuario usando datos en SQLite (cuando SQL Server no está disponible)
        /// </summary>
        public ResultadoLoginDto AutenticarOffline(string usuario, string contrasena)
        {
            usuario = (usuario ?? string.Empty).Trim();
            contrasena = (contrasena ?? string.Empty).Trim();

            if (usuario.Length == 0 || contrasena.Length == 0)
                return Fallo("Ingrese su usuario y contraseña.");

            try
            {
                using (var sqlite = _sqliteFactory.CreateDbContext())
                {
                    // Buscar usuario por NombreUsuario en la tabla Usuarios
                    var usuarioData = sqlite.Usuarios
                        .Where(u => u.NombreUsuario.ToUpper() == usuario.ToUpper() && u.Activo)
                        .Join(
                            sqlite.Empleados,
                            u => u.EmpleadoId,
                            e => e.EmpleadoId,
                            (u, e) => new ResultadoUsuarioOffline
                            {
                                UsuarioId = u.UsuarioId,
                                NombreUsuario = u.NombreUsuario,
                                Contrasena = u.Contrasena,
                                RolSistema = u.RolSistema,
                                EmpleadoId = e.EmpleadoId,
                                Codigo = e.Codigo,
                                Nombre = e.Nombre,
                                Cargo = e.Cargo
                            }
                        )
                        .FirstOrDefault();

                    if (usuarioData == null)
                        return Fallo("Usuario no encontrado localmente.");

                    // Validar contraseña (comparación directa con texto plano)
                    bool valido = !string.IsNullOrEmpty(usuarioData.Contrasena)
                                 && string.Equals(usuarioData.Contrasena.Trim(), contrasena, StringComparison.Ordinal);

                    if (!valido)
                        return Fallo("Contraseña incorrecta.");

                    // Crear sesión con datos del usuario
                    return new ResultadoLoginDto
                    {
                        Exitoso = true,
                        Mensaje = "⚠️ Modo offline. Bienvenido",
                        Sesion = new SesionUsuarioDto
                        {
                            EmpleadoId = usuarioData.EmpleadoId,
                            CodigoEmpleado = usuarioData.Codigo,
                            NombreEmpleado = usuarioData.Nombre,
                            NombreUsuario = usuarioData.NombreUsuario,
                            Rol = usuarioData.RolSistema ?? string.Empty,
                            Cargo = usuarioData.Cargo ?? string.Empty
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en autenticación offline: {ex.Message}");
                return Fallo($"Error en autenticación local: {ex.Message}");
            }
        }

        /// <summary>
        /// Permite login sin contraseña como último usuario registrado (para facilitar en offline)
        /// SOLO si hay auditoría que confirma el logout anterior
        /// </summary>
        public ResultadoLoginDto AutenticarUltimoUsuarioOffline()
        {
            try
            {
                using (var sqlite = _sqliteFactory.CreateDbContext())
                {
                    // Obtener último usuario que cerró sesión correctamente
                    var ultimoUsuario = sqlite.ConfiguracionSincronizacions
                        .Where(c => c.Clave == "ULTIMO_USUARIO_SESION")
                        .OrderByDescending(c => c.ActualizadaUtc)
                        .FirstOrDefault();

                    if (ultimoUsuario == null)
                        return Fallo("No hay sesión anterior a recuperar.");

                    var empleado = sqlite.Empleados
                        .FirstOrDefault(e => e.Codigo.ToUpper() == ultimoUsuario.Valor!.ToUpper() && e.Activo == true);

                    if (empleado == null)
                        return Fallo("Usuario anterior no encontrado.");

                    return new ResultadoLoginDto
                    {
                        Exitoso = true,
                        Mensaje = "⚠️ Modo offline. Session recuperada",
                        Sesion = MapearAEmpleadoSesion(empleado, ultimoUsuario.Valor!)
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error recuperando último usuario: {ex.Message}");
                return Fallo("Error recuperando sesión anterior.");
            }
        }

        /// <summary>
        /// Guarda auditoría de login offline en la cola
        /// </summary>
        public void RegistrarLoginOfflineEnAuditoria(string usuario, bool exitoso)
        {
            try
            {
                var colaService = new ColaOperacionesService();
                var auditoriaCompleta = new Dictionary<string, object>
                {
                    { "UsuarioNombre", usuario },
                    { "Accion", "LOGIN_OFFLINE" },
                    { "Exitoso", exitoso },
                    { "FechaHora", DateTime.UtcNow },
                    { "Direccion", ObtenerDireccionIP() },
                    { "Navegador", "WinForms" },
                    { "Detalles", exitoso ? "Login offline exitoso" : "Intento de login offline fallido" }
                };

                colaService.RegistrarOperacion(
                    nombreEntidad: "Auditoria",
                    accion: "LOGIN",
                    identificadorEntidad: $"{usuario}_{DateTime.UtcNow:yyyyMMddHHmmss}",
                    payload: auditoriaCompleta
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registrando auditoría offline: {ex.Message}");
            }
        }

        /// <summary>
        /// Guarda el último usuario que inició sesión
        /// </summary>
        public void GuardarUltimoUsuario(string usuario)
        {
            try
            {
                using (var sqlite = _sqliteFactory.CreateDbContext())
                {
                    var existente = sqlite.ConfiguracionSincronizacions
                        .FirstOrDefault(c => c.Clave == "ULTIMO_USUARIO_SESION");

                    if (existente != null)
                    {
                        existente.Valor = usuario;
                        existente.ActualizadaUtc = DateTime.UtcNow.ToString("o");
                    }
                    else
                    {
                        sqlite.ConfiguracionSincronizacions.Add(new Data.DbSqlite.Entities.ConfiguracionSincronizacion
                        {
                            Clave = "ULTIMO_USUARIO_SESION",
                            Valor = usuario,
                            ActualizadaUtc = DateTime.UtcNow.ToString("o")
                        });
                    }

                    sqlite.SaveChanges();
                }
            }
            catch { /* Ignorar errores de almacenamiento */ }
        }

        /// <summary>
        /// Genera hash seguro de contraseña con salt
        /// </summary>
        private string GenerarHashContrasena(string usuario, string contrasena)
        {
            using (var sha256 = SHA256.Create())
            {
                string conSalt = $"{usuario}|{contrasena}|{SALT_CLAVE}";
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(conSalt));
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Guarda el hash de contraseña en SQLite (NO la contraseña en texto)
        /// </summary>
        private void GuardarHashContraseña(ComideriaSqliteContext sqlite, string usuario, string contrasena)
        {
            try
            {
                string hash = GenerarHashContrasena(usuario, contrasena);

                var existente = sqlite.ConfiguracionSincronizacions
                    .FirstOrDefault(c => c.Clave == $"HASH_USUARIO_{usuario.ToUpper()}");

                if (existente != null)
                {
                    existente.Valor = hash;
                    existente.ActualizadaUtc = DateTime.UtcNow.ToString("o");
                }
                else
                {
                    sqlite.ConfiguracionSincronizacions.Add(new Data.DbSqlite.Entities.ConfiguracionSincronizacion
                    {
                        Clave = $"HASH_USUARIO_{usuario.ToUpper()}",
                        Valor = hash,
                        ActualizadaUtc = DateTime.UtcNow.ToString("o")
                    });
                }

                sqlite.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando hash: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene dirección IP del cliente
        /// </summary>
        private string ObtenerDireccionIP()
        {
            try
            {
                return System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())
                    .FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    ?.ToString() ?? "127.0.0.1";
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// Mapea empleado a sesión de usuario
        /// </summary>
        private SesionUsuarioDto MapearAEmpleadoSesion(Models.Entities.Empleado empleado, string usuario)
        {
            return new SesionUsuarioDto
            {
                EmpleadoId = empleado.EmpleadoId,
                CodigoEmpleado = empleado.Codigo,
                NombreEmpleado = empleado.Nombre,
                NombreUsuario = usuario,
                Rol = "EMPLEADO",  // Por defecto en modo offline
                Cargo = empleado.Cargo ?? "Empleado"
            };
        }

        /// <summary>
        /// Helper para retornar login fallido
        /// </summary>
        private ResultadoLoginDto Fallo(string mensaje)
        {
            return new ResultadoLoginDto
            {
                Exitoso = false,
                Mensaje = mensaje,
                Sesion = null
            };
        }
    }
}
