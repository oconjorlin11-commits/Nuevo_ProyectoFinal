using System.Data;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class EmpleadoPresenter
    {
        private readonly IEmpleadoView _view;
        private readonly IEmpleadoRepository _empleados;

        public EmpleadoPresenter(IEmpleadoView view, IEmpleadoRepository? empleados = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _empleados = empleados ?? new EmpleadoRepository();

            _view.GuardarClicked += OnGuardarClicked;
            _view.CancelarClicked += OnCancelarClicked;
        }

        // ---------------------------------------------------------------- consultas para la vista

        public List<EmpleadoDto> GetEmpleadosActivos() => _empleados.GetActivos().ToList();

        public DataTable GetCargos() => DataTableMapper.Cargos(_empleados.GetCargos());

        public string GetNextCodigoEmpleado() => _empleados.GetSiguienteCodigo();

        public DataTable GetUsuariosAdministradores() =>
            DataTableMapper.EmpleadosCodigoNombre(_empleados.GetPorCargos("Administrador", "Admin"));

        public DataTable BuscarEmpleadoPorCodigo(string codigo) =>
            DataTableMapper.Empleados(_empleados.BuscarPorCodigo(codigo));

        // ---------------------------------------------------------------- comandos

        public bool ActualizarEmpleado(string codigo, string nombre, string cargo, string cedula, string telefono, decimal salario) =>
            _empleados.Actualizar(codigo, nombre, cargo, cedula, telefono, salario);

        public bool EliminarEmpleado(string codigo) => _empleados.Desactivar(codigo);

        public bool ReactivarEmpleado(string codigo) => _empleados.Reactivar(codigo);

        // ---------------------------------------------------------------- eventos de la vista

        private void OnCancelarClicked(object? sender, EventArgs e) => _view.ResetFields();

        private void OnGuardarClicked(object? sender, EventArgs e)
        {
            string codigo = _view.Codigo?.Trim() ?? string.Empty;
            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string cedula = _view.Cedula?.Trim() ?? string.Empty;
            string? telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono.Trim();
            string? cargo = string.IsNullOrWhiteSpace(_view.Cargo) ? null : _view.Cargo.Trim();

            if (codigo.Length == 0 || nombre.Length == 0 || cedula.Length == 0)
            {
                _view.showMessage("El código, el nombre y la cédula son obligatorios.", "Validacion", true);
                return;
            }

            if (_view.Salario < 0)
            {
                _view.showMessage("El salario no puede ser negativo.", "Validacion", true);
                return;
            }

            if (codigo.Length > 10 || nombre.Length > 100 || cedula.Length > 20 ||
                (telefono?.Length ?? 0) > 20 || (cargo?.Length ?? 0) > 50)
            {
                _view.showMessage("Algún campo excede el largo permitido (código 10, nombre 100, cédula 20, teléfono 20, cargo 50).",
                                  "Validacion", true);
                return;
            }

            try
            {
                if (_empleados.CodigoExiste(codigo))
                {
                    _view.showMessage($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Error", true);
                    return;
                }

                _empleados.Crear(new EmpleadoDto
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    Cedula = cedula,
                    Telefono = telefono,
                    Cargo = cargo,
                    Salario = _view.Salario,
                    FechaIngreso = _view.FechaIngreso,
                    Activo = _view.Activo
                });

                _view.showMessage("Empleado guardado exitosamente.", "Éxito", false);
                _view.ResetFields();
                try { _view.CloseView(); } catch { /* la vista puede no ser modal */ }
            }
            catch (DbUpdateException ex)
            {
                _view.showMessage($"Error al guardar el empleado en la base de datos: {ex.InnerException?.Message ?? ex.Message}", "Error BD", true);
            }
            catch (Exception ex)
            {
                _view.showMessage($"Ocurrió un error inesperado: {ex.Message}", "Error", true);
            }
        }
    }
}
