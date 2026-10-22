using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Data.DbSqlite;
using Nuevo_Proyecto.Services.Helpers;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Clase base para repositories que necesitan resilencia offline
    /// Maneja automáticamente la captura de errores de conexión y guardado en cola
    /// </summary>
    public abstract class RepositoryBaseResilient
    {
        protected readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;
        protected readonly ColaOperacionesService _colaService;

        protected RepositoryBaseResilient(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
            _colaService = new ColaOperacionesService();
        }

        /// <summary>
        /// Ejecuta una operación con manejo automático de fallos de conexión
        /// Guarda en la cola si falla la conexión a SQL Server
        /// </summary>
        protected T EjecutarConResiliencia<T>(
            Func<Dev_ComideriaDbContext, T> operacion,
            string nombreEntidad,
            string accion,
            string identificadorEntidad,
            object? payloadParaCola = null,
            T? valorPorDefecto = default)
        {
            try
            {
                if (ConnectionStateManager.UsandoSqlite)
                {
                    throw new InvalidOperationException("Usando SQLite como fallback");
                }

                using (var db = _factory.CreateDbContext())
                {
                    return operacion(db);
                }
            }
            catch (SqlException ex)
            {
                ManejarErrorConexion(
                    nombreEntidad: nombreEntidad,
                    accion: accion,
                    identificadorEntidad: identificadorEntidad,
                    payload: payloadParaCola,
                    excepcion: ex
                );
                return valorPorDefecto!;
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("SQLite"))
            {
                ManejarErrorConexion(
                    nombreEntidad: nombreEntidad,
                    accion: accion,
                    identificadorEntidad: identificadorEntidad,
                    payload: payloadParaCola,
                    excepcion: ex
                );
                return valorPorDefecto!;
            }
            catch (Exception ex)
            {
                MensajeHelper.ErrorSistema($"Error inesperado: {ex.Message}");
                return valorPorDefecto!;
            }
        }

        /// <summary>
        /// Maneja el error de conexión guardando en la cola
        /// </summary>
        protected void ManejarErrorConexion(
            string nombreEntidad,
            string accion,
            string identificadorEntidad,
            object? payload,
            Exception excepcion)
        {
            try
            {
                // Cambiar a SQLite si no estamos ya
                if (!ConnectionStateManager.UsandoSqlite)
                {
                    ConnectionStateManager.VerificarConexionAlServidor();
                }

                // Registrar en la cola
                if (payload != null)
                {
                    _colaService.RegistrarOperacion(
                        nombreEntidad: nombreEntidad,
                        accion: accion,
                        identificadorEntidad: identificadorEntidad,
                        payload: payload
                    );
                }

                Console.WriteLine($"⚠️ Operación {accion} en {nombreEntidad} guardada en cola para sincronización posterior");

                // Mostrar mensaje al usuario
                MensajeHelper.Advertencia(
                    $"No hay conexión al servidor en este momento.\n\n" +
                    $"La operación ha sido guardada localmente y se sincronizará\n" +
                    $"automáticamente cuando se restablezca la conexión.",
                    "Modo Offline"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error manejando error de conexión: {ex.Message}");
            }
        }
    }
}
