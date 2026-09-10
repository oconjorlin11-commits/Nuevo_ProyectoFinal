using System;
using System.Linq;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Views.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Nuevo_Proyecto.Presenters
{
    public class EmpleadoPresenter
    {
        private readonly IEmpleadoView _view;

        public EmpleadoPresenter(IEmpleadoView view)
        {
            // suscripcion a eventos de la vista
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
            // 1. Extraer y limpiar los datos de la vista
            string codigo = _view.Codigo?.Trim() ?? string.Empty;
            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string cedula = _view.Cedula?.Trim() ?? string.Empty;
            string telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono.Trim();
            string cargo = string.IsNullOrWhiteSpace(_view.Cargo) ? null : _view.Cargo.Trim();
            decimal salario = _view.Salario;
            DateTime fechaIngreso = _view.FechaIngreso;
            bool activo = _view.Activo;

            // 2. validaciones de negocio / presentacion
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(cedula))
            {
                _view.showMessage("El código, el nombre y la cédula son obligatorios.", "Validacion", true);
                return;
            }
            if (salario < 0)
            {
                _view.showMessage("El salario no puede ser negativo.", "Validacion", true);
                return;
            }


            // 3. persistencia con EF core

            try
            {
                using (var db = new Dev_ComideriaDbContext())
                {
                    bool codigoExiste = db.Empleado.AsNoTracking().Any(x => x.Codigo == codigo);
                    if (codigoExiste)
                    {
                        _view.showMessage($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Error", true);
                        return;
                    }
                    var nuevoEmpleado = new Empleado
                    {
                        Codigo = codigo,
                        Nombre = nombre,
                        Cedula = cedula,
                        Telefono = telefono,
                        Cargo = cargo,
                        Salario = salario,
                        Fechaingreso = fechaIngreso,
                        Activo = activo
                    };
                    db.Empleado.Add(nuevoEmpleado);
                    db.SaveChanges();

                    _view.showMessage("Empleado guardado exitosamente.", "Éxito", false);
                    _view.ResetFields();



                }




             
            }
            catch (DbUpdateException ex)
            {
                // Manejo de errores de base de datos
                _view.showMessage($"Error al guardar el empleado en la base de datos: {ex.InnerException?.Message ?? ex.Message}", "Error BD", true);
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                _view.showMessage($"Ocurrió un error inesperado: {ex.Message}", "Error", true);
            }


        }

    }
}
