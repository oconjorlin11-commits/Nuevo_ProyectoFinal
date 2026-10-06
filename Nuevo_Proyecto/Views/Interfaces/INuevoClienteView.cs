using System;

namespace Nuevo_Proyecto.Views.Interfaces
{
    /// <summary>
    /// Contrato para el formulario de creación de nuevos clientes (modal).
    /// Cumple con el Principio de Segregación de Interfaces (ISP).
    /// </summary>
    public interface INuevoClienteView
    {
        string Codigo { get; set; }
        string Nombre { get; set; }
        string Telefono { get; set; }
        string Direccion { get; set; }
        string Nota { get; set; }
        bool Activo { get; set; }
        string AutorizadoPor { get; set; }

        event EventHandler? GuardarClicked;
        event EventHandler? CancelarClicked;

        void showMessage(string message, string titulo, bool esError);
        void ResetFields();
        void CloseView();
    }
}

