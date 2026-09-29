using System;

namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IInventarioView
    {
        void showMessage(string message, string titulo, bool esError);
        void ResetFields();
    }
}
