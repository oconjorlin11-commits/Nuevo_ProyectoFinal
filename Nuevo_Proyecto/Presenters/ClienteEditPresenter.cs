using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class ClienteEditPresenter
    {
        private readonly IClienteEditView _view;
        private readonly IClienteRepository _repository;
        private readonly int _clienteId;

        public ClienteEditPresenter(
            IClienteEditView view,
            IClienteRepository repository,
            int clienteId)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            if (clienteId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(clienteId), "El Id del cliente no es válido.");
            }

            _clienteId = clienteId;
        }

        /// <summary>
        /// Consulta el cliente y envía sus datos a la vista.
        /// </summary>
        public async Task LoadCustomerAsync()
        {
            try
            {
                ClientesDto? customer = await _repository.GetCustomerByIdAsync(_clienteId);
                _view.ShowCustomer(customer);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"No fue posible cargar el cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los valores de la vista y solicita al repositorio actualizar el cliente.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            try
            {
                var customer = new ClienteCreateDto
                {
                    Codigo = _view.Codigo,
                    Nombre = _view.Nombre,
                    Telefono = _view.Telefono,
                    Direccion = _view.Direccion,
                    Nota = _view.Nota,
                    Activo = _view.Activo
                };

                await _repository.UpdateCustomerAsync(_clienteId, customer);
                _view.ShowMessage("Cliente actualizado correctamente.");
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"No fue posible guardar los cambios: {ex.Message}");
            }
        }
    }
}

