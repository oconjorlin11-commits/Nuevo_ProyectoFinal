using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
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
            if (esError)
            {
                MensajeHelper.Error(message, titulo);
            }
            else
            {
                MensajeHelper.Exito(message, titulo);
            }
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
            // Ya no necesitamos cargar usuarios manualmente
            // El campo se pobla automáticamente con el usuario de la sesión
        }


        private void btnCancelarProduct_Click(object? sender, EventArgs e)
        {
            // Avisar al presenter que se canceló (si hay suscriptores)
            CancelarClicked?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void NuevoProducto_Load(object? sender, EventArgs e)
        {
            // Código automático
            txtCodigoProduc.Text = _presenter.ObtenerProximoCodigoProducto();
            txtCodigoProduc.Enabled = false;

            // Categorías - Agregar placeholder
            DataTable dtCategorias = _presenter.GetCategoriasActivas();
            if (!dtCategorias.Columns.Contains("Nombre") || !dtCategorias.Columns.Contains("CategoriaID"))
            {
                dtCategorias = new DataTable();
                dtCategorias.Columns.Add("CategoriaID", typeof(int));
                dtCategorias.Columns.Add("Nombre", typeof(string));
            }

            DataRow placeholderCat = dtCategorias.NewRow();
            placeholderCat["Nombre"] = "Seleccione una categoría";
            placeholderCat["CategoriaID"] = -1;
            dtCategorias.Rows.InsertAt(placeholderCat, 0);
            ComboBoxBindingHelper.Bind(cmboxCategoriaProduc, dtCategorias, "Nombre", "CategoriaID");
            cmboxCategoriaProduc.SelectedIndex = 0;

            // Unidades - Agregar placeholder
            DataTable dtUnidades = _presenter.GetUnidades();
            if (!dtUnidades.Columns.Contains("Nombre") || !dtUnidades.Columns.Contains("UnidadID"))
            {
                dtUnidades = new DataTable();
                dtUnidades.Columns.Add("UnidadID", typeof(int));
                dtUnidades.Columns.Add("Nombre", typeof(string));
            }

            DataRow placeholderUni = dtUnidades.NewRow();
            placeholderUni["Nombre"] = "Seleccione una unidad de medida";
            placeholderUni["UnidadID"] = -1;
            dtUnidades.Rows.InsertAt(placeholderUni, 0);
            ComboBoxBindingHelper.Bind(cmboxUnidadProduct, dtUnidades, "Nombre", "UnidadID");
            cmboxUnidadProduct.SelectedIndex = 0;
        }

        private void btnGuardarProduc_Click(object? sender, EventArgs e)
        {
            // La vista solo dispara el evento; la validación y persistencia quedan en el presenter.
            GuardarClicked?.Invoke(this, EventArgs.Empty);

            // Verificar que el producto se guardó correctamente (UltimoProductoIDCreado será asignado)
            if (_presenter.UltimoProductoIDCreado == null || _presenter.UltimoProductoIDCreado <= 0)
            {
                // Si no se guardó, no hacer nada más (el presenter ya mostró el error)
                return;
            }

            // Registrar auditoría con los datos correctos del producto creado
            var auditoriaService = new AuditoriaService();
            auditoriaService.RegistrarAuditoria(
                seccion: "Productos",
                accion: "CREAR",
                usuario: SesionActual.NombreUsuario ?? "Desconocido",
                rolUsuario: SesionActual.Usuario?.Cargo ?? "Usuario",
                registroID: _presenter.UltimoProductoIDCreado,  // ID del producto creado
                usuarioAutorizador: SesionActual.UsuarioAutorizador,  // Usuario autorizador
                detalle: $"Producto: {_presenter.UltimoProductoNombre}, Código: {_presenter.UltimoProductoCodigo}, Precio: {_presenter.UltimoProductoPrecio}",
                descripcion: $"Nuevo producto creado: {_presenter.UltimoProductoNombre}"
            );

            // Cerrar el formulario con success para que Inventario lo detecte y refresque
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Evento FormClosing para mostrar advertencia si hay datos sin guardar
        /// </summary>
        private void NuevoProducto_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Si la razón de cierre no es una acción del usuario, permitir cerrar
            if (e.CloseReason != CloseReason.UserClosing)
                return;

            // Si DialogResult ya está configurado (guardó exitosamente), permitir cerrar
            if (this.DialogResult == DialogResult.OK)
                return;

            // Verificar si hay datos incompletos
            bool hayDatos = !string.IsNullOrWhiteSpace(Nombre) ||
                            !string.IsNullOrWhiteSpace(Codigo) ||
                            CategoriaId > 0 ||
                            UnidadId > 0 ||
                            !string.IsNullOrWhiteSpace(Descripcion) ||
                            PrecioVenta > 0 ||
                            stockInicial > 0 ||
                            StockMinimo > 0;

            // Si no hay datos, permitir cerrar
            if (!hayDatos)
                return;

            // Mostrar advertencia estandarizada
            string mensaje = AdvertenciaFormHelper.ObtenerMensajeAdvertencia("Producto");
            string titulo = AdvertenciaFormHelper.ObtenerTituloConfirmacion("Producto");

            DialogResult resultado = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // Si el usuario selecciona "No", cancelar el cierre
            if (resultado == DialogResult.No)
                e.Cancel = true;
        }
    }
}
