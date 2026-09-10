using System;
using System.Linq;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Views.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace Nuevo_Proyecto.Presenters
{
    public class ClientePresenter
    {
        private readonly IClienteView _view;

        public ClientePresenter(IClienteView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _view.GuardarClicked += OnGuardarClicked;
            _view.CancelarClicked += OnCancelarClicked;
        }

        private void OnCancelarClicked(object? sender, EventArgs e)
        {
            _view.ResetFields();

        }

        private void OnGuardarClicked(object? sender, EventArgs e)
        {
            string codigo = _view.Codigo?.Trim() ?? string.Empty;
            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string? telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono.Trim();
            string? direccion = string.IsNullOrWhiteSpace(_view.Direccion) ? null : _view.Direccion.Trim();
            string? nota = string.IsNullOrWhiteSpace(_view.Nota) ? null : _view.Nota.Trim();
            bool activo = _view.Activo;

            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(direccion))
            {
                _view.showMessage("El código, el nombre y la dirección son obligatorios.", "Error", true);
                return;
            }

            try
            {
                using (var db = new Dev_ComideriaDbContext())
                {
                    bool codigoExiste = db.Clientes.AsNoTracking().Any(x => x.Codigo == codigo);
                    if (codigoExiste)
                    {
                        _view.showMessage($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Error", true);
                        return;
                    }

                    var nuevoCliente = new Cliente
                    {
                        Codigo = codigo,
                        Nombre = nombre,
                        Telefono = telefono,
                        Direccion = direccion,
                        Nota = nota,
                        Activo = activo
                    };

                    db.Clientes.Add(nuevoCliente);
                    db.SaveChanges();

                    _view.showMessage("Cliente guardado exitosamente.", "Éxito", false);
                    _view.ResetFields();

                }

            }

            catch (DbUpdateException ex)
            {
                _view.showMessage($"Error de base de datos: {ex.InnerException?.Message}", "Error BD", true);
            }
            catch (Exception ex)
            {
                _view.showMessage($"Ocurrió un error inesperado: {ex.Message}", "Error", true);


            }







        }

    }
}

