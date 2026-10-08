using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class EmpleadoPresenter
    {
        private readonly IEmpleadoView? _view;
        private readonly INuevoEmpleadoView? _nuevoEmpleadoView;
        private readonly IEmpleadoRepository _empleados;
        private readonly IUsuarioRepository _usuarios;

        // Estado original seleccionado para control de cambios y reactivación
        private string? _codigoOriginal;
        private string? _nombreOriginal;
        private string? _cargoOriginal;
        private string? _cedulaOriginal;
        private string? _telefonoOriginal;
        private decimal _salarioOriginal;
        private DateTime _fechaIngresoOriginal;
        private bool _estadoOriginal;

        // Constructor para la administración de empleados (Empleados)
        public EmpleadoPresenter(IEmpleadoView view, IEmpleadoRepository? empleados = null, IUsuarioRepository? usuarios = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _empleados = empleados ?? new EmpleadoRepository();
            _usuarios = usuarios ?? new UsuarioRepository();

            _view.EditarClicked += OnEditarClicked;
            _view.EliminarClicked += OnEliminarClicked;
            _view.BuscarChanged += OnBuscarChanged;
        }

        // Constructor para el formulario de nuevo empleado (NuevoEmpleado)
        public EmpleadoPresenter(INuevoEmpleadoView nuevoEmpleadoView, IEmpleadoRepository? empleados = null, IUsuarioRepository? usuarios = null)
        {
            _nuevoEmpleadoView = nuevoEmpleadoView ?? throw new ArgumentNullException(nameof(nuevoEmpleadoView));
            _empleados = empleados ?? new EmpleadoRepository();
            _usuarios = usuarios ?? new UsuarioRepository();

            _nuevoEmpleadoView.GuardarClicked += OnGuardarNuevoEmpleadoClicked;
            _nuevoEmpleadoView.CancelarClicked += OnCancelarNuevoEmpleadoClicked;
        }

        // Constructor genérico para consultas/tests
        public EmpleadoPresenter(IEmpleadoRepository? empleados = null)
        {
            _empleados = empleados ?? new EmpleadoRepository();
        }

        // ---------------------------------------------------------------- control de la vista

        public void InicializarVista()
        {
            CargarEmpleadosActivos();
            LimpiarCampos();
        }

        public void CargarEmpleadosActivos()
        {
            if (_view == null) return;
            var empleados = _empleados.GetActivos();
            _view.MostrarEmpleados(DataTableMapper.Empleados(empleados));
            _view.CargarCargos(GetCargos());
        }

        private void RefrescarYLimpiar()
        {
            if (_view != null)
            {
                _view.BuscarTexto = string.Empty;  // Limpiar el textbox de búsqueda
            }
            CargarEmpleadosActivos();  // Recargar los empleados activos
            LimpiarCampos();  // Limpiar los campos de edición
        }

        public void SeleccionarEmpleado(string codigo, string nombre, string cargo, string cedula, string? telefono,
                                        decimal salario, DateTime fechaIngreso, bool activo)
        {
            _codigoOriginal = codigo;
            _nombreOriginal = nombre;
            _cargoOriginal = cargo;
            _cedulaOriginal = cedula;
            _telefonoOriginal = telefono;
            _salarioOriginal = salario;
            _fechaIngresoOriginal = fechaIngreso;
            _estadoOriginal = activo;

            if (_view != null)
            {
                _view.Codigo = codigo;
                _view.Nombre = nombre;
                _view.Cargo = cargo;
                _view.Cedula = cedula;
                _view.Telefono = telefono ?? string.Empty;
                _view.Salario = salario;
                _view.FechaIngreso = fechaIngreso;
                _view.Activo = activo;
                _view.SetActivoEnabled(!activo);
                _view.CargarCargos(GetCargos());
            }
        }

        public void LimpiarCampos()
        {
            _codigoOriginal = null;
            _nombreOriginal = null;
            _cargoOriginal = null;
            _cedulaOriginal = null;
            _telefonoOriginal = null;
            _salarioOriginal = 0;
            _fechaIngresoOriginal = DateTime.Now;
            _estadoOriginal = false;

            if (_view != null)
            {
                _view.ResetFields();
                _view.SetActivoEnabled(false);
            }
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

        // ---------------------------------------------------------------- eventos de NuevoEmpleado

        private void OnCancelarNuevoEmpleadoClicked(object? sender, EventArgs e)
        {
            _nuevoEmpleadoView?.ResetFields();
        }

        private void OnGuardarNuevoEmpleadoClicked(object? sender, EventArgs e)
        {
            if (_nuevoEmpleadoView == null) return;

            string codigo = _nuevoEmpleadoView.Codigo?.Trim() ?? string.Empty;
            string nombre = _nuevoEmpleadoView.Nombre?.Trim() ?? string.Empty;
            string cedula = _nuevoEmpleadoView.Cedula?.Trim() ?? string.Empty;
            string? telefono = string.IsNullOrWhiteSpace(_nuevoEmpleadoView.Telefono) ? null : _nuevoEmpleadoView.Telefono.Trim();
            string? cargo = string.IsNullOrWhiteSpace(_nuevoEmpleadoView.Cargo) ? null : _nuevoEmpleadoView.Cargo.Trim();

            if (codigo.Length == 0 || nombre.Length == 0 || cedula.Length == 0)
            {
                _nuevoEmpleadoView.showMessage("El código, el nombre y la cédula son obligatorios.", "Validación", true);
                return;
            }

            if (_nuevoEmpleadoView.Salario <= 1)
            {
                _nuevoEmpleadoView.showMessage("El salario debe ser mayor a 1.", "Validación", true);
                return;
            }

            if (codigo.Length > 10 || nombre.Length > 100 || cedula.Length > 20 ||
                (telefono?.Length ?? 0) > 20 || (cargo?.Length ?? 0) > 50)
            {
                _nuevoEmpleadoView.showMessage("Algún campo excede el largo permitido (código 10, nombre 100, cédula 20, teléfono 20, cargo 50).",
                                              "Validación", true);
                return;
            }

            // Validar formatos específicos
            if (!ValidarFormatoNombre(nombre))
            {
                _nuevoEmpleadoView.showMessage("El nombre debe contener al menos dos palabras (ej: Jose Alejandro Ordoñez Medina).", "Validación", true);
                return;
            }

            if (!ValidarFormatoCedula(cedula))
            {
                _nuevoEmpleadoView.showMessage("El formato de cédula es incorrecto. Use el formato: xxx-xxxxxx-xxxxA (ej: 561-021007-1000A).", "Validación", true);
                return;
            }

            if (telefono != null && !ValidarFormatoTelefono(telefono))
            {
                _nuevoEmpleadoView.showMessage("El formato de teléfono es incorrecto. Use el formato: xxxx-xxxx (ej: 7635-7836).", "Validación", true);
                return;
            }

            try
            {
                if (_empleados.CodigoExiste(codigo))
                {
                    _nuevoEmpleadoView.showMessage($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Error", true);
                    return;
                }

                if (_empleados.CedulaExiste(cedula))
                {
                    _nuevoEmpleadoView.showMessage($"La cédula '{cedula}' ya está registrada en el sistema. No se puede ingresar cédulas duplicadas.", "Error", true);
                    return;
                }

                if (telefono != null && _empleados.TelefonoExiste(telefono))
                {
                    _nuevoEmpleadoView.showMessage($"El teléfono '{telefono}' ya está en uso por otro empleado. No se puede ingresar teléfonos duplicados.", "Error", true);
                    return;
                }

                var empleadoDto = new EmpleadoDto
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    Cedula = cedula,
                    Telefono = telefono,
                    Cargo = cargo,
                    Salario = _nuevoEmpleadoView.Salario,
                    FechaIngreso = _nuevoEmpleadoView.FechaIngreso,
                    Activo = _nuevoEmpleadoView.Activo
                };

                // 👉 Crear empleado y usuario automáticamente usando el procedimiento almacenado
                bool usuarioCreado = _usuarios.CrearUsuarioEmpleado(empleadoDto);

                if (usuarioCreado)
                {
                    _nuevoEmpleadoView.showMessage("Empleado y usuario guardados exitosamente.", "Éxito", false);
                }
                else
                {
                    _nuevoEmpleadoView.showMessage("Empleado guardado, pero hubo un error al crear el usuario del sistema.", "Aviso", false);
                }

                _nuevoEmpleadoView.ResetFields();
                try { _nuevoEmpleadoView.CloseView(); } catch { /* la vista puede no ser modal */ }
            }
            catch (DbUpdateException ex)
            {
                _nuevoEmpleadoView.showMessage($"Error al guardar el empleado en la base de datos: {ex.InnerException?.Message ?? ex.Message}", "Error BD", true);
            }
            catch (Exception ex)
            {
                _nuevoEmpleadoView.showMessage($"Ocurrió un error inesperado: {ex.Message}", "Error", true);
            }
        }

        // ---------------------------------------------------------------- eventos de Empleados

        private void OnEditarClicked(object? sender, EventArgs e)
        {
            if (_view == null) return;

            if (string.IsNullOrEmpty(_codigoOriginal))
            {
                _view.showMessage("Debe seleccionar un empleado primero.", "Aviso", true);
                return;
            }

            // Si estaba inactivo y se marca para reactivar
            if (!_estadoOriginal && _view.Activo)
            {
                bool ok = _empleados.Reactivar(_codigoOriginal);
                if (ok)
                {
                    _view.showMessage("Empleado reactivado correctamente.", "Éxito", false);
                    RefrescarYLimpiar();
                }
                else
                {
                    _view.showMessage("No se pudo reactivar el empleado.", "Error", true);
                }
                return;
            }

            // Validar si hubo cambios
            bool huboCambios =
                _view.Nombre?.Trim() != _nombreOriginal ||
                _view.Cargo?.Trim() != _cargoOriginal ||
                _view.Cedula?.Trim() != _cedulaOriginal ||
                (_view.Telefono?.Trim() ?? string.Empty) != (_telefonoOriginal ?? string.Empty) ||
                _view.Salario != _salarioOriginal;

            if (!huboCambios)
            {
                _view.showMessage("No se ha hecho ningún cambio.", "Aviso", false);
                return;
            }

            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string cargo = _view.Cargo?.Trim() ?? string.Empty;
            string cedula = _view.Cedula?.Trim() ?? string.Empty;
            string? telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(cedula))
            {
                _view.showMessage("El nombre y la cédula son obligatorios.", "Validación", true);
                return;
            }

            if (_view.Salario <= 1)
            {
                _view.showMessage("El salario debe ser mayor a 1.", "Validación", true);
                return;
            }

            if (nombre.Length > 100 || cedula.Length > 20 || (telefono?.Length ?? 0) > 20 || (cargo?.Length ?? 0) > 50)
            {
                _view.showMessage("Algún campo excede el largo permitido.", "Validación", true);
                return;
            }

            // Validar formatos específicos
            if (!ValidarFormatoNombre(nombre))
            {
                _view.showMessage("El nombre debe contener al menos dos palabras (ej: Jose Alejandro Ordoñez Medina).", "Validación", true);
                return;
            }

            if (!ValidarFormatoCedula(cedula))
            {
                _view.showMessage("El formato de cédula es incorrecto. Use el formato: xxx-xxxxxx-xxxxA (ej: 561-021007-1000A).", "Validación", true);
                return;
            }

            if (telefono != null && !ValidarFormatoTelefono(telefono))
            {
                _view.showMessage("El formato de teléfono es incorrecto. Use el formato: xxxx-xxxx (ej: 7635-7836).", "Validación", true);
                return;
            }

            // Validar que cédula y teléfono no estén duplicados (excluyendo el empleado actual)
            if (_empleados.CedulaExiste(cedula, _codigoOriginal))
            {
                _view.showMessage($"La cédula '{cedula}' ya está registrada en otro empleado. No se puede ingresar cédulas duplicadas.", "Error", true);
                return;
            }

            if (telefono != null && _empleados.TelefonoExiste(telefono, _codigoOriginal))
            {
                _view.showMessage($"El teléfono '{telefono}' ya está en uso por otro empleado. No se puede ingresar teléfonos duplicados.", "Error", true);
                return;
            }

            bool okUpdate = _empleados.Actualizar(_codigoOriginal, nombre, cargo, cedula, telefono, _view.Salario);
            if (okUpdate)
            {
                _view.showMessage("Empleado actualizado correctamente.", "Éxito", false);
                RefrescarYLimpiar();
            }
            else
            {
                _view.showMessage("No se pudo actualizar el empleado.", "Error", true);
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (_view == null) return;

            if (string.IsNullOrEmpty(_codigoOriginal))
            {
                _view.showMessage("Debe seleccionar un empleado primero.", "Aviso", true);
                return;
            }

            bool ok = _empleados.Desactivar(_codigoOriginal);
            if (ok)
            {
                _view.showMessage("Empleado despedido (inactivado) correctamente.", "Éxito", false);
                RefrescarYLimpiar();
            }
            else
            {
                _view.showMessage("No se pudo despedir al empleado.", "Error", true);
            }
        }

        private void OnBuscarChanged(object? sender, EventArgs e)
        {
            if (_view == null) return;

            string codigo = _view.BuscarTexto?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(codigo))
            {
                // Si el textbox está vacío, mostrar solo los activos y limpiar campos
                CargarEmpleadosActivos();
                LimpiarCampos();
                return;
            }

            // Buscar código EXACTO en activos e inactivos
            var resultados = _empleados.BuscarPorCodigo(codigo);
            var dt = DataTableMapper.Empleados(resultados);

            if (dt.Rows.Count > 0)
            {
                // Mostrar resultado exacto encontrado
                _view.MostrarEmpleados(dt);

                // Cargar el resultado en los campos de edición
                var row = dt.Rows[0];
                SeleccionarEmpleado(
                    row["Codigo"]?.ToString() ?? string.Empty,
                    row["Nombre"]?.ToString() ?? string.Empty,
                    row["Cargo"]?.ToString() ?? string.Empty,
                    row["Cedula"]?.ToString() ?? string.Empty,
                    row["Telefono"]?.ToString(),
                    row["Salario"] != null && row["Salario"] != DBNull.Value ? Convert.ToDecimal(row["Salario"]) : 0,
                    row["FechaIngreso"] != null && row["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(row["FechaIngreso"]) : DateTime.Now,
                    row["Activo"] != null && row["Activo"] != DBNull.Value && Convert.ToBoolean(row["Activo"])
                );
            }
            else
            {
                // Si no hay resultado exacto, mostrar solo los activos
                CargarEmpleadosActivos();
                LimpiarCampos();
            }
        }

        // ---------------------------------------------------------------- validaciones de formato

        private bool ValidarFormatoNombre(string nombre)
        {
            // Validar que tenga mínimo 2 palabras (separadas por espacios)
            // Ejemplo: "Jose Alejandro Ordoñez Medina"
            if (string.IsNullOrWhiteSpace(nombre)) return false;

            var palabras = nombre.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return palabras.Length >= 2;
        }

        private bool ValidarFormatoCedula(string cedula)
        {
            // Validar formato: xxx-xxxxxx-xxxxA
            // Ejemplo: "561-021007-1000A"
            // Patrón: 3 dígitos - 6 dígitos - 4 dígitos/letras
            if (string.IsNullOrWhiteSpace(cedula)) return false;

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{3}-\d{6}-\d{4}[A-Z]?$", 
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return regex.IsMatch(cedula.Trim());
        }

        private bool ValidarFormatoTelefono(string telefono)
        {
            // Validar formato: xxxx-xxxx
            // Ejemplo: "7635-7836" (4 dígitos - 4 dígitos)
            if (string.IsNullOrWhiteSpace(telefono)) return true; // Teléfono es opcional

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{4}-\d{4}$");
            return regex.IsMatch(telefono.Trim());
        }

        private bool ValidarFormatoSalario(decimal salario)
        {
            // Validar que sea mayor a 1
            // El formato con comas se maneja automáticamente en la conversión
            return salario > 1;
        }
    }
}

