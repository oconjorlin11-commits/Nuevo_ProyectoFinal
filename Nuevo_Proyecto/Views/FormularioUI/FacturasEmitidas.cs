using System;
using System.Collections.Generic;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Views.Helpers;
using Nuevo_Proyecto.Views.Interfaces;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Nuevo_Proyecto.Models.Views
{
    public partial class FacturasEmitidas : Form, IFacturacionView
    {
        private readonly FacturacionPresenter _presenter;

        public FacturasEmitidas()
        {
            InitializeComponent();
            _presenter = new FacturacionPresenter(this);
        }

        private void CargarTodasLasFacturas()
        {
            dataGridFacturasEmitidas.DataSource = _presenter.GetTodasLasFacturasConDetalles();
            btnVerComprob.Enabled = false;
        }

        private void BuscarFacturaPorCodigo()
        {
            string codigo = txtBuscarFact.Text.Trim();

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                DataTable dt = _presenter.BuscarFacturaPorCodigo(codigo);
                dataGridFacturasEmitidas.DataSource = dt;

                // 👉 habilitar solo si hay exactamente 1 resultado
                btnVerComprob.Enabled = dt.Rows.Count == 1;
            }
            else
            {
                // 👉 si está vacío, mostrar todas
                CargarTodasLasFacturas();
                btnVerComprob.Enabled = false;
            }
        }

        private void btnVerComprob_Click(object sender, EventArgs e)
        {
            if (dataGridFacturasEmitidas.CurrentRow != null)
            {
                // 👉 Obtiene el código de factura (ej. FACT-002)
                string facturaCodigo = dataGridFacturasEmitidas.CurrentRow.Cells["Numero"].Value.ToString();

                // 👉 Abre el comprobante con ese código
                Comprobante frmComprobante = new Comprobante(facturaCodigo);
                frmComprobante.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione una factura para ver el comprobante.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void FacturasEmitidas_Load(object sender, EventArgs e)
        {
            CargarTodasLasFacturas(); // 👉 al abrir, carga todas las facturas con detalles
            ConfigurarMenuAnular();
        }

        // Clic derecho sobre una factura -> "Anular factura" (solo administradores).
        // Se arma por código para no tocar el diseñador ni el layout existente.
        private void ConfigurarMenuAnular()
        {
            if (!SesionActual.EsAdministrador) return;

            var menu = new ContextMenuStrip();
            var item = new ToolStripMenuItem("Anular factura...");
            item.Click += (s, ev) => AnularFacturaSeleccionada();
            menu.Items.Add(item);
            dataGridFacturasEmitidas.ContextMenuStrip = menu;

            // el clic derecho también selecciona la fila bajo el cursor
            dataGridFacturasEmitidas.CellMouseDown += (s, ev) =>
            {
                if (ev.Button == MouseButtons.Right && ev.RowIndex >= 0)
                {
                    dataGridFacturasEmitidas.ClearSelection();
                    dataGridFacturasEmitidas.Rows[ev.RowIndex].Selected = true;
                    dataGridFacturasEmitidas.CurrentCell = dataGridFacturasEmitidas.Rows[ev.RowIndex].Cells[0];
                }
            };
        }

        private void AnularFacturaSeleccionada()
        {
            var fila = dataGridFacturasEmitidas.CurrentRow;
            var numero = fila?.Cells["Numero"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(numero))
            {
                MessageBox.Show("Seleccione una factura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var motivo = PromptDialog.Pedir("Anular factura", $"Motivo de la anulación de {numero}:");
            if (motivo == null) return;

            if (MessageBox.Show($"¿Anular la factura {numero}? Se devolverá el stock al inventario.",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                _presenter.AnularFactura(numero, motivo);
                MessageBox.Show($"Factura {numero} anulada.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarTodasLasFacturas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo anular", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtBuscarFact_TextChanged(object sender, EventArgs e)
        {
            BuscarFacturaPorCodigo();
        }

        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        public void ResetFields()
        {
            dataGridFacturasEmitidas.DataSource = null;
        }

        // Implementación de métodos de la interfaz IFacturacionView requeridos
        public void LoadCategorias(DataTable categorias) { }
        public void LoadProductosPorCategoria(DataTable productos) { }
        public void LoadEmpleados(DataTable empleados) { }
        public void LoadClientes(DataTable clientes) { }
        public void LoadFormasPago(DataTable formasPago) { }
        public void AgregarLineaDetalle(int productoId, string nombreProducto, int cantidad, decimal precioUnitario, decimal subtotal) { }
        public void LimpiarDetalles() { }
        public int ObtenerFilasDetalles() => 0;

        // Propiedades de la interfaz
        public int? ClienteSeleccionado => null;
        public int? EmpleadoSeleccionado => null;
        public int? FormaPagoSeleccionado => null;
        public int? CategoriaSeleccionada => null;
        public int? ProductoSeleccionado => null;
        public int CantidadProducto => 0;
        public string Observacion => string.Empty;
        public decimal Total { get => 0; set { } }

        // Eventos de la interfaz
        public event EventHandler NuevoClienteClick;
        public event EventHandler AgregarProductoClick;
        public event EventHandler QuitarLineaClick;
        public event EventHandler LimpiarTodoClick;
        public event EventHandler VerImprimirClick;
        public event EventHandler GuardarFacturaClick;
    }
}
