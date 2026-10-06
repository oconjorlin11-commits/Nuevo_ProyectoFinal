using System;
using System.Data;

namespace Nuevo_Proyecto.Views.Interfaces
{
    /// <summary>
    /// Contrato para el formulario de emisión y captura de facturación.
    /// Enfocada exclusivamente en la gestión de facturas activas.
    /// </summary>
    public interface IFacturacionView
    {
        // Métodos de control visual y mensajes
        void showMessage(string message, string titulo, bool esError);
        void ResetFields();

        // Métodos de carga de datos en controles
        void LoadCategorias(DataTable categorias);
        void LoadProductosPorCategoria(DataTable productos);
        void LoadEmpleados(DataTable empleados);
        void LoadClientes(DataTable clientes);
        void LoadFormasPago(DataTable formasPago);

        // Propiedades enlazadas a los controles de la vista
        int? ClienteSeleccionado { get; }
        int? EmpleadoSeleccionado { get; }
        int? FormaPagoSeleccionado { get; }
        int? CategoriaSeleccionada { get; }
        int? ProductoSeleccionado { get; }
        int CantidadProducto { get; }
        string Observacion { get; }
        decimal Total { get; set; }

        // Métodos para la grilla de líneas de detalle
        void AgregarLineaDetalle(int productoId, string nombreProducto, int cantidad, decimal precioUnitario, decimal subtotal);
        void LimpiarDetalles();
        int ObtenerFilasDetalles();
    }
}

