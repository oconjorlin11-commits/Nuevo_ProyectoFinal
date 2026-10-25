using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
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

        // Propiedad para capturar el ID del ultimo empleado creado (para auditoría)
        public int? UltimoEmpleadoIDCreado { get; private set; }

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
            _usuarios = new UsuarioRepository();
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
                // Primero cargar los cargos disponibles
                _view.CargarCargos(GetCargos());

                // Luego asignar los valores del empleado
                _view.Codigo = codigo;
                _view.Nombre = nombre;
                _view.Cargo = cargo;
                _view.Cedula = cedula;
                _view.Telefono = telefono ?? string.Empty;
                _view.Salario = salario;
                _view.FechaIngreso = fechaIngreso;
                _view.Activo = activo;
                _view.SetActivoEnabled(!activo);
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
                MensajeHelper.Empleado.CamposObligatorios();
                return;
            }

            if (_nuevoEmpleadoView.Salario <= 1)
            {
                MensajeHelper.Empleado.SalarioInvalido();
                return;
            }

            if (codigo.Length > 10 || nombre.Length > 100 || cedula.Length > 20 ||
                (telefono?.Length ?? 0) > 20 || (cargo?.Length ?? 0) > 50)
            {
                MensajeHelper.Empleado.LongitudCamposExcedida();
                return;
            }

            // Validar formatos específicos
            if (!ValidarFormatoNombre(nombre))
            {
                MensajeHelper.Empleado.NombreInvalido();
                return;
            }

            if (!ValidarFormatoCedula(cedula))
            {
                MensajeHelper.Empleado.CedulaInvalida();
                return;
            }

            if (telefono != null && !ValidarFormatoTelefono(telefono))
            {
                MensajeHelper.Empleado.TelefonoInvalido();
                return;
            }

            try
            {
                if (_empleados.CodigoExiste(codigo))
                {
                    MensajeHelper.Empleado.CodigoDuplicado(codigo);
                    return;
                }

                if (_empleados.CedulaExiste(cedula))
                {
                    MensajeHelper.Empleado.CedulaDuplicada(cedula);
                    return;
                }

                if (telefono != null && _empleados.TelefonoExiste(telefono))
                {
                    MensajeHelper.Empleado.TelefonoDuplicado(telefono);
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
                    MensajeHelper.Empleado.GuardadoExitoso();

                    // Capturar el ID del empleado recién creado (buscarlo por código)
                    var empleadoCreado = _empleados.BuscarPorCodigo(codigo).FirstOrDefault();
                    if (empleadoCreado != null)
                    {
                        UltimoEmpleadoIDCreado = empleadoCreado.EmpleadoId;
                    }
                }
                else
                {
                    MensajeHelper.Empleado.UsuarioNoCreado();
                }

                _nuevoEmpleadoView.ResetFields();
                try { _nuevoEmpleadoView.CloseView(); } catch { /* la vista puede no ser modal */ }
            }
            catch (DbUpdateException ex)
            {
                MensajeHelper.BaseDatos.ErrorAlGuardar(ex.InnerException?.Message ?? ex.Message);
            }
            catch (Exception ex)
            {
                MensajeHelper.BaseDatos.ErrorInesperado(ex.Message);
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
                    MensajeHelper.Empleado.ReactivadoExitoso();
                    RefrescarYLimpiar();
                }
                else
                {
                    MensajeHelper.Empleado.NoSeReactivo();
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
                MensajeHelper.Empleado.SinCambios();
                return;
            }

            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string cargo = _view.Cargo?.Trim() ?? string.Empty;
            string cedula = _view.Cedula?.Trim() ?? string.Empty;
            string? telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(cedula))
            {
                MensajeHelper.Validacion.CampoObligatorio("nombre y cédula");
                return;
            }

            if (_view.Salario <= 1)
            {
                MensajeHelper.Empleado.SalarioInvalido();
                return;
            }

            if (nombre.Length > 100 || cedula.Length > 20 || (telefono?.Length ?? 0) > 20 || (cargo?.Length ?? 0) > 50)
            {
                MensajeHelper.Empleado.LongitudCamposExcedida();
                return;
            }

            // Validar formatos específicos
            if (!ValidarFormatoNombre(nombre))
            {
                MensajeHelper.Empleado.NombreInvalido();
                return;
            }

            if (!ValidarFormatoCedula(cedula))
            {
                MensajeHelper.Empleado.CedulaInvalida();
                return;
            }

            if (telefono != null && !ValidarFormatoTelefono(telefono))
            {
                MensajeHelper.Empleado.TelefonoInvalido();
                return;
            }

            // Validar que cédula y teléfono no estén duplicados (excluyendo el empleado actual)
            if (_empleados.CedulaExiste(cedula, _codigoOriginal))
            {
                MensajeHelper.Empleado.CedulaDuplicada(cedula);
                return;
            }

            if (telefono != null && _empleados.TelefonoExiste(telefono, _codigoOriginal))
            {
                MensajeHelper.Empleado.TelefonoDuplicado(telefono);
                return;
            }

            bool okUpdate = _empleados.Actualizar(_codigoOriginal, nombre, cargo, cedula, telefono, _view.Salario);
            if (okUpdate)
            {
                MensajeHelper.Empleado.ActualizadoExitoso();
                RefrescarYLimpiar();
            }
            else
            {
                MensajeHelper.Empleado.NoSeActualizo();
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (_view == null) return;

            if (string.IsNullOrEmpty(_codigoOriginal))
            {
                MensajeHelper.Empleado.SeleccionarPrimero();
                return;
            }

            bool ok = _empleados.Desactivar(_codigoOriginal);
            if (ok)
            {
                MensajeHelper.Empleado.EliminandoExitoso();
                RefrescarYLimpiar();
            }
            else
            {
                MensajeHelper.Empleado.NoSeElimino();
            }
        }

        private void OnBuscarChanged(object? sender, EventArgs e)
        {
            if (_view == null) return;

            string termino = _view.BuscarTexto?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(termino))
            {
                // Si el textbox está vacío, mostrar solo los activos
                _view.HayMultiplesResultados = false;
                CargarEmpleadosActivos();
                LimpiarCampos();
                return;
            }

            // Determinar si es búsqueda por código o nombre
            // Códigos tienen guion (ej: "EMP-001"), nombres no
            bool esCodigoFormat = termino.Contains('-');

            // Validar longitud mínima según tipo de búsqueda
            // Códigos: mínimo 1 carácter
            // Nombres: mínimo 1 carácter
            if (termino.Length < 1)
            {
                // No mostrar nada, DataGrid vacío (sin mezclar con activos)
                _view.HayMultiplesResultados = false;
                var dtVacio = DataTableMapper.Empleados(Enumerable.Empty<EmpleadoDto>());
                _view.MostrarEmpleados(dtVacio);
                LimpiarCampos();
                return;
            }

            // Buscar por código O nombre (parcial) - busca en TODO
            var resultados = _empleados.BuscarPorCodigoONombre(termino);
            var dt = DataTableMapper.Empleados(resultados);

            if (dt.Rows.Count == 0)
            {
                // No hay coincidencias - mostrar tabla vacía
                _view.HayMultiplesResultados = false;
                _view.MostrarEmpleados(dt);
                LimpiarCampos();
                return;
            }

            if (dt.Rows.Count == 1)
            {
                // Una única coincidencia - cargar automáticamente
                _view.HayMultiplesResultados = false;
                _view.MostrarEmpleados(dt);

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
                // Múltiples coincidencias - mostrar en DataGrid para elegir
                _view.HayMultiplesResultados = true;
                _view.MostrarEmpleados(dt);
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

