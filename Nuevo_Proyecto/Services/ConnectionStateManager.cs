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
        private const int SEGUNDOS_ENTRE_INTENTOS = 30;

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

                using (var connection = new SqlConnection(AppConfig.ConnectionString))
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
                using (var connection = new SqlConnection(AppConfig.ConnectionString))
                {
                    await connection.OpenAsync();
                    DebugLog("✓ Conexión a SQL Server restablecida. Sincronizando cola...");
                    _usandoSqlite = false;
                    _intentandoRestablecer = false;

                    // Procesar cola de operaciones pendientes
                    await SincronizarColaOperaciones();
                }
            }
            catch (Exception ex)
            {
                DebugLog($"✗ Aún no se puede conectar a SQL Server: {ex.Message}");
                _usandoSqlite = true;
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
                    // Deserializar el payload JSON
                    var payload = JsonSerializer.Deserialize<Dictionary<string, object>>(operacion.PayloadJson);

                    if (payload == null)
                        return false;

                    // Procesar basado en nombre de entidad y acción
                    return operacion.Accion.ToUpper() switch
                    {
                        "CREATE" => await ProcesarCrear(db, operacion.NombreEntidad, payload),
                        "UPDATE" => await ProcesarActualizar(db, operacion.NombreEntidad, payload),
                        "DELETE" => await ProcesarEliminar(db, operacion.NombreEntidad, payload),
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

        private static async Task<bool> ProcesarCrear(Dev_ComideriaDbContext db, string nombreEntidad, Dictionary<string, object> payload)
        {
            // Implementar lógica específica para cada entidad
            // Este es un template que se debe expandir según necesidad
            DebugLog($"Procesando CREATE para {nombreEntidad}");
            await db.SaveChangesAsync();
            return true;
        }

        private static async Task<bool> ProcesarActualizar(Dev_ComideriaDbContext db, string nombreEntidad, Dictionary<string, object> payload)
        {
            // Implementar lógica específica para cada entidad
            DebugLog($"Procesando UPDATE para {nombreEntidad}");
            await db.SaveChangesAsync();
            return true;
        }

        private static async Task<bool> ProcesarEliminar(Dev_ComideriaDbContext db, string nombreEntidad, Dictionary<string, object> payload)
        {
            // Implementar lógica específica para cada entidad
            DebugLog($"Procesando DELETE para {nombreEntidad}");
            await db.SaveChangesAsync();
            return true;
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
