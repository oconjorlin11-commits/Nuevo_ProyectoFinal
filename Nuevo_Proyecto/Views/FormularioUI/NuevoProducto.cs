using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;
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
    public partial class NuevoProducto : Form, IProductoView
    {
        private readonly ProductosPresenter _presenter;

        public NuevoProducto()
        {
            InitializeComponent();
            _presenter = new ProductosPresenter(this);
        }

        // IProductoView properties
        public string Codigo { get => txtCodigoProduc.Text; set => txtCodigoProduc.Text = value; }
        public string Nombre { get => txtNombreProduct.Text; set => txtNombreProduct.Text = value; }
        public int CategoriaId { get => cmboxCategoriaProduc.SelectedValue == null ? 0 : Convert.ToInt32(cmboxCategoriaProduc.SelectedValue); set => cmboxCategoriaProduc.SelectedValue = value; }
        public int UnidadId { get => cmboxUnidadProduct.SelectedValue == null ? 0 : Convert.ToInt32(cmboxUnidadProduct.SelectedValue); set => cmboxUnidadProduct.SelectedValue = value; }
        public string Descripcion { get => txtDescripcion.Text; set => txtDescripcion.Text = value; }
        public decimal PrecioVenta { get => decimal.TryParse(txtPrecioProduct.Text, out var p) ? p : 0; set => txtPrecioProduct.Text = value.ToString(); }
        public bool Activo { get => checkBoxProductosActi.Checked; set => checkBoxProductosActi.Checked = value; }
        public int stockInicial { get => int.TryParse(txtStockInicial.Text, out var s) ? s : 0; set => txtStockInicial.Text = value.ToString(); }
        public int StockMinimo { get => int.TryParse(txtStockMinimo.Text, out var m) ? m : 0; set => txtStockMinimo.Text = value.ToString(); }

        public event EventHandler? GuardarClicked;
        public event EventHandler? CancelarClicked;

        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        public void ResetFields()
        {
            Codigo = string.Empty;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            PrecioVenta = 0;
            stockInicial = 0;
            StockMinimo = 0;
            Activo = true;
        }

        private void CargarUsuarios()
        {
            var dt = _presenter.CargarUsuariosAdmin();
            CmboxEmpleado.DataSource = dt;
            CmboxEmpleado.DisplayMember = "Nombre";
            CmboxEmpleado.ValueMember = "EmpleadoID";
        }


        private bool ValidarCampos()
        {

            if (string.IsNullOrWhiteSpace(txtCodigoProduc.Text))
            {
                MessageBox.Show("Debe ingresar un Codigo  al producto.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombreProduct.Text))
            {
                MessageBox.Show("Debe ingresar un nombre de producto.");
                return false;
            }

            if (!decimal.TryParse(txtPrecioProduct.Text, out decimal precioVenta) || precioVenta <= 0)
            {
                MessageBox.Show("Debe ingresar un precio de venta válido.");
                return false;
            }

            if (!int.TryParse(txtStockInicial.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Debe ingresar un stock inicial válido.");
                return false;
            }

            if (!int.TryParse(txtStockMinimo.Text, out int minimo) || minimo < 0)
            {
                MessageBox.Show("Debe ingresar un stock mínimo válido.");
                return false;
            }

            if (cmboxCategoriaProduc.SelectedValue == null || (int)cmboxCategoriaProduc.SelectedValue < 0)
            {
                MessageBox.Show("Debe seleccionar una categoría.");
                return false;
            }

            if (cmboxUnidadProduct.SelectedValue == null || (int)cmboxUnidadProduct.SelectedValue < 0)
            {
                MessageBox.Show("Debe seleccionar una unidad.");
                return false;
            }

            // 👉 Validar que el producto esté marcado como activo
            if (!checkBoxProductosActi.Checked)
            {
                MessageBox.Show("El producto debe estar marcado como ACTIVO para poder guardarse.");
                return false;
            }

            return true;
        }

        private void btnCancelarProduct_Click(object sender, EventArgs e)
        {
            // Avisar al presenter que se canceló (si hay suscriptores)
            CancelarClicked?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void NuevoProducto_Load(object sender, EventArgs e)
        {
            // Código automático
            txtCodigoProduc.Text = _presenter.ObtenerProximoCodigoProducto();
            txtCodigoProduc.Enabled = false;

            // 👉 Solo admins - Agregar placeholder
            CmboxEmpleado.DataSource = _presenter.CargarUsuariosAdmin();
            CmboxEmpleado.DisplayMember = "Nombre";
            CmboxEmpleado.ValueMember = "EmpleadoID";
            // Agregar item placeholder al inicio
            DataTable dtEmpleados = _presenter.CargarUsuariosAdmin();
            DataRow placeholderEmpl = dtEmpleados.NewRow();
            placeholderEmpl["Nombre"] = "Seleccione un administrador";
            placeholderEmpl["EmpleadoID"] = -1;  // ID inválido para identificar placeholder
            dtEmpleados.Rows.InsertAt(placeholderEmpl, 0);
            CmboxEmpleado.DataSource = dtEmpleados;
            CmboxEmpleado.SelectedIndex = 0;

            // Categorías - Agregar placeholder
            DataTable dtCategorias = _presenter.GetCategoriasActivas();
            DataRow placeholderCat = dtCategorias.NewRow();
            placeholderCat["Nombre"] = "Seleccione una categoría";
            placeholderCat["CategoriaID"] = -1;
            dtCategorias.Rows.InsertAt(placeholderCat, 0);
            cmboxCategoriaProduc.DataSource = dtCategorias;
            cmboxCategoriaProduc.DisplayMember = "Nombre";
            cmboxCategoriaProduc.ValueMember = "CategoriaID";
            cmboxCategoriaProduc.SelectedIndex = 0;

            // Unidades - Agregar placeholder
            DataTable dtUnidades = _presenter.GetUnidades();
            DataRow placeholderUni = dtUnidades.NewRow();
            placeholderUni["Nombre"] = "Seleccione una unidad de medida";
            placeholderUni["UnidadID"] = -1;
            dtUnidades.Rows.InsertAt(placeholderUni, 0);
            cmboxUnidadProduct.DataSource = dtUnidades;
            cmboxUnidadProduct.DisplayMember = "Nombre";
            cmboxUnidadProduct.ValueMember = "UnidadID";
            cmboxUnidadProduct.SelectedIndex = 0;
        }

        private void btnGuardarProduc_Click(object sender, EventArgs e)
        {

            if (!ValidarCampos())
                return;

            // 👉 Validar que el usuario seleccionado sea admin
            DataRowView? usuarioSeleccionado = CmboxEmpleado.SelectedItem as DataRowView;
            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un usuario autorizado.");
                return;
            }

            string cargo = usuarioSeleccionado["Cargo"]?.ToString() ?? string.Empty;
            int empleadoID = Convert.ToInt32(usuarioSeleccionado["EmpleadoID"]);

            if (!cargo.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Solo un usuario con cargo ADMIN puede guardar productos.");
                return;
            }

            // Delegar al presenter (el presenter verifica permisos y persiste)
            GuardarClicked?.Invoke(this, EventArgs.Empty);


        }
    }
}
