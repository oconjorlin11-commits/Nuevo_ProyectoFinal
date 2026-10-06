using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class Empleados : Form, IEmpleadoView
    {
        private readonly EmpleadoPresenter _presenter;

        public Empleados()
        {
            InitializeComponent();
            _presenter = new EmpleadoPresenter(this);
        }

        // =========================================================================
        // Implementación de IEmpleadoView: propiedades enlazadas a controles UI
        // =========================================================================

        public string Codigo { get => txtCodigoEmpl.Text; set => txtCodigoEmpl.Text = value; }
        public string Nombre { get => txtNombreEmpl.Text; set => txtNombreEmpl.Text = value; }
        public string Cedula { get => txtCedulaEmpl.Text; set => txtCedulaEmpl.Text = value; }
        public string Telefono { get => txtTelefonoEmpl.Text; set => txtTelefonoEmpl.Text = value; }
        public string Cargo { get => cmboxCargoEmpl.Text; set => cmboxCargoEmpl.Text = value; }
        public decimal Salario
        {
            get => decimal.TryParse(txtSalarioEmpl.Text, out var s) ? s : 0;
            set => txtSalarioEmpl.Text = value.ToString("0.00");
        }
        public DateTime? FechaIngreso
        {
            get => dateTimePickerEmpleado.Value;
            set => dateTimePickerEmpleado.Value = value ?? DateTime.Now;
        }
        public bool Activo { get => checkBoxEmpleado.Checked; set => checkBoxEmpleado.Checked = value; }
        public string BuscarTexto { get => txtBuscarEmpl.Text; set => txtBuscarEmpl.Text = value; }

        // =========================================================================
        // Eventos de la vista notificados al presentador
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
            txtCodigoEmpl.Clear();
            txtNombreEmpl.Clear();
            cmboxCargoEmpl.SelectedIndex = -1;
            cmboxCargoEmpl.Text = string.Empty;
            txtCedulaEmpl.Clear();
            txtTelefonoEmpl.Clear();
            txtSalarioEmpl.Clear();
            dateTimePickerEmpleado.Value = DateTime.Now;
            checkBoxEmpleado.Checked = false;
            checkBoxEmpleado.Enabled = false;
        }

        public void MostrarEmpleados(DataTable dt)
        {
            dataGridEmpleados.DataSource = dt;
        }

        public void CargarCargos(DataTable cargos)
        {
            cmboxCargoEmpl.DataSource = cargos;
            cmboxCargoEmpl.DisplayMember = "Cargo";
            cmboxCargoEmpl.ValueMember = "Cargo";
            cmboxCargoEmpl.DropDownStyle = ComboBoxStyle.DropDown;
        }

        public void SetActivoEnabled(bool enabled)
        {
            checkBoxEmpleado.Enabled = enabled;
        }

        // =========================================================================
        // Configuración y eventos visuales
        // =========================================================================

        private void Empleados_Load(object sender, EventArgs e)
        {
            txtCodigoEmpl.ReadOnly = true;
            txtCodigoEmpl.BackColor = Color.LightGray;
            dateTimePickerEmpleado.Enabled = false;
            checkBoxEmpleado.Enabled = false;

            dataGridEmpleados.ReadOnly = true;
            dataGridEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridEmpleados.MultiSelect = false;

            ConfigurarDataGridView();
            dataGridEmpleados.CellFormatting += datagrewEmpleados_CellFormatting;
            txtBuscarEmpl.TextChanged += (s, e) => BuscarChanged?.Invoke(this, EventArgs.Empty);

            _presenter.InicializarVista();
        }

        private void ConfigurarDataGridView()
        {
            dataGridEmpleados.AutoGenerateColumns = false;
            dataGridEmpleados.Columns.Clear();

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Codigo",
                HeaderText = "Código",
                DataPropertyName = "Codigo"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cedula",
                HeaderText = "Cédula",
                DataPropertyName = "Cedula"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cargo",
                HeaderText = "Cargo",
                DataPropertyName = "Cargo"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                HeaderText = "Teléfono",
                DataPropertyName = "Telefono"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Salario",
                HeaderText = "Salario",
                DataPropertyName = "Salario"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaIngreso",
                HeaderText = "Fecha Ingreso",
                DataPropertyName = "FechaIngreso"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Activo",
                HeaderText = "Estado",
                DataPropertyName = "Activo"
            });
        }

        private void dataGridEmpleados_SelectionChanged(object? sender, EventArgs e)
        {
            // El DataGrid es solo de observación, no tiene funcionalidad de búsqueda o filtro
            // No se carga información en los campos de edición desde el grid
        }

        private void btnNuevoEmpleado_Click(object sender, EventArgs e)
        {
            using (var frm = new NuevoEmpleado())
            {
                frm.ShowDialog();
            }
            _presenter.CargarEmpleadosActivos();
        }

        private void btnEditarEmpl_Click(object sender, EventArgs e) =>
            EditarClicked?.Invoke(this, EventArgs.Empty);

        private void btnEliminarEmpl_Click(object sender, EventArgs e) =>
            EliminarClicked?.Invoke(this, EventArgs.Empty);

        private void datagrewEmpleados_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridEmpleados.Columns[e.ColumnIndex].Name == "Activo" && e.Value is bool activo)
            {
                e.Value = activo ? "Trabajando" : "Despedido";
                e.FormattingApplied = true;
            }
        }
    }
}

