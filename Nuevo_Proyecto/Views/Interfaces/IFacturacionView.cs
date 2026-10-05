using System;
using System.Data;

namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IFacturacionView
    {
        // Métodos de mostrar mensajes
        void showMessage(string message, string titulo, bool esError);
        void ResetFields();

        // Métodos de carga de datos (para los ComboBox)
        void LoadCategorias(DataTable categorias);
        void LoadProductosPorCategoria(DataTable productos);
        void LoadEmpleados(DataTable empleados);
        void LoadClientes(DataTable clientes);
        void LoadFormasPago(DataTable formasPago);

        // Propiedades para obtener valores de la UI
        int? ClienteSeleccionado { get; }
        int? EmpleadoSeleccionado { get; }
        int? FormaPagoSeleccionado { get; }
        int? CategoriaSeleccionada { get; }
        int? ProductoSeleccionado { get; }
        int CantidadProducto { get; }
        string Observacion { get; }
        decimal Total { get; set; }

        // Métodos para el DataGrid de detalles
        void AgregarLineaDetalle(int productoId, string nombreProducto, int cantidad, decimal precioUnitario, decimal subtotal);
        void LimpiarDetalles();
        int ObtenerFilasDetalles();

        // Eventos de botones (métodos stub)
        event EventHandler NuevoClienteClick;
        event EventHandler AgregarProductoClick;
        event EventHandler QuitarLineaClick;
        event EventHandler LimpiarTodoClick;
        event EventHandler VerImprimirClick;
        event EventHandler GuardarFacturaClick;
    }
}
