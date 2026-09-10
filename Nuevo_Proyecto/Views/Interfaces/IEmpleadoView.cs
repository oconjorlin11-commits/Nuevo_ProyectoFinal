using System;


namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IEmpleadoView
    {

        string Codigo { get; set; }

        string Nombre { get; set; }

        string Cedula { get; set; }

        string Telefono { get; set; }

        string Cargo { get; set; }

        decimal Salario { get; set; }

        DateTime FechaIngreso { get; set; }

        bool Activo { get; set; }

        string AutorizadoPor { get; set; }


        // Eventos que la vista notifica al presentador

        event EventHandler GuardarClicked;

        event EventHandler CancelarClicked;


        // Métodos de control visual ordenados por el presentador

        void showMessage(string message, string titulo, bool esError);

        void ResetFields();




    }
}
