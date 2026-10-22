using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IClienteEditView
    {
        string Codigo { get; }
        string Nombre { get; }
        string? Telefono { get; }
        string? Direccion { get; }
        string? Nota { get; }
        bool Activo { get; }

        void ShowCustomer(ClientesDto? customer);
        void ShowMessage(string message);
    }
}

