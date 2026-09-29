using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;

namespace Nuevo_Proyecto.Presenters
{
    // Utilidad ligera para ejecutar consultas parametrizadas cuando no existe entidad
    public static class DbExecutor
    {
        public static DataTable ExecuteQuery(string sql, SqlParameter[]? parameters = null)
        {
            using var db = new Dev_ComideriaDbContext();
            var conn = db.Database.GetDbConnection();
            var dt = new DataTable();

            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        var dp = cmd.CreateParameter();
                        dp.ParameterName = p.ParameterName;
                        dp.Value = p.Value ?? DBNull.Value;
                        dp.DbType = ConvertSqlDbTypeToDbType(p.SqlDbType);
                        cmd.Parameters.Add(dp);
                    }
                }

                using var reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            return dt;
        }

        private static DbType ConvertSqlDbTypeToDbType(SqlDbType sqlDbType)
        {
            // Conversión básica necesaria para crear parámetros DbParameter
            return sqlDbType switch
            {
                SqlDbType.Int => DbType.Int32,
                SqlDbType.VarChar => DbType.String,
                SqlDbType.NVarChar => DbType.String,
                SqlDbType.DateTime => DbType.DateTime,
                SqlDbType.Decimal => DbType.Decimal,
                SqlDbType.Bit => DbType.Boolean,
                SqlDbType.BigInt => DbType.Int64,
                _ => DbType.String,
            };
        }
    }
}
