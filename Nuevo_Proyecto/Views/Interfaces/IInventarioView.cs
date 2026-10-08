using System;

namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IInventarioView
    {
        string BuscarTexto { get; set; }

        event EventHandler? BuscarChanged;

        void showMessage(string message, string titulo, bool esError);
        void ResetFields();
        void LimpiarCamposEdicion();
        void MostrarInventario(System.Data.DataTable dt);
        void CargarDatosEdicion(System.Data.DataRow fila);
    }
}
