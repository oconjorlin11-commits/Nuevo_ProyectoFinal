using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Models.Views;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.FormularioUI;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class ClientePresenter
    {
        private readonly IClienteView? _view;
        private readonly INuevoClienteView? _nuevoClienteView;
        private readonly IClienteRepository _clientes;
        private readonly IEmpleadoRepository _empleados;

        // Propiedad para capturar el ID del ultimo cliente creado (para auditoría)
        public int? UltimoClienteIDCreado { get; private set; }

        // Estado original seleccionado para control de cambios y reactivación
        private string? _codigoOriginal;
        private string? _nombreOriginal;
        private string? _telefonoOriginal;
        private string? _direccionOriginal;
        private string? _notaOriginal;
        private bool _estadoOriginal;

        // Constructor para la administración de clientes (Clientescs)
        public ClientePresenter(IClienteView view,
                                IClienteRepository? clientes = null,
                                IEmpleadoRepository? empleados = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _clientes = clientes ?? new ClienteRepository();
            _empleados = empleados ?? new EmpleadoRepository();

            _view.EditarClicked += OnEditarClicked;
            _view.EliminarClicked += OnEliminarClicked;
            _view.BuscarChanged += OnBuscarChanged;
        }

        // Constructor para la creación de nuevo cliente (NuevoCliente)
        public ClientePresenter(INuevoClienteView nuevoClienteView,
                                IClienteRepository? clientes = null,
                                IEmpleadoRepository? empleados = null)
        {
            _nuevoClienteView = nuevoClienteView ?? throw new ArgumentNullException(nameof(nuevoClienteView));
            _clientes = clientes ?? new ClienteRepository();
            _empleados = empleados ?? new EmpleadoRepository();

            _nuevoClienteView.GuardarClicked += OnGuardarNuevoClienteClicked;
            _nuevoClienteView.CancelarClicked += OnCancelarNuevoClienteClicked;
        }

        // Constructor genérico para consultas/tests
        public ClientePresenter(IClienteRepository? clientes = null,
                                IEmpleadoRepository? empleados = null)
        {
            _clientes = clientes ?? new ClienteRepository();
            _empleados = empleados ?? new EmpleadoRepository();
        }

        // ---------------------------------------------------------------- control de la vista

        public void InicializarVista()
        {
            LimpiarCampos();
            CargarClientesActivos();
        }

        public void CargarClientesActivos()
        {
            if (_view == null) return;
            var clientes = _clientes.GetActivos();
            _view.MostrarClientes(DataTableMapper.Clientes(clientes));
            _view.CargarNotas(GetNotas());
        }

        private void RefrescarYLimpiar()
        {
            if (_view != null)
            {
                _view.BuscarTexto = string.Empty;  // Limpiar el textbox de búsqueda
            }
            CargarClientesActivos();  // Recargar los clientes activos
            LimpiarCampos();  // Limpiar los campos de edición
        }

        public void SeleccionarCliente(string codigo, string nombre, string? telefono, string direccion, string? nota, bool activo)
        {
            _codigoOriginal = codigo;
            _nombreOriginal = nombre;
            _telefonoOriginal = telefono;
            _direccionOriginal = direccion;
            _notaOriginal = nota;
            _estadoOriginal = activo;

            if (_view != null)
            {
                _view.Codigo = codigo;
                _view.Nombre = nombre;
                _view.Telefono = telefono ?? string.Empty;
                _view.Direccion = direccion;
                _view.Activo = activo;
                _view.SetActivoEnabled(!activo);

                // IMPORTANTE: Cargar notas PRIMERO antes de setear el valor
                _view.CargarNotas(GetNotas());

                // LUEGO setear la nota seleccionada (después de que el combo tenga el DataSource)
                _view.Nota = nota ?? string.Empty;
            }
        }

        public void LimpiarCampos()
        {
            _codigoOriginal = null;
            _nombreOriginal = null;
            _telefonoOriginal = null;
            _direccionOriginal = null;
            _notaOriginal = null;
            _estadoOriginal = false;

            if (_view != null)
            {
                _view.ResetFields();
                _view.SetActivoEnabled(false);
            }
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

        // ---------------------------------------------------------------- métodos auxiliares

        /// <summary>
        /// Verifica si el usuario actual es administrador.
        /// Si no, solicita credenciales de administrador.
        /// Retorna true si acceso fue otorgado; false en caso contrario.
        /// </summary>
        private bool VerificarAccesoAdmin()
        {
            // Si es administrador puro, permitir acceso directo
            if (SesionActual.EsAdministrador)
                return true;

            // Mostrar diálogo solicitando credenciales de admin
            if (!MensajeHelper.Confirmacion(
                "Para editar o eliminar un cliente necesita permiso de administrador.\n\n" +
                "¿Desea ingresar las credenciales de un administrador?",
                "Permiso requerido"))
                return false;

            // Mostrar diálogo de verificación de usuario
            var verificacion = new Verificacion_de_usuario();
            if (verificacion.ShowDialog() == DialogResult.OK)
            {
                // Guardar el nombre del admin verificado en la sesión
                SesionActual.UsuarioAutorizador = verificacion.AdminNombreVerificado;

                // Registrar la auditoría cuando se otorga acceso temporal
                var auditoriaService = new AuditoriaService();
                auditoriaService.RegistrarAuditoria(
                    seccion: "Clientes",
                    accion: "ACCESO_TEMPORAL",
                    usuario: SesionActual.NombreUsuario ?? "Desconocido",
                    rolUsuario: SesionActual.Usuario?.Cargo ?? "Usuario",
                    descripcion: $"Acceso temporal a Clientes otorgado por usuario autorizado ({SesionActual.UsuarioAutorizador})"
                );

                return true;
            }

            return false;
        }

        /// <summary>
        /// Solicita un motivo de ACTUALIZACIÓN usando el formulario específico de Cliente.
        /// Retorna el motivo si es válido; string.Empty si fue cancelado.
        /// </summary>
        private string SolicitarMotivoActualizacionCliente()
        {
            var formMotivo = new Motivo_de_actualizacion_cliente();
            if (formMotivo.ShowDialog() == DialogResult.OK)
            {
                string motivo = formMotivo.Motivo?.Trim() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(motivo))
                {
                    return motivo;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Solicita un motivo de ELIMINACIÓN usando el formulario específico de Cliente.
        /// Retorna el motivo si es válido; string.Empty si fue cancelado.
        /// </summary>
        private string SolicitarMotivoEliminacionCliente()
        {
            var formMotivo = new Motivo_de_eliminacion_de_cliente();
            if (formMotivo.ShowDialog() == DialogResult.OK)
            {
                string motivo = formMotivo.Motivo?.Trim() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(motivo))
                {
                    return motivo;
                }
            }

            return string.Empty;
        }

        // ---------------------------------------------------------------- eventos de NuevoCliente

        private void OnCancelarNuevoClienteClicked(object? sender, EventArgs e)
        {
            _nuevoClienteView?.ResetFields();
        }

        private void OnGuardarNuevoClienteClicked(object? sender, EventArgs e)
        {
            if (_nuevoClienteView == null) return;

            string codigo = _nuevoClienteView.Codigo?.Trim() ?? string.Empty;
            string nombre = _nuevoClienteView.Nombre?.Trim() ?? string.Empty;
            string? telefono = string.IsNullOrWhiteSpace(_nuevoClienteView.Telefono) ? null : _nuevoClienteView.Telefono.Trim();
            string direccion = _nuevoClienteView.Direccion?.Trim() ?? string.Empty;
            string? nota = string.IsNullOrWhiteSpace(_nuevoClienteView.Nota) ? null : _nuevoClienteView.Nota.Trim();

            // Validar campos básicos
            if (string.IsNullOrEmpty(codigo))
            {
                MensajeHelper.Cliente.CodigoRequerido();
                return;
            }

            // Validar con reglas de negocio desde ClienteRepository
            string? errorValidacion = _clientes.ValidarNuevoCliente(codigo, nombre, telefono, direccion, nota);
            if (errorValidacion != null)
            {
                MensajeHelper.Error(errorValidacion, "Validación");
                return;
            }

            try
            {
                if (_clientes.CodigoExiste(codigo))
                {
                    MensajeHelper.Cliente.CodigoDuplicado(codigo);
                    return;
                }

                _clientes.Crear(new ClienteDto
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    Telefono = telefono,
                    Direccion = direccion,
                    Nota = nota,
                    Activo = true  // Siempre activo al crear
                });

                // Capturar el ID del cliente recién creado (se asigna automáticamente al objeto)
                var clienteCreado = _clientes.BuscarPorCodigo(codigo).FirstOrDefault();
                if (clienteCreado != null)
                {
                    UltimoClienteIDCreado = clienteCreado.ClienteId;
                }

                MensajeHelper.Cliente.GuardadoExitoso();
                _nuevoClienteView.ResetFields();
                try { _nuevoClienteView.CloseView(); } catch { /* la vista puede no ser modal */ }
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

        // ---------------------------------------------------------------- eventos de Clientescs

        private void OnEditarClicked(object? sender, EventArgs e)
        {
            if (_view == null) return;

            if (string.IsNullOrEmpty(_codigoOriginal))
            {
                MensajeHelper.Cliente.SeleccionarPrimero();
                return;
            }

            // Paso 1: PRIMERO verificar si realmente hay cambios (incluyendo reactivación)
            bool intentaReactivar = !_estadoOriginal && _view.Activo;

            bool huboCambios = intentaReactivar ||
                (_view.Nombre?.Trim() != _nombreOriginal ||
                 (_view.Telefono?.Trim() ?? string.Empty) != (_telefonoOriginal ?? string.Empty) ||
                 _view.Direccion?.Trim() != _direccionOriginal ||
                 (_view.Nota?.Trim() ?? string.Empty) != (_notaOriginal ?? string.Empty));

            if (!huboCambios)
            {
                MensajeHelper.Cliente.SinCambios();
                return;
            }

            // Paso 2: LUEGO verificar permisos de administrador
            if (!VerificarAccesoAdmin())
            {
                MensajeHelper.Seguridad.PermisoModificarDenegado();
                return;
            }

            // Paso 3: Si intenta reactivar
            if (intentaReactivar)
            {
                string motivo = SolicitarMotivoActualizacionCliente();
                if (string.IsNullOrWhiteSpace(motivo))
                {
                    MensajeHelper.Motivo.MotivoRequerido("reactivar el cliente");
                    return;
                }

                try
                {
                    bool okReactivar = _clientes.Reactivar(_codigoOriginal, motivo);
                    if (okReactivar)
                    {
                        MensajeHelper.Cliente.ReactivadoExitoso();
                        RefrescarYLimpiar();
                    }
                    else
                    {
                        MensajeHelper.Cliente.NoSeReactivo();
                    }
                }
                catch (Exception ex)
                {
                    MensajeHelper.BaseDatos.ErrorAlActualizar(ex.Message);
                }
                return;
            }

            // Paso 4: Validar formato de campos modificados
            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string direccion = _view.Direccion?.Trim() ?? string.Empty;
            string? telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono.Trim();
            string? nota = string.IsNullOrWhiteSpace(_view.Nota) ? null : _view.Nota.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(direccion))
            {
                MensajeHelper.Cliente.NombreDireccionObligatorios();
                return;
            }

            if (nombre.Length > 100 || direccion.Length > 200 || (telefono?.Length ?? 0) > 20 || (nota?.Length ?? 0) > 200)
            {
                MensajeHelper.Cliente.LongitudCamposExcedida();
                return;
            }

            // Paso 5: Solicitar motivo de modificación
            string motivoModificacion = SolicitarMotivoActualizacionCliente();
            if (string.IsNullOrWhiteSpace(motivoModificacion))
            {
                MensajeHelper.Motivo.MotivoRequerido("modificar el cliente");
                return;
            }

            // Paso 6: Guardar cambios en BD
            try
            {
                bool okUpdate = _clientes.Actualizar(_codigoOriginal, nombre, telefono, direccion, nota, motivoModificacion);
                if (okUpdate)
                {
                    MensajeHelper.Cliente.ActualizadoExitoso();
                    RefrescarYLimpiar();
                }
                else
                {
                    MensajeHelper.Cliente.NoSeActualizo();
                }
            }
            catch (Exception ex)
            {
                MensajeHelper.BaseDatos.ErrorAlActualizar(ex.Message);
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (_view == null) return;

            if (string.IsNullOrEmpty(_codigoOriginal))
            {
                MensajeHelper.Cliente.SeleccionarPrimero();
                return;
            }

            // Paso 1: Confirmación inicial antes de pedir credenciales
            if (!MensajeHelper.Operacion.ConfirmarEliminacion("cliente", $"{_nombreOriginal} (Código: {_codigoOriginal})"))
            {
                return;
            }

            // Paso 2: Verificar permisos de administrador
            if (!VerificarAccesoAdmin())
            {
                MensajeHelper.Seguridad.PermisoEliminarDenegado();
                return;
            }

            // Paso 3: Solicitar motivo de eliminación usando formulario específico de Cliente
            string motivo = SolicitarMotivoEliminacionCliente();
            if (string.IsNullOrWhiteSpace(motivo))
            {
                MensajeHelper.Motivo.MotivoRequerido("dar de baja el cliente");
                return;
            }

            try
            {
                bool ok = _clientes.Desactivar(_codigoOriginal, motivo);
                if (ok)
                {
                    MensajeHelper.Cliente.EliminandoExitoso();
                    RefrescarYLimpiar();
                }
                else
                {
                    MensajeHelper.Cliente.NoSeElimino();
                }
            }
            catch (Exception ex)
            {
                MensajeHelper.BaseDatos.ErrorAlEliminar(ex.Message);
            }
        }

        private void OnBuscarChanged(object? sender, EventArgs e)
        {
            if (_view == null) return;

            string termino = _view.BuscarTexto?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(termino))
            {
                // Si el textbox está vacío, mostrar solo los activos y limpiar campos
                _view.HayMultiplesResultados = false;
                CargarClientesActivos();
                LimpiarCampos();
                return;
            }

            // Determinar si es búsqueda por código o nombre
            // Códigos tienen guion (ej: "CLI-001"), nombres no
            bool esCodigoFormat = termino.Contains('-');

            // Validar longitud mínima según tipo de búsqueda
            // Códigos: mínimo 1 carácter
            // Nombres: mínimo 1 carácter
            if (termino.Length < 1)
            {
                // No mostrar nada, DataGrid vacío (sin mezclar con activos)
                _view.HayMultiplesResultados = false;
                var dtVacio = DataTableMapper.Clientes(Enumerable.Empty<ClienteDto>());
                _view.MostrarClientes(dtVacio);
                LimpiarCampos();
                return;
            }

            // Buscar por código O nombre (parcial) - busca en TODO
            var resultados = _clientes.BuscarPorCodigoONombre(termino);
            var dt = DataTableMapper.Clientes(resultados);

            if (dt.Rows.Count == 0)
            {
                // No hay coincidencias - mostrar tabla vacía
                _view.HayMultiplesResultados = false;
                _view.MostrarClientes(dt);
                LimpiarCampos();
                return;
            }

            if (dt.Rows.Count == 1)
            {
                // Una única coincidencia - cargar automáticamente
                _view.HayMultiplesResultados = false;
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
                // Múltiples coincidencias - mostrar en DataGrid para elegir
                _view.HayMultiplesResultados = true;
                _view.MostrarClientes(dt);
                LimpiarCampos();
            }
        }
    }
}

