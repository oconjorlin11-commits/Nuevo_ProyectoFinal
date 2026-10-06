using System;

namespace Nuevo_Proyecto.Views.Interfaces
{
    /// <summary>
    /// Contrato para el formulario de registro de nuevos empleados (modal).
    /// Segregada de IEmpleadoView según ISP (Interface Segregation Principle).
    /// </summary>
    public interface INuevoEmpleadoView
    {
        string Codigo { get; set; }
        string Nombre { get; set; }
        string Cedula { get; set; }
        string Telefono { get; set; }
        string Cargo { get; set; }
        decimal Salario { get; set; }
        DateTime? FechaIngreso { get; set; }
        bool Activo { get; set; }
        string AutorizadoPor { get; set; }

        event EventHandler? GuardarClicked;
        event EventHandler? CancelarClicked;

        void showMessage(string message, string titulo, bool esError);
        void ResetFields();
        void CloseView();
    }
}

