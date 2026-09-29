using System;

namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IFacturacionView
    {
        void showMessage(string message, string titulo, bool esError);
        void ResetFields();
    }
}
