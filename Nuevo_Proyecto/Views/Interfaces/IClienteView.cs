using System;
using System.Data;

namespace Nuevo_Proyecto.Views.Interfaces
{
    /// <summary>
    /// Contrato para el formulario de administración y listado de clientes.
    /// Segregada de INuevoClienteView para cumplir con ISP y SOLID.
    /// </summary>
    public interface IClienteView
    {
        // Propiedades enlazadas a los controles de la vista
        string Codigo { get; set; }
        string Nombre { get; set; }
        string Telefono { get; set; }
        string Direccion { get; set; }
        string Nota { get; set; }
        bool Activo { get; set; }
        string BuscarTexto { get; set; }

        // Eventos notificados al presentador
        event EventHandler? EditarClicked;
        event EventHandler? EliminarClicked;
        event EventHandler? BuscarChanged;

        // Métodos de control visual ordenados por el presentador
        void showMessage(string message, string titulo, bool esError);
        void ResetFields();
        void MostrarClientes(DataTable dt);
        void CargarNotas(DataTable notas);
        void SetActivoEnabled(bool enabled);
    }
}

