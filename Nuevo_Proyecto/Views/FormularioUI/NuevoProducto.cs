using Nuevo_Proyecto.Models.Entities;
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
    public partial class NuevoProducto : Form
    {
        public NuevoProducto()
        {
            InitializeComponent();
        }

        private void CargarUsuarios()
        {
            SelectQuery sq = new SelectQuery();
            DataTable dt = sq.CargarUsuariosAdmin(); // SELECT EmpleadoID, Nombre, Rol FROM Empleados
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

            if (cmboxCategoriaProduc.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una categoría.");
                return false;
            }

            if (cmboxUnidadProduct.SelectedValue == null)
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
            this.Close();
        }

        private void NuevoProducto_Load(object sender, EventArgs e)
        {
            SelectQuery sq = new SelectQuery();

            // Código automático

            txtCodigoProduc.Text = sq.ObtenerProximoCodigoProducto();
            txtCodigoProduc.Enabled = false;

            // 👉 Solo admins

            CmboxEmpleado.DataSource = sq.CargarUsuariosAdmin();
            CmboxEmpleado.DisplayMember = "Nombre";
            CmboxEmpleado.ValueMember = "EmpleadoID";

            // Categorías

            cmboxCategoriaProduc.DataSource = sq.GetCategoriasActivas();
            cmboxCategoriaProduc.DisplayMember = "Nombre";
            cmboxCategoriaProduc.ValueMember = "CategoriaID";

            // Unidades

            cmboxUnidadProduct.DataSource = sq.GetUnidades();
            cmboxUnidadProduct.DisplayMember = "Nombre";
            cmboxUnidadProduct.ValueMember = "UnidadID";

        }

        private void btnGuardarProduc_Click(object sender, EventArgs e)
        {

            if (!ValidarCampos())
                return;

            // 👉 Validar que el usuario seleccionado sea admin
            DataRowView usuarioSeleccionado = CmboxEmpleado.SelectedItem as DataRowView;
            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un usuario autorizado.");
                return;
            }

            string cargo = usuarioSeleccionado["Cargo"].ToString();
            int empleadoID = Convert.ToInt32(usuarioSeleccionado["EmpleadoID"]);

            if (!cargo.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Solo un usuario con cargo ADMIN puede guardar productos.");
                return;
            }

            // 👉 Si es admin, continuar con el guardado
            InsertCommand insertService = new InsertCommand();

            string codigo = txtCodigoProduc.Text;
            string nombre = txtNombreProduct.Text.Trim();
            int categoriaID = Convert.ToInt32(cmboxCategoriaProduc.SelectedValue);
            int unidadID = Convert.ToInt32(cmboxUnidadProduct.SelectedValue);
            string descripcion = txtDescripcion.Text.Trim();
            decimal precioVenta = Convert.ToDecimal(txtPrecioProduct.Text);
            int stockInicial = Convert.ToInt32(txtStockInicial.Text);
            int stockMinimo = Convert.ToInt32(txtStockMinimo.Text);
            bool activo = checkBoxProductosActi.Checked;

            string observacion = $"Alta producto nuevo por {usuarioSeleccionado["Nombre"]}";

            int productoID = insertService.AgregarProductoConInventarioSP(
                codigo, nombre, categoriaID, unidadID,
                descripcion, precioVenta, activo,
                stockInicial, stockMinimo,
                empleadoID, observacion
            );

            if (productoID > 0)
            {
                MessageBox.Show("Producto insertado correctamente.");
                this.DialogResult = DialogResult.OK; // 👉 marcar éxito
                this.Close(); // 👉 cerrar formulario después de guardar
            }
            else
            {
                MessageBox.Show("No se pudo insertar el producto.");
            }


        }
    }
}
