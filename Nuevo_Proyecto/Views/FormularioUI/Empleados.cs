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

            txtBuscarEmpl.TextChanged += (s, e) => BuscarChanged?.Invoke(this, EventArgs.Empty);
            dataGridEmpleados.DataBindingComplete += (s, e) => RellenarEstado();
            dataGridEmpleados.CellFormatting += datagrewEmpleados_CellFormatting;
            dataGridEmpleados.DataError += DataGridEmpleados_DataError;
            dataGridEmpleados.SelectionChanged += dataGridEmpleados_SelectionChanged;
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
            ConfigurarColumnasGrid();
            RellenarEstado();
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

            _presenter.InicializarVista();
        }

        private void ConfigurarColumnasGrid()
        {
            if (dataGridEmpleados.Columns.Contains("Codigo")) dataGridEmpleados.Columns["Codigo"]!.HeaderText = "Código";
            if (dataGridEmpleados.Columns.Contains("Nombre")) dataGridEmpleados.Columns["Nombre"]!.HeaderText = "Nombre";
            if (dataGridEmpleados.Columns.Contains("Cargo")) dataGridEmpleados.Columns["Cargo"]!.HeaderText = "Cargo";
            if (dataGridEmpleados.Columns.Contains("FechaIngreso")) dataGridEmpleados.Columns["FechaIngreso"]!.HeaderText = "Fecha Ingreso";
            if (dataGridEmpleados.Columns.Contains("Cedula")) dataGridEmpleados.Columns["Cedula"]!.HeaderText = "Cédula";
            if (dataGridEmpleados.Columns.Contains("Telefono")) dataGridEmpleados.Columns["Telefono"]!.HeaderText = "Teléfono";
            if (dataGridEmpleados.Columns.Contains("Salario")) dataGridEmpleados.Columns["Salario"]!.HeaderText = "Salario";

            if (dataGridEmpleados.Columns.Contains("Activo")) dataGridEmpleados.Columns["Activo"]!.Visible = false;
            if (dataGridEmpleados.Columns.Contains("Estado")) dataGridEmpleados.Columns["Estado"]!.HeaderText = "Estado";
        }

        private void RellenarEstado()
        {
            if (dataGridEmpleados.Columns.Contains("Activo"))
            {
                dataGridEmpleados.Columns["Activo"]!.Visible = false;
            }
            if (dataGridEmpleados.Columns.Contains("Estado"))
            {
                dataGridEmpleados.Columns["Estado"]!.Visible = true;
                dataGridEmpleados.Columns["Estado"]!.HeaderText = "Estado";
            }
        }

        private void dataGridEmpleados_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridEmpleados.SelectedRows == null || dataGridEmpleados.SelectedRows.Count == 0) return;
            var row = dataGridEmpleados.SelectedRows[0];
            if (row.Cells[0].Value == null) return;

            try
            {
                string codigo = row.Cells["Codigo"].Value?.ToString() ?? string.Empty;
                string nombre = row.Cells["Nombre"].Value?.ToString() ?? string.Empty;
                string cargo = row.Cells["Cargo"].Value?.ToString() ?? string.Empty;
                string cedula = row.Cells["Cedula"].Value?.ToString() ?? string.Empty;
                string? telefono = row.Cells["Telefono"].Value?.ToString();
                decimal salario = decimal.TryParse(row.Cells["Salario"].Value?.ToString(), out var s) ? s : 0;
                DateTime fecha = DateTime.TryParse(row.Cells["FechaIngreso"].Value?.ToString(), out var f) ? f : DateTime.Now;
                bool activo = ParseBoolDb(row.Cells["Activo"].Value);

                _presenter.SeleccionarEmpleado(codigo, nombre, cargo, cedula, telefono, salario, fecha, activo);
            }
            catch
            {
                // ignorar errores de formato al cambiar de fila
            }
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
            try
            {
                if (dataGridEmpleados.Columns[e.ColumnIndex].Name == "Activo")
                {
                    bool activo = ParseBoolDb(e.Value);
                    e.Value = activo ? "Trabajando" : "Despedido";
                    e.FormattingApplied = true;
                }
            }
            catch
            {
            }
        }

        private void DataGridEmpleados_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private static bool ParseBoolDb(object? value)
        {
            if (value == null || value == DBNull.Value) return false;
            try
            {
                if (value is bool b) return b;
                var txt = value.ToString()?.Trim() ?? string.Empty;
                if (txt == "1") return true;
                if (txt == "0") return false;
                if (bool.TryParse(txt, out var bb)) return bb;
            }
            catch
            {
            }
            return false;
        }

        private void dateTimePickerEmpleado_ValueChanged(object sender, EventArgs e) { }
    }
}

