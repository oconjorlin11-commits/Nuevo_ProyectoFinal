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
            btnVerComprob.Enabled = true;
        }

        private void BuscarFacturaPorCodigo()
        {
            string busqueda = txtBuscarFact.Text.Trim();

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                CargarTodasLasFacturas();
                return;
            }

            // Detectar si es una fecha (formato d/m/a o dd/mm/aa)
            if (EsFormatoFecha(busqueda))
            {
                if (DateTime.TryParseExact(busqueda, new[] { "d/M/yy", "dd/MM/yy", "d/M/yyyy", "dd/MM/yyyy" }, 
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, out var fecha))
                {
                    DataTable dt = _presenter.BuscarFacturasPorFecha(fecha);
                    MostrarFacturas(dt);
                    btnVerComprob.Enabled = true;

                    // 👉 Limpiar selección para que el usuario deba seleccionar explícitamente
                    dataGridFacturasEmitidas.ClearSelection();
                }
                else
                {
                    CargarTodasLasFacturas();
                }
            }
            else
            {
                // Es un código de factura
                DataTable dt = _presenter.BuscarFacturaPorCodigo(busqueda);
                MostrarFacturas(dt);
                btnVerComprob.Enabled = true;
            }
        }

        private bool EsFormatoFecha(string texto)
        {
            // Verificar si contiene "/" y si podría ser una fecha
            if (!texto.Contains("/")) return false;

            var partes = texto.Split('/');
            if (partes.Length != 3) return false;

            // Intentar parsear como números
            return int.TryParse(partes[0], out _) && 
                   int.TryParse(partes[1], out _) && 
                   int.TryParse(partes[2], out _);
        }

        private DataTable FiltrarDataTablePorFecha(DataTable dt, DateTime fecha)
        {
            // Crear una copia del DataTable para filtrar
            var dtFiltrada = dt.Clone();
            var inicioDelDia = fecha.Date;
            var finDelDia = inicioDelDia.AddDays(1).AddTicks(-1);

            foreach (DataRow row in dt.Rows)
            {
                if (row["Fecha"] is DateTime fechaFactura)
                {
                    if (fechaFactura >= inicioDelDia && fechaFactura <= finDelDia)
                    {
                        dtFiltrada.ImportRow(row);
                    }
                }
            }

            return dtFiltrada;
        }

        private void DtpFechaFiltro_ValueChanged(object sender, EventArgs e)
        {
            BuscarFacturaPorCodigo();
        }

        private void btnVerComprob_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscarFact.Text.Trim();

            // Validar 1: Si no hay búsqueda (se cargaron todas las facturas)
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                showMessage("No puede ver el comprobante, necesita buscar la factura que desea ver.", "Aviso", false);
                return;
            }

            // Validar 2: Verificar que el usuario haya seleccionado una fila explícitamente
            if (dataGridFacturasEmitidas.SelectedRows.Count == 0)
            {
                showMessage("Seleccione una factura en el DataGrid para ver el comprobante.", "Aviso", false);
                return;
            }

            // Obtener la fila seleccionada
            DataGridViewRow filaSeleccionada = dataGridFacturasEmitidas.SelectedRows[0];
            string facturaCodigo = filaSeleccionada.Cells["Numero"].Value?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(facturaCodigo))
            {
                showMessage("No se pudo obtener el número de factura. Intente de nuevo.", "Error", true);
                return;
            }

            // 👉 Abre el comprobante con ese código
            using (Comprobante frmComprobante = new Comprobante(facturaCodigo))
            {
                frmComprobante.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnularFacturaSeleccionada();
        }

        private void FacturasEmitidas_Load(object sender, EventArgs e)
        {
            CargarTodasLasFacturas(); // 👉 al abrir, carga todas las facturas con detalles

            // Agregar placeholder al TextBox de búsqueda
            if (txtBuscarFact != null)
            {
                // En .NET Framework/WinForms, usamos un evento GotFocus/LostFocus para simular placeholder
                // O si el textbox soporta PlaceholderText directamente (Windows Forms moderno)
                try
                {
                    // Intentar establecer PlaceholderText (disponible en .NET 5+)
                    var propiedadPlaceholder = txtBuscarFact.GetType().GetProperty("PlaceholderText");
                    if (propiedadPlaceholder != null)
                    {
                        propiedadPlaceholder.SetValue(txtBuscarFact, "ej: FACT-000 o d/m/a");
                    }
                }
                catch
                {
                    // Si no está disponible, se usa el método alternativo con eventos
                    // (que puede extenderse si es necesario)
                }
            }

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

