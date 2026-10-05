using System.Data;
using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Presenters
{
    /// <summary>
    /// Adaptador DTO -> DataTable para los formularios WinForms que aún enlazan combos y grillas
    /// con DataTable. Está en UN solo lugar; cuando una pantalla pase a enlazar DTOs directamente
    /// (como ListCustomerForm en VetCare) se deja de usar aquí.
    /// </summary>
    public static class DataTableMapper
    {
        public static DataTable Lookup(IEnumerable<LookupDto> items, string columnaId, string columnaNombre = "Nombre")
        {
            var dt = new DataTable();
            dt.Columns.Add(columnaId, typeof(int));
            dt.Columns.Add(columnaNombre, typeof(string));
            foreach (var i in items) dt.Rows.Add(i.Id, i.Nombre);
            return dt;
        }

        public static DataTable Notas(IEnumerable<string> notas)
        {
            var dt = new DataTable();
            dt.Columns.Add("Nota", typeof(string));
            foreach (var n in notas) dt.Rows.Add(n);
            return dt;
        }

        public static DataTable Cargos(IEnumerable<string> cargos)
        {
            var dt = new DataTable();
            dt.Columns.Add("Cargo", typeof(string));
            foreach (var c in cargos) dt.Rows.Add(c);
            return dt;
        }

        public static DataTable Clientes(IEnumerable<ClienteDto> clientes)
        {
            var dt = new DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Direccion");
            dt.Columns.Add("Nota");
            dt.Columns.Add("Activo", typeof(bool));
            foreach (var c in clientes) dt.Rows.Add(c.Codigo, c.Nombre, c.Telefono, c.Direccion, c.Nota, c.Activo);
            return dt;
        }

        public static DataTable Empleados(IEnumerable<EmpleadoDto> empleados)
        {
            var dt = new DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Cargo");
            dt.Columns.Add("FechaIngreso");
            dt.Columns.Add("Cedula");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Salario");
            dt.Columns.Add("Activo", typeof(bool));
            foreach (var e in empleados)
                dt.Rows.Add(e.Codigo, e.Nombre, e.Cargo, e.FechaIngreso, e.Cedula, e.Telefono, e.Salario, e.Activo);
            return dt;
        }

        public static DataTable EmpleadosCodigoNombre(IEnumerable<EmpleadoDto> empleados)
        {
            var dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            foreach (var e in empleados) dt.Rows.Add(e.Codigo, e.Nombre);
            return dt;
        }

        public static DataTable EmpleadosIdNombreCargo(IEnumerable<EmpleadoDto> empleados)
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpleadoID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Cargo", typeof(string));
            foreach (var e in empleados) dt.Rows.Add(e.EmpleadoId, e.Nombre, e.Cargo);
            return dt;
        }

        public static DataTable Inventario(IEnumerable<ProductoInventarioDto> items)
        {
            var dt = new DataTable();
            dt.Columns.Add("ProductoID", typeof(int));
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("CategoriaID", typeof(int));
            dt.Columns.Add("Categoria", typeof(string));
            dt.Columns.Add("UnidadID", typeof(int));
            dt.Columns.Add("Unidad", typeof(string));
            dt.Columns.Add("PrecioVenta", typeof(decimal));
            dt.Columns.Add("Stock", typeof(int));
            dt.Columns.Add("StockMinimo", typeof(int));
            dt.Columns.Add("Activo", typeof(bool));
            dt.Columns.Add("Observacion", typeof(string));
            foreach (var i in items)
                dt.Rows.Add(i.ProductoId, i.Codigo, i.Nombre, i.CategoriaId, i.Categoria, i.UnidadId, i.Unidad,
                            i.PrecioVenta, i.Stock, i.StockMinimo, i.Activo, string.Empty);
            return dt;
        }

        public static DataTable ProductosParaVenta(IEnumerable<ProductoInventarioDto> items)
        {
            var dt = new DataTable();
            dt.Columns.Add("ProductoID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("PrecioVenta", typeof(decimal));
            foreach (var i in items) dt.Rows.Add(i.ProductoId, i.Nombre, i.PrecioVenta);
            return dt;
        }

        public static DataTable Movimientos(IEnumerable<MovimientoInventarioDto> items)
        {
            var dt = new DataTable();
            dt.Columns.Add("MovimientoID", typeof(int));
            dt.Columns.Add("Fecha", typeof(DateTime));
            dt.Columns.Add("Tipo", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("StockAnterior", typeof(int));
            dt.Columns.Add("StockNuevo", typeof(int));
            dt.Columns.Add("Observacion", typeof(string));
            dt.Columns.Add("NombreProducto", typeof(string));
            dt.Columns.Add("NombreEmpleado", typeof(string));
            dt.Columns.Add("PrecioVenta", typeof(decimal));
            dt.Columns.Add("ValorInventario", typeof(decimal));
            dt.Columns.Add("CantidadFacturasEmitidas", typeof(int));
            dt.Columns.Add("CantidadProductosDescontados", typeof(int));
            foreach (var m in items)
                dt.Rows.Add(m.MovimientoId, m.Fecha, m.Tipo, m.Cantidad, m.StockAnterior, m.StockNuevo,
                            m.Observacion, m.NombreProducto, m.NombreEmpleado, m.PrecioVenta, m.ValorInventario, 0, 0);
            return dt;
        }

        public static DataTable FacturasResumen(IEnumerable<FacturaResumenDto> items)
        {
            var dt = new DataTable();
            dt.Columns.Add("Numero", typeof(string));
            dt.Columns.Add("Fecha", typeof(DateTime));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("Empleado", typeof(string));
            dt.Columns.Add("FormaPago", typeof(string));
            dt.Columns.Add("Estado", typeof(string));
            foreach (var f in items) dt.Rows.Add(f.Numero, f.Fecha, f.Total, f.Empleado, f.FormaPago, f.Estado);
            return dt;
        }

        public static DataTable FacturaDetalle(IEnumerable<FacturaDetalleDto> items)
        {
            var dt = new DataTable();
            dt.Columns.Add("Numero", typeof(string));
            dt.Columns.Add("Fecha", typeof(DateTime));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Empleado", typeof(string));
            dt.Columns.Add("FormaPago", typeof(string));
            dt.Columns.Add("Estado", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Subtotal", typeof(decimal));
            foreach (var d in items)
                dt.Rows.Add(d.Numero, d.Fecha, d.Cliente, d.Empleado, d.FormaPago, d.Estado, d.Producto, d.Cantidad, d.Precio, d.Subtotal);
            return dt;
        }
    }
}
