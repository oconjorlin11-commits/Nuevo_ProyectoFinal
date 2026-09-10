using System;

namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface ILoginView
    {
        string Usuario { get; set; }

        string Contrasena { get; set; }


        event EventHandler IngresarClicked;


        void showMessage(string message, string titulo, bool esError);

        void ResetFields();


    }
}
