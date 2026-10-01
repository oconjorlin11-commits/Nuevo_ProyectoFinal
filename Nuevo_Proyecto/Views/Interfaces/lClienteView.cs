using System;


namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IClienteView
    {

        // Propiedades de lectura/escritura de los controles de la vista
        string Codigo { get; set; }

        string Nombre { get; set; }

        string Telefono { get; set; }

        string Direccion { get; set; }

        string Nota { get; set; }

        bool Activo { get; set; }

        string AutorizadoPor { get; set; }


        //Eventos que la vista notifica al presentador

        event EventHandler GuardarClicked;

        event EventHandler CancelarClicked;


        // Métodos de control visual ordenados por el presentador

        void showMessage(string message, string titulo, bool esError);

        void ResetFields();

        // Pedir a la vista que se cierre (usado por formularios modales como NuevoCliente)
        void CloseView();



    }
}
