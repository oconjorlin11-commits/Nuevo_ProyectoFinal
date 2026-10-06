using System;
using System.Data;

namespace Nuevo_Proyecto.Views.Interfaces
{
    /// <summary>
    /// Contrato para el formulario de administración y listado de empleados.
    /// Segregada de INuevoEmpleadoView según ISP (Interface Segregation Principle).
    /// </summary>
    public interface IEmpleadoView
    {
        string Codigo { get; set; }
        string Nombre { get; set; }
        string Cedula { get; set; }
        string Telefono { get; set; }
        string Cargo { get; set; }
        decimal Salario { get; set; }
        DateTime? FechaIngreso { get; set; }
        bool Activo { get; set; }
        string BuscarTexto { get; set; }

        event EventHandler? EditarClicked;
        event EventHandler? EliminarClicked;
        event EventHandler? BuscarChanged;

        void showMessage(string message, string titulo, bool esError);
        void ResetFields();
        void MostrarEmpleados(DataTable dt);
        void CargarCargos(DataTable cargos);
        void SetActivoEnabled(bool enabled);
    }
}

