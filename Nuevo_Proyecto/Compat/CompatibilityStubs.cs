using System.Data;
using Microsoft.Data.SqlClient;
using Nuevo_Proyecto.Presenters;

namespace Nuevo_Proyecto.Compat
{
    // Stubs de compatibilidad para facilitar la migración: delegan a DbExecutor.
    public static class SelectQuery
    {
        public static DataTable ExecuteSelect(string sql, SqlParameter[]? parameters = null)
        {
            return DbExecutor.ExecuteQuery(sql, parameters);
        }
    }

    public static class InsertCommand
    {
        public static int ExecuteInsert(string sql, SqlParameter[]? parameters = null)
        {
            // Ejecuta y retorna filas afectadas
            var dt = DbExecutor.ExecuteQuery(sql, parameters);
            return dt.Rows.Count; // aproximación
        }
    }

    public static class UpdateCommand
    {
        public static int ExecuteUpdate(string sql, SqlParameter[]? parameters = null)
        {
            var dt = DbExecutor.ExecuteQuery(sql, parameters);
            return dt.Rows.Count;
        }
    }

    public static class DeleteCommand
    {
        public static int ExecuteDelete(string sql, SqlParameter[]? parameters = null)
        {
            var dt = DbExecutor.ExecuteQuery(sql, parameters);
            return dt.Rows.Count;
        }
    }
}
