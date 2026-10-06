using System.Data;

namespace Nuevo_Proyecto.Views.Interfaces
{
    /// <summary>
    /// Contrato para el formulario de consulta y anulación de facturas emitidas.
    /// Reemplaza la incorrecta implementación de IFacturacionView.
    /// </summary>
    public interface IFacturasEmitidasView
    {
        void MostrarFacturas(DataTable facturas);
        void showMessage(string message, string titulo, bool esError);
        void ResetFields();
    }
}

