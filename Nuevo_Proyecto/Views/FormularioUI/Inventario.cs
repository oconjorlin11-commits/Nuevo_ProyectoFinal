using Nuevo_Proyecto.Catalogos;
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
    public partial class Inventario : Form, Nuevo_Proyecto.Views.Interfaces.IInventarioView
    {

        private int ProductoIDOriginal;
        private string CodigoOriginal = string.Empty;
        private string NombreOriginal = string.Empty;
        private decimal PrecioVentaOriginal;
        private int StockOriginal;
        private int StockMinimoOriginal;
        private int CategoriaOriginalID;
        private int UnidadOriginalID;
        private bool EstadoOriginal;
        private int EmpleadoID;


        private readonly InventarioPresenter _presenter;

        public string BuscarTexto { get => txtBucarInvet.Text; set => txtBucarInvet.Text = value; }

        public event EventHandler? BuscarChanged;

        public Inventario()
        {
            InitializeComponent();
            _presenter = new InventarioPresenter(this);
            // Suscribir formateo y manejo de errores para la columna Activo
            dataGridInventario.CellFormatting += dataGridInventario_CellFormatting;
            dataGridInventario.DataError += DataGridInventario_DataError;
            dataGridInventario.DataBindingComplete += (s, e) => { if (dataGridInventario.Columns.Contains("Activo")) dataGridInventario.Columns["Activo"]!.HeaderText = "Estado"; };
            // Suscribir textbox de bsqueda (nombre en diseador: txtBucarInvet)
            txtBucarInvet.TextChanged += txtBuscarInventario_TextChanged;
            // Suscribir cambio de categora de filtro (cmboxCategoriaInve) para mostrar por categora sin filtrar por stock
            cmboxCategoriaInve.SelectedIndexChanged += CmboxCategoriaInve_SelectedIndexChanged;
        }

        // Devuelve una tabla con solo filas cuyo Stock > 0 (mantiene esquema y orden)
        private DataTable FilterOnlyWithStock(DataTable? source)
        {
            if (source == null) return new DataTable();

            // Crear un nuevo DataTable con el mismo esquema
            var dt = new DataTable();

            // Copiar definiciÃ³n de columnas
            foreach (DataColumn col in source.Columns)
            {
                dt.Columns.Add(col.ColumnName, col.DataType);
            }

            // Filtrar y copiar filas (preserva orden)
            foreach (DataRow r in source.Rows)
            {
                try
                {
                    // Validar que tenga cÃ³digo vÃ¡lido
                    var codigo = r["Codigo"];
                    if (codigo == null || codigo == DBNull.Value || string.IsNullOrWhiteSpace(codigo.ToString()))
                    {
                        continue;
                    }

                    var stockVal = r["Stock"];
                    int stock = 0;

                    if (stockVal != null && stockVal != DBNull.Value)
                    {
                        if (int.TryParse(stockVal.ToString(), out int s))
                        {
                            stock = s;
                        }
                    }

                    // Incluir solo si tiene stock > 0
                    if (stock > 0)
                    {
                        dt.ImportRow(r);
                    }
                }
                catch
                {
                    // Si hay error, ignorar la fila
                }
            }

            return dt;
        }

        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        public void ResetFields()
        {
            LimpiarDespuesDeAccion();
            dataGridInventario.DataSource = null;
        }

        public void LimpiarCamposEdicion()
        {
            txtCodigoInvent.Text = string.Empty;
            txtProductosInven.Text = string.Empty;
            txtPrecioVentas.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtMinimo.Text = string.Empty;
        }

        public void MostrarInventario(DataTable dt)
        {
            // Limpiar filas vacías o sin información válida
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        var codigo = dt.Rows[i]["Codigo"];
                        var nombre = dt.Rows[i]["Nombre"];

                        // Remover si no tiene código Y nombre válidos
                        if ((codigo == null || codigo == DBNull.Value || string.IsNullOrWhiteSpace(codigo.ToString())) ||
                            (nombre == null || nombre == DBNull.Value || string.IsNullOrWhiteSpace(nombre.ToString())))
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                    catch
                    {
                        // Si hay error, remover la fila
                        dt.Rows.RemoveAt(i);
                    }
                }
            }

            dataGridInventario.DataSource = dt;

            // Aplicar ordenamiento por Código
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.Sort = "Codigo ASC";
                dataGridInventario.DataSource = dv;
            }
        }

        private void ConfigurardataGridInventario()
        {
            dataGridInventario.AutoGenerateColumns = false;
            dataGridInventario.Columns.Clear();

            dataGridInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Codigo",
                HeaderText = "Cdigo",
                DataPropertyName = "Codigo"
            });

            dataGridInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Producto",
                DataPropertyName = "Nombre"
            });

            dataGridInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Categoria",
                HeaderText = "Categora",
                DataPropertyName = "Categoria"
            });

            dataGridInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Unidad",
                HeaderText = "Unidad",
                DataPropertyName = "Unidad"
            });

            dataGridInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioVenta",
                HeaderText = "Precio Venta",
                DataPropertyName = "PrecioVenta"
            });

            dataGridInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock",
                DataPropertyName = "Stock"
            });

            dataGridInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockMinimo",
                HeaderText = "Mnimo",
                DataPropertyName = "StockMinimo"
            });

            dataGridInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Activo",
                HeaderText = "Estado",
                DataPropertyName = "Activo"
            });

            // Columna para observaciones/nota sobre el producto
            dataGridInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Observacion",
                HeaderText = "Observacin",
                DataPropertyName = "Observacion"
            });
        }
        private bool HayCambios()
        {
            return txtProductosInven.Text != NombreOriginal ||
                   Convert.ToDecimal(txtPrecioVentas.Text) != PrecioVentaOriginal ||
                   Convert.ToInt32(txtStock.Text) != StockOriginal ||
                   Convert.ToInt32(txtMinimo.Text) != StockMinimoOriginal ||
                   Convert.ToInt32(cmboxCategoriaInve.SelectedValue) != CategoriaOriginalID ||
                   Convert.ToInt32(cmboxUnidad.SelectedValue) != UnidadOriginalID;
        }
        private (string motivo, int empleadoID) PedirMotivo(string tipoCambio)
        {
            Form motivoForm = new Form();
            motivoForm.Text = tipoCambio;
            motivoForm.Size = new Size(1200, 700);
            motivoForm.StartPosition = FormStartPosition.CenterScreen;
            motivoForm.FormBorderStyle = FormBorderStyle.FixedDialog;

            Label lblMensaje = new Label
            {
                Text = $"Explique detalladamente el motivo de la {tipoCambio.ToLower()}:",
                Dock = DockStyle.Top,
                Height = 60,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            ComboBox cbEmpleados = new ComboBox
            {
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Cargar empleados desde presenter
            var empleadosDt = _presenter.GetEmpleadosActivos();
            cbEmpleados.DisplayMember = "Nombre";
            cbEmpleados.ValueMember = "EmpleadoID";
            cbEmpleados.DataSource = empleadosDt; // DataTable con empleados

            TextBox txtMotivo = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Both,
                Font = new Font("Segoe UI", 13),
                MaxLength = 2000
            };

            FlowLayoutPanel panelBotones = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                FlowDirection = FlowDirection.RightToLeft
            };

            Button btnAceptar = new Button { Text = "Aceptar", Width = 160, Height = 60, DialogResult = DialogResult.OK };
            Button btnCancelar = new Button { Text = "Cancelar", Width = 160, Height = 60, DialogResult = DialogResult.Cancel };

            panelBotones.Controls.Add(btnAceptar);
            panelBotones.Controls.Add(btnCancelar);

            motivoForm.Controls.Add(txtMotivo);
            motivoForm.Controls.Add(cbEmpleados);
            motivoForm.Controls.Add(lblMensaje);
            motivoForm.Controls.Add(panelBotones);

            motivoForm.AcceptButton = btnAceptar;
            motivoForm.CancelButton = btnCancelar;

            if (motivoForm.ShowDialog() == DialogResult.OK)
            {
                int empleadoID = 0;

                //  Manejo seguro del SelectedValue
                if (cbEmpleados.SelectedValue != null && cbEmpleados.SelectedValue is int)
                {
                    empleadoID = (int)cbEmpleados.SelectedValue;
                }
                else if (cbEmpleados.SelectedItem is DataRowView drv)
                {
                    empleadoID = Convert.ToInt32(drv["EmpleadoID"]);
                }

                return (txtMotivo.Text.Trim(), empleadoID);
            }

            return ("", 0); //  si cancela, no devuelve motivo ni empleado
        }


        private void RestaurarValoresOriginales()
        {
            txtProductosInven.Text = NombreOriginal;
            txtPrecioVentas.Text = PrecioVentaOriginal.ToString("0.00");
            txtStock.Text = StockOriginal.ToString();
            txtMinimo.Text = StockMinimoOriginal.ToString();
            cmboxCategoriaInve.SelectedValue = CategoriaOriginalID;
            cmboxUnidad.SelectedValue = UnidadOriginalID;
            checkBoxInventario.Checked = EstadoOriginal;
        }

        private void LimpiarDespuesDeAccion()
        {
            txtCodigoInvent.Text = string.Empty;
            txtProductosInven.Text = string.Empty;
            txtPrecioVentas.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtMinimo.Text = string.Empty;

            //  Resetear combos
            if (cmboxCategoriaInve.Items.Count > 0)
                cmboxCategoriaInve.SelectedIndex = 0; // "Todas"

            if (cmboxCategoriaInve.Items.Count > 0)
                cmboxCategoriaInve.SelectedIndex = -1; // nada seleccionado

            if (cmboxUnidad.Items.Count > 0)
                cmboxUnidad.SelectedIndex = -1;

            //  Resetear checkbox
            checkBoxInventario.Checked = false;

            //  Resetear variables originales
            CodigoOriginal = string.Empty;
            ProductoIDOriginal = 0;
            NombreOriginal = string.Empty;
            CategoriaOriginalID = 0;
            UnidadOriginalID = 0;
            PrecioVentaOriginal = 0;
            StockOriginal = 0;
            StockMinimoOriginal = 0;
            EstadoOriginal = false;
        }

        private void btnNuevoProduct_Click(object sender, EventArgs e)
        {

            using (NuevoProducto frm_NuevoProducto = new NuevoProducto())
            {
                if (frm_NuevoProducto.ShowDialog() == DialogResult.OK)
                {
                    //  refrescar inventario solo si se insert algo
                    dataGridInventario.DataSource = FilterOnlyWithStock(_presenter.ObtenerInventarioActivo());
                }
            }
        }



        private void Inventario_Load(object sender, EventArgs e)
        {
            //  Configurar columnas del DataGridView
            ConfigurardataGridInventario();

            //  Llenar combo de unidades
            cmboxUnidad.DisplayMember = "Nombre";
            cmboxUnidad.ValueMember = "UnidadID";
            cmboxUnidad.DataSource = _presenter.GetUnidades();

            //  Llenar combo de categoras para edicin tambin
            cmboxCategorias.DisplayMember = "Nombre";
            cmboxCategorias.ValueMember = "CategoriaID";
            cmboxCategorias.DataSource = _presenter.GetCategoriasActivas();

            //  Llenar combo de categoras solo con activas
            cmboxCategoriaInve.DisplayMember = "Nombre";
            cmboxCategoriaInve.ValueMember = "CategoriaID";
            cmboxCategoriaInve.DataSource = _presenter.GetCategoriasActivas();

            // Insertar opcin "Todas"
            DataTable dt = (DataTable)cmboxCategoriaInve.DataSource;
            DataRow row = dt.NewRow();
            row["CategoriaID"] = 0;
            row["Nombre"] = "Todas";
            dt.Rows.InsertAt(row, 0);
            cmboxCategoriaInve.SelectedIndex = 0;

            //  Mostrar inventario solo activo y con stock al inicio
            var dtInventario = FilterOnlyWithStock(_presenter.ObtenerInventarioActivo());

            // Limpiar filas vacÃ­as (sin cÃ³digo de producto)
            for (int i = dtInventario.Rows.Count - 1; i >= 0; i--)
            {
                var codigo = dtInventario.Rows[i]["Codigo"];
                if (codigo == null || codigo == DBNull.Value || string.IsNullOrWhiteSpace(codigo.ToString()))
                {
                    dtInventario.Rows.RemoveAt(i);
                }
            }

            dataGridInventario.DataSource = dtInventario;

                                    // Ordenar por cÃ³digo de forma explÃ­cita
                                    if (dtInventario.Rows.Count > 0)
                                    {
                                                DataView dv = new DataView(dtInventario);
                                                dv.Sort = "Codigo ASC";
                                                dataGridInventario.DataSource = dv;
                                    }

            

            //  Opciones de seleccin
            dataGridInventario.ReadOnly = true;
            dataGridInventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridInventario.MultiSelect = false;
            dataGridInventario.AllowUserToAddRows = false;

            // Suscribirse a selecci�n de fila en el DataGrid
            // // Solo bÃºsqueda por textbox, no por clic en grid  // Deshabilitado: solo buscar por textbox
        }

        private void DataGridInventario_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Cargar datos de la fila seleccionada
            if (e.RowIndex >= 0 && dataGridInventario.Rows.Count > e.RowIndex)
            {
                try
                {
                    var row = dataGridInventario.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    if (row != null)
                    {
                        CargarDatosEdicion(row.Row);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar datos de fila: {ex.Message}");
                }
            }
        }

        private void txtBuscarInventario_TextChanged(object? sender, EventArgs e)
        {
            BuscarChanged?.Invoke(this, EventArgs.Empty);
        }

        private void btnEditarInventar_Click(object sender, EventArgs e)
        {
            //  Paso 1: verificar si hay producto cargado
            if (ProductoIDOriginal == 0 || string.IsNullOrEmpty(CodigoOriginal))
            {
                MessageBox.Show("Debe buscar primero un producto.");
                return;
            }

            //  Paso 2: validar campos
            if (!int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Debe ingresar un stock vlido.");
                return;
            }

            if (!int.TryParse(txtMinimo.Text, out int minimo))
            {
                MessageBox.Show("Debe ingresar un mnimo vlido.");
                return;
            }

            if (!decimal.TryParse(txtPrecioVentas.Text, out decimal precioVenta))
            {
                MessageBox.Show("Debe ingresar un precio de venta vlido.");
                return;
            }

            //  Paso 3: lgica especial para reactivacin
            if (!EstadoOriginal && checkBoxInventario.Checked)
            {
                if (stock <= 0 || minimo <= 0)
                {
                    MessageBox.Show("Para reactivar el producto debe actualizar Stock y Stock Mnimo.");
                    return;
                }

                int filas = _presenter.ReactivarProducto(CodigoOriginal, stock, minimo);
                MessageBox.Show(filas > 0 ? "Producto reactivado correctamente." : "No se pudo reactivar.");
                LimpiarDespuesDeAccion();
                return;
            }

            //  Paso 4: bloquear cambios en Cdigo y Activo
            if (CodigoOriginal != txtCodigoInvent.Text)
            {
                MessageBox.Show("No se puede editar el cdigo del producto.");
                txtCodigoInvent.Text = CodigoOriginal; // restaura el cdigo original
                return;
            }

            if (EstadoOriginal != checkBoxInventario.Checked)
            {
                MessageBox.Show("No se puede cambiar el estado desde Editar. Use Eliminar/Activar.");
                checkBoxInventario.Checked = EstadoOriginal;
                return;
            }

            //  Paso 5: validar si hubo cambios
            if (!HayCambios())
            {
                MessageBox.Show("No se ha hecho ningn cambio.");
                return;
            }

            //  Paso 6: pedir motivo y actualizar
            var tipoCambio = TipoMovimientoHelper.DetectarTipoCambio(
                txtProductosInven.Text,
                Convert.ToInt32(cmboxCategoriaInve.SelectedValue),
                Convert.ToInt32(cmboxUnidad.SelectedValue),
                precioVenta,
                checkBoxInventario.Checked,
                stock,
                minimo,
                NombreOriginal,
                CategoriaOriginalID,
                UnidadOriginalID,
                PrecioVentaOriginal,
                EstadoOriginal,
                StockOriginal,
                StockMinimoOriginal
            );

            var resultado = PedirMotivo(TipoMovimientoHelper.ToDescription(tipoCambio));

            if (string.IsNullOrEmpty(resultado.motivo) || resultado.empleadoID == 0)
            {
                RestaurarValoresOriginales();
                MessageBox.Show("No se aplicaron cambios. Se restauraron los valores originales.");
                return;
            }

            EmpleadoID = resultado.empleadoID;
            string observacion = resultado.motivo;


            int filasUpdate = _presenter.ActualizarProducto(
        ProductoIDOriginal,
        txtProductosInven.Text,
        Convert.ToInt32(cmboxCategorias.SelectedValue),
        Convert.ToInt32(cmboxUnidad.SelectedValue),
        null,   // descripcin: se conserva la actual (antes se sobrescriba con el motivo del cambio)
        precioVenta,
        checkBoxInventario.Checked,
        stock,
        minimo,
        EmpleadoID,
        observacion
    );

            MessageBox.Show(filasUpdate > 0 ? "Producto actualizado correctamente." : "No se pudo actualizar.");
            if (filasUpdate > 0)
            {
                // Actualizar la fila visible en el grid para reflejar la observacin
                foreach (DataGridViewRow r in dataGridInventario.Rows)
                {
                    try
                    {
                        if (r.Cells["ProductoID"].Value != null && Convert.ToInt32(r.Cells["ProductoID"].Value) == ProductoIDOriginal)
                        {
                            if (r.Cells["Observacion"] != null)
                                r.Cells["Observacion"].Value = observacion;
                        }
                    }
                    catch
                    {
                        // ignorar errores de actualizacin de celda
                    }
                }
            }
            LimpiarDespuesDeAccion();
        }




        private void btnEliminarInvent_Click(object sender, EventArgs e)
        {
            //  Paso 1: verificar si hay producto cargado
            if (ProductoIDOriginal == 0 || string.IsNullOrEmpty(CodigoOriginal))
            {
                MessageBox.Show("Debe buscar primero un producto.");
                return;
            }

            //  Paso 2: pedir motivo
            var tipoCambio = TipoMovimientoInventario.InhabilitacionProducto;
            var resultado = PedirMotivo(TipoMovimientoHelper.ToDescription(tipoCambio));

            if (string.IsNullOrWhiteSpace(resultado.motivo) || resultado.empleadoID == 0)
            {
                MessageBox.Show("No se aplicaron cambios. El producto sigue igual.");
                return;
            }

            try
            {
                EmpleadoID = resultado.empleadoID;
                string motivo = resultado.motivo;

                int filas = _presenter.InhabilitarProducto(
                    ProductoIDOriginal,   //  usa el ID cargado en bsqueda
                    EmpleadoID,
                    motivo
                );

                MessageBox.Show(filas > 0 ? "Producto inhabilitado correctamente." : "No se pudo inhabilitar el producto.");
                LimpiarDespuesDeAccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inhabilitar producto: " + ex.Message);
            }
        }

        private void CbCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {


            //  Evitar que el diseador ejecute este cdigo
            if (this.DesignMode) return;
            // Este manejador corresponde al combo cmboxCategorias (edicin de producto)
            // No debe filtrar el grid de inventario: slo se usa para seleccionar la categora del producto en el formulario de edicin.
            // Si necesitas alguna accin al cambiar la categora en el editor, implementarla aqu.
        }

        private void CmboxCategoriaInve_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (this.DesignMode) return;

            // Si hay una búsqueda activa (txtBucarInvet no está vacío), no filtrar por categoría
            // La búsqueda tiene PRIORIDAD
            if (!string.IsNullOrEmpty(txtBucarInvet.Text?.Trim()))
            {
                return;
            }

            if (cmboxCategoriaInve.SelectedValue != null && cmboxCategoriaInve.SelectedValue is int)
            {
                int categoriaID = (int)cmboxCategoriaInve.SelectedValue;

                if (categoriaID == 0)
                {
                    // Mostrar slo activos con stock por defecto
                    dataGridInventario.DataSource = FilterOnlyWithStock(_presenter.ObtenerInventarioActivo());
                }
                else
                {
                    // Mostrar inventario filtrado por categora SIN filtrar por stock (incluye agotados)
                    dataGridInventario.DataSource = _presenter.GetInventarioActivoPorCategoria(categoriaID);
                }

                // Ajustar apariencia de la columna Estado/Activo
                var colActivo = dataGridInventario.Columns["Activo"];
                if (colActivo != null)
                {
                    colActivo.HeaderText = "Estado";
                    colActivo.ReadOnly = true;
                    colActivo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void dataGridInventario_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e) 
        {
            try
            {
                if (dataGridInventario.Columns[e.ColumnIndex].Name == "Activo")
                {
                    // Verificar el Stock: si Stock <= 0 -> Agotado, si >0 -> Activo
                    string estado = "Activo";
                    if (dataGridInventario.Columns.Contains("Stock") && e.RowIndex >= 0 && e.RowIndex < dataGridInventario.Rows.Count)
                    {
                        var stockVal = dataGridInventario.Rows[e.RowIndex].Cells["Stock"].Value;
                        if (stockVal != null && stockVal != DBNull.Value)
                        {
                            if (int.TryParse(stockVal.ToString(), out int stock))
                            {
                                estado = stock <= 0 ? "Agotado" : "Activo";
                            }
                        }
                    }

                    

                    // Solo asignar estado si hay datos
                    e.Value = estado;
                    e.FormattingApplied = true;
                }
            }
            catch
            {
                // no propagar excepcin de formateo
            }

        }

        private void DataGridInventario_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        // Carga los datos de edici�n desde un DataRow (usado por OnBuscarChanged del presenter)
        public void CargarDatosEdicion(System.Data.DataRow fila)
        {
            if (fila == null) return;

            try
            {
                CodigoOriginal = fila["Codigo"]?.ToString() ?? string.Empty;
                ProductoIDOriginal = Convert.ToInt32(fila["ProductoID"]);
                NombreOriginal = fila["Nombre"]?.ToString() ?? string.Empty;
                CategoriaOriginalID = Convert.ToInt32(fila["CategoriaID"]);
                UnidadOriginalID = Convert.ToInt32(fila["UnidadID"]);
                PrecioVentaOriginal = Convert.ToDecimal(fila["PrecioVenta"]);
                StockOriginal = Convert.ToInt32(fila["Stock"]);
                StockMinimoOriginal = Convert.ToInt32(fila["StockMinimo"]);
                EstadoOriginal = Convert.ToBoolean(fila["Activo"]);

                // Mostrar en controles
                txtCodigoInvent.Text = CodigoOriginal;
                txtProductosInven.Text = NombreOriginal;
                txtPrecioVentas.Text = PrecioVentaOriginal.ToString("0.00");
                txtStock.Text = StockOriginal.ToString();
                txtMinimo.Text = StockMinimoOriginal.ToString();
                // NO llenar cmboxCategoriaInve (es el filtro de búsqueda)
                // cmboxCategoriaInve.SelectedValue = CategoriaOriginalID;
                cmboxUnidad.SelectedValue = UnidadOriginalID;
                checkBoxInventario.Checked = EstadoOriginal;

                // Llenar combos de edici�n solo con categor�as activas
                cmboxCategorias.DisplayMember = "Nombre";
                cmboxCategorias.ValueMember = "CategoriaID";
                cmboxCategorias.DataSource = _presenter.GetCategoriasActivas();
                cmboxCategorias.SelectedValue = CategoriaOriginalID;

                cmboxUnidad.DisplayMember = "Nombre";
                cmboxUnidad.ValueMember = "UnidadID";
                cmboxUnidad.DataSource = _presenter.GetUnidades();
                cmboxUnidad.SelectedValue = UnidadOriginalID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de edici�n: {ex.Message}");
            }
        }

        // Helper para convertir valores devueltos por la BD (bit 0/1, byte, int, bool, string) a bool
        private static bool ParseBoolDb(object? value)
        {
            if (value == null || value == DBNull.Value) return false;
            try
            {
                if (value is bool b) return b;
                if (value is byte by) return by != 0;
                if (value is short s) return s != 0;
                if (value is int i) return i != 0;
                if (value is long l) return l != 0L;
                var txt = value.ToString();
                if (string.IsNullOrWhiteSpace(txt)) return false;
                if (int.TryParse(txt, out var n)) return n != 0;
                if (bool.TryParse(txt, out var bb)) return bb;
            }
            catch
            {
            }
            return false;
        }

    }
    
}













