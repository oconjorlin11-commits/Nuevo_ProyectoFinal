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
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Nuevo_Proyecto.Models.Views
{
    public partial class Clientescs : Form
    {
        private bool EstadoOriginal; // guarda el estado activo real del cliente
        private string CodigoOriginal;
        private string NombreOriginal;
        private string TelefonoOriginal;
        private string DireccionOriginal;
        private string NotaOriginal;

        public Clientescs()
        {
            InitializeComponent();
        }

        private void ConfigurarDataGridView()
        {
            dataGridClientes.AutoGenerateColumns = false;
            dataGridClientes.Columns.Clear();

            // Definir columnas manualmente
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
                Name = "Activo",              // 👉 el Name se mantiene
                HeaderText = "Estado",        // 👉 solo cambia el HeaderText
                DataPropertyName = "Activo"
            });
        }

        public void LimpiarControles()
        {
            CodigoOriginal = null;
            NombreOriginal = null;
            TelefonoOriginal = null;
            DireccionOriginal = null;
            NotaOriginal = null;
            EstadoOriginal = false;

            txtCodigoClient.Clear();
            txtNombreClient.Clear();
            txtTelefonoClient.Clear();
            txtDireccionClient.Clear();
            cmboxNotasClient.DataSource = null;
            cmboxNotasClient.Text = "";
            checboxClient.Checked = false;
            checboxClient.Enabled = false;
        }

        private void LimpiarDespuesDeAccion()
        {
            CargarClientesActivos();
            LimpiarControles();
            txtBuscarClient.Clear();
        }

        public void CargarClientesActivos()
        {
            SelectQuery selectQuery = new SelectQuery();
            DataTable clientes = selectQuery.GetClientesActivos();
            dataGridClientes.DataSource = clientes;
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevoClien_Click(object sender, EventArgs e)
        {

            // Crear instancia del formulario Facturacion
            NuevoCliente frm = new NuevoCliente();

            // Mostrar el formulario embebido
            frm.Show();
        }

        private void btnEditarClien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodigoOriginal))
            {
                MessageBox.Show("Debe buscar primero un cliente.");
                return;
            }

            UpdateCommand updateService = new UpdateCommand();

            // 👉 Si estaba inactivo y el usuario marcó el CheckBox
            if (!EstadoOriginal && checboxClient.Checked)
            {
                int filas = updateService.ReactivarCliente(CodigoOriginal);

                if (filas > 0)
                {
                    MessageBox.Show("Cliente reactivado correctamente.");
                    LimpiarDespuesDeAccion();
                }
                else
                {
                    MessageBox.Show("No se pudo reactivar el cliente.");
                }
                return;
            }

            // 👉 Si estaba activo, validar cambios
            bool huboCambios =
                txtNombreClient.Text != NombreOriginal ||
                txtTelefonoClient.Text != TelefonoOriginal ||
                txtDireccionClient.Text != DireccionOriginal ||
                cmboxNotasClient.Text != NotaOriginal;

            if (!huboCambios)
            {
                MessageBox.Show("No se ha hecho ningún cambio.");
                return;
            }

            int filasUpdate = updateService.ActualizarCliente(
                CodigoOriginal,
                txtNombreClient.Text,
                txtTelefonoClient.Text,
                txtDireccionClient.Text,
                cmboxNotasClient.Text
            );

            if (filasUpdate > 0)
            {
                MessageBox.Show("Cliente actualizado correctamente.");
                LimpiarDespuesDeAccion();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el cliente.");
            }

        }

        private void btnEliminarClie_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodigoOriginal))
            {
                MessageBox.Show("Debe seleccionar un cliente primero.");
                return;
            }

            DeleteCommand deleteService = new DeleteCommand();
            int filas = deleteService.EliminarCliente(CodigoOriginal);

            if (filas > 0)
            {
                MessageBox.Show("Cliente eliminado (inactivado) correctamente.");
                LimpiarDespuesDeAccion();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar el cliente.");
            }

        }

        private void txtBuscarClientes_TextChanged(object sender, EventArgs e)
        {

            string codigo = txtBuscarClient.Text.Trim();
            SelectQuery query = new SelectQuery();

            if (string.IsNullOrEmpty(codigo))
            {
                CargarClientesActivos();
                LimpiarControles();
                return;
            }

            DataTable cliente = query.BuscarClientePorCodigo(codigo);

            if (cliente.Rows.Count > 0)
            {
                dataGridClientes.DataSource = cliente;

                DataRow row = cliente.Rows[0];
                CodigoOriginal = row["Codigo"].ToString();
                NombreOriginal = row["Nombre"].ToString();
                TelefonoOriginal = row["Telefono"].ToString();
                DireccionOriginal = row["Direccion"].ToString();
                NotaOriginal = row["Nota"].ToString();
                EstadoOriginal = Convert.ToBoolean(row["Activo"]);

                txtCodigoClient.Text = CodigoOriginal;
                txtNombreClient.Text = NombreOriginal;
                txtTelefonoClient.Text = TelefonoOriginal;
                txtDireccionClient.Text = DireccionOriginal;
                checboxClient.Checked = EstadoOriginal;
                checboxClient.Enabled = !EstadoOriginal;

                // 👉 cargar notas
                DataTable notas = query.GetNotas();
                cmboxNotasClient.DataSource = notas;
                cmboxNotasClient.DisplayMember = "Nota";
                cmboxNotasClient.ValueMember = "Nota";
                cmboxNotasClient.DropDownStyle = ComboBoxStyle.DropDown;
                cmboxNotasClient.Text = NotaOriginal;
            }
            else
            {
                CargarClientesActivos();
                LimpiarControles();
            }

        }
        private void Clientescs_Load(object sender, EventArgs e)
        {
            txtCodigoClient.ReadOnly = true;
            txtCodigoClient.BackColor = Color.LightGray;
            checboxClient.Enabled = false;

            dataGridClientes.ReadOnly = true;
            dataGridClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridClientes.MultiSelect = false;

            ConfigurarDataGridView();   // 👉 configurar columnas manualmente
            CargarClientesActivos();
            LimpiarControles();

        }

        private void datagrewClientes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {


            if (dataGridClientes.Columns[e.ColumnIndex].Name == "Activo")
            {
                if (e.Value != null && e.Value != DBNull.Value && e.Value is bool)
                {
                    bool activo = (bool)e.Value;
                    e.Value = activo ? "Activo" : "Inactivo";
                    e.FormattingApplied = true;
                }
            }
        }

    }
}
