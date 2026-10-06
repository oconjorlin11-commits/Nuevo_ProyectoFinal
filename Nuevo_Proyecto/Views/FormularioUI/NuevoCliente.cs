using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;
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
        public string AutorizadoPor { get => cmboxAutizado.Text; set => cmboxAutizado.Text = value; }

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
            cmboxAutizado.SelectedIndexChanged += (s, ev) => UpdateControlsState();
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
            // Delegar al presenter
            GuardarClicked?.Invoke(this, EventArgs.Empty);
            // El presenter mostrará mensajes y, si procede, la vista se cerrará mediante CloseView

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

            // Cargar autorizados (solo Cajeros y Admins)
            var autorizados = _presenter.GetUsuariosCajerosAdmins();
            cmboxAutizado.DataSource = autorizados;
            cmboxAutizado.DisplayMember = "Nombre";
            cmboxAutizado.ValueMember = "Codigo";
            cmboxAutizado.SelectedIndex = -1;

            checboxActico.Checked = true;
            // Inicializar estado de controles
            UpdateControlsState();
        }

        // Habilita/deshabilita el botón Guardar según validaciones simples
        private void UpdateControlsState()
        {
            bool codigoOk = !string.IsNullOrWhiteSpace(txtCodigoClient.Text);
            bool nombreOk = !string.IsNullOrWhiteSpace(txtNombreClient.Text);
            bool direccionOk = !string.IsNullOrWhiteSpace(txtDireccionClient.Text);
            bool telefonoOk = !string.IsNullOrWhiteSpace(txtTelefonoClient.Text);

            // Requerir código, nombre, dirección y teléfono para habilitar guardar
            btnGuardarClient.Enabled = codigoOk && nombreOk && direccionOk && telefonoOk;
        }



        // IClienteView - mostrar mensaje
        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        // IClienteView - resetear campos
        public void ResetFields()
        {
            Codigo = string.Empty;
            Nombre = string.Empty;
            Telefono = string.Empty;
            Direccion = string.Empty;
            Nota = string.Empty;
            Activo = true;
            cmboxAutizado.SelectedIndex = -1;
        }

        // INuevoClienteView - cerrar la vista (cuando el presentador lo solicite)
        public void CloseView()
        {
            this.Close();
        }
    }
}
