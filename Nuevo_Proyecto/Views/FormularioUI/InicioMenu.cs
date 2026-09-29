using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class InicioMenu : Form
    {
        private readonly Nuevo_Proyecto.Presenters.InventarioPresenter _inventarioPresenter;
        private readonly Nuevo_Proyecto.Presenters.FacturacionPresenter _facturacionPresenter;

        public InicioMenu()
        {
            InitializeComponent();
            // Instanciar presenters que proveen datos al tablero
            _inventarioPresenter = new Nuevo_Proyecto.Presenters.InventarioPresenter();
            // FacturacionPresenter acepta view opcional; no pasar 'this' porque InicioMenu no implementa IFacturacionView
            _facturacionPresenter = new Nuevo_Proyecto.Presenters.FacturacionPresenter();
        }

        // Refresca todo el tablero del menu

        public void RefrescarMenu()
        {
            // Configurar vistas
            ConfigurarDataGriProductosBajos();
            ConfigurarDataGriUltimasFacturas();

            // Cargar productos con stock bajo
            try
            {
                var inventario = _inventarioPresenter.ObtenerInventarioActivo();
                // Filtrar filas donde Stock <= StockMinimo
                var bajos = inventario.AsEnumerable()
                    .Where(r => r.Field<int>("Stock") <= r.Field<int>("StockMinimo"))
                    .CopyToDataTable();
                dataGriProductosBajos.DataSource = bajos;
            }
            catch
            {
                // Si no hay filas que cumplan la condición, poner vacío
                dataGriProductosBajos.DataSource = new DataTable();
            }

            // Cargar métricas: ventas hoy, ventas semanales, facturas semanales, valor inventario
            try
            {
                // Usar cultura de Nicaragua para mostrar C$ (Córdoba) si está disponible
                var ci = new CultureInfo("es-NI");

                DateTime today = DateTime.Today;
                DateTime todayEnd = today.AddDays(1).AddTicks(-1);

                var dtHoy = _facturacionPresenter.FiltrarFacturasPorFecha(today, todayEnd);
                decimal ventasHoy = 0m;
                if (dtHoy != null && dtHoy.Rows.Count > 0)
                {
                    ventasHoy = dtHoy.AsEnumerable().Sum(r => r.Field<decimal?>("Total") ?? 0m);
                }
                LblVentasHoy.Text = ventasHoy.ToString("C2", ci);

                DateTime weekStart = today.AddDays(-6);
                DateTime weekEnd = todayEnd;
                var dtSemana = _facturacionPresenter.FiltrarFacturasPorFecha(weekStart, weekEnd);
                decimal ventasSemana = 0m;
                int facturasSemana = 0;
                if (dtSemana != null && dtSemana.Rows.Count > 0)
                {
                    ventasSemana = dtSemana.AsEnumerable().Sum(r => r.Field<decimal?>("Total") ?? 0m);
                    facturasSemana = dtSemana.Rows.Count;
                }
                VentasSemanales.Text = ventasSemana.ToString("C2", ci);
                LblFacturasSemanales.Text = facturasSemana.ToString();

                // Valor inventario: sumar PrecioVenta * Stock sobre inventario activo
                decimal valorInventario = 0m;
                var dtInv = _inventarioPresenter.ObtenerInventarioActivo();
                if (dtInv != null && dtInv.Rows.Count > 0)
                {
                    foreach (DataRow r in dtInv.Rows)
                    {
                        decimal precio = 0m;
                        int stock = 0;
                        try { precio = r["PrecioVenta"] != DBNull.Value ? Convert.ToDecimal(r["PrecioVenta"]) : 0m; } catch { precio = 0m; }
                        try { stock = r["Stock"] != DBNull.Value ? Convert.ToInt32(r["Stock"]) : 0; } catch { stock = 0; }
                        valorInventario += precio * stock;
                    }
                }
                LblValorInventario.Text = valorInventario.ToString("C2", ci);
            }
            catch
            {
                var ci = new CultureInfo("es-NI");
                LblVentasHoy.Text = (0m).ToString("C2", ci);
                VentasSemanales.Text = (0m).ToString("C2", ci);
                LblFacturasSemanales.Text = "0";
                LblValorInventario.Text = (0m).ToString("C2", ci);
            }

            // Cargar últimas facturas (ordenar por Fecha desc y tomar 10)
            try
            {
                var todas = _facturacionPresenter.GetTodasLasFacturasConDetalles();
                var view = todas.AsEnumerable()
                    .OrderByDescending(r => r.Field<DateTime?>("Fecha"))
                    .Take(10)
                    .CopyToDataTable();
                dataGriUltimasFacturas.DataSource = view;
            }
            catch
            {
                dataGriUltimasFacturas.DataSource = new DataTable();
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void InicioMenu_Load(object sender, EventArgs e)
        {
            // Al cargar el formulario refrescar el tablero
            RefrescarMenu();
        }

        private void ConfigurarDataGriProductosBajos()
        {
            dataGriProductosBajos.AutoGenerateColumns = false;
            dataGriProductosBajos.Columns.Clear();

            dataGriProductosBajos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Codigo",
                HeaderText = "Código",
                DataPropertyName = "Codigo"
            });

            dataGriProductosBajos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Producto",
                DataPropertyName = "Nombre"
            });

            dataGriProductosBajos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock Actual",
                DataPropertyName = "Stock"
            });

            dataGriProductosBajos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockMinimo",
                HeaderText = "Stock Mínimo",
                DataPropertyName = "StockMinimo"
            });

            dataGriProductosBajos.ReadOnly = true;
            dataGriProductosBajos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGriProductosBajos.MultiSelect = false;

        }

        private void ConfigurarDataGriUltimasFacturas()
        {
            dataGriUltimasFacturas.AutoGenerateColumns = false;
            dataGriUltimasFacturas.Columns.Clear();

            dataGriUltimasFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Numero",
                HeaderText = "Código Factura",
                DataPropertyName = "Numero"
            });

            dataGriUltimasFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha",
                DataPropertyName = "Fecha"
            });

            dataGriUltimasFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                DefaultCellStyle = { Format = "C2" } // formato moneda
            });

            dataGriUltimasFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Empleado",
                HeaderText = "Empleado",
                DataPropertyName = "Empleado"
            });

            dataGriUltimasFacturas.ReadOnly = true;
            dataGriUltimasFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGriUltimasFacturas.MultiSelect = false;
        }
    }
}
