using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Subclase para ejecutar operaciones UPDATE.
    /// </summary>
    public class UpdateCommand : DataBaseConnection
    {
        public UpdateCommand() : base() { }
        public UpdateCommand(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Ejecuta un UPDATE y devuelve el número de filas afectadas.
        /// </summary>
        /// <param name="query">Sentencia UPDATE parametrizada.</param>
        /// <param name="parameters">Parámetros SQL.</param>
        /// <returns>Número de filas modificadas.</returns>
        /// 


        ///////////////////////////////////////////////////// CLIENTE  ////////////////////////////////////////////////////////


        public int ActualizarCliente(string codigo, string nombre, string telefono, string direccion, string nota)
        {
            int filasAfectadas = 0;

            try
            {
                OpenConnection();

                string query = @"UPDATE Clientes 
                      SET Nombre = @Nombre,
                          Telefono = @Telefono,
                          Direccion = @Direccion,
                          Nota = @Nota
                      WHERE Codigo = @Codigo AND Activo = 1"; // solo si está activo

                _Command = new SqlCommand(query, _connection);
                _Command.Parameters.AddWithValue("@Codigo", codigo);
                _Command.Parameters.AddWithValue("@Nombre", nombre);
                _Command.Parameters.AddWithValue("@Telefono", telefono);
                _Command.Parameters.AddWithValue("@Direccion", direccion);
                _Command.Parameters.AddWithValue("@Nota", nota);

                filasAfectadas = _Command.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }

            return filasAfectadas;
        }

        // 👉 Método específico para reactivar si estaba inactivo
        public int ReactivarCliente(string codigo)
        {
            int filasAfectadas = 0;

            try
            {
                OpenConnection();

                string query = @"UPDATE Clientes 
                      SET Activo = 1 
                      WHERE Codigo = @Codigo AND Activo = 0";

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

        ///////////////////////////////////////////////////// Empleado  ////////////////////////////////////////////////////////

        public int ActualizarEmpleado(string codigo, string nombre, string cargo, string cedula, string telefono, decimal salario)
        {
            int filas = 0;
            try
            {
                OpenConnection();
                string query = @"UPDATE Empleados
                      SET Nombre = @Nombre,
                          Cargo = @Cargo,
                          Cedula = @Cedula,
                          Telefono = @Telefono,
                          Salario = @Salario
                      WHERE Codigo = @Codigo";
                SqlCommand cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Cargo", cargo);
                cmd.Parameters.AddWithValue("@Cedula", cedula);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Salario", salario);
                cmd.Parameters.AddWithValue("@Codigo", codigo);

                filas = cmd.ExecuteNonQuery();
            }
            finally { CloseConnection(); }
            return filas;
        }

        public int ReactivarEmpleado(string codigo)
        {
            int filas = 0;
            try
            {
                OpenConnection();
                string query = @"UPDATE Empleados SET Activo = 1 WHERE Codigo = @Codigo";
                SqlCommand cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                filas = cmd.ExecuteNonQuery();
            }
            finally { CloseConnection(); }
            return filas;
        }


        ///////////////////////////////////////////////////// Inventario  ////////////////////////////////////////////////////////


        public int ActualizarProducto(
       int productoID,
       string nuevoNombre,
       int nuevaCategoriaID,
       int nuevaUnidadID,
       string nuevaDescripcion,
       decimal nuevoPrecioVenta,
       bool nuevoActivo,
       int nuevoStock,
       int nuevoStockMinimo,
       int empleadoID,
       string observacion)
        {
            int filasProd = 0;
            int filasInv = 0;

            using (SqlConnection conn = new SqlConnection(this._connection.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Obtener stock anterior
                        SqlCommand cmdSelect = new SqlCommand(
                            "SELECT Stock FROM Inventario WHERE ProductoID=@ProductoID", conn, tran);
                        cmdSelect.Parameters.AddWithValue("@ProductoID", productoID);
                        int stockAnterior = Convert.ToInt32(cmdSelect.ExecuteScalar());

                        // Actualizar producto
                        SqlCommand cmdUpdateProd = new SqlCommand(
                            @"UPDATE Productos 
               SET Nombre=@Nombre, CategoriaID=@CategoriaID, UnidadID=@UnidadID,
                   Descripcion=@Descripcion, PrecioVenta=@PrecioVenta, Activo=@Activo
               WHERE ProductoID=@ProductoID", conn, tran);

                        cmdUpdateProd.Parameters.AddWithValue("@Nombre", nuevoNombre);
                        cmdUpdateProd.Parameters.AddWithValue("@CategoriaID", nuevaCategoriaID);
                        cmdUpdateProd.Parameters.AddWithValue("@UnidadID", nuevaUnidadID);
                        cmdUpdateProd.Parameters.AddWithValue("@Descripcion", nuevaDescripcion);
                        cmdUpdateProd.Parameters.AddWithValue("@PrecioVenta", nuevoPrecioVenta);
                        cmdUpdateProd.Parameters.AddWithValue("@Activo", nuevoActivo);
                        cmdUpdateProd.Parameters.AddWithValue("@ProductoID", productoID);
                        filasProd = cmdUpdateProd.ExecuteNonQuery();

                        // Actualizar inventario
                        SqlCommand cmdUpdateInv = new SqlCommand(
                            @"UPDATE Inventario 
               SET Stock=@Stock, StockMinimo=@StockMinimo 
               WHERE ProductoID=@ProductoID", conn, tran);

                        cmdUpdateInv.Parameters.AddWithValue("@Stock", nuevoStock);
                        cmdUpdateInv.Parameters.AddWithValue("@StockMinimo", nuevoStockMinimo);
                        cmdUpdateInv.Parameters.AddWithValue("@ProductoID", productoID);
                        filasInv = cmdUpdateInv.ExecuteNonQuery();

                        // Registrar movimiento con SP general
                        SqlCommand cmdMov = new SqlCommand("sp_RegistrarMovimientoInventario", conn, tran);
                        cmdMov.CommandType = CommandType.StoredProcedure;
                        cmdMov.Parameters.AddWithValue("@ProductoID", productoID);
                        cmdMov.Parameters.AddWithValue("@Tipo", "Actualización producto");
                        cmdMov.Parameters.AddWithValue("@Cantidad", nuevoStock - stockAnterior);
                        cmdMov.Parameters.AddWithValue("@StockAnterior", stockAnterior);
                        cmdMov.Parameters.AddWithValue("@StockNuevo", nuevoStock);
                        cmdMov.Parameters.AddWithValue("@EmpleadoID", empleadoID);
                        cmdMov.Parameters.AddWithValue("@Observacion", observacion);
                        cmdMov.ExecuteNonQuery();

                        // Confirmar transacción
                        tran.Commit();
                    }
                    catch
                    {
                        // Si algo falla, revertir todo
                        tran.Rollback();
                        throw;
                    }
                }
            }

            return filasProd + filasInv;
        }
        public int ReactivarProducto(string codigo, int stock, int minimo)
        {
            if (this._connection.State != ConnectionState.Open)
            {
                this._connection.Open();
            }

            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE i
   SET i.Stock = @Stock,
       i.StockMinimo = @Minimo
   FROM Inventario i
   INNER JOIN Productos p ON i.ProductoID = p.ProductoID
   WHERE p.Codigo = @Codigo;
   
   UPDATE Productos
   SET Activo = 1
   WHERE Codigo = @Codigo;", this._connection))
            {
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Stock", stock);
                cmd.Parameters.AddWithValue("@Minimo", minimo);

                return cmd.ExecuteNonQuery();
            }
        }

    }
}
