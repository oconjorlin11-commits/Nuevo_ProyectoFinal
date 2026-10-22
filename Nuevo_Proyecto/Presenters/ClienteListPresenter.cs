using System.Diagnostics;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    /// <summary>
    /// Presenter responsable exclusivamente de la pantalla de listado de clientes.
    /// </summary>
    public class ClienteListPresenter
    {
        private readonly IClienteListView _view;
        private readonly IClienteRepository _repository;

        public ClienteListPresenter(IClienteListView view, IClienteRepository repository)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Obtiene los clientes desde el repositorio y solicita a la Vista que los muestre.
        /// </summary>
        public async Task LoadCustomersAsync()
        {
            try
            {
                IReadOnlyList<ClientesDto> customers = await _repository.GetAllCustomersAsync();

                if (customers.Count == 0)
                {
                    _view.ShowNoRecords();
                    return;
                }

                _view.ShowCustomers(customers);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                _view.ShowErrorMessage("No fue posible cargar la lista de clientes.");
            }
        }
    }
}

