using System;
using System.Collections.Generic;
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
    public partial class FacturasEmitidas : Form
    {
        public FacturasEmitidas()
        {
            InitializeComponent();
        }

        private void CargarTodasLasFacturas()
        {
            SelectQuery sq = new SelectQuery();
            dataGridFacturasEmitidas.DataSource = sq.GetTodasLasFacturasConDetalles();
            btnVerComprob.Enabled = false;
        }

        private void BuscarFacturaPorCodigo()
        {
            string codigo = txtBuscarFact.Text.Trim();

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                SelectQuery sq = new SelectQuery();
                DataTable dt = sq.BuscarFacturaPorCodigo(codigo);
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

        private void FiltrarFacturasPorFecha()
        {
            DateTime desde = dateTimeDesde.Value.Date;
            DateTime hasta = dateTimeHasta.Value.Date;

            SelectQuery sq = new SelectQuery();
            dataGridFacturasEmitidas.DataSource = sq.FiltrarFacturasPorFecha(desde, hasta);
            btnVerComprob.Enabled = false;
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
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarFacturaPorCodigo();
        }

        private void btnBucarFacturas_Click(object sender, EventArgs e)
        {
            FiltrarFacturasPorFecha();
        }
    }
}
