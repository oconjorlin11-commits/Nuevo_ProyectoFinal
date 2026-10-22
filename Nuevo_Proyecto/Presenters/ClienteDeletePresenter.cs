using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class ClienteDeletePresenter
    {
        private readonly IClienteDeleteView _view;
        private readonly IClienteRepository _repository;

        public ClienteDeletePresenter(
            IClienteDeleteView view,
            IClienteRepository repository)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Solicita al repositorio desactivar lógicamente al cliente.
        /// </summary>
        public async Task DeleteCustomerAsync(ClienteDeleteDto customer)
        {
            try
            {
                if (customer is null)
                {
                    throw new ArgumentNullException(nameof(customer));
                }

                await _repository.DeleteCustomerAsync(customer.ClienteId, customer.Motivo);

                _view.ShowMessage("El cliente fue desactivado correctamente.");
                _view.CloseView();
            }
            catch (Exception)
            {
                _view.ShowMessage("No fue posible desactivar el cliente.");
            }
        }
    }
}

