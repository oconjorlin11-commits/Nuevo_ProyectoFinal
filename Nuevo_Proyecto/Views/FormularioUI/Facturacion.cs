using Microsoft.Data.SqlClient;
using Nuevo_Proyecto.Catalogos;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Facturacion : Form
    {
        public Facturacion()
        {
            InitializeComponent();
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


            SelectQuery query = new SelectQuery();

            // Categorías
            cmboxCategorias.DataSource = query.ObtenerCategorias();
            cmboxCategorias.DisplayMember = "Nombre";
            cmboxCategorias.ValueMember = "CategoriaID";
            cmboxCategorias.SelectedIndex = -1;

            // Productos (se cargan después de seleccionar categoría)
            cmboxProductos.DataSource = null;

            // Empleados
            cmboxAtendidoPor.DataSource = query.ObtenerEmpleados();
            cmboxAtendidoPor.DisplayMember = "Nombre";
            cmboxAtendidoPor.ValueMember = "EmpleadoID";
            cmboxAtendidoPor.SelectedIndex = -1;

            // Clientes
            cmboxClientes.DataSource = query.ObtenerClientes();
            cmboxClientes.DisplayMember = "Nombre";
            cmboxClientes.ValueMember = "ClienteID";
            cmboxClientes.SelectedIndex = -1;

            // Formas de pago
            comboBox2.DataSource = query.ObtenerFormasPago();
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
                SelectQuery query = new SelectQuery();
                string codigoFactura = query.GenerarCodigoFacturaSiguiente();

                InsertCommand insertCmd = new InsertCommand();

                int nuevaFacturaId = insertCmd.InsertarFacturaConDetalle(
                    string.IsNullOrEmpty(cmboxAtendidoPor.SelectedValue?.ToString()) ? (int?)null : Convert.ToInt32(cmboxClientes.SelectedValue),
                    Convert.ToInt32(cmboxAtendidoPor.SelectedValue),
                    Convert.ToInt32(comboBox2.SelectedValue),
                    string.IsNullOrEmpty(txtObseravciones.Text) ? null : txtObseravciones.Text,
                    Convert.ToDecimal(txtTotal.Text),
                    Convert.ToDecimal(txtTotal.Text),
                    dataGridDetallesFacturas,
                    codigoFactura // se guarda el código amigable
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

        private void btnQuitarLinea_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiarAll_Click(object sender, EventArgs e)
        {
            dataGridDetallesFacturas.Rows.Clear();

            cmboxAtendidoPor.SelectedIndex = -1;
            cmboxAtendidoPor.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            txtObseravciones.Clear();
          

            txtTotal.Text = "$0.00";

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
            SelectQuery query = new SelectQuery();
            int productoID = Convert.ToInt32(cmboxProductos.SelectedValue);
            string nombreProducto = cmboxProductos.Text;
            decimal precioUnitario = query.ObtenerPrecioProducto(productoID);
            int cantidad = (int)numericUpCantidad.Value;
            decimal subtotal = cantidad * precioUnitario;

            // Código actual de factura (se mantiene mientras agregás productos)
            string codigoFactura = query.ObtenerCodigoFacturaActual();

            // Insertar en el DataGridView
            dataGridDetallesFacturas.Rows.Add(codigoFactura, productoID, nombreProducto, cantidad, precioUnitario, subtotal);

            CalcularTotales();
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
