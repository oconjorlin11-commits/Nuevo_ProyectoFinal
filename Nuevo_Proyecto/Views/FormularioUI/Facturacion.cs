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

        // Eventos
        public event EventHandler NuevoClienteClick;
        public event EventHandler AgregarProductoClick;
        public event EventHandler QuitarLineaClick;
        public event EventHandler LimpiarTodoClick;
        public event EventHandler VerImprimirClick;
        public event EventHandler GuardarFacturaClick;

        public Facturacion()
        {
            InitializeComponent();
            _presenter = new FacturacionPresenter(this);
        }

        // Implementación de propiedades
        public int? ClienteSeleccionado
        {
            get => cmboxClientes.SelectedValue == null ? (int?)null : Convert.ToInt32(cmboxClientes.SelectedValue);
        }

        public int? EmpleadoSeleccionado
        {
            get => cmboxAtendidoPor.SelectedValue == null ? (int?)null : Convert.ToInt32(cmboxAtendidoPor.SelectedValue);
        }

        public int? FormaPagoSeleccionado
        {
            get => comboBox2.SelectedValue == null ? (int?)null : Convert.ToInt32(comboBox2.SelectedValue);
        }

        public int? CategoriaSeleccionada
        {
            get => cmboxCategorias.SelectedValue == null ? (int?)null : Convert.ToInt32(cmboxCategorias.SelectedValue);
        }

        public int? ProductoSeleccionado
        {
            get => cmboxProductos.SelectedValue == null ? (int?)null : Convert.ToInt32(cmboxProductos.SelectedValue);
        }

        public int CantidadProducto
        {
            get => (int)numericUpCantidad.Value;
        }

        public string Observacion
        {
            get => txtObseravciones.Text;
        }

        public decimal Total
        {
            get => txtTotal.Text == "" || !decimal.TryParse(txtTotal.Text, out decimal total) ? 0 : total;
            set => txtTotal.Text = value.ToString("F2");
        }

        // Implementación de métodos de la interfaz
        public void LoadCategorias(DataTable categorias)
        {
            cmboxCategorias.DataSource = categorias;
            cmboxCategorias.DisplayMember = "Nombre";
            cmboxCategorias.ValueMember = "CategoriaID";
            cmboxCategorias.SelectedIndex = -1;
        }

        public void LoadProductosPorCategoria(DataTable productos)
        {
            cmboxProductos.DataSource = productos;
            cmboxProductos.DisplayMember = "Nombre";
            cmboxProductos.ValueMember = "ProductoID";
            cmboxProductos.SelectedIndex = -1;
        }

        public void LoadEmpleados(DataTable empleados)
        {
            cmboxAtendidoPor.DataSource = empleados;
            cmboxAtendidoPor.DisplayMember = "Nombre";
            cmboxAtendidoPor.ValueMember = "EmpleadoID";
            cmboxAtendidoPor.SelectedIndex = -1;
        }

        public void LoadClientes(DataTable clientes)
        {
            cmboxClientes.DataSource = clientes;
            cmboxClientes.DisplayMember = "Nombre";
            cmboxClientes.ValueMember = "ClienteID";
            cmboxClientes.SelectedIndex = -1;
        }

        public void LoadFormasPago(DataTable formasPago)
        {
            comboBox2.DataSource = formasPago;
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "FormaPagoID";
            comboBox2.SelectedIndex = -1;
        }

        public void AgregarLineaDetalle(int productoId, string nombreProducto, int cantidad, decimal precioUnitario, decimal subtotal)
        {
            string codigoFactura = _presenter.ObtenerCodigoFacturaActual();
            dataGridDetallesFacturas.Rows.Add(codigoFactura, productoId, nombreProducto, cantidad, precioUnitario, subtotal, "Normal");
        }

        public void LimpiarDetalles()
        {
            dataGridDetallesFacturas.Rows.Clear();
        }

        public int ObtenerFilasDetalles()
        {
            return dataGridDetallesFacturas.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);
        }

        private void CalcularTotales()
        {
            decimal subtotal = 0;
            foreach (DataGridViewRow row in dataGridDetallesFacturas.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                    subtotal += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }

            txtTotal.Text = subtotal.ToString("F2");
        }

        private void Detalle_Factura_Load(object sender, EventArgs e)
        {
            // Cargar datos mediante el presenter
            LoadCategorias(_presenter.ObtenerCategorias());

            // Asegurar que el combo de categorías cargue los productos al cambiar
            // Regla de negocio: solo cargar productos si hay categoría seleccionada
            cmboxCategorias.SelectedIndexChanged += (s, ev) =>
            {
                int? categoriaSeleccionada = cmboxCategorias.SelectedValue == null ? (int?)null : Convert.ToInt32(cmboxCategorias.SelectedValue);
                var productos = _presenter.ObtenerProductosPorCategoriaSeguro(categoriaSeleccionada);
                LoadProductosPorCategoria(productos);
            };

            LoadEmpleados(_presenter.ObtenerEmpleados());
            LoadClientes(_presenter.ObtenerClientes());
            LoadFormasPago(_presenter.ObtenerFormasPago());

            // Configuración del DataGridView
            dataGridDetallesFacturas.Columns.Clear();
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

        private void BtnNuevoCliente_Click(object sender, EventArgs e)
        {
            // Crear instancia del formulario NuevoCliente
            NuevoCliente frm = new NuevoCliente();
            frm.Show();

            // Raizar evento si está suscrito
            NuevoClienteClick?.Invoke(this, EventArgs.Empty);
        }

        // Método esperado por el Designer.cs para btnNuevoCliente
        private void button1_Click(object sender, EventArgs e)
        {
            BtnNuevoCliente_Click(sender, e);
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

                // Validaciones de campos obligatorios
                if (cmboxAtendidoPor.SelectedValue == null || !int.TryParse(cmboxAtendidoPor.SelectedValue.ToString(), out int empleadoId) || empleadoId <= 0)
                {
                    MessageBox.Show("Debes seleccionar un empleado válido.");
                    return;
                }

                if (comboBox2.SelectedValue == null || !int.TryParse(comboBox2.SelectedValue.ToString(), out int formaPagoId) || formaPagoId <= 0)
                {
                    MessageBox.Show("Debes seleccionar una forma de pago válida.");
                    return;
                }

                var clienteId = ClienteSeleccionado;

                // La vista solo envía producto y cantidad; precios, totales, stock y número los resuelve el repositorio
                var lineas = dataGridDetallesFacturas.Rows.Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .Select(r => new Nuevo_Proyecto.Models.DTOs.LineaFacturaDto
                    {
                        ProductoId = Convert.ToInt32(r.Cells["ProductoID"].Value),
                        Cantidad = Convert.ToInt32(r.Cells["Cantidad"].Value)
                    })
                    .ToList();

                var resultado = _presenter.CrearFactura(new Nuevo_Proyecto.Models.DTOs.NuevaFacturaDto
                {
                    ClienteId = clienteId,
                    EmpleadoId = empleadoId,
                    FormaPagoId = formaPagoId,
                    Observacion = string.IsNullOrWhiteSpace(Observacion) ? null : Observacion.Trim(),
                    Lineas = lineas
                });

                MessageBox.Show($"Factura guardada correctamente con código: {resultado.Numero}");
                dataGridDetallesFacturas.Rows.Clear();
                ResetFields();

                GuardarFacturaClick?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                // Mostrar el error completo con el InnerException para diagnóstico
                string mensaje = "Error al guardar la factura:\n\n" + ex.Message;
                if (ex.InnerException != null)
                    mensaje += "\n\nDetalle interno:\n" + ex.InnerException.Message;
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Crear instancia del formulario FacturasEmitidas
            FacturasEmitidas frm = new FacturasEmitidas();
            frm.Show();

            VerImprimirClick?.Invoke(this, EventArgs.Empty);
        }

        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        public void ResetFields()
        {
            cmboxClientes.SelectedIndex = -1;
            cmboxAtendidoPor.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            cmboxCategorias.SelectedIndex = -1;
            cmboxProductos.DataSource = null;
            numericUpCantidad.Value = 0;
            txtObseravciones.Clear();
            dataGridDetallesFacturas.Rows.Clear();
            txtTotal.Text = "0.00";
        }

        private void btnQuitarLinea_Click(object sender, EventArgs e)
        {
            // Quitar la(s) filas seleccionadas del detalle
            try
            {
                if (dataGridDetallesFacturas.SelectedRows != null && dataGridDetallesFacturas.SelectedRows.Count > 0)
                {
                    // Crear una lista para evitar problemas de modificación durante iteración
                    var filasAQuitar = new List<DataGridViewRow>();
                    foreach (DataGridViewRow r in dataGridDetallesFacturas.SelectedRows)
                    {
                        if (!r.IsNewRow)
                            filasAQuitar.Add(r);
                    }

                    // Ahora eliminar sin conflictos
                    foreach (var fila in filasAQuitar)
                    {
                        dataGridDetallesFacturas.Rows.Remove(fila);
                    }

                    CalcularTotales();
                    MessageBox.Show($"Se eliminaron {filasAQuitar.Count} línea(s) del detalle.");
                }
                else
                {
                    MessageBox.Show("Debe seleccionar la línea que desea quitar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                QuitarLineaClick?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar línea: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarAll_Click(object sender, EventArgs e)
        {
            ResetFields();
            LimpiarTodoClick?.Invoke(this, EventArgs.Empty);
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
                MessageBox.Show("La cantidad debe ser mayor que cero.");
                return;
            }

            int productoID = Convert.ToInt32(cmboxProductos.SelectedValue);
            string nombreProducto = cmboxProductos.Text;
            decimal precioUnitario = _presenter.ObtenerPrecioProducto(productoID);
            int cantidad = (int)numericUpCantidad.Value;
            decimal subtotal = cantidad * precioUnitario;

            // Código actual de factura (se mantiene mientras agregás productos)
            string codigoFactura = _presenter.ObtenerCodigoFacturaActual();

            // Insertar en el DataGridView (incluye Estado)
            AgregarLineaDetalle(productoID, nombreProducto, cantidad, precioUnitario, subtotal);

            CalcularTotales();

            // Limpiar selección de producto y cantidad
            cmboxProductos.SelectedIndex = -1;
            numericUpCantidad.Value = 0;

            AgregarProductoClick?.Invoke(this, EventArgs.Empty);
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
