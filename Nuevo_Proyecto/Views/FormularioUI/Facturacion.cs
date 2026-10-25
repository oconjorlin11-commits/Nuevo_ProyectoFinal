using Microsoft.Data.SqlClient;
using Nuevo_Proyecto.Catalogos;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
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
        private bool _datosModificados = false;

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
            ComboBoxBindingHelper.Bind(cmboxCategorias, categorias, "Nombre", "CategoriaID");
            cmboxCategorias.SelectedIndex = -1;
        }

        public void LoadProductosPorCategoria(DataTable productos)
        {
            ComboBoxBindingHelper.Bind(cmboxProductos, productos, "Nombre", "ProductoID");
            cmboxProductos.SelectedIndex = -1;
        }

        public void LoadEmpleados(DataTable empleados)
        {
            ComboBoxBindingHelper.Bind(cmboxAtendidoPor, empleados, "Nombre", "EmpleadoID");
            cmboxAtendidoPor.SelectedIndex = -1;

            // Preseleccionar automáticamente el usuario que inició sesión
            if (SesionActual.EmpleadoId > 0)
            {
                cmboxAtendidoPor.SelectedValue = SesionActual.EmpleadoId;
            }
        }

        public void LoadClientes(DataTable clientes)
        {
            ComboBoxBindingHelper.Bind(cmboxClientes, clientes, "Nombre", "ClienteID");
            cmboxClientes.SelectedIndex = -1;
        }

        public void LoadFormasPago(DataTable formasPago)
        {
            ComboBoxBindingHelper.Bind(comboBox2, formasPago, "Nombre", "FormaPagoID");
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

        private void Detalle_Factura_Load(object? sender, EventArgs e)
        {
            try
            {
                // Cargar datos mediante el presenter con fallback automático a SQLite si SQL cae.
                _presenter.CargarCatalogosBase(this);
                cmboxCategorias.SelectedIndexChanged += CmboxCategorias_SelectedIndexChanged;

                dataGridDetallesFacturas.Columns.Clear();
                dataGridDetallesFacturas.Columns.Add("CodigoFactura", "Código Factura");
                dataGridDetallesFacturas.Columns.Add("ProductoID", "ID Producto");
                dataGridDetallesFacturas.Columns.Add("NombreProducto", "Producto");
                dataGridDetallesFacturas.Columns.Add("Cantidad", "Cantidad");
                dataGridDetallesFacturas.Columns.Add("PrecioUnitario", "Precio Unitario");
                dataGridDetallesFacturas.Columns.Add("Subtotal", "Subtotal");
                dataGridDetallesFacturas.Columns.Add("Estado", "Estado");

                dataGridDetallesFacturas.Columns["PrecioUnitario"]!.DefaultCellStyle.Format = "C2";
                dataGridDetallesFacturas.Columns["Subtotal"]!.DefaultCellStyle.Format = "C2";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando Facturación: {ex.Message}");
                ConnectionStateManager.VerificarConexionAlServidor();
                MessageBox.Show(
                    "Se detectó una caída del servidor SQL Server. La facturación continuará en modo offline temporal.",
                    "Modo Offline",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void BtnNuevoCliente_Click(object? sender, EventArgs e)
        {
            // Crear instancia del formulario NuevoCliente
            NuevoCliente frm = new NuevoCliente();
            frm.Show();
        }

        // Método esperado por el Designer.cs para btnNuevoCliente
        private void button1_Click(object? sender, EventArgs e)
        {
            BtnNuevoCliente_Click(sender, e);
        }

        private void btnGuardarfact_Click(object? sender, EventArgs e)
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
                var lineas = dataGridDetallesFacturas.Rows.Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .Select(r => new Nuevo_Proyecto.Models.DTOs.LineaFacturaDto
                    {
                        ProductoId = Convert.ToInt32(r.Cells["ProductoID"].Value),
                        Cantidad = Convert.ToInt32(r.Cells["Cantidad"].Value)
                    })
                    .ToList();

                var resultado = _presenter.GuardarFacturaDesdeVista(empleadoId, formaPagoId, clienteId, Observacion, lineas);

                MessageBox.Show($"Factura guardada correctamente con código: {resultado.Numero}");

                // 👉 Preguntar al usuario si desea ver/imprimir el comprobante
                DialogResult respuesta = MessageBox.Show(
                    $"¿Desea ver e imprimir el comprobante de la factura {resultado.Numero} ahora?",
                    "Comprobante",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Abrir el formulario de comprobante directamente con el número de factura
                    using (Comprobante frmComprobante = new Comprobante(resultado.Numero))
                    {
                        frmComprobante.ShowDialog();
                    }
                }

                dataGridDetallesFacturas.Rows.Clear();
                ResetFields();
                _datosModificados = false;
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

        private void btnImprimir_Click(object? sender, EventArgs e)
        {
            // Crear instancia del formulario FacturasEmitidas
            FacturasEmitidas frm = new FacturasEmitidas();
            frm.Show();
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

        private void CmboxCategorias_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                var productos = _presenter.ObtenerProductosPorCategoriaSeleccionada(CategoriaSeleccionada);
                LoadProductosPorCategoria(productos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando productos por categoría: {ex.Message}");
                LoadProductosPorCategoria(new DataTable());
            }
        }

        private void btnQuitarLinea_Click(object? sender, EventArgs e)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar línea: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarAll_Click(object? sender, EventArgs e)
        {
            ResetFields();
        }

        private void btnAgregarProduc_Click(object? sender, EventArgs e)
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

            // Marcar que hay cambios sin guardar
            _datosModificados = true;

            CalcularTotales();

            // Limpiar selección de producto y cantidad
            cmboxProductos.SelectedIndex = -1;
            numericUpCantidad.Value = 0;
        }

        private void panel1_Paint(object? sender, PaintEventArgs e)
        {
        }

        private void groupBox2_Enter(object? sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object? sender, EventArgs e)
        {
        }

        /// <summary>
        /// Evento FormClosing para mostrar advertencia si hay cambios sin guardar
        /// </summary>
        private void Facturacion_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Si hay datos modificados (productos agregados pero no guardados), mostrar advertencia
            if (_datosModificados)
            {
                DialogResult resultado = MessageBox.Show(
                    "Tiene productos agregados pero no guardados. ¿Está seguro de que no desea continuar facturando?",
                    "Confirmar cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                // Si el usuario selecciona "No", cancelar el cierre
                if (resultado == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    // Si selecciona "Sí", limpiar estado y permitir cerrar
                    _datosModificados = false;
                }
            }
        }
    }
}
