using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;


namespace Nuevo_Proyecto.Services
{
    public abstract class DataBaseConnection: IDisposable
    {
        protected SqlConnection? _connection;

        protected SqlCommand? _Command;

        private bool _disposed = false;


        // Contructor por defecto: lee de appsettings.json

        protected DataBaseConnection() 
        {
            string ConnectionStrings = getConnectionStrings();
            _connection = new SqlConnection();
            
        
        }

        // Constructor alternativo: recibe cadena explicita

        protected DataBaseConnection(string connectionString)
        {
            _connection = new SqlConnection(connectionString);

        }

        protected void OpenConnection() 
        {
            if (_connection == null)
                throw new InvalidOperationException("La coneccion no ha sido inicializada.");

            if (_connection.State == System.Data.ConnectionState.Closed)
                _connection.Open();
        }

        protected void CloseConnection() 
        {

            if (_connection is not null &&
                _connection.State == System.Data.ConnectionState.Open) 
            
            {
                _connection.Close();
            }



        }

        private static string getConnectionStrings() 
        {

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false , reloadOnChange: false)
                .Build();

            string? connStr = config.GetConnectionString("Dev_ComideriaDbConnection");

            if (string.IsNullOrEmpty(connStr))
                throw new InvalidCastException(
                    "No se encontro 'Dev_ComideriDbConnetion' en appsettings.json.");
            return connStr;





        }

        public bool TestConnection() 
        {
            try
            {
                OpenConnection();
                return true;
            }
            catch
            {
                return false;
            }
            finally 
            {
                CloseConnection();
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    CloseConnection();
                    _connection?.Dispose();
                }
                _disposed = true;
            }
        }
        protected int ExecuteNonQuery(string sql, SqlParameter[] parametros)
        {
            try
            {
                OpenConnection();
                _Command = new SqlCommand(sql, _connection);
                _Command.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                    _Command.Parameters.AddRange(parametros);

                return _Command.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }








    }
}
