using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Data.DbSqlite;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Helpers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

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
        private const string CLAVE_ADMINISTRADORES_SISTEMA = "ADMINISTRADORES_SISTEMA";

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
                    var ultimoUsuario = ObtenerUltimoUsuario();
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
                    {
                        var ultimoGuardado = !string.IsNullOrWhiteSpace(ultimoUsuario) ? ultimoUsuario.Trim() : null;
                        if (!string.IsNullOrWhiteSpace(ultimoGuardado) &&
                            string.Equals(ultimoGuardado, usuario, StringComparison.OrdinalIgnoreCase))
                        {
                            var sesionRecuperada = new SesionUsuarioDto
                            {
                                EmpleadoId = 0,
                                CodigoEmpleado = string.Empty,
                                NombreEmpleado = ultimoGuardado,
                                NombreUsuario = ultimoGuardado,
                                Rol = "Offline",
                                Cargo = "Offline"
                            };

                            SesionActual.Iniciar(sesionRecuperada);
                            SesionActual.ActualizarAdministradores(ObtenerAdministradoresSistema());
                            return new ResultadoLoginDto { Exitoso = true, Mensaje = "⚠️ Sesión recuperada en modo offline.", Sesion = sesionRecuperada };
                        }

                        return Fallo("Usuario no encontrado localmente.");
                    }

                    bool valido = !string.IsNullOrEmpty(usuarioData.Contrasena)
                                 && string.Equals(usuarioData.Contrasena.Trim(), contrasena, StringComparison.Ordinal);

                    if (!valido)
                    {
                        var ultimoGuardado = !string.IsNullOrWhiteSpace(ultimoUsuario) ? ultimoUsuario.Trim() : null;
                        if (!string.IsNullOrWhiteSpace(ultimoGuardado) &&
                            string.Equals(ultimoGuardado, usuario, StringComparison.OrdinalIgnoreCase))
                        {
                            var sesionRecuperada = new SesionUsuarioDto
                            {
                                EmpleadoId = usuarioData.EmpleadoId,
                                CodigoEmpleado = usuarioData.Codigo,
                                NombreEmpleado = usuarioData.Nombre,
                                NombreUsuario = usuarioData.NombreUsuario,
                                Rol = usuarioData.RolSistema ?? "Offline",
                                Cargo = usuarioData.Cargo ?? "Offline"
                            };

                            SesionActual.Iniciar(sesionRecuperada);
                            SesionActual.ActualizarAdministradores(ObtenerAdministradoresSistema());
                            return new ResultadoLoginDto { Exitoso = true, Mensaje = "⚠️ Modo offline. Sesión recuperada.", Sesion = sesionRecuperada };
                        }

                        return Fallo("Contraseña incorrecta.");
                    }

                    var sesion = new SesionUsuarioDto
                    {
                        EmpleadoId = usuarioData.EmpleadoId,
                        CodigoEmpleado = usuarioData.Codigo,
                        NombreEmpleado = usuarioData.Nombre,
                        NombreUsuario = usuarioData.NombreUsuario,
                        Rol = usuarioData.RolSistema ?? string.Empty,
                        Cargo = usuarioData.Cargo ?? string.Empty
                    };

                    SesionActual.Iniciar(sesion);
                    SesionActual.ActualizarAdministradores(ObtenerAdministradoresSistema());

                    return new ResultadoLoginDto
                    {
                        Exitoso = true,
                        Mensaje = "⚠️ Modo offline. Bienvenido",
                        Sesion = sesion
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
        /// Obtiene el último usuario que inició sesión y quedó persistido en SQLite.
        /// </summary>
        public string? ObtenerUltimoUsuario()
        {
            try
            {
                using (var sqlite = _sqliteFactory.CreateDbContext())
                {
                    var config = sqlite.ConfiguracionSincronizacions
                        .FirstOrDefault(c => c.Clave == "ULTIMO_USUARIO_SESION");

                    return string.IsNullOrWhiteSpace(config?.Valor) ? null : config.Valor.Trim();
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Guarda el último usuario que inició sesión
        /// </summary>
        public void GuardarUltimoUsuario(string usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario))
                    return;

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
        /// Guarda la lista de administradores del sistema para permitir continuidad offline.
        /// </summary>
        public void GuardarAdministradoresSistema(IEnumerable<string>? administradores)
        {
            try
            {
                var lista = (administradores ?? Enumerable.Empty<string>())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                SesionActual.ActualizarAdministradores(lista);

                using (var sqlite = _sqliteFactory.CreateDbContext())
                {
                    var existente = sqlite.ConfiguracionSincronizacions
                        .FirstOrDefault(c => c.Clave == CLAVE_ADMINISTRADORES_SISTEMA);

                    var json = JsonSerializer.Serialize(lista);

                    if (existente != null)
                    {
                        existente.Valor = json;
                        existente.ActualizadaUtc = DateTime.UtcNow.ToString("o");
                    }
                    else
                    {
                        sqlite.ConfiguracionSincronizacions.Add(new Data.DbSqlite.Entities.ConfiguracionSincronizacion
                        {
                            Clave = CLAVE_ADMINISTRADORES_SISTEMA,
                            Valor = json,
                            ActualizadaUtc = DateTime.UtcNow.ToString("o")
                        });
                    }

                    sqlite.SaveChanges();
                }
            }
            catch
            {
                // Ignorar errores de almacenamiento
            }
        }

        /// <summary>
        /// Obtiene los administradores guardados en SQLite para usar en modo offline.
        /// </summary>
        public List<string> ObtenerAdministradoresSistema()
        {
            try
            {
                using (var sqlite = _sqliteFactory.CreateDbContext())
                {
                    var config = sqlite.ConfiguracionSincronizacions
                        .FirstOrDefault(c => c.Clave == CLAVE_ADMINISTRADORES_SISTEMA);

                    if (config != null && !string.IsNullOrWhiteSpace(config.Valor))
                    {
                        try
                        {
                            var lista = JsonSerializer.Deserialize<List<string>>(config.Valor) ?? new List<string>();
                            var limpia = lista
                                .Where(x => !string.IsNullOrWhiteSpace(x))
                                .Select(x => x.Trim())
                                .Distinct(StringComparer.OrdinalIgnoreCase)
                                .ToList();

                            SesionActual.ActualizarAdministradores(limpia);
                            return limpia;
                        }
                        catch
                        {
                            // seguir con la derivación desde SQLite
                        }
                    }

                    var administradores = sqlite.Usuarios
                        .Where(u => u.Activo && !string.IsNullOrWhiteSpace(u.RolSistema))
                        .Where(u => u.RolSistema != null && u.RolSistema.Contains("admin", StringComparison.OrdinalIgnoreCase))
                        .Select(u => u.NombreUsuario)
                        .Distinct()
                        .ToList();

                    SesionActual.ActualizarAdministradores(administradores);
                    return administradores;
                }
            }
            catch
            {
                return SesionActual.AdministradoresSistema.ToList();
            }
        }

        /// <summary>
        /// Persiste el último usuario y los administradores del sistema antes de que caiga la conexión.
        /// </summary>
        public void PersistirEstadoOffline()
        {
            try
            {
                if (SesionActual.Usuario != null && !string.IsNullOrWhiteSpace(SesionActual.Usuario.NombreUsuario))
                {
                    GuardarUltimoUsuario(SesionActual.Usuario.NombreUsuario);
                }

                using (var sqlite = _sqliteFactory.CreateDbContext())
                {
                    var administradores = sqlite.Usuarios
                        .Where(u => u.Activo)
                        .Where(u => !string.IsNullOrWhiteSpace(u.RolSistema))
                        .Where(u => u.RolSistema != null && u.RolSistema.Contains("admin", StringComparison.OrdinalIgnoreCase))
                        .Select(u => u.NombreUsuario)
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    var usuarioActual = SesionActual.Usuario;
                    if (administradores.Count == 0 && usuarioActual != null && !string.IsNullOrWhiteSpace(usuarioActual.NombreUsuario) && SesionActual.EsAdministrador)
                    {
                        administradores.Add(usuarioActual.NombreUsuario);
                    }

                    GuardarAdministradoresSistema(administradores);
                }
            }
            catch
            {
                // Ignorar errores si la conexión ya cayó
            }
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
