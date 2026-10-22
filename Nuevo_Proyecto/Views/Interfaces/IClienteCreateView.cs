namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IClienteCreateView
    {
        string Codigo { get; }
        string Nombre { get; }
        string? Telefono { get; }
        string? Direccion { get; }
        string? Nota { get; }
        bool Activo { get; }

        void ShowMessage(string message);
        void ClearCustomerForm();
    }
}

