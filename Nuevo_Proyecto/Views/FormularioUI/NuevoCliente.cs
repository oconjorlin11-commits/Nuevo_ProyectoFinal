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
                MessageBox.Show("El código es obligatorio.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigoClient.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreClient.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreClient.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTelefonoClient.Text))
            {
                MessageBox.Show("El teléfono es obligatorio.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefonoClient.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDireccionClient.Text))
            {
                MessageBox.Show("La dirección es obligatoria.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccionClient.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cmboxNota.Text) || cmboxNota.Text == "Selecciona una nota")
            {
                MessageBox.Show("Debes seleccionar una nota.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            cmboxNota.SelectedIndex = -1;
            cmboxNota.Text = "Selecciona una nota";
            Activo = true;
        }

        // INuevoClienteView - cerrar la vista (cuando el presentador lo solicite)
        public void CloseView()
        {
            this.Close();
        }
    }
}
