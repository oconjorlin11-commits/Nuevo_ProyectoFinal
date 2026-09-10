using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Subclase para ejecutar operaciones Delete.
    /// </summary>


    public  class DeleteCommand  : DataBaseConnection
    {

        public DeleteCommand() : base() { }

        public DeleteCommand (string connectionString) : base(connectionString) { }

        /// <summary>
        /// Ejecuta un DELETE y devuelve el número de filas eliminadas.
        /// </summary>
        /// <param name="query">Sentencia DELETE parametrizada.</param>
        /// <param name="parameters">Parámetros SQL.</param>
        /// <returns>Número de filas eliminadas.</returns>
        public int ExecuteDelete(string query, SqlParameter[] parameters = null)
        {
            try
            {
                OpenConnection();
                using SqlCommand cmd = new SqlCommand(query, _connection);
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al ejecutar Delete: {ex.Message}", ex);
            }
            finally
            {
                CloseConnection();
            }


        }

        ///////////////////////////////////////////////////// CLIENTE ////////////////////////////////////////////////////////



        public int EliminarCliente(string codigo)
        {
            int filasAfectadas = 0;

            try
            {
                OpenConnection();

                string query = @"UPDATE Clientes 
                     SET Activo = 0 
                     WHERE Codigo = @Codigo";

                _Command = new SqlCommand(query, _connection);
                _Command.Parameters.AddWithValue("@Codigo", codigo);

                filasAfectadas = _Command.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }

            return filasAfectadas;
        }


        ///////////////////////////////////////////////////// EMPLEADO ////////////////////////////////////////////////////////

        public int EliminarEmpleado(string codigo)
        {
            int filas = 0;
            try
            {
                OpenConnection();
                string query = @"UPDATE Empleados SET Activo = 0 WHERE Codigo = @Codigo";
                SqlCommand cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                filas = cmd.ExecuteNonQuery();
            }
            finally { CloseConnection(); }
            return filas;
        }

        ///////////////////////////////////////////////////// INVENTARIO ////////////////////////////////////////////////////////


        public int InhabilitarProducto(int productoID, int empleadoID, string observacion)
        {
            if (this._connection.State != ConnectionState.Open)
            {
                this._connection.Open();
            }

            // 👉 Obtener stock anterior desde Inventario
            int stockAnterior = 0;
            using (SqlCommand cmdSelect = new SqlCommand(
                "SELECT Stock FROM Inventario WHERE ProductoID=@ProductoID", this._connection))
            {
                cmdSelect.Parameters.AddWithValue("@ProductoID", productoID);
                object result = cmdSelect.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    stockAnterior = Convert.ToInt32(result);
                }
            }

            // 👉 Inhabilitar producto (Activo = 0 en Productos)
            int filasProd = 0;
            using (SqlCommand cmdUpdateProd = new SqlCommand(
                "UPDATE Productos SET Activo=0 WHERE ProductoID=@ProductoID", this._connection))
            {
                cmdUpdateProd.Parameters.AddWithValue("@ProductoID", productoID);
                filasProd = cmdUpdateProd.ExecuteNonQuery();
            }

            // 👉 Poner stock en 0 en Inventario
            int filasInv = 0;
            using (SqlCommand cmdUpdateInv = new SqlCommand(
                "UPDATE Inventario SET Stock=0 WHERE ProductoID=@ProductoID", this._connection))
            {
                cmdUpdateInv.Parameters.AddWithValue("@ProductoID", productoID);
                filasInv = cmdUpdateInv.ExecuteNonQuery();
            }

            // 👉 Registrar movimiento con SP general
            using (SqlCommand cmdMov = new SqlCommand("sp_RegistrarMovimientoInventario", this._connection))
            {
                cmdMov.CommandType = CommandType.StoredProcedure;
                cmdMov.Parameters.AddWithValue("@ProductoID", productoID);
                cmdMov.Parameters.AddWithValue("@Tipo", "Inhabilitación producto");
                cmdMov.Parameters.AddWithValue("@Cantidad", -stockAnterior); // se descuenta todo
                cmdMov.Parameters.AddWithValue("@StockAnterior", stockAnterior);
                cmdMov.Parameters.AddWithValue("@StockNuevo", 0);
                cmdMov.Parameters.AddWithValue("@EmpleadoID", empleadoID);
                cmdMov.Parameters.AddWithValue("@Observacion", observacion);
                cmdMov.ExecuteNonQuery();
            }

            return filasProd + filasInv;
        }


    }
}
