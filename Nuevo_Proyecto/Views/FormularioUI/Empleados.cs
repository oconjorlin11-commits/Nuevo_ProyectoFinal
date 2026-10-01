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
using Microsoft.Data.SqlClient;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class Empleados : Form, Nuevo_Proyecto.Views.Interfaces.IEmpleadoView
    {
        private bool EstadoOriginal; // true= trabajando, false= despedido

        private string CodigoOriginal;

        private string NombreOriginal;

        private string CargoOriginal;

        private string CedulaOriginal;

        private string TelefonoOriginal;

        private decimal SalarioOriginal;

        private DateTime FechaIngresoOriginal;


        private readonly EmpleadoPresenter _presenter;

        public Empleados()
        {
            InitializeComponent();
            _presenter = new EmpleadoPresenter(this);
            // Asegurar que el textbox de búsqueda ejecute el handler incluso si el diseñador no lo enlazó
            txtBuscarEmpl.TextChanged += txtBuscarEmpleados_TextChanged;
            // Volver a rellenar estado cuando termine el binding para asegurar visualización
            dataGridEmpleados.DataBindingComplete += (s, e) => RellenarEstado();
            // Convertir el valor bit (0/1) de la columna Activo en texto legible
            dataGridEmpleados.CellFormatting += datagrewEmpleados_CellFormatting;
            // Manejar errores de datos para evitar el dialogo predeterminado
            dataGridEmpleados.DataError += DataGridEmpleados_DataError;
        }

        // IEmpleadoView implementation
        public string Codigo { get => txtCodigoEmpl.Text; set => txtCodigoEmpl.Text = value; }
        public string Nombre { get => txtNombreEmpl.Text; set => txtNombreEmpl.Text = value; }
        public string Cedula { get => txtCedulaEmpl.Text; set => txtCedulaEmpl.Text = value; }
        public string Telefono { get => txtTelefonoEmpl.Text; set => txtTelefonoEmpl.Text = value; }
        public string Cargo { get => cmboxCargoEmpl.Text; set => cmboxCargoEmpl.Text = value; }
        public decimal Salario { get => decimal.TryParse(txtSalarioEmpl.Text, out var s) ? s : 0; set => txtSalarioEmpl.Text = value.ToString(); }
        public DateTime? FechaIngreso { get => dateTimePickerEmpleado.Value; set => dateTimePickerEmpleado.Value = value ?? DateTime.Now; }
        public bool Activo { get => checkBoxEmpleado.Checked; set => checkBoxEmpleado.Checked = value; }
        public string AutorizadoPor { get => string.Empty; set { } }

        public event EventHandler GuardarClicked;
        public event EventHandler CancelarClicked;

        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        public void ResetFields()
        {
            LimpiarControles();
        }

        // IEmpleadoView - cerrar la vista (no aplica para la vista principal, implementar como no-op)
        public void CloseView()
        {
            // No cerrar la ventana principal desde el presentador
        }

        private void RellenarEstado()
        {
            // Asegurar que solo exista una columna visible llamada "Estado"
            // Ocultar la columna cruda 'Activo' y mostrar la columna derivada 'Estado' si existe
            if (dataGridEmpleados.Columns.Contains("Activo"))
            {
                dataGridEmpleados.Columns["Activo"].Visible = false;
            }
            if (dataGridEmpleados.Columns.Contains("Estado"))
            {
                dataGridEmpleados.Columns["Estado"].Visible = true;
                dataGridEmpleados.Columns["Estado"].HeaderText = "Estado";
            }
            // Forzar refresco para que los cambios se reflejen
            dataGridEmpleados.Refresh();
        }

        private void DataGridEmpleados_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Evitar el cuadro de diálogo predeterminado y suprimir la excepción de formato
            e.ThrowException = false;
            // Opcional: podríamos registrar o mostrar un mensaje corto si es necesario
        }

        // Helper para convertir valores devueltos por la BD (bit 0/1, byte, int, bool, string) a bool
        private static bool ParseBoolDb(object? value)
        {
            if (value == null || value == DBNull.Value) return false;
            try
            {
                if (value is bool b) return b;
                if (value is byte by) return by != 0;
                if (value is short s) return s != 0;
                if (value is int i) return i != 0;
                if (value is long l) return l != 0L;
                var txt = value.ToString();
                if (string.IsNullOrWhiteSpace(txt)) return false;
                if (int.TryParse(txt, out var n)) return n != 0;
                if (bool.TryParse(txt, out var bb)) return bb;
            }
            catch
            {
                // ignorar y retornar false
            }
            return false;
        }



        private void CargarEmpleadosActivos()
        {
            var empleados = _presenter.GetEmpleadosActivos();
            var dt = new DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Cargo");
            dt.Columns.Add("FechaIngreso");
            dt.Columns.Add("Cedula");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Salario");
            dt.Columns.Add("Activo", typeof(object)); // mantener el tipo original pero como object para evitar conversiones automáticas
            // Columna visible con texto legible
            dt.Columns.Add("Estado", typeof(string));

            foreach (var e in empleados)
            {
                var row = dt.NewRow();
                row["Codigo"] = e.Codigo;
                row["Nombre"] = e.Nombre;
                row["Cargo"] = e.Cargo;
                row["FechaIngreso"] = e.Fechaingreso;
                row["Cedula"] = e.Cedula;
                row["Telefono"] = e.Telefono;
                row["Salario"] = e.Salario;
                row["Activo"] = e.Activo;
                row["Estado"] = ParseBoolDb(e.Activo) ? "Trabajando" : "Despedido";
                dt.Rows.Add(row);
            }

            dataGridEmpleados.DataSource = dt;

            dataGridEmpleados.Columns["Codigo"].HeaderText = "Código";
            dataGridEmpleados.Columns["Nombre"].HeaderText = "Nombre";
            dataGridEmpleados.Columns["Cargo"].HeaderText = "Cargo";
            dataGridEmpleados.Columns["FechaIngreso"].HeaderText = "Fecha Ingreso";
            dataGridEmpleados.Columns["Cedula"].HeaderText = "Cédula";
            dataGridEmpleados.Columns["Telefono"].HeaderText = "Teléfono";
            dataGridEmpleados.Columns["Salario"].HeaderText = "Salario";
            // Ajustar visual: ocultar la columna cruda Activo y mostrar la columna Estado
            if (dataGridEmpleados.Columns.Contains("Activo"))
            {
                dataGridEmpleados.Columns["Activo"].Visible = false;
            }
            if (dataGridEmpleados.Columns.Contains("Estado"))
            {
                dataGridEmpleados.Columns["Estado"].HeaderText = "Estado";
            }

            // Estado ya fue calculado en la tabla, pero llamar a RellenarEstado para mantener compatibilidad
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
            DataTable cargos = _presenter.GetCargos();
            cmboxCargoEmpl.DataSource = cargos;
            cmboxCargoEmpl.DisplayMember = "Cargo";
            cmboxCargoEmpl.ValueMember = "Cargo";
            cmboxCargoEmpl.DropDownStyle = ComboBoxStyle.DropDown;

            // 👉 cargar empleados activos al inicio
            CargarEmpleadosActivos();

            // Ocultar la columna Activo cruda (bit) y usar el formateo de celda para mostrar "Trabajando"/"Despedido"
            if (dataGridEmpleados.Columns.Contains("Activo"))
            {
                dataGridEmpleados.Columns["Activo"].Visible = false;
            }

            LimpiarControles();
        }

        private void txtBuscarEmpleados_TextChanged(object sender, EventArgs e)
        {

            string codigo = txtBuscarEmpl.Text.Trim();

            if (string.IsNullOrEmpty(codigo))
            {
                CargarEmpleadosActivos();
                RellenarEstado();   // 👉 recalcular siempre
                LimpiarControles();
                return;
            }

            DataTable empleado = _presenter.BuscarEmpleadoPorCodigo(codigo);

            // Añadir columna Estado legible y ocultar Activo crudo
            if (!empleado.Columns.Contains("Estado"))
            {
                empleado.Columns.Add("Estado", typeof(string));
                foreach (DataRow r in empleado.Rows)
                {
                    r["Estado"] = ParseBoolDb(r["Activo"]) ? "Trabajando" : "Despedido";
                }
            }

            if (empleado.Rows.Count > 0)
            {
                dataGridEmpleados.DataSource = empleado;
                if (dataGridEmpleados.Columns.Contains("Activo")) dataGridEmpleados.Columns["Activo"].Visible = false;
                if (dataGridEmpleados.Columns.Contains("Estado")) dataGridEmpleados.Columns["Estado"].HeaderText = "Estado";

                DataRow row = empleado.Rows[0];
                CodigoOriginal = row["Codigo"].ToString();
                NombreOriginal = row["Nombre"].ToString();
                CargoOriginal = row["Cargo"].ToString();
                CedulaOriginal = row["Cedula"].ToString();
                TelefonoOriginal = row["Telefono"].ToString();
                SalarioOriginal = Convert.ToDecimal(row["Salario"]);
                FechaIngresoOriginal = Convert.ToDateTime(row["FechaIngreso"]);
                EstadoOriginal = ParseBoolDb(row["Activo"]);

                txtCodigoEmpl.Text = CodigoOriginal;
                txtNombreEmpl.Text = NombreOriginal;
                DataTable cargos = _presenter.GetCargos();
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

            // 👉 Caso 1: Reactivar empleado
            if (!EstadoOriginal && checkBoxEmpleado.Checked)
            {
                bool ok = _presenter.ReactivarEmpleado(CodigoOriginal);

                if (ok)
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

            bool okUpdate = _presenter.ActualizarEmpleado(
                CodigoOriginal,
                txtNombreEmpl.Text,
                cmboxCargoEmpl.Text,
                txtCedulaEmpl.Text,
                txtTelefonoEmpl.Text,
                Convert.ToDecimal(txtSalarioEmpl.Text)
            );

            if (okUpdate)
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

            bool ok = _presenter.EliminarEmpleado(CodigoOriginal);

            if (ok)
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
            try
            {
                if (dataGridEmpleados.Columns[e.ColumnIndex].Name == "Activo")
                {
                    var val = e.Value;
                    bool activo = ParseBoolDb(val);
                    e.Value = activo ? "Trabajando" : "Despedido";
                    e.FormattingApplied = true;
                }
            }
            catch
            {
                // No permitir que el formateo rompa la UI
            }
        }

        private void dateTimePickerEmpleado_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Empleados_Load(object sender, EventArgs e)
        {
            // Llamar al inicializador existente para mantener compatibilidad con el código anterior
            Frm_Empleados_Load(sender, e);
        }
    }
}
