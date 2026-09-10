using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data.Common;



namespace Nuevo_Proyecto.Services
{
    public class SelectQuery : DataBaseConnection
    {
        private readonly string connectionString=
             "Server=localhost;Database=Dev_Comideria;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public SelectQuery() : base() { }
        public SelectQuery(string connectionString) : base(connectionString) { }

        public DataTable ExecuteSelect(string query, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text)
        {
            OpenConnection();

            using SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.CommandType = commandType; // 👈 aquí está la clave

            if (parameters is not null)
                cmd.Parameters.AddRange(parameters);

            using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            CloseConnection();
            return dt;
        }

        ///////////////////////////////////////////////////// FACTURA  ////////////////////////////////////////////////////////
        public DataTable GetTodasLasFacturasConDetalles()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
    SELECT 
        f.Numero,
        f.Fecha,
        f.Total,
        e.Nombre AS Empleado,
        fp.Nombre AS FormaPago,
        f.Estado AS Estado
    FROM Facturas f
    INNER JOIN Empleados e ON f.EmpleadoID = e.EmpleadoID
    INNER JOIN FormasPago fp ON f.FormaPagoID = fp.FormaPagoID
    ORDER BY f.Fecha DESC", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public DataTable BuscarFacturaPorCodigo(string codigoFactura)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
    SELECT 
        f.Numero,
        f.Fecha,
        f.Total,
        e.Nombre AS Empleado,
        fp.Nombre AS FormaPago,
        f.Estado
    FROM Facturas f
    INNER JOIN Empleados e ON f.EmpleadoID = e.EmpleadoID
    INNER JOIN FormasPago fp ON f.FormaPagoID = fp.FormaPagoID
    WHERE f.Numero LIKE @codigoFactura + '%'
    ORDER BY f.Fecha DESC", conn);

                cmd.Parameters.AddWithValue("@codigoFactura", codigoFactura);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }


        public DataTable FiltrarFacturasPorFecha(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
    SELECT 
        f.Numero,
        f.Fecha,
        f.Total,
        e.Nombre AS Empleado,
        fp.Nombre AS FormaPago,
        f.Estado AS Estado
    FROM Facturas f
    INNER JOIN Empleados e ON f.EmpleadoID = e.EmpleadoID
    INNER JOIN FormasPago fp ON f.FormaPagoID = fp.FormaPagoID
    WHERE f.Fecha BETWEEN @desde AND @hasta
    ORDER BY f.Fecha DESC", conn);

                cmd.Parameters.AddWithValue("@desde", desde.Date);
                cmd.Parameters.AddWithValue("@hasta", hasta.Date);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable ObtenerDetalleFacturaPorCodigo(string codigoFactura)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
    SELECT 
        f.Numero,
        f.Fecha,
        c.Nombre AS Cliente,
        e.Nombre AS Empleado,
        fp.Nombre AS FormaPago,
        f.Estado,
        p.Nombre AS Producto,
        df.Cantidad,
        df.PrecioUnitario AS Precio,
        (df.Cantidad * df.PrecioUnitario) AS Subtotal
    FROM Facturas f
    INNER JOIN Clientes c ON f.ClienteID = c.ClienteID
    INNER JOIN Empleados e ON f.EmpleadoID = e.EmpleadoID
    INNER JOIN FormasPago fp ON f.FormaPagoID = fp.FormaPagoID
    INNER JOIN DetalleFactura df ON f.FacturaId = df.FacturaId
    INNER JOIN Productos p ON df.ProductoID = p.ProductoID
    WHERE f.Numero = @CodigoFactura", conn);

                cmd.Parameters.AddWithValue("@CodigoFactura", codigoFactura);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }


        public decimal ObtenerPrecioProducto(int productoID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT PrecioVenta FROM Productos WHERE ProductoID = @ProductoID", conn);
                cmd.Parameters.AddWithValue("@ProductoID", productoID);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        public DataTable ObtenerCategorias()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT CategoriaID, Nombre FROM Categorias", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Código actual de la factura (para agregar productos)
        public string ObtenerCodigoFacturaActual()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(FacturaID),0) FROM Facturas", conn);
                int ultimoID = Convert.ToInt32(cmd.ExecuteScalar());

                // El código actual es el último + 1 (pero se mantiene mientras agregás productos)
                int nuevoID = ultimoID + 1;
                return $"FACT-{nuevoID:D3}";
            }
        }

        // Código siguiente (cuando guardás la factura)
        public string GenerarCodigoFacturaSiguiente()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(FacturaID),0) FROM Facturas", conn);
                int ultimoID = Convert.ToInt32(cmd.ExecuteScalar());

                // Al guardar, se confirma el siguiente
                int nuevoID = ultimoID + 1;
                return $"FACT-{nuevoID:D3}";
            }
        }



        public DataTable ObtenerProductosPorCategoria(int categoriaID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ProductoID, Nombre FROM Productos WHERE CategoriaID = @CategoriaID", conn);
                cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerEmpleados()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmpleadoID, Nombre FROM Empleados", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerClientes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ClienteID, Nombre FROM Clientes", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerFormasPago()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT FormaPagoID, Nombre FROM FormasPago", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerEstados()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT EstadoID, NombreEstado FROM Estados", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }




        ///////////////////////////////////////////////////// CLIENTE  ////////////////////////////////////////////////////////



        public string GetNextCodigoCliente()
        {
            string nextCode = "CLI-001"; // valor inicial por defecto

            using (SqlConnection conn = new SqlConnection(_connection.ConnectionString))
            {
                conn.Open();

                string query = @"SELECT TOP 1 Codigo 
                 FROM Clientes 
                 ORDER BY CAST(SUBSTRING(Codigo, 5, LEN(Codigo)) AS INT) DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string lastCode = result.ToString(); // Ej: CLI-001
                    int number = int.Parse(lastCode.Substring(4)); // extrae "001" → 1
                    number++;
                    nextCode = "CLI-" + number.ToString("D3"); // genera CLI-002
                }
            }

            return nextCode;
        }
        public DataTable GetNotas()
        {
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();

                string query = "SELECT DISTINCT Nota FROM Clientes WHERE Nota IS NOT NULL ORDER BY Nota";

                _Command = new SqlCommand(query, _connection);
                SqlDataAdapter adapter = new SqlDataAdapter(_Command);
                adapter.Fill(dt);
            }
            finally
            {
                CloseConnection();
            }

            return dt;
        }



        public DataTable GetClientesActivos()
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = @"SELECT Codigo,
                        Nombre,
                        Telefono,
                        Direccion,
                        Nota,
                        Activo
                 FROM Clientes
                 WHERE Activo = 1";   // 👉 solo activos
                SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
                adapter.Fill(dt);
            }
            finally { CloseConnection(); }
            return dt;
        }



        public DataTable BuscarClientePorCodigo(string codigo)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = @"SELECT Codigo,
                        Nombre,
                        Telefono,
                        Direccion,
                        Nota,
                        Activo,
                        CASE WHEN Activo = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado
                 FROM Clientes
                 WHERE Codigo = @Codigo";
                SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
                adapter.SelectCommand.Parameters.AddWithValue("@Codigo", codigo);
                adapter.Fill(dt);
            }
            finally { CloseConnection(); }
            return dt;
        }


        ///////////////////////////////////////////////////// EMPLEADO  ////////////////////////////////////////////////////////



        public bool EsEmpleadoAdmin(string nombreEmpleado)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
    SELECT COUNT(*) 
    FROM Empleados 
    WHERE Nombre = @nombre AND Cargo = 'Admin' AND Activo = 1", conn);

                cmd.Parameters.AddWithValue("@nombre", nombreEmpleado);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool EsEmpleadoCajero(string nombreEmpleado)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
    SELECT COUNT(*) 
    FROM Empleados 
    WHERE Nombre = @nombre AND Cargo = 'Cajero' AND Activo = 1", conn);

                cmd.Parameters.AddWithValue("@nombre", nombreEmpleado);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public DataTable GetUsuariosCajerosAdmins()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(@"
    SELECT Codigo, Nombre 
    FROM Empleados 
    WHERE (Cargo = 'Admin' OR Cargo = 'Cajero') AND Activo = 1", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // 👉 Generar el próximo código de empleado
        public string GetNextCodigoEmpleado()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                // 👉 Extraemos solo la parte numérica después de 'EMP-'
                SqlCommand cmd = new SqlCommand(@"
    SELECT ISNULL(MAX(CAST(SUBSTRING(Codigo, 5, LEN(Codigo)) AS INT)), 0) + 1 
    FROM Empleados", conn);

                int nextCodigo = (int)cmd.ExecuteScalar();

                // 👉 Formato EMP-0001 (4 dígitos)
                return "EMP-" + nextCodigo.ToString("D3"); // si querés 3 dígitos: EMP-001, EMP-002...
                                                           // o "D4" si querés EMP-0001, EMP-0002...
            }
        }


        // 👉 Obtener lista de cargos actuales
        // 👉 Obtener lista de cargos desde la tabla Empleados
        public DataTable GetCargosDisponibles()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Cargo FROM Empleados", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // 👉 Obtener lista de usuarios administradores
        public DataTable GetUsuariosAdministradores()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(@"
    SELECT Codigo, Nombre 
    FROM Empleados 
    WHERE Cargo = 'Admin' AND Activo = 1", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }




        public DataTable GetEmpleadosActivos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"
    SELECT 
        EmpleadoID,
        Nombre
    FROM Empleados
    WHERE Activo = 1;", conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }


        public DataTable GetEmpleadosActivosGrid()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"
    SELECT 
        Codigo,
        Nombre,
        Cargo,
        FechaIngreso,
        Cedula,
        Telefono,
        Salario,
        Activo
    FROM Empleados
    WHERE Activo = 1;", conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }



        // 👉 Buscar por código (trae activo o despedido)
        public DataTable BuscarEmpleadoPorCodigo(string codigo)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
    SELECT Codigo, Nombre, Cargo, FechaIngreso, Cedula, Telefono, Salario, Activo
    FROM Empleados
    WHERE Codigo LIKE @codigo + '%'", conn);

                cmd.Parameters.AddWithValue("@codigo", codigo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }



        public DataTable GetCargos()
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                // 👉 Si los cargos están en la misma tabla Empleados:
                string query = @"SELECT DISTINCT Cargo FROM Empleados WHERE Cargo IS NOT NULL";

                SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
                adapter.Fill(dt);
            }
            finally { CloseConnection(); }
            return dt;
        }

        ///////////////////////////////////////////////////// INVENTARIO  ////////////////////////////////////////////////////////


        public DataTable ObtenerInventarioActivo()
        {
            string sql = @"
SELECT p.ProductoID,
       p.Codigo,
       p.Nombre,
       c.Nombre AS Categoria,
       u.Nombre AS Unidad,
       p.PrecioVenta,
       i.Stock,
       i.StockMinimo,
       (i.Stock * p.PrecioVenta) AS Valor,
       CASE WHEN p.Activo = 1 THEN 'Activo' ELSE 'Inactivo' END AS Activo
FROM Productos p
INNER JOIN Inventario i ON p.ProductoID = i.ProductoID
INNER JOIN Categorias c ON p.CategoriaID = c.CategoriaID
INNER JOIN Unidades u ON p.UnidadID = u.UnidadID
WHERE p.Activo = 1";   // 👉 solo activos

            return ExecuteSelect(sql);
        }



        public DataTable ObtenerInventarioCompleto()
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT p.ProductoID, p.Codigo, p.Nombre, c.Nombre AS Categoria,
         u.Nombre AS Unidad, p.PrecioVenta, i.Stock, i.StockMinimo, p.Activo
  FROM Productos p
  INNER JOIN Categorias c ON p.CategoriaID = c.CategoriaID
  INNER JOIN Unidades u ON p.UnidadID = u.UnidadID
  INNER JOIN Inventario i ON p.ProductoID = i.ProductoID", this._connection);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }



        // 👉 Obtener inventario filtrado por categoría
        public DataTable GetInventarioPorCategoria(int categoriaID)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT p.ProductoID, p.Codigo, p.Nombre, c.Nombre AS Categoria,
         u.Nombre AS Unidad, p.PrecioVenta, i.Stock, i.StockMinimo,
         CASE WHEN p.Activo = 1 THEN 'Activo' ELSE 'Inactivo' END AS Activo
  FROM Productos p
  INNER JOIN Categorias c ON p.CategoriaID = c.CategoriaID
  INNER JOIN Unidades u ON p.UnidadID = u.UnidadID
  INNER JOIN Inventario i ON p.ProductoID = i.ProductoID
  WHERE p.CategoriaID = @CategoriaID", this._connection);

            cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        // 👉 Buscar inventario por código o nombre

        public DataTable BuscarInventarioPorCodigoONombre(string filtro)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT p.ProductoID, p.Codigo, p.Nombre, p.CategoriaID, c.Nombre AS Categoria,
         p.UnidadID, u.Nombre AS Unidad, p.PrecioVenta, i.Stock, i.StockMinimo, p.Activo
  FROM Productos p
  INNER JOIN Categorias c ON p.CategoriaID = c.CategoriaID
  INNER JOIN Unidades u ON p.UnidadID = u.UnidadID
  INNER JOIN Inventario i ON p.ProductoID = i.ProductoID
  WHERE p.Codigo = @Filtro OR p.Nombre LIKE '%' + @Filtro + '%'", this._connection);

            cmd.Parameters.AddWithValue("@Filtro", filtro);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt; // 👉 devuelve todas las filas encontradas
        }


        public DataTable GetUnidades()
        {
            SqlCommand cmd = new SqlCommand("SELECT UnidadID, Nombre FROM Unidades", this._connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        // 👉 Obtener todas las categorías
        public DataTable GetCategoriasActivas()
        {
            SqlCommand cmd = new SqlCommand(@"
SELECT DISTINCT c.CategoriaID, c.Nombre
FROM Categorias c
INNER JOIN Productos p ON c.CategoriaID = p.CategoriaID
WHERE p.Activo = 1", this._connection);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        public DataTable GetInventarioActivoPorCategoria(int categoriaID)
        {
            SqlCommand cmd = new SqlCommand(@"
SELECT p.ProductoID,
       p.Codigo,
       p.Nombre,
       c.Nombre AS Categoria,
       u.Nombre AS Unidad,
       p.PrecioVenta,
       i.Stock,
       i.StockMinimo,
       p.Activo
FROM Productos p
INNER JOIN Inventario i ON p.ProductoID = i.ProductoID
INNER JOIN Categorias c ON p.CategoriaID = c.CategoriaID
INNER JOIN Unidades u ON p.UnidadID = u.UnidadID
WHERE p.CategoriaID = @CategoriaID
  AND p.Activo = 1", this._connection);

            cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }




        public string ObtenerProximoCodigoProducto()
        {
            using (SqlConnection conn = new SqlConnection(this._connection.ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    @"SELECT ISNULL(MAX(CAST(SUBSTRING(Codigo, 5, LEN(Codigo)) AS INT)), 0) + 1
      FROM Productos", conn);

                object result = cmd.ExecuteScalar();
                int siguiente = Convert.ToInt32(result);

                return $"PRD-{siguiente:D3}";
            }
        }

        public DataTable CargarUsuariosAdmin()
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT EmpleadoID, Nombre, Cargo FROM Empleados WHERE Cargo = 'Admin'", this._connection);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        ///////////////////////////////////////////////////// REPORTE  ////////////////////////////////////////////////////////

        public DataTable ObtenerMovimientosInventario(DateTime? desde, DateTime? hasta)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(@"
SELECT *
FROM vw_InventarioConValor
WHERE (@desde IS NULL OR Fecha >= @desde)
  AND (@hasta IS NULL OR Fecha <= @hasta)", GetConnection()))
            {
                cmd.Parameters.AddWithValue("@desde", (object)desde ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@hasta", (object)hasta ?? DBNull.Value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }


        ///////////////////////////////////////////////////// MENU  ////////////////////////////////////////////////////////


        // 1. Ventas de hoy (dinero)
        public decimal GetVentasHoy()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
    SELECT ISNULL(SUM(PrecioUnitario * Cantidad),0)
    FROM DetalleFactura DF
    INNER JOIN Facturas F ON DF.FacturaID = F.FacturaID
    WHERE CAST(F.Fecha AS DATE) = CAST(GETDATE() AS DATE)", conn);

                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        // 2. Productos vendidos hoy (cantidad)
        public int GetProductosVendidosHoy()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
    SELECT ISNULL(SUM(Cantidad),0)
    FROM DetalleFactura DF
    INNER JOIN Facturas F ON DF.FacturaID = F.FacturaID
    WHERE CAST(F.Fecha AS DATE) = CAST(GETDATE() AS DATE)", conn);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // 3. Ventas semanales (dinero)
        public decimal GetVentasSemana()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
    SELECT ISNULL(SUM(PrecioUnitario * Cantidad),0)
    FROM DetalleFactura DF
    INNER JOIN Facturas F ON DF.FacturaID = F.FacturaID
    WHERE F.Fecha >= DATEADD(DAY,-7,GETDATE())", conn);

                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        // 4. Productos vendidos semana (cantidad)
        public int GetProductosVendidosSemana()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
    SELECT ISNULL(SUM(Cantidad),0)
    FROM DetalleFactura DF
    INNER JOIN Facturas F ON DF.FacturaID = F.FacturaID
    WHERE F.Fecha >= DATEADD(DAY,-7,GETDATE())", conn);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // 5. Facturas semanales (únicas)
        public int GetFacturasSemana()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
    SELECT COUNT(DISTINCT Numero)
    FROM Facturas
    WHERE Fecha >= DATEADD(DAY,-7,GETDATE())", conn);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // 6. Valor del inventario
        public decimal GetValorInventario()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
    SELECT ISNULL(SUM(p.PrecioVenta * i.Stock),0)
    FROM Inventario i
    INNER JOIN Productos p ON i.ProductoID = p.ProductoID
    WHERE p.Activo = 1", conn);

                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }


        // Productos con stock bajo (Stock <= StockMinimo + 5)
        public DataTable GetProductosStockBajo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(@"
    SELECT p.Codigo,
           p.Nombre,
           i.Stock,
           i.StockMinimo
    FROM Inventario i
    INNER JOIN Productos p ON i.ProductoID = p.ProductoID
    WHERE i.Stock <= (i.StockMinimo + 5)
      AND p.Activo = 1
    ORDER BY i.Stock ASC", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }


        // Últimas 5 facturas
        public DataTable GetUltimasFacturas()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(@"
    SELECT TOP 5 Numero, Total, Fecha, E.Nombre AS Empleado
    FROM Facturas F
    INNER JOIN Empleados E ON F.EmpleadoID = E.EmpleadoID
    ORDER BY F.Fecha DESC", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

    }
}
