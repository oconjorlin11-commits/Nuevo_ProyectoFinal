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
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Services;

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
            IntentarSincronizarSQLiteSiHayDatosEnSQL();

            ConfigurarDataGriProductosBajos();
            ConfigurarDataGriUltimasFacturas();

            var ci = new CultureInfo("es-NI");   // C$ (córdoba)

            // Productos con stock bajo (el cálculo vive en el presenter/repositorio)
            try
            {
                dataGriProductosBajos.DataSource = _inventarioPresenter.GetProductosStockBajo();
            }
            catch (Exception ex)
            {
                dataGriProductosBajos.DataSource = new DataTable();
                MostrarErrorTablero("productos con stock bajo", ex);
            }

            // Métricas: ventas de hoy, ventas de la semana, facturas de la semana, valor del inventario.
            // Las facturas anuladas NO cuentan como venta.
            DateTime hoy = DateTime.Today;
            try
            {
                CargarResumenTablero(ci, hoy);
            }
            catch (Exception ex)
            {
                // Si SQL cayó o hubo un fallo temporal, no dejamos valores en 0.
                // Prioridad: usar SQLite, y si no hay snapshot, mostrar "Sin datos".
                if (TryCargarResumenOffline(ci, hoy))
                {
                    CargarUltimasFacturasTablero();
                    return;
                }

                LblVentasHoy.Text = "Sin datos";
                VentasSemanales.Text = "Sin datos";
                LblFacturasSemanales.Text = "0";
                LblValorInventario.Text = "Sin datos";

                MostrarErrorTablero("métricas", ex);
            }

            CargarUltimasFacturasTablero();
        }

        private void CargarResumenTablero(CultureInfo ci, DateTime hoy)
        {
            var ventasHoy = _facturacionPresenter.ObtenerResumenVentas(hoy, hoy);
            LblVentasHoy.Text = ventasHoy.Total.ToString("C2", ci);

            var ventasSemana = _facturacionPresenter.ObtenerResumenVentas(hoy.AddDays(-6), hoy);
            VentasSemanales.Text = ventasSemana.Total.ToString("C2", ci);
            LblFacturasSemanales.Text = ventasSemana.CantidadFacturas.ToString();

            LblValorInventario.Text = _inventarioPresenter.GetValorInventarioActivo().ToString("C2", ci);
        }

        private void IntentarSincronizarSQLiteSiHayDatosEnSQL()
        {
            try
            {
                if (ConnectionStateManager.UsandoSqlite)
                    return;

                if (!ConnectionStateManager.VerificarConexionAlServidor())
                    return;

                using var sqlite = new ComideriaSqliteContextFactory().CreateDbContext();
                if (sqlite.Facturas.Any())
                    return;

                var sincronizacion = new SincronizacionCompletaService();
                sincronizacion.SincronizarCompleto().GetAwaiter().GetResult();
            }
            catch
            {
                // Si no hay datos ni conexión, el tablero seguirá usando lo que ya esté en SQLite.
            }
        }

        private void CargarUltimasFacturasTablero()
        {
            try
            {
                var facturas = _facturacionPresenter.GetTodasLasFacturasConDetalles();
                var ultimas = facturas.AsEnumerable()
                    .Where(r => r.Field<DateTime?>("Fecha") != null)
                    .OrderByDescending(r => r.Field<DateTime>("Fecha"))
                    .Take(10)
                    .ToList();

                dataGriUltimasFacturas.DataSource = ultimas.Count > 0
                    ? ultimas.CopyToDataTable()
                    : new DataTable();
            }
            catch (Exception ex)
            {
                if (ConnectionStateManager.UsandoSqlite || !ConnectionStateManager.VerificarConexionAlServidor())
                {
                    try
                    {
                        var facturas = _facturacionPresenter.GetTodasLasFacturasConDetalles();
                        var ultimas = facturas.AsEnumerable()
                            .Where(r => r.Field<DateTime?>("Fecha") != null)
                            .OrderByDescending(r => r.Field<DateTime>("Fecha"))
                            .Take(10)
                            .ToList();

                        dataGriUltimasFacturas.DataSource = ultimas.Count > 0
                            ? ultimas.CopyToDataTable()
                            : new DataTable();
                        return;
                    }
                    catch
                    {
                        // El fallback de SQLite ya fue intentado y siguió fallando.
                    }
                }

                dataGriUltimasFacturas.DataSource = new DataTable();
                MostrarErrorTablero("últimas facturas", ex);
            }
        }

        private bool TryCargarResumenOffline(CultureInfo ci, DateTime hoy)
        {
            try
            {
                var sqlCaido = !ConnectionStateManager.VerificarConexionAlServidor();
                if (!ConnectionStateManager.UsandoSqlite && !sqlCaido)
                {
                    return false;
                }

                var snapshotHoy = _facturacionPresenter.ObtenerResumenVentas(hoy, hoy);
                var snapshotSemana = _facturacionPresenter.ObtenerResumenVentas(hoy.AddDays(-6), hoy);
                var valorInventario = _inventarioPresenter.GetValorInventarioActivo();

                LblVentasHoy.Text = snapshotHoy.Total.ToString("C2", ci);
                VentasSemanales.Text = snapshotSemana.Total.ToString("C2", ci);
                LblFacturasSemanales.Text = snapshotSemana.CantidadFacturas.ToString();
                LblValorInventario.Text = valorInventario.ToString("C2", ci);
                return true;
            }
            catch
            {
                LblVentasHoy.Text = "Sin datos";
                VentasSemanales.Text = "Sin datos";
                LblFacturasSemanales.Text = "0";
                LblValorInventario.Text = "Sin datos";
                return false;
            }
        }

        // Antes los errores del tablero se tragaban en silencio (catch vacío) y las métricas salían en 0 sin avisar.
        private bool _errorTableroMostrado;
        private void MostrarErrorTablero(string seccion, Exception ex)
        {
            if (_errorTableroMostrado) return;
            _errorTableroMostrado = true;
            MessageBox.Show($"No se pudo cargar {seccion}: {ex.Message}", "Tablero",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void groupBox6_Enter(object? sender, EventArgs e)
        {

        }

        private void InicioMenu_Load(object? sender, EventArgs e)
        {
            // No bloquear la apertura del menú principal con consultas pesadas del tablero.
            // El refresco se programa al final del hilo de UI para que la app siga respondiendo.
            BeginInvoke(new Action(RefrescarMenu));
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
