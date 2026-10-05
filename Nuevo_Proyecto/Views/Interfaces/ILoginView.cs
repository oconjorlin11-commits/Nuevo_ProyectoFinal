namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface ILoginView
    {
        string Usuario { get; set; }
        string Contrasena { get; set; }

        event EventHandler IngresarClicked;

        void showMessage(string message, string titulo, bool esError);
        void ResetFields();

        /// <summary>El presenter avisa que el acceso fue válido y la vista debe cerrarse con DialogResult.OK.</summary>
        void CompletarLogin();
    }
}
