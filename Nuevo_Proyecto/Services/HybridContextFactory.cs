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

            if (!ConnectionStateManager.VerificarConexionAlServidor())
            {
                throw new InvalidOperationException(
                    "SQL Server no está disponible. La operación se debe ejecutar en modo SQLite temporal o esperar a la reconexión."
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
                catch (Exception ex) when (EsErrorDeConexion(ex))
                {
                    ConnectionStateManager.VerificarConexionAlServidor();
                }
            }

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
                catch (Exception ex) when (EsErrorDeConexion(ex))
                {
                    ConnectionStateManager.VerificarConexionAlServidor();
                }
            }

            using (var db = CreateSqliteContext())
            {
                return operacionSqlite(db);
            }
        }

        /// <summary>
        /// Determina si un SqlException es de conexión (que justifica fallback a SQLite)
        /// Códigos de error comunes: 26, 40, 64, 233, 4060, 40197, 40501, 40613
        /// </summary>
        private static bool EsErrorDeConexion(Exception ex)
        {
            if (ex is Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                int[] erroresConexion =
                {
                    26, 40, 64, 233, 4060, 40197, 40501, 40613
                };

                return sqlEx.Number == 0 || erroresConexion.Contains(sqlEx.Number)
                    || sqlEx.Message.Contains("network", StringComparison.OrdinalIgnoreCase)
                    || sqlEx.Message.Contains("server not found", StringComparison.OrdinalIgnoreCase)
                    || sqlEx.Message.Contains("connection timeout", StringComparison.OrdinalIgnoreCase)
                    || sqlEx.Message.Contains("error al buscar", StringComparison.OrdinalIgnoreCase);
            }

            return ex.Message.Contains("network", StringComparison.OrdinalIgnoreCase)
                || ex.Message.Contains("server not found", StringComparison.OrdinalIgnoreCase)
                || ex.Message.Contains("connection timeout", StringComparison.OrdinalIgnoreCase)
                || ex.Message.Contains("error al buscar", StringComparison.OrdinalIgnoreCase);
        }
    }
}
