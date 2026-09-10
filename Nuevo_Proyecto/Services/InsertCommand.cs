using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Nuevo_Proyecto.Services
{
    /// Subclase para ejecutar operaciones INSERT.
    /// Puede devolver el ID generado (IDENTITY) del nuevo registro.
    /// </summary>
    ///

    public class InsertCommand : DataBaseConnection
    {
        private readonly string ConnectionString =
            "Server=localhost;Database=Dev_Comideria;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";

        public InsertCommand() : base() { }
        public InsertCommand(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Ejecuta un INSERT y devuelve el número de filas afectadas.
        /// </summary>
        /// <param name="query">Sentencia INSERT parametrizada.</param>
        /// <param name="parameters">Parámetros SQL.</param>
        /// <returns>Número de filas insertadas (generalmente 1).</returns>
        /// 

        public int ExecuteInsert(string query, SqlParameter[]? parameters = null)
        {
            try
            {
                OpenConnection();

                _Command = new SqlCommand(query, _connection);
                _Command.CommandType = CommandType.Text;

                if (parameters is not null)
                    _Command.Parameters.AddRange(parameters);

                return _Command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al ejecutar INSERT: {ex.Message}", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        ////////////////////////// FACTURA INSERT ///////////////////////////////////////////

        public int InsertarFacturaConDetalle(
    int? clienteID,
    int empleadoID,
    int formaPagoID,
    string observacion,
    int estadoID,
    decimal subtotal,
    decimal total,
    DataGridView dgv,
    string codigoFactura // nuevo parámetro
)
        {
            int nuevaFacturaId = 0;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // Armar DataTable con los detalles
                    DataTable dtDetalles = new DataTable();
                    dtDetalles.Columns.Add("ProductoID", typeof(int));
                    dtDetalles.Columns.Add("Cantidad", typeof(int));
                    dtDetalles.Columns.Add("PrecioUnitario", typeof(decimal));
                    dtDetalles.Columns.Add("Subtotal", typeof(decimal));

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells["ProductoID"].Value != null)
                        {
                            dtDetalles.Rows.Add(
                                Convert.ToInt32(row.Cells["ProductoID"].Value),
                                Convert.ToInt32(row.Cells["Cantidad"].Value),
                                Convert.ToDecimal(row.Cells["PrecioUnitario"].Value),
                                Convert.ToDecimal(row.Cells["Subtotal"].Value)
                            );
                        }
                    }

                    SqlCommand cmd = new SqlCommand("sp_InsertarFacturaConDetalle", conn, tran);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ahora pasamos el código de factura
                    cmd.Parameters.AddWithValue("@Numero", codigoFactura);
                    cmd.Parameters.AddWithValue("@ClienteID", (object)clienteID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EmpleadoID", empleadoID);
                    cmd.Parameters.AddWithValue("@FormaPagoID", formaPagoID);
                    cmd.Parameters.AddWithValue("@Observacion", (object)observacion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EstadoID", estadoID);
                    cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                    cmd.Parameters.AddWithValue("@Total", total);

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Detalles", dtDetalles);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "DetalleFacturaType";

                    nuevaFacturaId = Convert.ToInt32(cmd.ExecuteScalar());

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

            return nuevaFacturaId;
        }

        /////////////////////////// EMPLEADO INSERT //////////////////////////////////////

        public int InsertarEmpleado(
    string codigo,
    string nombre,
    string cedula,
    string telefono,
    string cargo,
    decimal salario,
    DateTime fechaIngreso,
    bool activo,
    string autorizadoPor // ⚡ solo validación, no se inserta
)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
           INSERT INTO Empleados
           (Codigo, Nombre, Cedula, Telefono, Cargo, Salario, FechaIngreso, Activo)
           VALUES (@Codigo, @Nombre, @Cedula, @Telefono, @Cargo, @Salario, @FechaIngreso, @Activo)", conn);

                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Cedula", cedula);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Cargo", cargo);
                cmd.Parameters.AddWithValue("@Salario", salario);
                cmd.Parameters.AddWithValue("@FechaIngreso", fechaIngreso);

                // ⚡ Convertimos explícitamente el bool a 0/1
                cmd.Parameters.AddWithValue("@Activo", activo ? 1 : 0);

                return cmd.ExecuteNonQuery();
            }
        }







        ///////////////////////////////////////////////////// CLIENTE INSERT ////////////////////////////////////////////////////////
        public int InsertarCliente(string codigo, string nombre, string telefono, string direccion, string nota, bool activo)
        {
            string query = @"INSERT INTO Clientes (Codigo, Nombre, Telefono, Direccion, Nota, Activo)
                            VALUES (@Codigo, @Nombre, @Telefono, @Direccion, @Nota, @Activo)";

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@Codigo", codigo),
               new SqlParameter("@Nombre", nombre),
               new SqlParameter("@Telefono", string.IsNullOrEmpty(telefono) ? DBNull.Value : telefono),
               new SqlParameter("@Direccion", string.IsNullOrEmpty(direccion) ? DBNull.Value : direccion),
               new SqlParameter("@Nota", string.IsNullOrEmpty(nota) ? DBNull.Value : nota),
               new SqlParameter("@Activo", activo)
            };

            return ExecuteInsert(query, parameters);
        }


        ///////////////////////////////////////////////////// iNVENTARIO ////////////////////////////////////////////////////////

        public int AgregarProductoConInventarioSP(
     string codigo, string nombre, int categoriaID, int unidadID,
     string descripcion, decimal precioVenta, bool activo,
     int stock, int stockMinimo, int empleadoID, string observacion)
        {
            SqlCommand cmd = new SqlCommand("sp_AgregarProductoConInventario", this._connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Codigo", codigo);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
            cmd.Parameters.AddWithValue("@UnidadID", unidadID);
            cmd.Parameters.AddWithValue("@Descripcion", descripcion);
            cmd.Parameters.AddWithValue("@PrecioVenta", precioVenta);
            cmd.Parameters.AddWithValue("@Activo", activo);
            cmd.Parameters.AddWithValue("@Stock", stock);
            cmd.Parameters.AddWithValue("@StockMinimo", stockMinimo);
            cmd.Parameters.AddWithValue("@EmpleadoID", empleadoID);
            cmd.Parameters.AddWithValue("@Observacion", observacion);

            if (this._connection.State == ConnectionState.Closed)
                this._connection.Open();

            object result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        internal int InsertarFacturaConDetalle(int? v1, int v2, int v3, string? v4, decimal v5, decimal v6, DataGridView dataGridDetallesFacturas, string codigoFactura)
        {
            throw new NotImplementedException();
        }
    }
}
