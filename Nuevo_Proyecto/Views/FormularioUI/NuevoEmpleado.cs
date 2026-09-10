using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nuevo_Proyecto.Services;
using Microsoft.Data.SqlClient;


namespace Nuevo_Proyecto.Models.Views
{
    public partial class NuevoEmpleado : Form
    {

        private readonly InsertCommand _InsertService;

        private readonly SelectQuery _selectService;


        public NuevoEmpleado()
        {
            InitializeComponent();
            _InsertService = new InsertCommand();
            _selectService = new SelectQuery();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarEmple_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardarEmple_Click(object sender, EventArgs e)
        {
            try
            {
                // 👉 Validar que todos los campos estén llenos
                if (string.IsNullOrWhiteSpace(txtCodigoEmple.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreEmple.Text) ||
                    string.IsNullOrWhiteSpace(txtCedulaEmpl.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefonoEmple.Text) ||
                    string.IsNullOrWhiteSpace(comboxCargoEmpleado.Text) ||
                    string.IsNullOrWhiteSpace(txtSalarioEmpleado.Text) ||
                    cmboxAutizadoEmple.SelectedIndex == -1 ||
                    !checkEmpleadoAct.Checked)
                {
                    MessageBox.Show("Debe llenar todos los campos, seleccionar un administrador y marcar el estado Activo.");
                    return;
                }

                // 👉 Validar salario
                if (!decimal.TryParse(txtSalarioEmpleado.Text, out decimal salario))
                {
                    MessageBox.Show("El salario debe ser un número válido.");
                    return;
                }

                // 👉 Validar que el autorizado sea Admin
                string autorizado = cmboxAutizadoEmple.Text;
                if (!_selectService.EsEmpleadoAdmin(autorizado))
                {
                    MessageBox.Show("Solo un administrador puede agregar empleados.");
                    return;
                }

                // 👉 Insertar empleado
                int filas = _InsertService.InsertarEmpleado(
                    txtCodigoEmple.Text,
                    txtNombreEmple.Text,
                    txtCedulaEmpl.Text,
                    txtTelefonoEmple.Text,
                    comboxCargoEmpleado.Text,
                    salario,
                    dateTimePicker1.Value,
                    checkEmpleadoAct.Checked,
                    autorizado
                );

                if (filas > 0)
                {
                    MessageBox.Show("Empleado guardado exitosamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se insertó ningún registro.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar empleado: {ex.Message}");
            }

        }

        private void NuevoEmpleado_Load(object sender, EventArgs e)
        {
            // 👉 Generar el próximo código automáticamente
            txtCodigoEmple.Text = _selectService.GetNextCodigoEmpleado();

            // 👉 Cargar cargos disponibles en el ComboBox
            DataTable cargos = _selectService.GetCargosDisponibles();
            comboxCargoEmpleado.DataSource = cargos;
            comboxCargoEmpleado.DisplayMember = "Cargo";
            comboxCargoEmpleado.ValueMember = "Cargo";
            comboxCargoEmpleado.SelectedIndex = -1;

            // Permitir escribir uno nuevo además de elegir
            comboxCargoEmpleado.DropDownStyle = ComboBoxStyle.DropDown;
            comboxCargoEmpleado.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboxCargoEmpleado.AutoCompleteSource = AutoCompleteSource.ListItems;

            // 👉 Cargar solo administradores en ComboBox de autorización
            DataTable admins = _selectService.GetUsuariosAdministradores();
            cmboxAutizadoEmple.DataSource = admins;
            cmboxAutizadoEmple.DisplayMember = "Nombre";
            cmboxAutizadoEmple.ValueMember = "Codigo";
            cmboxAutizadoEmple.SelectedIndex = -1;

            // 👉 Inicializar salario en 0
            txtSalarioEmpleado.Text = "0";

            // 👉 Estado activo por defecto
            checkEmpleadoAct.Checked = true;
            checkEmpleadoAct.Enabled = false; // ⚡ para que no lo desmarquen al agregar
        }

        private void txtCedulaEmple_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtTelefonoEmple.Focus();
            }
        }
    }

}
