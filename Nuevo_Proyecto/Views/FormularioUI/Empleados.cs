using Nuevo_Proyecto.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class Empleados : Form
    {
        private bool EstadoOriginal; // true= trabajando, false= despedido

        private string CodigoOriginal;

        private string NombreOriginal;

        private string CargoOriginal;

        private string CedulaOriginal;

        private string TelefonoOriginal;

        private decimal SalarioOriginal;

        private DateTime FechaIngresoOriginal;


        public Empleados()
        {
            InitializeComponent();
        }

        private void RellenarEstado()
        {
            // 👉 si no existe la columna Estado, la agregamos
            if (!dataGridEmpleados.Columns.Contains("Estado"))
            {
                DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
                colEstado.Name = "Estado";
                colEstado.HeaderText = "Estado";
                dataGridEmpleados.Columns.Add(colEstado);
            }

            foreach (DataGridViewRow row in dataGridEmpleados.Rows)
            {
                if (row.Cells["Activo"].Value != null && row.Cells["Activo"].Value != DBNull.Value)
                {
                    bool activo = (bool)row.Cells["Activo"].Value;
                    row.Cells["Estado"].Value = activo ? "Trabajando" : "Despedido";
                }
            }

            // 👉 ocultar columna Activo si no quieres mostrar 0/1
            dataGridEmpleados.Columns["Activo"].Visible = false;
        }



        private void CargarEmpleadosActivos()
        {
            SelectQuery selectQuery = new SelectQuery();
            DataTable empleados = selectQuery.GetEmpleadosActivosGrid();
            dataGridEmpleados.DataSource = empleados;

            dataGridEmpleados.Columns["Codigo"].HeaderText = "Código";
            dataGridEmpleados.Columns["Nombre"].HeaderText = "Nombre";
            dataGridEmpleados.Columns["Cargo"].HeaderText = "Cargo";
            dataGridEmpleados.Columns["FechaIngreso"].HeaderText = "Fecha Ingreso";
            dataGridEmpleados.Columns["Cedula"].HeaderText = "Cédula";
            dataGridEmpleados.Columns["Telefono"].HeaderText = "Teléfono";
            dataGridEmpleados.Columns["Salario"].HeaderText = "Salario";
            dataGridEmpleados.Columns["Activo"].HeaderText = "Estado"; // 👉 se renombra solo el encabezado

            RellenarEstado(); // 👉 recalcular columna Estado
        }

        private void LimpiarControles()
        {
            CodigoOriginal = null;
            NombreOriginal = null;
            CargoOriginal = null;
            CedulaOriginal = null;
            TelefonoOriginal = null;
            SalarioOriginal = 0;
            FechaIngresoOriginal = DateTime.MinValue;
            EstadoOriginal = false;

            txtCodigoEmpl.Clear();
            txtNombreEmpl.Clear();
            cmboxCargoEmpl.DataSource = null;
            cmboxCargoEmpl.Text = "";
            txtCedulaEmpl.Clear();
            txtTelefonoEmpl.Clear();
            txtSalarioEmpl.Clear();
            dateTimePickerEmpleado.Value = DateTime.Now;
            checkBoxEmpleado.Checked = false;
            checkBoxEmpleado.Enabled = false;
        }

        private void LimpiarDespuesDeAccion()
        {
            CargarEmpleadosActivos();
            LimpiarControles();
            txtBuscarEmpl.Clear();
        }


        private void btnNuevoEmpleado_Click(object sender, EventArgs e)
        {

            // Crear instancia del formulario Facturacion
            NuevoEmpleado frm = new NuevoEmpleado();

            // Mostrar el formulario embebido
            frm.Show();
        }

        private void Frm_Empleados_Load(object sender, EventArgs e)
        {

            // 👉 Configuración de controles
            txtCodigoEmpl.ReadOnly = true;
            txtCodigoEmpl.BackColor = Color.LightGray;
            dateTimePickerEmpleado.Enabled = false; // no editable
            checkBoxEmpleado.Enabled = false;

            dataGridEmpleados.ReadOnly = true;
            dataGridEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridEmpleados.MultiSelect = false;
            dataGridEmpleados.AutoGenerateColumns = true;


            // 👉 cargar combo de cargos al inicio
            SelectQuery query = new SelectQuery();
            DataTable cargos = query.GetCargos();
            cmboxCargoEmpl.DataSource = cargos;
            cmboxCargoEmpl.DisplayMember = "Cargo";
            cmboxCargoEmpl.ValueMember = "Cargo";
            cmboxCargoEmpl.DropDownStyle = ComboBoxStyle.DropDown;

            // 👉 cargar empleados activos al inicio
            CargarEmpleadosActivos();

            // 👉 agregar columna Estado (texto) y ocultar Activo si quieres
            if (!dataGridEmpleados.Columns.Contains("Estado"))
            {
                DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
                colEstado.Name = "Estado";
                colEstado.HeaderText = "Estado";
                dataGridEmpleados.Columns.Add(colEstado);
            }

            foreach (DataGridViewRow row in dataGridEmpleados.Rows)
            {
                if (row.Cells["Activo"].Value != null && row.Cells["Activo"].Value != DBNull.Value)
                {
                    bool activo = (bool)row.Cells["Activo"].Value;
                    row.Cells["Estado"].Value = activo ? "Trabajando" : "Despedido";
                }
            }

            // 👉 si no quieres mostrar el 0/1, oculta la columna Activo
            dataGridEmpleados.Columns["Activo"].Visible = false;

            LimpiarControles();
        }

        private void txtBuscarEmpleados_TextChanged(object sender, EventArgs e)
        {

            string codigo = txtBuscarEmpl.Text.Trim();
            SelectQuery query = new SelectQuery();

            if (string.IsNullOrEmpty(codigo))
            {
                CargarEmpleadosActivos();
                RellenarEstado();   // 👉 recalcular siempre
                LimpiarControles();
                return;
            }

            DataTable empleado = query.BuscarEmpleadoPorCodigo(codigo);

            if (empleado.Rows.Count > 0)
            {
                dataGridEmpleados.DataSource = empleado;

                DataRow row = empleado.Rows[0];
                CodigoOriginal = row["Codigo"].ToString();
                NombreOriginal = row["Nombre"].ToString();
                CargoOriginal = row["Cargo"].ToString();
                CedulaOriginal = row["Cedula"].ToString();
                TelefonoOriginal = row["Telefono"].ToString();
                SalarioOriginal = Convert.ToDecimal(row["Salario"]);
                FechaIngresoOriginal = Convert.ToDateTime(row["FechaIngreso"]);
                EstadoOriginal = Convert.ToBoolean(row["Activo"]);

                txtCodigoEmpl.Text = CodigoOriginal;
                txtNombreEmpl.Text = NombreOriginal;
                DataTable cargos = query.GetCargos();
                cmboxCargoEmpl.DataSource = cargos;
                cmboxCargoEmpl.DisplayMember = "Cargo";
                cmboxCargoEmpl.ValueMember = "Cargo";
                cmboxCargoEmpl.SelectedValue = CargoOriginal;
                txtCedulaEmpl.Text = CedulaOriginal;
                txtTelefonoEmpl.Text = TelefonoOriginal;
                txtSalarioEmpl.Text = SalarioOriginal.ToString();
                dateTimePickerEmpleado.Value = FechaIngresoOriginal;

                checkBoxEmpleado.Checked = EstadoOriginal;
                checkBoxEmpleado.Enabled = !EstadoOriginal;

                // 👉 recalcular columna Estado
                RellenarEstado();
            }
            else
            {
                // 👉 Si no se encontró el empleado
                CargarEmpleadosActivos();
                RellenarEstado();   // recalcular siempre
                LimpiarControles();

                // ⚡ Mostrar mensaje pero NO limpiar el textbox
                MessageBox.Show("Empleado no encontrado.");
            }
        }



        private void btnEditarEmpl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodigoOriginal))
            {
                MessageBox.Show("Debe buscar primero un empleado.");
                return;
            }

            UpdateCommand updateService = new UpdateCommand();

            // 👉 Caso 1: Reactivar empleado
            if (!EstadoOriginal && checkBoxEmpleado.Checked)
            {
                int filas = updateService.ReactivarEmpleado(CodigoOriginal);

                if (filas > 0)
                {
                    MessageBox.Show("Empleado reactivado correctamente.");
                    // ⚡ Mostrar lista inicial de empleados activos
                    LimpiarDespuesDeAccion();
                }
                else
                {
                    MessageBox.Show("No se pudo reactivar el empleado.");
                }
                return;
            }

            // 👉 Caso 2: Actualizar datos
            bool huboCambios =
                txtNombreEmpl.Text != NombreOriginal ||
                cmboxCargoEmpl.Text != CargoOriginal ||
                txtCedulaEmpl.Text != CedulaOriginal ||
                txtTelefonoEmpl.Text != TelefonoOriginal ||
                Convert.ToDecimal(txtSalarioEmpl.Text) != SalarioOriginal;

            if (!huboCambios)
            {
                MessageBox.Show("No se ha hecho ningún cambio.");
                return;
            }

            int filasUpdate = updateService.ActualizarEmpleado(
                CodigoOriginal,
                txtNombreEmpl.Text,
                cmboxCargoEmpl.Text,
                txtCedulaEmpl.Text,
                txtTelefonoEmpl.Text,
                Convert.ToDecimal(txtSalarioEmpl.Text)
            );

            if (filasUpdate > 0)
            {
                MessageBox.Show("Empleado actualizado correctamente.");
                // ⚡ Siempre volver a la vista inicial de empleados activos
                LimpiarDespuesDeAccion();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el empleado.");
            }
        }


        private void btnEliminarEmpl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodigoOriginal))
            {
                MessageBox.Show("Debe seleccionar un empleado primero.");
                return;
            }

            DeleteCommand deleteService = new DeleteCommand();
            int filas = deleteService.EliminarEmpleado(CodigoOriginal);

            if (filas > 0)
            {
                MessageBox.Show("Empleado despedido correctamente.");
                LimpiarDespuesDeAccion();
            }
            else
            {
                MessageBox.Show("No se pudo despedir al empleado.");
            }

        }

        private void datagrewEmpleados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridEmpleados.Columns[e.ColumnIndex].Name == "Activo")
            {
                if (e.Value != null && e.Value != DBNull.Value && e.Value is bool)
                {
                    bool activo = (bool)e.Value;
                    e.Value = activo ? "Trabajando" : "Despedido";
                    e.FormattingApplied = true;
                }
            }
        }

        private void dateTimePickerEmpleado_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Empleados_Load(object sender, EventArgs e)
        {

        }
    }
}
