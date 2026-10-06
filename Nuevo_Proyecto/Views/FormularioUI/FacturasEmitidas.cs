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
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Views.Helpers;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class FacturasEmitidas : Form, IFacturasEmitidasView
    {
        private readonly FacturacionPresenter _presenter;

        public FacturasEmitidas()
        {
            InitializeComponent();
            _presenter = new FacturacionPresenter(this);
        }

        public void MostrarFacturas(DataTable facturas)
        {
            dataGridFacturasEmitidas.DataSource = facturas;
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

        private void CargarTodasLasFacturas()
        {
            MostrarFacturas(_presenter.GetTodasLasFacturasConDetalles());
            btnVerComprob.Enabled = false;
        }

        private void BuscarFacturaPorCodigo()
        {
            string codigo = txtBuscarFact.Text.Trim();

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                DataTable dt = _presenter.BuscarFacturaPorCodigo(codigo);
                MostrarFacturas(dt);

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
                string facturaCodigo = dataGridFacturasEmitidas.CurrentRow.Cells["Numero"].Value?.ToString() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(facturaCodigo))
                {
                    // 👉 Abre el comprobante con ese código
                    using (Comprobante frmComprobante = new Comprobante(facturaCodigo))
                    {
                        frmComprobante.ShowDialog();
                    }
                }
            }
            else
            {
                showMessage("Seleccione una factura para ver el comprobante.", "Aviso", false);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnularFacturaSeleccionada();
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
                showMessage("Seleccione una factura.", "Aviso", false);
                return;
            }

            var motivo = PromptDialog.Pedir("Anular factura", $"Motivo de la anulación de {numero}:");
            if (motivo == null) return;

            if (MessageBox.Show($"¿Anular la factura {numero}? Se devolverá el stock al inventario.",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                _presenter.AnularFactura(numero, motivo);
                showMessage($"Factura {numero} anulada.", "Listo", false);
                CargarTodasLasFacturas();
            }
            catch (Exception ex)
            {
                showMessage(ex.Message, "No se pudo anular", true);
            }
        }

        private void txtBuscarFact_TextChanged(object sender, EventArgs e)
        {
            BuscarFacturaPorCodigo();
        }
    }
}

