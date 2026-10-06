using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;


namespace Nuevo_Proyecto.Models.Views
{
    public partial class NuevoEmpleado : Form, INuevoEmpleadoView
    {
        private readonly EmpleadoPresenter _presenter;

        public event EventHandler? GuardarClicked;
        public event EventHandler? CancelarClicked;

        public NuevoEmpleado()
        {
            InitializeComponent();
            _presenter = new EmpleadoPresenter(this);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Suscribir eventos para actualizar estado del botón Guardar
            txtCodigoEmple.TextChanged += (s, ev) => UpdateControlsState();
            txtNombreEmple.TextChanged += (s, ev) => UpdateControlsState();
            txtCedulaEmpl.TextChanged += (s, ev) => UpdateControlsState();
            txtTelefonoEmple.TextChanged += (s, ev) => UpdateControlsState();
            comboxCargoEmpleado.TextChanged += (s, ev) => UpdateControlsState();
            txtSalarioEmpleado.TextChanged += (s, ev) => UpdateControlsState();
            cmboxAutizadoEmple.SelectedIndexChanged += (s, ev) => UpdateControlsState();
            checkEmpleadoAct.CheckedChanged += (s, ev) => UpdateControlsState();
            UpdateControlsState();
        }

        private void UpdateControlsState()
        {
            // El botón Guardar siempre está habilitado
            btnGuardarEmple.Enabled = true;
            btnGuardarEmple.Text = "Guardar";
            btnGuardarEmple.BackColor = Color.DarkGreen;
        }

        // IEmpleadoView - mostrar mensaje
        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        // IEmpleadoView - resetear campos
        public void ResetFields()
        {
            txtCodigoEmple.Text = string.Empty;
            txtNombreEmple.Text = string.Empty;
            txtCedulaEmpl.Text = string.Empty;
            txtTelefonoEmple.Text = string.Empty;
            comboxCargoEmpleado.SelectedIndex = -1;
            comboxCargoEmpleado.Text = "Selecciona un cargo";
            txtSalarioEmpleado.Text = "0";
            cmboxAutizadoEmple.SelectedIndex = -1;
            cmboxAutizadoEmple.Text = "Seleccione un autorizado";
            checkEmpleadoAct.Checked = false;
        }

        // IEmpleadoView implementation
        public string Codigo { get => txtCodigoEmple.Text; set => txtCodigoEmple.Text = value; }
        public string Nombre { get => txtNombreEmple.Text; set => txtNombreEmple.Text = value; }
        public string Cedula { get => txtCedulaEmpl.Text; set => txtCedulaEmpl.Text = value; }
        public string Telefono { get => txtTelefonoEmple.Text; set => txtTelefonoEmple.Text = value; }
        public string Cargo { get => comboxCargoEmpleado.Text; set => comboxCargoEmpleado.Text = value; }
        public decimal Salario { get => decimal.TryParse(txtSalarioEmpleado.Text, out var s) ? s : 0; set => txtSalarioEmpleado.Text = value.ToString(); }
        public DateTime? FechaIngreso { get => dateTimePicker1.Value; set => dateTimePicker1.Value = value ?? DateTime.Now; }
        public bool Activo { get => checkEmpleadoAct.Checked; set => checkEmpleadoAct.Checked = value; }
        public string AutorizadoPor { get => cmboxAutizadoEmple.Text; set => cmboxAutizadoEmple.Text = value; }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarEmple_Click(object sender, EventArgs e)
        {
            // Notify presenter to reset fields if subscribed
            CancelarClicked?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void btnGuardarEmple_Click(object sender, EventArgs e)
        {
            // Validar campos antes de guardar
            if (string.IsNullOrWhiteSpace(txtCodigoEmple.Text))
            {
                MessageBox.Show("El código es obligatorio.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigoEmple.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreEmple.Text))
            {
                MessageBox.Show("El nombre es obligatorio. Formato: Mínimo 2 palabras (ej: Jose Alejandro Ordoñez Medina).", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreEmple.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCedulaEmpl.Text))
            {
                MessageBox.Show("La cédula es obligatoria. Formato: xxx-xxxxxx-xxxxA (ej: 561-021007-1000A).", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCedulaEmpl.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTelefonoEmple.Text))
            {
                MessageBox.Show("El teléfono es obligatorio. Formato: xxxx-xxxx (ej: 7635-7836).", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefonoEmple.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(comboxCargoEmpleado.Text))
            {
                MessageBox.Show("El cargo es obligatorio. Selecciona o ingresa un cargo.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboxCargoEmpleado.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSalarioEmpleado.Text))
            {
                MessageBox.Show("El salario es obligatorio. Formato: números con separadores de miles usando coma (ej: 120,25 o 123,123,123).", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSalarioEmpleado.Focus();
                return;
            }

            // Validar que el salario sea mayor a 1
            if (!decimal.TryParse(txtSalarioEmpleado.Text, out var salario) || salario <= 1)
            {
                MessageBox.Show("El salario debe ser mayor a 1. Formato: números con separadores de miles usando coma (ej: 120,25 o 123,123,123).", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSalarioEmpleado.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cmboxAutizadoEmple.Text) || cmboxAutizadoEmple.SelectedIndex == -1)
            {
                MessageBox.Show("Debes seleccionar un personal encargado (administrador).", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmboxAutizadoEmple.Focus();
                return;
            }

            // Si todos los campos están completos, delegar al presenter
            GuardarClicked?.Invoke(this, EventArgs.Empty);
            // El presenter puede solicitar cerrar la vista mediante CloseView
        }

        private void NuevoEmpleado_Load(object sender, EventArgs e)
        {
            // Obtener valores iniciales desde el presenter
            txtCodigoEmple.Text = _presenter.GetNextCodigoEmpleado();

            // Cargar cargos disponibles en el ComboBox
            DataTable cargos = _presenter.GetCargos();
            comboxCargoEmpleado.DataSource = cargos;
            comboxCargoEmpleado.DisplayMember = "Cargo";
            comboxCargoEmpleado.ValueMember = "Cargo";
            comboxCargoEmpleado.SelectedIndex = -1;
            comboxCargoEmpleado.Text = "Selecciona un cargo"; // Placeholder text

            // Permitir escribir uno nuevo además de elegir
            comboxCargoEmpleado.DropDownStyle = ComboBoxStyle.DropDown;
            comboxCargoEmpleado.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboxCargoEmpleado.AutoCompleteSource = AutoCompleteSource.ListItems;

            // Cargar solo administradores en ComboBox de autorización
            DataTable admins = _presenter.GetUsuariosAdministradores();
            cmboxAutizadoEmple.DataSource = admins;
            cmboxAutizadoEmple.DisplayMember = "Nombre";
            cmboxAutizadoEmple.ValueMember = "Codigo";
            cmboxAutizadoEmple.SelectedIndex = -1;
            cmboxAutizadoEmple.Text = "Seleccione un autorizado"; // Placeholder text

            // Inicializar salario en 0
            txtSalarioEmpleado.Text = "0";

            // Estado activo por defecto
            checkEmpleadoAct.Checked = true;
            checkEmpleadoAct.Enabled = false; // para que no lo desmarquen al agregar
        }

        private void txtCedulaEmple_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtTelefonoEmple.Focus();
            }
        }

        // IEmpleadoView - cerrar la vista cuando el presentador lo solicite
        public void CloseView()
        {
            this.Close();
        }
    }

}
