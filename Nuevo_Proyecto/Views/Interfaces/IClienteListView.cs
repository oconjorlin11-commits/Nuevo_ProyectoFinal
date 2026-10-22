using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IClienteListView
    {
        /// <summary>
        /// Notifica que el usuario desea registrar un nuevo cliente.
        /// </summary>
        event EventHandler NewCustomerRequested;

        /// <summary>
        /// Muestra la lista de clientes.
        /// </summary>
        void ShowCustomers(IReadOnlyList<ClientesDto> customers);

        /// <summary>
        /// Muestra un mensaje cuando no existen clientes.
        /// </summary>
        void ShowNoRecords();

        /// <summary>
        /// Muestra un mensaje de error.
        /// </summary>
        void ShowErrorMessage(string mensaje);
    }
}

