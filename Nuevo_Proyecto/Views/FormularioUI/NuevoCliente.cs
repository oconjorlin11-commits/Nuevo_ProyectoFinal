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

namespace Nuevo_Proyecto.Models.Views
{
    public partial class NuevoCliente : Form
    {

        private readonly InsertCommand _insertSevice;
        private readonly SelectQuery _selectService; public NuevoCliente()
        {
            InitializeComponent();

            _insertSevice = new InsertCommand();
            _selectService = new SelectQuery();
        }



        private void btnCancelarClient_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardarClient_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigoClient.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreClient.Text) ||
                    string.IsNullOrWhiteSpace(txtDireccionClient.Text))
                {
                    MessageBox.Show("Debe llenar al menos Código, Nombre y Dirección.");
                    return;
                }

                string autorizado = cmboxAutizado.Text;
                if (!_selectService.EsEmpleadoAdmin(autorizado) && !_selectService.EsEmpleadoCajero(autorizado))
                {
                    MessageBox.Show("Solo un Cajero o un Administrador puede agregar clientes.");
                    return;
                }

                int filas = _insertSevice.InsertarCliente(
                    txtCodigoClient.Text,
                    txtNombreClient.Text,
                    string.IsNullOrWhiteSpace(txtTelefonoClient.Text) ? null : txtTelefonoClient.Text,
                    string.IsNullOrWhiteSpace(txtDireccionClient.Text) ? null : txtDireccionClient.Text,
                    string.IsNullOrWhiteSpace(cmboxNota.Text) ? null : cmboxNota.Text,
                    checboxActico.Checked
                );

                if (filas > 0)
                {
                    MessageBox.Show("Cliente guardado exitosamente.");

                    // ⚡ refrescar automáticamente la grilla en Frm_Clientes
                    if (Owner is Clientescs frmClientes)
                    {
                        frmClientes.CargarClientesActivos();
                        frmClientes.LimpiarControles();

                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se insertó ningún registro.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar cliente: {ex.Message}");
            }

        }

        private void NuevoCliente_Load(object sender, EventArgs e)
        {
            SelectQuery clienteService = new SelectQuery();
            txtCodigoClient.Text = clienteService.GetNextCodigoCliente();

            DataTable notas = clienteService.GetNotas();
            cmboxNota.DataSource = notas;
            cmboxNota.DisplayMember = "Nota";
            cmboxNota.ValueMember = "Nota";
            cmboxNota.DropDownStyle = ComboBoxStyle.DropDown;

            // 👉 Cargar autorizados (solo Cajeros y Admins)
            DataTable autorizados = clienteService.GetUsuariosCajerosAdmins();
            cmboxAutizado.DataSource = autorizados;
            cmboxAutizado.DisplayMember = "Nombre";
            cmboxAutizado.ValueMember = "Codigo";
            cmboxAutizado.SelectedIndex = -1;

            checboxActico.Checked = true;
        }

        private void txtTelefonoClient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtDireccionClient.Focus();
            }
        }


        private void cmboxNota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                checboxActico.Focus();
            }
        }

        private void txtDireccionClient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cmboxNota.Focus();
            }
        }
        private void checboxActico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnGuardarClient.Focus();
            }
        }
    }
}
