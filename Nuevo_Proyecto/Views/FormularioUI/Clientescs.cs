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
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Nuevo_Proyecto.Models.Views
{
    public partial class Clientescs : Form, IClienteView
    {
        private bool EstadoOriginal; // guarda el estado activo real del cliente
        private string CodigoOriginal;
        private string NombreOriginal;
        private string TelefonoOriginal;
        private string DireccionOriginal;
        private string NotaOriginal;

        private readonly ClientePresenter _presenter;

        public Clientescs()
        {
            InitializeComponent();
            _presenter = new ClientePresenter(this);
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

        // IClienteView properties (implementación mínima usada por presenter)
        public string Codigo { get => txtCodigoClient.Text; set => txtCodigoClient.Text = value; }
        public string Nombre { get => txtNombreClient.Text; set => txtNombreClient.Text = value; }
        public string Telefono { get => txtTelefonoClient.Text; set => txtTelefonoClient.Text = value; }
        public string Direccion { get => txtDireccionClient.Text; set => txtDireccionClient.Text = value; }
        public string Nota { get => cmboxNotasClient.Text; set => cmboxNotasClient.Text = value; }
        public bool Activo { get => checboxClient.Checked; set => checboxClient.Checked = value; }
        public string AutorizadoPor { get => string.Empty; set { } }

        public event EventHandler GuardarClicked;
        public event EventHandler CancelarClicked;

        public void CargarClientesActivos()
        {
            var clientes = _presenter.GetClientesActivos();
            // Convertir lista de entidades a DataTable para compatibilidad con la UI existente
            var dt = new DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Direccion");
            dt.Columns.Add("Nota");
            dt.Columns.Add("Activo", typeof(bool));

            foreach (var c in clientes)
            {
                dt.Rows.Add(c.Codigo, c.Nombre, c.Telefono, c.Direccion, c.Nota, c.Activo);
            }

            dataGridClientes.DataSource = dt;
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
            LimpiarControles();
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

            // 👉 Si estaba inactivo y el usuario marcó el CheckBox
            if (!EstadoOriginal && checboxClient.Checked)
            {
                bool ok = _presenter.ReactivarCliente(CodigoOriginal);

                if (ok)
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

            bool okUpdate = _presenter.ActualizarCliente(
                CodigoOriginal,
                txtNombreClient.Text,
                txtTelefonoClient.Text,
                txtDireccionClient.Text,
                cmboxNotasClient.Text
            );

            if (okUpdate)
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

            bool ok = _presenter.EliminarCliente(CodigoOriginal);

            if (ok)
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

            if (string.IsNullOrEmpty(codigo))
            {
                CargarClientesActivos();
                LimpiarControles();
                return;
            }

            DataTable cliente = _presenter.BuscarClientePorCodigo(codigo);

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
                DataTable notas = _presenter.GetNotas();
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

        private void dataGridClientes_SelectionChanged(object sender, EventArgs e)
        {
            // Cuando el usuario selecciona una fila en el grid, cargar datos en los campos
            if (dataGridClientes.SelectedRows == null || dataGridClientes.SelectedRows.Count == 0)
                return;

            var row = dataGridClientes.SelectedRows[0];

            // Soportar origen DataTable (binding) o objetos
            try
            {
                if (row.Cells[0].Value == null) return;

                CodigoOriginal = row.Cells[0].Value?.ToString();
                NombreOriginal = row.Cells[1].Value?.ToString();
                TelefonoOriginal = row.Cells[2].Value?.ToString();
                DireccionOriginal = row.Cells[3].Value?.ToString();
                NotaOriginal = row.Cells[4].Value?.ToString();
                EstadoOriginal = row.Cells[5].Value != null && row.Cells[5].Value != DBNull.Value && Convert.ToBoolean(row.Cells[5].Value);

                txtCodigoClient.Text = CodigoOriginal;
                txtNombreClient.Text = NombreOriginal;
                txtTelefonoClient.Text = TelefonoOriginal;
                txtDireccionClient.Text = DireccionOriginal;
                checboxClient.Checked = EstadoOriginal;
                checboxClient.Enabled = !EstadoOriginal;

                // Cargar notas en combo
                DataTable notas = _presenter.GetNotas();
                cmboxNotasClient.DataSource = notas;
                cmboxNotasClient.DisplayMember = "Nota";
                cmboxNotasClient.ValueMember = "Nota";
                cmboxNotasClient.DropDownStyle = ComboBoxStyle.DropDown;
                cmboxNotasClient.Text = NotaOriginal;
            }
            catch
            {
                // ignore parsing errors
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
