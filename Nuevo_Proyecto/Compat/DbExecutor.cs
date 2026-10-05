using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Nuevo_Proyecto.Services.Helpers;

namespace Nuevo_Proyecto.Compat
{
    /// <summary>
    /// Implementación mínima de ejecuciones SQL usada por los stubs de compatibilidad.
    /// - Para SELECT devuelve el DataTable con los rows devueltos por la consulta.
    /// - Para INSERT/UPDATE/DELETE ejecuta ExecuteNonQuery y devuelve un DataTable con
    ///   una fila (columna AffectedRows) si hubo filas afectadas, para que las antiguas
    ///   llamadas que miran dt.Rows.Count > 0 sigan funcionando.
    /// </summary>
    public static class DbExecutor
    {
        public static DataTable ExecuteQuery(string sql, SqlParameter[]? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                return new DataTable();

            var dt = new DataTable();

            using var conn = new SqlConnection(AppConfig.ConnectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            conn.Open();

            var trimmed = sql.TrimStart();
            if (trimmed.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase)
                || trimmed.StartsWith("WITH", StringComparison.OrdinalIgnoreCase))
            {
                using var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }

            // Non-query (INSERT/UPDATE/DELETE/...)
            var affected = cmd.ExecuteNonQuery();
            if (affected > 0)
            {
                dt.Columns.Add("AffectedRows", typeof(int));
                var row = dt.NewRow();
                row[0] = affected;
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
