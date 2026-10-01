using Microsoft.Data.SqlClient;
using Nuevo_Proyecto.Catalogos;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;
using System;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Nuevo_Proyecto.Models.Views
{
    public partial class Facturacion : Form, Nuevo_Proyecto.Views.Interfaces.IFacturacionView
    {
        private readonly FacturacionPresenter _presenter;

        public Facturacion()
        {
            InitializeComponent();
            _presenter = new FacturacionPresenter(this);
        }

        private void CalcularTotales()
        {
            decimal subtotal = 0;
            foreach (DataGridViewRow row in dataGridDetallesFacturas.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                    subtotal += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }

            // Guardar en el TextBox como número plano
            txtTotal.Text = subtotal.ToString("F2"); // Ej: 80.00
        }

        private void Detalle_Factura_Load(object sender, EventArgs e)
        {


            // Cargar datos mediante el presenter
            cmboxCategorias.DataSource = _presenter.ObtenerCategorias();
            cmboxCategorias.DisplayMember = "Nombre";
            cmboxCategorias.ValueMember = "CategoriaID";
            cmboxCategorias.SelectedIndex = -1;

            cmboxProductos.DataSource = null;

            // Asegurar que el combo de categorías cargue los productos al cambiar
            cmboxCategorias.SelectedIndexChanged += (s, ev) =>
            {
                if (cmboxCategorias.SelectedValue != null && int.TryParse(cmboxCategorias.SelectedValue.ToString(), out int catId))
                {
                    var productos = _presenter.ObtenerProductosPorCategoria(catId);
                    cmboxProductos.DataSource = productos;
                    cmboxProductos.DisplayMember = "Nombre";
                    cmboxProductos.ValueMember = "ProductoID";
                    cmboxProductos.SelectedIndex = -1;
                }
                else
                {
                    cmboxProductos.DataSource = null;
                }
            };

            cmboxAtendidoPor.DataSource = _presenter.ObtenerEmpleados();
            cmboxAtendidoPor.DisplayMember = "Nombre";
            cmboxAtendidoPor.ValueMember = "EmpleadoID";
            cmboxAtendidoPor.SelectedIndex = -1;

            cmboxClientes.DataSource = _presenter.ObtenerClientes();
            cmboxClientes.DisplayMember = "Nombre";
            cmboxClientes.ValueMember = "ClienteID";
            cmboxClientes.SelectedIndex = -1;

            comboBox2.DataSource = _presenter.ObtenerFormasPago();
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "FormaPagoID";
            comboBox2.SelectedIndex = -1;


            // Configuración del DataGridView
            dataGridDetallesFacturas.Columns.Clear(); // limpiar columnas previas

            // Primera columna: Código Factura
            dataGridDetallesFacturas.Columns.Add("CodigoFactura", "Código Factura");

            dataGridDetallesFacturas.Columns.Add("ProductoID", "ID Producto");
            dataGridDetallesFacturas.Columns.Add("NombreProducto", "Producto");
            dataGridDetallesFacturas.Columns.Add("Cantidad", "Cantidad");
            dataGridDetallesFacturas.Columns.Add("PrecioUnitario", "Precio Unitario");
            dataGridDetallesFacturas.Columns.Add("Subtotal", "Subtotal");
            dataGridDetallesFacturas.Columns.Add("Estado", "Estado");

            dataGridDetallesFacturas.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
            dataGridDetallesFacturas.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
        }




        private void button1_Click(object sender, EventArgs e)
        {

            // Crear instancia del formulario Facturacion
            NuevoCliente frm = new NuevoCliente();

            // Mostrar el formulario embebido
            frm.Show();
        }

        private void btnGuardarfact_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que exista al menos una línea de detalle
                bool hayLineas = dataGridDetallesFacturas.Rows.Cast<DataGridViewRow>().Any(r => !r.IsNewRow);
                if (!hayLineas)
                {
                    MessageBox.Show("No has agregado productos. No puedes guardar la factura. Primero agrega al menos un producto.");
                    return;
                }
                string codigoFactura = _presenter.GenerarCodigoFacturaSiguiente();
                var clienteId = string.IsNullOrEmpty(cmboxClientes.SelectedValue?.ToString()) ? (int?)null : Convert.ToInt32(cmboxClientes.SelectedValue);
                var empleadoId = cmboxAtendidoPor.SelectedValue == null ? 0 : Convert.ToInt32(cmboxAtendidoPor.SelectedValue);
                var formaPagoId = comboBox2.SelectedValue == null ? 0 : Convert.ToInt32(comboBox2.SelectedValue);

                int nuevaFacturaId = _presenter.InsertarFacturaConDetalle(
                    clienteId,
                    empleadoId,
                    formaPagoId,
                    string.IsNullOrEmpty(txtObseravciones.Text) ? null : txtObseravciones.Text,
                    Convert.ToDecimal(txtTotal.Text),
                    Convert.ToDecimal(txtTotal.Text),
                    dataGridDetallesFacturas,
                    codigoFactura
                );

                MessageBox.Show($"Factura guardada correctamente con código: {codigoFactura}");
                dataGridDetallesFacturas.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la factura: " + ex.Message);
            }

        }



        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Crear instancia del formulario Facturacion
            FacturasEmitidas frm = new FacturasEmitidas();

            // Mostrar el formulario embebido
            frm.Show();

        }

        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        public void ResetFields()
        {
            dataGridDetallesFacturas.Rows.Clear();
        }

        private void btnQuitarLinea_Click(object sender, EventArgs e)
        {
            // Quitar la(s) filas seleccionadas del detalle
            try
            {
                if (dataGridDetallesFacturas.SelectedRows != null && dataGridDetallesFacturas.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow r in dataGridDetallesFacturas.SelectedRows)
                    {
                        if (!r.IsNewRow) dataGridDetallesFacturas.Rows.Remove(r);
                    }
                    CalcularTotales();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar la línea que desea quitar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar línea: " + ex.Message);
            }
        }

        private void btnLimpiarAll_Click(object sender, EventArgs e)
        {
            dataGridDetallesFacturas.Rows.Clear();

            cmboxAtendidoPor.SelectedIndex = -1;
            cmboxAtendidoPor.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            txtObseravciones.Clear();
          

            txtTotal.Text = "0.00";

        }

        private void btnAgregarProduc_Click(object sender, EventArgs e)
        {
            // Validaciones obligatorias
            if (cmboxAtendidoPor.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un empleado.");
                return;
            }
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar la forma de pago.");
                return;
            }
            if (cmboxProductos.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un producto.");
                return;
            }
            if (numericUpCantidad.Value <= 0)
            {
                MessageBox.Show("Debe ingresar una cantidad mayor a 0.");
                return;
            }
            

        
            // Obtener precio
            int productoID = Convert.ToInt32(cmboxProductos.SelectedValue);
            string nombreProducto = cmboxProductos.Text;
            decimal precioUnitario = _presenter.ObtenerPrecioProducto(productoID);
            int cantidad = (int)numericUpCantidad.Value;
            decimal subtotal = cantidad * precioUnitario;

            // Código actual de factura (se mantiene mientras agregás productos)
            string codigoFactura = _presenter.ObtenerCodigoFacturaActual();

            // Insertar en el DataGridView (incluye Estado)
            dataGridDetallesFacturas.Rows.Add(codigoFactura, productoID, nombreProducto, cantidad, precioUnitario, subtotal, "Normal");

            CalcularTotales();

            // Limpiar selección de producto y cantidad
            cmboxProductos.SelectedIndex = -1;
            numericUpCantidad.Value = 0;
        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
