using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class NuevoCliente : Form, INuevoClienteView
    {
        // INuevoClienteView properties implementation
        public string Codigo { get => txtCodigoClient.Text; set => txtCodigoClient.Text = value; }
        public string Nombre { get => txtNombreClient.Text; set => txtNombreClient.Text = value; }
        public string Telefono { get => txtTelefonoClient.Text; set => txtTelefonoClient.Text = value; }
        public string Direccion { get => txtDireccionClient.Text; set => txtDireccionClient.Text = value; }
        public string Nota { get => cmboxNota.Text; set => cmboxNota.Text = value; }
        public bool Activo { get => checboxActico.Checked; set => checboxActico.Checked = value; }

        public event EventHandler? GuardarClicked;
        public event EventHandler? CancelarClicked;

        private readonly ClientePresenter _presenter;

        public NuevoCliente()
        {
            InitializeComponent();
            _presenter = new ClientePresenter(this);
        }

        // Suscribir eventos de cambio para actualizar el estado del botón Guardar
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Suscripciones seguras (control existirá tras InitializeComponent)
            txtCodigoClient.TextChanged += (s, ev) => UpdateControlsState();
            txtNombreClient.TextChanged += (s, ev) => UpdateControlsState();
            txtTelefonoClient.TextChanged += (s, ev) => UpdateControlsState();
            txtDireccionClient.TextChanged += (s, ev) => UpdateControlsState();
            cmboxNota.TextChanged += (s, ev) => UpdateControlsState();
            checboxActico.CheckedChanged += (s, ev) => UpdateControlsState();
        }



        private void btnCancelarClient_Click(object sender, EventArgs e)
        {
            // Notify presenter to reset fields if subscribed
            CancelarClicked?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void btnGuardarClient_Click(object sender, EventArgs e)
        {
            // Validar campos antes de guardar
            if (string.IsNullOrWhiteSpace(txtCodigoClient.Text))
            {
                MensajeHelper.Validacion.CampoObligatorio("Código");
                txtCodigoClient.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreClient.Text))
            {
                MensajeHelper.Validacion.CampoObligatorio("Nombre");
                txtNombreClient.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTelefonoClient.Text))
            {
                MensajeHelper.Validacion.CampoObligatorio("Teléfono");
                txtTelefonoClient.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDireccionClient.Text))
            {
                MensajeHelper.Validacion.CampoObligatorio("Dirección");
                txtDireccionClient.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cmboxNota.Text) || cmboxNota.Text == "Selecciona una nota")
            {
                MensajeHelper.Validacion.CampoObligatorio("Nota");
                cmboxNota.Focus();
                return;
            }

            // Si todos los campos están completos, delegar al presenter
            GuardarClicked?.Invoke(this, EventArgs.Empty);

            // Registrar auditoría con el ClienteID recién creado
            var auditoriaService = new AuditoriaService();
            auditoriaService.RegistrarAuditoria(
                seccion: "Clientes",
                accion: "CREAR",
                usuario: SesionActual.NombreUsuario ?? "Desconocido",
                rolUsuario: SesionActual.Usuario?.Cargo ?? "Usuario",
                registroID: _presenter.UltimoClienteIDCreado,  // Pasar el ClienteID creado
                usuarioAutorizador: SesionActual.UsuarioAutorizador,  // Pasar el autorizado
                detalle: $"Cliente: {txtNombreClient.Text}, Teléfono: {txtTelefonoClient.Text}",
                descripcion: $"Nuevo cliente creado: {txtNombreClient.Text}"
            );
        }

        private void NuevoCliente_Load(object sender, EventArgs e)
        {
            // Obtener datos iniciales desde el presenter
            txtCodigoClient.Text = _presenter.GetNextCodigoCliente();

            var notas = _presenter.GetNotas();
            cmboxNota.DataSource = notas;
            cmboxNota.DisplayMember = "Nota";
            cmboxNota.ValueMember = "Nota";
            cmboxNota.DropDownStyle = ComboBoxStyle.DropDown;
            cmboxNota.SelectedIndex = -1;
            cmboxNota.Text = "Selecciona una nota";

            checboxActico.Checked = true;
            // Inicializar estado de controles
            UpdateControlsState();
        }

        // Habilita/deshabilita el botón Guardar y muestra avisos según validaciones
        private void UpdateControlsState()
        {
            // El botón Guardar siempre está habilitado
            btnGuardarClient.Enabled = true;
            btnGuardarClient.Text = "Guardar";
            btnGuardarClient.BackColor = Color.DarkGreen;
        }



        // IClienteView - mostrar mensaje
        public void showMessage(string message, string titulo, bool esError)
        {
            if (esError)
            {
                MensajeHelper.Error(message, titulo);
            }
            else
            {
                MensajeHelper.Exito(message, titulo);
            }
        }

        // IClienteView - resetear campos
        public void ResetFields()
        {
            Codigo = string.Empty;
            Nombre = string.Empty;
            Telefono = string.Empty;
            Direccion = string.Empty;
            cmboxNota.SelectedIndex = -1;
            cmboxNota.Text = "Selecciona una nota";
            Activo = true;
        }

        // INuevoClienteView - cerrar la vista (cuando el presentador lo solicite)
        public void CloseView()
        {
            this.Close();
        }

        /// <summary>
        /// Evento FormClosing para mostrar advertencia si hay datos sin guardar
        /// </summary>
        private void NuevoCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si la razón de cierre no es una acción del usuario, permitir cerrar
            if (e.CloseReason != CloseReason.UserClosing)
                return;

            // Verificar si hay datos incompletos
            bool hayDatos = !string.IsNullOrWhiteSpace(Nombre) ||
                            !string.IsNullOrWhiteSpace(Codigo) ||
                            !string.IsNullOrWhiteSpace(Telefono) ||
                            !string.IsNullOrWhiteSpace(Direccion) ||
                            (cmboxNota.SelectedIndex >= 0 && cmboxNota.Text != "Selecciona una nota");

            // Si no hay datos, permitir cerrar
            if (!hayDatos)
                return;

            // Mostrar advertencia estandarizada
            string mensaje = AdvertenciaFormHelper.ObtenerMensajeAdvertencia("Cliente");
            string titulo = AdvertenciaFormHelper.ObtenerTituloConfirmacion("Cliente");

            DialogResult resultado = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // Si el usuario selecciona "No", cancelar el cierre
            if (resultado == DialogResult.No)
                e.Cancel = true;
        }
    }
}
