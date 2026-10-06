using System.Data;

namespace Nuevo_Proyecto.Views.Interfaces
{
    /// <summary>
    /// Contrato para el formulario de reportes y exportación de movimientos de inventario.
    /// </summary>
    public interface IReportesView
    {
        void MostrarReportes(DataTable reportes);
        void showMessage(string message, string titulo, bool esError);
    }
}

