using System.Data;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class ClientePresenter
    {
        private readonly IClienteView _view;
        private readonly IClienteRepository _clientes;
        private readonly IEmpleadoRepository _empleados;

        // Estado original seleccionado para control de cambios y reactivación
        private string? _codigoOriginal;
        private string? _nombreOriginal;
        private string? _telefonoOriginal;
        private string? _direccionOriginal;
        private string? _notaOriginal;
        private bool _estadoOriginal;

        public ClientePresenter(IClienteView view,
                                IClienteRepository? clientes = null,
                                IEmpleadoRepository? empleados = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _clientes = clientes ?? new ClienteRepository();
            _empleados = empleados ?? new EmpleadoRepository();

            _view.GuardarClicked += OnGuardarClicked;
            _view.CancelarClicked += OnCancelarClicked;
            _view.EditarClicked += OnEditarClicked;
            _view.EliminarClicked += OnEliminarClicked;
            _view.BuscarChanged += OnBuscarChanged;
        }

        // ---------------------------------------------------------------- control de la vista

        public void InicializarVista()
        {
            CargarClientesActivos();
            LimpiarCampos();
        }

        public void CargarClientesActivos()
        {
            var clientes = _clientes.GetActivos();
            _view.MostrarClientes(DataTableMapper.Clientes(clientes));
            _view.CargarNotas(GetNotas());
        }

        public void SeleccionarCliente(string codigo, string nombre, string? telefono, string direccion, string? nota, bool activo)
        {
            _codigoOriginal = codigo;
            _nombreOriginal = nombre;
            _telefonoOriginal = telefono;
            _direccionOriginal = direccion;
            _notaOriginal = nota;
            _estadoOriginal = activo;

            _view.Codigo = codigo;
            _view.Nombre = nombre;
            _view.Telefono = telefono ?? string.Empty;
            _view.Direccion = direccion;
            _view.Nota = nota ?? string.Empty;
            _view.Activo = activo;
            _view.SetActivoEnabled(!activo);
            _view.CargarNotas(GetNotas());
        }

        public void LimpiarCampos()
        {
            _codigoOriginal = null;
            _nombreOriginal = null;
            _telefonoOriginal = null;
            _direccionOriginal = null;
            _notaOriginal = null;
            _estadoOriginal = false;

            _view.ResetFields();
            _view.SetActivoEnabled(false);
        }

        // ---------------------------------------------------------------- consultas para la vista

        public List<ClienteDto> GetClientesActivos() => _clientes.GetActivos().ToList();

        public string GetNextCodigoCliente() => _clientes.GetSiguienteCodigo();

        public DataTable GetNotas() => DataTableMapper.Notas(_clientes.GetNotas());

        public DataTable BuscarClientePorCodigo(string codigo) =>
            DataTableMapper.Clientes(_clientes.BuscarPorCodigo(codigo));

        public DataTable GetUsuariosCajerosAdmins() =>
            DataTableMapper.EmpleadosCodigoNombre(_empleados.GetPorCargos("Cajero", "Administrador"));

        // ---------------------------------------------------------------- comandos

        public bool ActualizarCliente(string codigo, string nombre, string? telefono, string direccion, string? nota) =>
            _clientes.Actualizar(codigo, nombre, telefono, direccion, nota);

        public bool EliminarCliente(string codigo) => _clientes.Desactivar(codigo);

        public bool ReactivarCliente(string codigo) => _clientes.Reactivar(codigo);

        // ---------------------------------------------------------------- lógica de negocio en métodos privados

        private void OnCancelarClicked(object? sender, EventArgs e) => LimpiarCampos();

        private void OnGuardarClicked(object? sender, EventArgs e)
        {
            string codigo = _view.Codigo?.Trim() ?? string.Empty;
            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string? telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono.Trim();
            string direccion = _view.Direccion?.Trim() ?? string.Empty;
            string? nota = string.IsNullOrWhiteSpace(_view.Nota) ? null : _view.Nota.Trim();

            if (codigo.Length == 0 || nombre.Length == 0 || direccion.Length == 0)
            {
                _view.showMessage("El código, el nombre y la dirección son obligatorios.", "Error", true);
                return;
            }

            // Largos máximos de las columnas (evita errores de truncamiento al guardar)
            if (codigo.Length > 10 || nombre.Length > 100 || direccion.Length > 200 ||
                (telefono?.Length ?? 0) > 20 || (nota?.Length ?? 0) > 200)
            {
                _view.showMessage("Algún campo excede el largo permitido (código 10, nombre 100, teléfono 20, dirección 200, nota 200).",
                                  "Validación", true);
                return;
            }

            try
            {
                if (_clientes.CodigoExiste(codigo))
                {
                    _view.showMessage($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Error", true);
                    return;
                }

                _clientes.Crear(new ClienteDto
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    Telefono = telefono,
                    Direccion = direccion,
                    Nota = nota,
                    Activo = _view.Activo
                });

                _view.showMessage("Cliente guardado exitosamente.", "Éxito", false);
                _view.ResetFields();
                try { _view.CloseView(); } catch { /* la vista puede no ser modal */ }
            }
            catch (DbUpdateException ex)
            {
                _view.showMessage($"Error de base de datos: {ex.InnerException?.Message ?? ex.Message}", "Error BD", true);
            }
            catch (Exception ex)
            {
                _view.showMessage($"Ocurrió un error inesperado: {ex.Message}", "Error", true);
            }
        }

        private void OnEditarClicked(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_codigoOriginal))
            {
                _view.showMessage("Debe seleccionar un cliente primero.", "Aviso", true);
                return;
            }

            // Si estaba inactivo y el usuario marcó el CheckBox para reactivarlo
            if (!_estadoOriginal && _view.Activo)
            {
                bool okReactivar = _clientes.Reactivar(_codigoOriginal);
                if (okReactivar)
                {
                    _view.showMessage("Cliente reactivado correctamente.", "Éxito", false);
                    InicializarVista();
                }
                else
                {
                    _view.showMessage("No se pudo reactivar el cliente.", "Error", true);
                }
                return;
            }

            // Si estaba activo, validar cambios
            bool huboCambios =
                _view.Nombre?.Trim() != _nombreOriginal ||
                (_view.Telefono?.Trim() ?? string.Empty) != (_telefonoOriginal ?? string.Empty) ||
                _view.Direccion?.Trim() != _direccionOriginal ||
                (_view.Nota?.Trim() ?? string.Empty) != (_notaOriginal ?? string.Empty);

            if (!huboCambios)
            {
                _view.showMessage("No se ha hecho ningún cambio.", "Aviso", false);
                return;
            }

            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string direccion = _view.Direccion?.Trim() ?? string.Empty;
            string? telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono.Trim();
            string? nota = string.IsNullOrWhiteSpace(_view.Nota) ? null : _view.Nota.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(direccion))
            {
                _view.showMessage("El nombre y la dirección son obligatorios.", "Validación", true);
                return;
            }

            if (nombre.Length > 100 || direccion.Length > 200 || (telefono?.Length ?? 0) > 20 || (nota?.Length ?? 0) > 200)
            {
                _view.showMessage("Algún campo excede el largo permitido.", "Validación", true);
                return;
            }

            bool okUpdate = _clientes.Actualizar(_codigoOriginal, nombre, telefono, direccion, nota);
            if (okUpdate)
            {
                _view.showMessage("Cliente actualizado correctamente.", "Éxito", false);
                InicializarVista();
            }
            else
            {
                _view.showMessage("No se pudo actualizar el cliente.", "Error", true);
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_codigoOriginal))
            {
                _view.showMessage("Debe seleccionar un cliente primero.", "Aviso", true);
                return;
            }

            bool ok = _clientes.Desactivar(_codigoOriginal);
            if (ok)
            {
                _view.showMessage("Cliente eliminado (inactivado) correctamente.", "Éxito", false);
                InicializarVista();
            }
            else
            {
                _view.showMessage("No se pudo eliminar el cliente.", "Error", true);
            }
        }

        private void OnBuscarChanged(object? sender, EventArgs e)
        {
            string codigo = _view.BuscarTexto?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(codigo))
            {
                InicializarVista();
                return;
            }

            var resultados = _clientes.BuscarPorCodigo(codigo);
            var dt = DataTableMapper.Clientes(resultados);

            if (dt.Rows.Count > 0)
            {
                _view.MostrarClientes(dt);
                var row = dt.Rows[0];
                SeleccionarCliente(
                    row["Codigo"]?.ToString() ?? string.Empty,
                    row["Nombre"]?.ToString() ?? string.Empty,
                    row["Telefono"]?.ToString(),
                    row["Direccion"]?.ToString() ?? string.Empty,
                    row["Nota"]?.ToString(),
                    row["Activo"] != null && row["Activo"] != DBNull.Value && Convert.ToBoolean(row["Activo"])
                );
            }
            else
            {
                InicializarVista();
            }
        }
    }
}

