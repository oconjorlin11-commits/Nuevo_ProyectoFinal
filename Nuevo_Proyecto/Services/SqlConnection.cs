namespace Nuevo_Proyecto.Services
{
    internal class SqlConnection
    {
        private string connectionString;

        public SqlConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}