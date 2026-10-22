using System.Diagnostics;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    /// <summary>
    /// Presenter encargado exclusivamente de registrar clientes.
    /// </summary>
    public class ClienteCreatePresenter
    {
        private readonly IClienteCreateView _view;
        private readonly IClienteRepository _repository;

        public ClienteCreatePresenter(IClienteCreateView view, IClienteRepository repository)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Captura los datos de la Vista y solicita al repositorio registrar el cliente.
        /// </summary>
        public async Task CreateCustomerAsync()
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

                await _repository.CreateCustomerAsync(customer);

                _view.ShowMessage("El cliente fue registrado correctamente.");
                _view.ClearCustomerForm();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Creación de cliente falló: {ex}");
                _view.ShowMessage("No fue posible registrar el cliente.");
            }
        }
    }
}

