using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Data.DbSqlite;
using Nuevo_Proyecto.Data.DbSqlite.Entities;
using Nuevo_Proyecto.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Gestiona el estado de conexión a SQL Server y maneja fallback a SQLite
    /// Sincroniza automáticamente operaciones en cola cuando se restablece la conexión
    /// </summary>
    public static class ConnectionStateManager
    {
        private static bool _usandoSqlite = false;
        private static bool _intentandoRestablecer = false;
        private static DateTime _ultimoIntento = DateTime.UtcNow;
        private static System.Threading.Timer? _monitorConexion;
        private static readonly object _monitorLock = new();
        private static DateTime _ultimaSincronizacionSqlite = DateTime.MinValue;
        private const int SEGUNDOS_ENTRE_INTENTOS = 30;
        private const int SEGUNDOS_ENTRE_SYNC_SQLITE = 5;
        private const int SEGUNDOS_TIMEOUT_SQL = 3;

        private static string CadenaConexionConTimeout(int segundos = SEGUNDOS_TIMEOUT_SQL)
        {
            var builder = new SqlConnectionStringBuilder(AppConfig.ConnectionString)
            {
                ConnectTimeout = segundos
            };
            return builder.ConnectionString;
        }

        public static void IniciarMonitoreoConexion(int intervaloMs = 5000)
        {
            lock (_monitorLock)
            {
                if (_monitorConexion != null)
                    return;

                _monitorConexion = new System.Threading.Timer(_ =>
                {
                    try
                    {
                        if (_usandoSqlite)
                        {
                            _ = Task.Run(IntentarRestablecerConexion);
                            return;
                        }

                        // Solo comprobamos la salud de SQL Server. No forzamos una sincronización completa
                        // del catálogo cada 5 segundos porque eso vuelve el sistema lento y recarga toda la BD.
                        VerificarConexionAlServidor();
                    }
                    catch (Exception ex)
                    {
                        DebugLog($"[Monitor] Error supervisando conexión: {ex.Message}");
                    }
                }, null, intervaloMs, intervaloMs);
            }
        }

        public static void DetenerMonitoreoConexion()
        {
            lock (_monitorLock)
            {
                _monitorConexion?.Dispose();
                _monitorConexion = null;
            }
        }

        /// <summary>
        /// Obtiene si la aplicación está usando SQLite en modo offline
        /// </summary>
        public static bool UsandoSqlite => _usandoSqlite;

        /// <summary>
        /// Verifica la conexión a SQL Server y determina si usar SQLite
        /// Se ejecuta automáticamente al iniciar la aplicación
        /// </summary>
        public static bool VerificarConexionAlServidor()
        {
            try
            {
                // Evitar verificaciones muy frecuentes
                if (_intentandoRestablecer && (DateTime.UtcNow - _ultimoIntento).TotalSeconds < SEGUNDOS_ENTRE_INTENTOS)
                {
                    return !_usandoSqlite;
                }

                _ultimoIntento = DateTime.UtcNow;

                using (var connection = new SqlConnection(CadenaConexionConTimeout()))
                {
                    connection.Open();
                    _usandoSqlite = false;
                    _intentandoRestablecer = false;
                    DebugLog($"✓ Conexión a SQL Server establecida exitosamente");
                    return true;
                }
            }
            catch (SqlException ex)
            {
                DebugLog($"✗ Error de conexión a SQL Server: {ex.Number} - {ex.Message}");
                PersistirEstadoOfflineAntesDeCaer();
                _usandoSqlite = true;
                _intentandoRestablecer = true;

                // Mostrar advertencia amigable al usuario
                if (!_usandoSqlite || ex.Number == 26) // Error 26 = servidor no encontrado
                {
                    Console.WriteLine($"⚠️ No se pudo conectar a SQL Server. Usando SQLite como respaldo.");
                    Console.WriteLine($"   Se guardará una cola de operaciones para sincronizar cuando se restablezca la conexión.");
                }

                return false;
            }
            catch (Exception ex)
            {
                DebugLog($"✗ Error inesperado al verificar conexión: {ex.Message}");
                PersistirEstadoOfflineAntesDeCaer();
                _usandoSqlite = true;
                return false;
            }
        }

        /// <summary>
        /// Verifica periódicamente si se ha restablecido la conexión a SQL Server
        /// Se debe ejecutar regularmente (por ejemplo, cada X segundos)
        /// </summary>
        public static async Task IntentarRestablecerConexion()
        {
            if (!_usandoSqlite || !_intentandoRestablecer)
                return;

            // Evitar verificaciones muy frecuentes
            if ((DateTime.UtcNow - _ultimoIntento).TotalSeconds < SEGUNDOS_ENTRE_INTENTOS)
                return;

            try
            {
                using var connection = new SqlConnection(CadenaConexionConTimeout());
                await connection.OpenAsync();

                DebugLog("✓ Conexión a SQL Server restablecida. Sincronizando cola...");
                _usandoSqlite = false;
                _intentandoRestablecer = false;
                _ultimoIntento = DateTime.UtcNow;

                // Procesar cola de operaciones pendientes
                await SincronizarColaOperaciones();

                // SQLite es un caché temporal. Cuando SQL Server vuelve, debe limpiarse completamente.
                LimpiezaBDSQLite.LimpiarBDTemporal();
                DebugLog("🧹 SQLite temporal limpiado tras la sincronización completa con SQL Server.");
            }
            catch (Exception ex)
            {
                DebugLog($"✗ Aún no se puede conectar a SQL Server: {ex.Message}");
                _usandoSqlite = true;
                _intentandoRestablecer = true;
            }
        }

        private static void PersistirEstadoOfflineAntesDeCaer()
        {
            try
            {
                var offline = new AutenticacionOfflineService();
                offline.PersistirEstadoOffline();
            }
            catch (Exception ex)
            {
                DebugLog($"⚠️ No se pudo persistir estado offline: {ex.Message}");
            }
        }

        /// <summary>
        /// Procesa la cola de operaciones guardadas en SQLite
        /// Envía cada operación a SQL Server en orden
        /// </summary>
        private static async Task SincronizarColaOperaciones()
        {
            try
            {
                var sqliteContextFactory = new ComideriaSqliteContextFactory();
                using (var sqlite = sqliteContextFactory.CreateDbContext())
                {
                    // Obtener operaciones pendientes ordenadas por secuencia
                    var operacionesPendientes = await sqlite.ColaOperaciones
                        .Where(x => x.Estado == "Pendiente")
                        .OrderBy(x => x.Secuencia)
                        .ToListAsync();

                    if (operacionesPendientes.Count == 0)
                    {
                        DebugLog("No hay operaciones pendientes para sincronizar");
                        return;
                    }

                    DebugLog($"Iniciando sincronización de {operacionesPendientes.Count} operaciones...");

                    int operacionesExitosas = 0;
                    var sqlContextFactory = new ComideriaContextFactory();

                    foreach (var operacion in operacionesPendientes)
                    {
                        try
                        {
                            // Procesar cada operación basada en su tipo
                            bool exito = await ProcesarOperacion(sqlContextFactory, operacion);

                            if (exito)
                            {
                                // Marcar como confirmada
                                operacion.Estado = "Confirmada";
                                operacion.ConfirmadaUtc = DateTime.UtcNow.ToString("o");
                                operacionesExitosas++;

                                DebugLog($"✓ Operación {operacion.OperacionId} ({operacion.Accion} {operacion.NombreEntidad}) sincronizada");
                            }
                            else
                            {
                                // Marcar como fallida
                                operacion.Estado = "Error";
                                operacion.Intentos++;
                                operacion.UltimoIntentoUtc = DateTime.UtcNow.ToString("o");
                                DebugLog($"✗ Operación {operacion.OperacionId} falló al sincronizar");
                            }
                        }
                        catch (Exception ex)
                        {
                            operacion.Estado = "Error";
                            operacion.Intentos++;
                            operacion.UltimoIntentoUtc = DateTime.UtcNow.ToString("o");
                            operacion.UltimoError = ex.Message;
                            DebugLog($"✗ Error procesando operación {operacion.OperacionId}: {ex.Message}");
                        }
                    }

                    // Guardar cambios en SQLite
                    await sqlite.SaveChangesAsync();

                    DebugLog($"🔄 Sincronización completada: {operacionesExitosas}/{operacionesPendientes.Count} operaciones exitosas");
                }
            }
            catch (Exception ex)
            {
                DebugLog($"✗ Error durante sincronización de cola: {ex.Message}");
            }
        }

        /// <summary>
        /// Procesa una operación individual y la ejecuta en SQL Server
        /// </summary>
        private static async Task<bool> ProcesarOperacion(IDbContextFactory<Dev_ComideriaDbContext> sqlFactory, ColaOperacione operacion)
        {
            try
            {
                using (var db = sqlFactory.CreateDbContext())
                {
                    var payload = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(operacion.PayloadJson);
                    if (payload == null)
                        return false;

                    var accion = operacion.Accion.ToUpperInvariant();
                    var entidad = operacion.NombreEntidad.Trim();

                    if (entidad.Equals("Auditoria", StringComparison.OrdinalIgnoreCase) || accion == "LOGIN")
                    {
                        return await ProcesarAuditoria(db, payload, operacion);
                    }

                    return accion switch
                    {
                        "CREATE" or "INSERT" => await ProcesarCrear(db, entidad, payload),
                        "UPDATE" => await ProcesarActualizar(db, entidad, payload),
                        "DELETE" or "REMOVE" => await ProcesarEliminar(db, entidad, payload),
                        _ => false
                    };
                }
            }
            catch (Exception ex)
            {
                DebugLog($"Error procesando operación de tipo {operacion.Accion}: {ex.Message}");
                return false;
            }
        }

        private static async Task<bool> ProcesarAuditoria(Dev_ComideriaDbContext db, Dictionary<string, JsonElement> payload, ColaOperacione operacion)
        {
            try
            {
                var usuario = ObtenerValorCadena(payload, "UsuarioNombre", "Usuario", "UsuarioNombreUsuario") ?? "Sistema";
                var accion = ObtenerValorCadena(payload, "Accion", "Operacion", "TipoAccion") ?? operacion.Accion;
                var rolUsuario = ObtenerValorCadena(payload, "RolUsuario", "Rol") ?? "Usuario";
                var usuarioAutorizador = ObtenerValorCadena(payload, "UsuarioAutorizador", "Autorizador");
                var detalle = ObtenerValorCadena(payload, "Detalles", "Detalle", "Descripcion") ?? "Operación ejecutada en modo offline";
                var ipEquipo = ObtenerValorCadena(payload, "Direccion", "IP_Equipo", "DireccionIP") ?? "Desconocida";
                var descripcion = ObtenerValorCadena(payload, "Descripcion", "DescripcionEvento") ?? detalle;
                var fechaHora = ObtenerValorFecha(payload, "FechaHora", "Fecha") ?? DateTime.UtcNow;

                var seccion = "Sistema";
                await db.Database.ExecuteSqlInterpolatedAsync(
                    $@"EXEC dbo.usp_RegistrarAuditoria
                        @Seccion = {seccion},
                        @Accion = {accion},
                        @RegistroID = null,
                        @Usuario = {usuario},
                        @RolUsuario = {rolUsuario},
                        @UsuarioAutorizador = {usuarioAutorizador},
                        @Detalle = {detalle},
                        @IP_Equipo = {ipEquipo},
                        @Descripcion = {descripcion}");

                DebugLog($"✓ Auditoría reinsertada en SQL: {accion} / {usuario} / {fechaHora:O}");
                return true;
            }
            catch (Exception ex)
            {
                DebugLog($"✗ Error reinsertando auditoría: {ex.Message}");
                return false;
            }
        }

        private static string? ObtenerValorCadena(Dictionary<string, JsonElement> payload, params string[] nombres)
        {
            foreach (var nombre in nombres)
            {
                if (payload.TryGetValue(nombre, out var valor) && valor.ValueKind != JsonValueKind.Null)
                {
                    return valor.ValueKind switch
                    {
                        JsonValueKind.String => valor.GetString(),
                        JsonValueKind.Number => valor.GetRawText(),
                        JsonValueKind.True => "true",
                        JsonValueKind.False => "false",
                        _ => valor.GetString() ?? valor.GetRawText()
                    };
                }
            }

            return null;
        }

        private static DateTime? ObtenerValorFecha(Dictionary<string, JsonElement> payload, params string[] nombres)
        {
            foreach (var nombre in nombres)
            {
                if (payload.TryGetValue(nombre, out var valor) && valor.ValueKind != JsonValueKind.Null)
                {
                    if (valor.ValueKind == JsonValueKind.String && DateTime.TryParse(valor.GetString(), out var fecha))
                    {
                        return fecha;
                    }

                    if (valor.ValueKind == JsonValueKind.Number && long.TryParse(valor.GetRawText(), out var epoch))
                    {
                        return DateTimeOffset.FromUnixTimeSeconds(epoch).UtcDateTime;
                    }
                }
            }

            return null;
        }

        private static async Task<bool> ProcesarCrear(Dev_ComideriaDbContext db, string nombreEntidad, Dictionary<string, JsonElement> payload)
        {
            var entityType = ResolverTipoEntidad(nombreEntidad);
            if (entityType == null)
            {
                DebugLog($"No se pudo resolver tipo para creación de {nombreEntidad}");
                return false;
            }

            var entity = Activator.CreateInstance(entityType);
            if (entity == null)
                return false;

            AplicarPayloadAEntidad(entity, payload);
            var dbSet = ObtenerDbSet(db, entityType);
            dbSet.GetType().GetMethod("Add")?.Invoke(dbSet, new[] { entity });
            await db.SaveChangesAsync();
            DebugLog($"✓ CREATE reinsertado para {nombreEntidad}");
            return true;
        }

        private static async Task<bool> ProcesarActualizar(Dev_ComideriaDbContext db, string nombreEntidad, Dictionary<string, JsonElement> payload)
        {
            var entityType = ResolverTipoEntidad(nombreEntidad);
            if (entityType == null)
            {
                DebugLog($"No se pudo resolver tipo para actualización de {nombreEntidad}");
                return false;
            }

            var key = ObtenerValorClavePrimaria(payload, entityType);
            if (key == null)
            {
                DebugLog($"No se encontró clave primaria para {nombreEntidad}");
                return false;
            }

            var entity = db.Find(entityType, key);
            if (entity == null)
            {
                DebugLog($"No se encontró {nombreEntidad} con clave {key} para actualizar");
                return false;
            }

            AplicarPayloadAEntidad(entity, payload);
            await db.SaveChangesAsync();
            DebugLog($"✓ UPDATE reinsertado para {nombreEntidad}");
            return true;
        }

        private static async Task<bool> ProcesarEliminar(Dev_ComideriaDbContext db, string nombreEntidad, Dictionary<string, JsonElement> payload)
        {
            var entityType = ResolverTipoEntidad(nombreEntidad);
            if (entityType == null)
            {
                DebugLog($"No se pudo resolver tipo para eliminación de {nombreEntidad}");
                return false;
            }

            var key = ObtenerValorClavePrimaria(payload, entityType);
            if (key == null)
            {
                DebugLog($"No se encontró clave primaria para eliminar en {nombreEntidad}");
                return false;
            }

            var entity = db.Find(entityType, key);
            if (entity == null)
            {
                DebugLog($"No se encontró {nombreEntidad} con clave {key} para eliminar");
                return true;
            }

            var dbSet = ObtenerDbSet(db, entityType);
            dbSet.GetType().GetMethod("Remove")?.Invoke(dbSet, new[] { entity });
            await db.SaveChangesAsync();
            DebugLog($"✓ DELETE reinsertado para {nombreEntidad}");
            return true;
        }

        private static object ObtenerDbSet(Dev_ComideriaDbContext db, Type entityType)
        {
            var method = typeof(DbContext).GetMethod(nameof(DbContext.Set), Type.EmptyTypes);
            if (method == null)
            {
                throw new InvalidOperationException("No se pudo localizar el método Set<TEntity> en DbContext.");
            }

            return method.MakeGenericMethod(entityType).Invoke(db, null)
                ?? throw new InvalidOperationException($"No se pudo crear DbSet para {entityType.Name}.");
        }

        private static Type? ResolverTipoEntidad(string nombreEntidad)
        {
            var nombre = nombreEntidad.Trim();
            var tipos = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.Namespace == "Nuevo_Proyecto.Models.Entities")
                .FirstOrDefault(t => t.Name.Equals(nombre, StringComparison.OrdinalIgnoreCase) ||
                    t.Name.Equals(nombre + "s", StringComparison.OrdinalIgnoreCase) ||
                    t.Name.Equals(nombre.Replace("es", string.Empty), StringComparison.OrdinalIgnoreCase));

            return tipos ?? AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        private static void AplicarPayloadAEntidad(object entity, Dictionary<string, JsonElement> payload)
        {
            var type = entity.GetType();
            foreach (var property in type.GetProperties())
            {
                if (!property.CanWrite)
                    continue;

                var key = payload.Keys.FirstOrDefault(k =>
                    string.Equals(k, property.Name, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(k, property.Name + "ID", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(k, property.Name.Replace("Id", "ID", StringComparison.OrdinalIgnoreCase), StringComparison.OrdinalIgnoreCase));

                if (key == null)
                    continue;

                var value = payload[key];
                var converted = ConvertirJsonAPropiedad(value, property.PropertyType);
                if (converted != null || Nullable.GetUnderlyingType(property.PropertyType) != null)
                {
                    property.SetValue(entity, converted);
                }
            }
        }

        private static object? ObtenerValorClavePrimaria(Dictionary<string, JsonElement> payload, Type entityType)
        {
            var properties = entityType.GetProperties()
                .Where(p => p.CanRead)
                .OrderBy(p => p.Name.Length)
                .ToList();

            foreach (var property in properties)
            {
                if (!property.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) && !property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                    continue;

                var key = payload.Keys.FirstOrDefault(k => string.Equals(k, property.Name, StringComparison.OrdinalIgnoreCase));
                if (key != null)
                {
                    return ConvertirJsonAPropiedad(payload[key], property.PropertyType);
                }
            }

            var fallback = entityType.GetProperties().FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
            if (fallback != null)
            {
                var key = payload.Keys.FirstOrDefault(k => string.Equals(k, fallback.Name, StringComparison.OrdinalIgnoreCase));
                if (key != null)
                    return ConvertirJsonAPropiedad(payload[key], fallback.PropertyType);
            }

            return null;
        }

        private static object? ConvertirJsonAPropiedad(JsonElement value, Type targetType)
        {
            var type = Nullable.GetUnderlyingType(targetType) ?? targetType;
            if (value.ValueKind == JsonValueKind.Null || value.ValueKind == JsonValueKind.Undefined)
                return null;

            if (type == typeof(string))
                return value.GetString();

            if (type == typeof(int) || type == typeof(int?))
                return value.GetInt32();

            if (type == typeof(long) || type == typeof(long?))
                return value.GetInt64();

            if (type == typeof(decimal) || type == typeof(decimal?))
                return value.GetDecimal();

            if (type == typeof(double) || type == typeof(double?))
                return value.GetDouble();

            if (type == typeof(bool) || type == typeof(bool?))
                return value.GetBoolean();

            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return value.TryGetDateTime(out var fecha) ? fecha : DateTime.Parse(value.GetString() ?? DateTime.UtcNow.ToString("o"));

            if (type.IsEnum)
                return Enum.Parse(type, value.GetString() ?? string.Empty, true);

            if (type == typeof(Guid))
                return Guid.Parse(value.GetString() ?? Guid.Empty.ToString());

            return value.GetRawText();
        }

        /// <summary>
        /// Registra un mensaje de debug con timestamp
        /// </summary>
        private static void DebugLog(string mensaje)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            Debug.WriteLine($"[{timestamp}] [ConnectionState] {mensaje}");
            Console.WriteLine($"[{timestamp}] [ConnectionState] {mensaje}");
        }
    }
}
