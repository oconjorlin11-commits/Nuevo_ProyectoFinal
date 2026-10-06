using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class Clientescs : Form, IClienteView
    {
        private readonly ClientePresenter _presenter;

        public Clientescs()
        {
            InitializeComponent();
            _presenter = new ClientePresenter(this);
        }

        // =========================================================================
        // Implementación de IClienteView: propiedades enlazadas a los controles UI
        // =========================================================================

        public string Codigo { get => txtCodigoClient.Text; set => txtCodigoClient.Text = value; }
        public string Nombre { get => txtNombreClient.Text; set => txtNombreClient.Text = value; }
        public string Telefono { get => txtTelefonoClient.Text; set => txtTelefonoClient.Text = value; }
        public string Direccion { get => txtDireccionClient.Text; set => txtDireccionClient.Text = value; }
        public string Nota { get => cmboxNotasClient.Text; set => cmboxNotasClient.Text = value; }
        public bool Activo { get => checboxClient.Checked; set => checboxClient.Checked = value; }
        public string BuscarTexto { get => txtBuscarClient.Text; set => txtBuscarClient.Text = value; }

        // =========================================================================
        // Eventos que la vista UI notifica al presentador
        // =========================================================================

        public event EventHandler? EditarClicked;
        public event EventHandler? EliminarClicked;
        public event EventHandler? BuscarChanged;

        // =========================================================================
        // Métodos de control visual ordenados por el presentador
        // =========================================================================

        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        public void ResetFields()
        {
            txtCodigoClient.Clear();
            txtNombreClient.Clear();
            txtTelefonoClient.Clear();
            txtDireccionClient.Clear();
            cmboxNotasClient.DataSource = null;
            cmboxNotasClient.Text = string.Empty;
            checboxClient.Checked = false;
            checboxClient.Enabled = false;
        }

        public void MostrarClientes(DataTable dt)
        {
            dataGridClientes.DataSource = dt;
        }

        public void CargarNotas(DataTable notas)
        {
            cmboxNotasClient.DataSource = notas;
            cmboxNotasClient.DisplayMember = "Nota";
            cmboxNotasClient.ValueMember = "Nota";
            cmboxNotasClient.DropDownStyle = ComboBoxStyle.DropDown;
        }

        public void SetActivoEnabled(bool enabled)
        {
            checboxClient.Enabled = enabled;
        }

        // =========================================================================
        // Carga y configuración visual de la interfaz UI (sin lógica de negocio)
        // =========================================================================

        private void Clientescs_Load(object sender, EventArgs e)
        {
            txtCodigoClient.ReadOnly = true;
            txtCodigoClient.BackColor = Color.LightGray;
            checboxClient.Enabled = false;

            dataGridClientes.ReadOnly = true;
            dataGridClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridClientes.MultiSelect = false;

            ConfigurarDataGridView();
            dataGridClientes.CellFormatting += datagrewClientes_CellFormatting;

            _presenter.InicializarVista();
        }

        private void ConfigurarDataGridView()
        {
            dataGridClientes.AutoGenerateColumns = false;
            dataGridClientes.Columns.Clear();

            dataGridClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Codigo",
                HeaderText = "Código",
                DataPropertyName = "Codigo"
            });

            dataGridClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre"
            });

            dataGridClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                HeaderText = "Teléfono",
                DataPropertyName = "Telefono"
            });

            dataGridClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Direccion",
                HeaderText = "Dirección",
                DataPropertyName = "Direccion"
            });

            dataGridClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nota",
                HeaderText = "Nota",
                DataPropertyName = "Nota"
            });

            dataGridClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Activo",
                HeaderText = "Estado",
                DataPropertyName = "Activo"
            });
        }

        private void btnNuevoClien_Click(object sender, EventArgs e)
        {
            // No-op handler kept for clarity.
            using var frm = new NuevoCliente();
            frm.ShowDialog();
            _presenter.CargarClientesActivos();
        }

        private void btnEditarClien_Click(object sender, EventArgs e) =>
            EditarClicked?.Invoke(this, EventArgs.Empty);

        private void btnEliminarClie_Click(object sender, EventArgs e) =>
            EliminarClicked?.Invoke(this, EventArgs.Empty);

        private void txtBuscarClientes_TextChanged(object sender, EventArgs e) =>
            BuscarChanged?.Invoke(this, EventArgs.Empty);

        private void dataGridClientes_SelectionChanged(object sender, EventArgs e)
        {
            // El DataGrid es solo de observación, no tiene funcionalidad de búsqueda o filtro
            // No se carga información en los campos de edición desde el grid
        }

        private void datagrewClientes_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridClientes.Columns[e.ColumnIndex].Name == "Activo" && e.Value is bool activo)
            {
                e.Value = activo ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
        }

        private void label1_Click(object? sender, EventArgs e) { }
    }
}
