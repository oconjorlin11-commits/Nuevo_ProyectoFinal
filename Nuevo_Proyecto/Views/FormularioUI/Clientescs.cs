using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;
using Nuevo_Proyecto.Services.Helpers;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class Clientescs : Form, IClienteView
    {
        private readonly ClientePresenter _presenter;
        private bool _datosModificados = false;
        private string _datosOriginales = string.Empty;

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
        public string Nota
        {
            get => cmboxNotasClient.Text;
            set
            {
                // Intentar seleccionar por SelectedValue primero (valor vinculado)
                if (cmboxNotasClient.DataSource != null && !string.IsNullOrEmpty(value))
                {
                    cmboxNotasClient.SelectedValue = value;
                    // Si SelectedValue falla o retorna -1, setear el Text directamente
                    if (cmboxNotasClient.SelectedIndex == -1)
                    {
                        cmboxNotasClient.Text = value;
                    }
                }
                else
                {
                    cmboxNotasClient.Text = value;
                }
            }
        }
        public bool Activo { get => checboxClient.Checked; set => checboxClient.Checked = value; }
        public string BuscarTexto { get => txtBuscarClient.Text; set => txtBuscarClient.Text = value; }
        public bool HayMultiplesResultados { get; set; }

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
            ComboBoxBindingHelper.Bind(cmboxNotasClient, notas, "Nota", "Nota");
            cmboxNotasClient.DropDownStyle = ComboBoxStyle.DropDown;
        }

        public void SetActivoEnabled(bool enabled)
        {
            checboxClient.Enabled = enabled;
        }

        // =========================================================================
        // Carga y configuración visual de la interfaz UI (sin lógica de negocio)
        // =========================================================================

        private void Clientescs_Load(object? sender, EventArgs e)
        {
            txtCodigoClient.ReadOnly = true;
            txtCodigoClient.BackColor = Color.LightGray;
            checboxClient.Enabled = false;

            dataGridClientes.ReadOnly = true;
            dataGridClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridClientes.MultiSelect = false;

            ConfigurarDataGridView();
            dataGridClientes.CellFormatting += datagrewClientes_CellFormatting;
            dataGridClientes.SelectionChanged += dataGridClientes_SelectionChanged;

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

        private void btnNuevoClien_Click(object? sender, EventArgs e)
        {
            // No-op handler kept for clarity.
            using var frm = new NuevoCliente();
            frm.ShowDialog();
            _presenter.CargarClientesActivos();
        }

        private void btnEditarClien_Click(object? sender, EventArgs e) =>
            EditarClicked?.Invoke(this, EventArgs.Empty);

        private void btnEliminarClie_Click(object? sender, EventArgs e) =>
            EliminarClicked?.Invoke(this, EventArgs.Empty);

        private void txtBuscarClientes_TextChanged(object? sender, EventArgs e) =>
            BuscarChanged?.Invoke(this, EventArgs.Empty);

        private void dataGridClientes_SelectionChanged(object? sender, EventArgs e)
        {
            // Solo cargar campos si hay múltiples resultados (resultado de búsqueda)
            if (!HayMultiplesResultados)
                return;

            if (dataGridClientes.SelectedRows.Count > 0)
            {
                var row = dataGridClientes.SelectedRows[0];
                if (row.DataBoundItem is DataRowView drv)
                {
                    var dataRow = drv.Row;
                    _presenter?.SeleccionarCliente(
                        dataRow["Codigo"]?.ToString() ?? string.Empty,
                        dataRow["Nombre"]?.ToString() ?? string.Empty,
                        dataRow["Telefono"]?.ToString(),
                        dataRow["Direccion"]?.ToString() ?? string.Empty,
                        dataRow["Nota"]?.ToString(),
                        dataRow["Activo"] != null && dataRow["Activo"] != DBNull.Value && Convert.ToBoolean(dataRow["Activo"])
                    );

                    // 🔖 Guardar estado original y inicializar detección de cambios
                    GuardarDatosOriginales();
                    ActivarDeteccionCambios();
                }
            }
        }

        /// <summary>
        /// Guarda los datos actuales como estado original para comparar después
        /// </summary>
        private void GuardarDatosOriginales()
        {
            _datosOriginales = $"{Codigo}|{Nombre}|{Telefono}|{Direccion}|{Nota}|{Activo}";
            _datosModificados = false;
        }

        /// <summary>
        /// Activa los listeners para detectar cambios en los controles
        /// </summary>
        private void ActivarDeteccionCambios()
        {
            txtNombreClient.TextChanged += OnDatosChanged;
            txtTelefonoClient.TextChanged += OnDatosChanged;
            txtDireccionClient.TextChanged += OnDatosChanged;
            cmboxNotasClient.TextChanged += OnDatosChanged;
            checboxClient.CheckedChanged += OnDatosChanged;
        }

        /// <summary>
        /// Handler para detectar cualquier cambio en los datos
        /// </summary>
        private void OnDatosChanged(object? sender, EventArgs e)
        {
            if (!_datosModificados)
            {
                _datosModificados = true;
            }
        }

        /// <summary>
        /// Desactiva los listeners de detección de cambios
        /// </summary>
        private void DesactivarDeteccionCambios()
        {
            txtNombreClient.TextChanged -= OnDatosChanged;
            txtTelefonoClient.TextChanged -= OnDatosChanged;
            txtDireccionClient.TextChanged -= OnDatosChanged;
            cmboxNotasClient.TextChanged -= OnDatosChanged;
            checboxClient.CheckedChanged -= OnDatosChanged;
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

        private void txtDireccionClient_TextChanged(object? sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object? sender, EventArgs e)
        {

        }

        /// <summary>
        /// Evento FormClosing para mostrar advertencia si hay cambios sin guardar
        /// </summary>
        private void Clientescs_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Si hay datos modificados, mostrar advertencia
            if (_datosModificados)
            {
                string mensaje = AdvertenciaFormHelper.ObtenerMensajeAdvertencia("Cliente", esEdicion: true);
                string titulo = AdvertenciaFormHelper.ObtenerTituloConfirmacion("Cliente", esEdicion: true);

                DialogResult resultado = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // Si el usuario selecciona "No", cancelar el cierre
                if (resultado == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    // Si selecciona "Sí", limpiar estado y permitir cerrar
                    DesactivarDeteccionCambios();
                    _datosModificados = false;
                }
            }
        }
    }
}
