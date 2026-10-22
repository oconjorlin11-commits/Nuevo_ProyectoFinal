using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Data.DbSqlite;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Factory que automáticamente elige entre SQL Server y SQLite basado en conectividad
    /// Cuando hay error de conexión, automáticamente usa SQLite como respaldo
    /// </summary>
    public class HybridContextFactory
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _sqlFactory;
        private readonly IDbContextFactory<ComideriaSqliteContext> _sqliteFactory;

        public HybridContextFactory()
        {
            _sqlFactory = new ComideriaContextFactory();
            _sqliteFactory = new ComideriaSqliteContextFactory();
        }

        /// <summary>
        /// Crea un contexto del tipo apropiado basado en disponibilidad de conexión
        /// </summary>
        public Dev_ComideriaDbContext CreateSqlContext()
        {
            if (ConnectionStateManager.UsandoSqlite)
            {
                throw new InvalidOperationException(
                    "SQL Server no está disponible. Use CreateSqliteContext() o restablezca la conexión."
                );
            }
            return _sqlFactory.CreateDbContext();
        }

        /// <summary>
        /// Crea un contexto SQLite para operaciones offline
        /// </summary>
        public ComideriaSqliteContext CreateSqliteContext()
        {
            return _sqliteFactory.CreateDbContext();
        }

        /// <summary>
        /// Intenta usar SQL Server, si falla usa SQLite automáticamente
        /// </summary>
        public async Task<T> EjecutarConFallback<T>(
            Func<Dev_ComideriaDbContext, Task<T>> operacionSql,
            Func<ComideriaSqliteContext, Task<T>> operacionSqlite)
        {
            if (!ConnectionStateManager.UsandoSqlite)
            {
                try
                {
                    using (var db = CreateSqlContext())
                    {
                        return await operacionSql(db);
                    }
                }
                catch (Microsoft.Data.SqlClient.SqlException ex) when (EsErrorDeConexion(ex))
                {
                    // Error de conexión - cambiar a SQLite
                    ConnectionStateManager.VerificarConexionAlServidor();
                }
            }

            // Usar SQLite como fallback
            using (var db = CreateSqliteContext())
            {
                return await operacionSqlite(db);
            }
        }

        /// <summary>
        /// Intenta usar SQL Server, si falla usa SQLite automáticamente (versión síncrona)
        /// </summary>
        public T EjecutarConFallbackSync<T>(
            Func<Dev_ComideriaDbContext, T> operacionSql,
            Func<ComideriaSqliteContext, T> operacionSqlite)
        {
            if (!ConnectionStateManager.UsandoSqlite)
            {
                try
                {
                    using (var db = CreateSqlContext())
                    {
                        return operacionSql(db);
                    }
                }
                catch (Microsoft.Data.SqlClient.SqlException ex) when (EsErrorDeConexion(ex))
                {
                    // Error de conexión - cambiar a SQLite
                    ConnectionStateManager.VerificarConexionAlServidor();
                }
            }

            // Usar SQLite como fallback
            using (var db = CreateSqliteContext())
            {
                return operacionSqlite(db);
            }
        }

        /// <summary>
        /// Determina si un SqlException es de conexión (que justifica fallback a SQLite)
        /// Códigos de error comunes: 26, 40, 64, 233, 4060, 40197, 40501, 40613
        /// </summary>
        private static bool EsErrorDeConexion(Microsoft.Data.SqlClient.SqlException ex)
        {
            // Errores de conexión comunes en SQL Server
            int[] erroresConexion = 
            { 
                26,     // Error al buscar servidor/instancia especificado
                40,     // No se pudo establecer conexión
                64,     // Error al comunicarse con servidor
                233,    // La conexión se inicializó con un error
                4060,   // No se puede abrir la base de datos
                40197,  // Límite de recursos alcanzado
                40501,  // Servicio ocupado
                40613   // Base de datos no disponible
            };

            return ex.Number == 0 || erroresConexion.Contains(ex.Number) || ex.Message.Contains("network");
        }
    }
}
