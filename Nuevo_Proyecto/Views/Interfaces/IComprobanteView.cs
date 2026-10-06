using System.Data;

namespace Nuevo_Proyecto.Views.Interfaces
{
    /// <summary>
    /// Contrato para la visualización e impresión de comprobantes de factura.
    /// Reemplaza la incorrecta implementación de IFacturacionView.
    /// </summary>
    public interface IComprobanteView
    {
        void MostrarComprobante(DataTable detalleFactura);
        void showMessage(string message, string titulo, bool esError);
    }
}

