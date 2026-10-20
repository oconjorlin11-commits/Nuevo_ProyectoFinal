using Nuevo_Proyecto.Catalogos;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Views.FormularioUI;
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
        private readonly AuditoriaService _auditoria;

        public string BuscarTexto { get => txtBucarInvet.Text; set => txtBucarInvet.Text = value; }
        public bool HayMultiplesResultados { get; set; }

        public event EventHandler? BuscarChanged;

        public Inventario()
        {
            InitializeComponent();
            _presenter = new InventarioPresenter(this);
            _auditoria = new AuditoriaService();
            // Suscribir formateo y manejo de errores para la columna Activo
            dataGridInventario.CellFormatting += dataGridInventario_CellFormatting;
            dataGridInventario.DataError += DataGridInventario_DataError;
            dataGridInventario.DataBindingComplete += (s, e) => { if (dataGridInventario.Columns.Contains("Activo")) dataGridInventario.Columns["Activo"]!.HeaderText = "Estado"; };
            // Suscribir selección de fila en el DataGrid
            dataGridInventario.SelectionChanged += dataGridInventario_SelectionChanged;
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
            // Limpiar campos de texto
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

            // Limpiar búsqueda y recargar productos activos con stock
            BuscarTexto = string.Empty;
            var dtActualizado = FilterOnlyWithStock(_presenter.ObtenerInventarioActivo());
            dataGridInventario.DataSource = dtActualizado;
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
            // Registrar acceso a Inventario
            _auditoria.RegistrarAccion(
                seccion: "Inventario",
                accion: "ACCESO_TEMPORAL",
                usuario: SesionActual.NombreEmpleado,
                rolUsuario: SesionActual.Rol,
                descripcion: "Acceso temporal a inventario"
            );

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

            // IMPORTANTE: La rama de REACTIVACIÓN debe evaluarse PRIMERO, antes de cualquier otra validación
            // Detectar intento de reactivación: EstadoOriginal=false (inactivo)
            bool intentaReactivar = !EstadoOriginal;

            //  Paso 3: lógica especial para reactivación
            if (intentaReactivar)
            {
                // ✅ VALIDACIÓN CRÍTICA: El checkbox DEBE estar checked para poder reactivar
                // Si no está checked, NO mostrar el formulario de motivo
                if (!checkBoxInventario.Checked)
                {
                    MessageBox.Show(
                        "Para reactivar un producto INACTIVO, debe:\n\n" +
                        "1. Activar el CheckBox (Estado: ACTIVO)\n" +
                        "2. Ingresar el nuevo Stock (debe ser mayor que el actual)\n" +
                        "3. Hacer clic en Editar\n\n" +
                        "El CheckBox DEBE estar activado antes de continuar.",
                        "CheckBox no activado"
                    );
                    return;
                }

                // Validación 1: si el producto fue dado de baja, el stock actual debe estar en 0
                if (StockOriginal > 0)
                {
                    MessageBox.Show(
                        $"El producto fue dado de baja pero aún tiene Stock:{StockOriginal}.\n" +
                        "Cuando se da de baja un producto, el Stock debe quedar en 0.",
                        "Verificar estado de baja"
                    );
                    return;
                }

                // Validación 2: Stock debe ser mayor a 0
                if (stock <= 0)
                {
                    MessageBox.Show(
                        $"Debe ingresar un Stock mayor a 0.\n" +
                        $"Stock Mínimo requerido: {StockMinimoOriginal}",
                        "Stock inválido"
                    );
                    return;
                }

                // Validación 3: Mínimo debe ser mayor a 0
                if (minimo <= 0)
                {
                    MessageBox.Show("Stock Mínimo debe ser mayor a 0.");
                    return;
                }

                // Validación 4: Stock ingresado debe ser MAYOR al Stock actual (0)
                if (stock <= StockOriginal)
                {
                    MessageBox.Show(
                        $"Debe AUMENTAR el Stock (actual: {StockOriginal}).\n" +
                        $"Ingrese un valor mayor a {StockOriginal}.",
                        "Stock no ha aumentado"
                    );
                    return;
                }

                // Validación 5: Stock debe ser >= Mínimo
                if (stock < minimo)
                {
                    MessageBox.Show(
                        $"El Stock ingresado ({stock}) debe ser mayor o igual al Stock Mínimo ({minimo}).",
                        "Stock insuficiente para reactivar"
                    );
                    return;
                }

                // ✅ SOLO AQUÍ - Todas las validaciones pasaron, pedir motivo de reactivación
                // En este punto: Checkbox ESTÁ checked, Stock es válido y mayor al mínimo
                var formMotivoReactivacion = new Motivo_de_actualizacion_Inventario();
                if (formMotivoReactivacion.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string motivoReactivacion = formMotivoReactivacion.Motivo;

                // Validación final: rechazar motivos vacíos o inválidos
                if (string.IsNullOrWhiteSpace(motivoReactivacion))
                {
                    MessageBox.Show("El motivo de reactivación no puede estar vacío.", "Motivo requerido");
                    return;
                }

                try
                {
                    int filas = _presenter.ReactivarProducto(CodigoOriginal, stock, minimo, motivoReactivacion);
                    if (filas > 0)
                    {
                        MessageBox.Show("Producto reactivado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo reactivar el producto.");
                    }

                    LimpiarDespuesDeAccion();
                }
                catch (InvalidOperationException ex)
                {
                    // Capturar específicamente las excepciones de validación de motivo
                    MessageBox.Show(
                        ex.Message,
                        "Motivo no válido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al reactivar el producto: " + ex.Message);
                }

                return;
            }

            //  Paso 4: bloquear cambios en Código
            if (CodigoOriginal != txtCodigoInvent.Text)
            {
                MessageBox.Show("No se puede editar el código del producto.");
                txtCodigoInvent.Text = CodigoOriginal;
                return;
            }

            // Paso 4.5: Detectar intento de cambio de estado (EN CUALQUIER DIRECCIÓN)
            bool cambioDeEstado = EstadoOriginal != checkBoxInventario.Checked;

            if (cambioDeEstado)
            {
                // Si intenta cambiar estado, verificar qué tipo de cambio
                bool esDesactivacion = EstadoOriginal && !checkBoxInventario.Checked;  // activo → inactivo
                bool esReactivacion = !EstadoOriginal && checkBoxInventario.Checked;  // inactivo → activo

                if (esDesactivacion)
                {
                    // La desactivación SOLO se hace a través de "Eliminar", NO en "Editar"
                    MessageBox.Show(
                        "Para desactivar un producto, use el botón 'Eliminar'.",
                        "Cambio de estado no permitido"
                    );
                    checkBoxInventario.Checked = EstadoOriginal;
                    return;
                }

                if (esReactivacion)
                {
                    // La reactivación SÍ es permitida en "Editar", pero con validaciones rigurosas

                    if (stock <= 0)
                    {
                        MessageBox.Show(
                            $"Para reactivar el producto, debe ingresar un Stock mayor a 0.\n" +
                            $"Stock Mínimo requerido: {StockMinimoOriginal}",
                            "Stock inválido"
                        );
                        return;
                    }

                    if (minimo <= 0)
                    {
                        MessageBox.Show("Stock Mínimo debe ser mayor a 0.");
                        return;
                    }

                    // Stock debe ser diferente al actual
                    if (stock == StockOriginal)
                    {
                        MessageBox.Show(
                            $"Para reactivar, debe ingresar un Stock NUEVO diferente al actual ({StockOriginal}).",
                            "Stock no ha cambiado"
                        );
                        return;
                    }

                    // Stock debe ser mayor o igual al mínimo
                    if (stock < minimo)
                    {
                        MessageBox.Show(
                            $"El Stock ingresado ({stock}) debe ser mayor o igual al Stock Mínimo ({minimo}).",
                            "Stock insuficiente para reactivar"
                        );
                        return;
                    }
                }
            }

            //  Paso 5: validar si hubo cambios
            if (!HayCambios())
            {
                MessageBox.Show("No se ha hecho ningún cambio.");
                return;
            }

            //  Paso 6: mostrar formulario de motivo SOLO si hay cambios reales
            // El UsuarioAutorizador ya fue obtenido en Inventario_Load y está guardado en SesionActual

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

            // Crear y mostrar el formulario de motivo de actualización
            var formMotivo = new Motivo_de_actualizacion_Inventario();
            if (formMotivo.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(formMotivo.Motivo))
                {
                    RestaurarValoresOriginales();
                    MessageBox.Show("No se aplicaron cambios. Se restauraron los valores originales.");
                    return;
                }

                EmpleadoID = SesionActual.EmpleadoId;
                string observacion = formMotivo.Motivo;

                int filasUpdate = _presenter.ActualizarProducto(
                    ProductoIDOriginal,
                    txtProductosInven.Text,
                    Convert.ToInt32(cmboxCategorias.SelectedValue),
                    Convert.ToInt32(cmboxUnidad.SelectedValue),
                    null,   // descripción: se conserva la actual
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
                    // Registrar auditoría
                    _auditoria.RegistrarAccion(
                        seccion: "Inventario",
                        accion: "MODIFICAR",
                        registroId: ProductoIDOriginal,
                        usuario: SesionActual.NombreEmpleado,
                        rolUsuario: SesionActual.Rol,
                        usuarioAutorizador: SesionActual.UsuarioAutorizador,
                        detalle: $"Producto: {txtProductosInven.Text}, Stock: {stock}, Precio: {precioVenta}",
                        descripcion: observacion
                    );

                    // Actualizar la fila visible en el grid para reflejar la observación
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
                            // ignorar errores de actualización de celda
                        }
                    }
                }
                LimpiarDespuesDeAccion();
            }
        }

        private void btnEliminarInvent_Click(object sender, EventArgs e)
        {
            //  Paso 1: verificar si hay producto cargado
            if (ProductoIDOriginal == 0 || string.IsNullOrEmpty(CodigoOriginal))
            {
                MessageBox.Show("Debe buscar primero un producto.");
                return;
            }

            //  Paso 2: Confirmación inicial antes de mostrar formulario
            DialogResult confirmDialog = MessageBox.Show(
                $"¿Desea dar de baja permanentemente el producto:\n\n" +
                $"Código: {CodigoOriginal}\n" +
                $"Nombre: {NombreOriginal}\n\n" +
                $"Deberá ingresar una razón válida y justificada.",
                "Confirmar baja de producto",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmDialog != DialogResult.Yes)
            {
                return;
            }

            //  Paso 3: pedir motivo de baja
            // El UsuarioAutorizador ya fue obtenido en Inventario_Load y está guardado en SesionActual

            // Crear y mostrar el formulario de motivo de baja del producto
            var formMotivoBaja = new Motivo_de_la_baja_del_producto();
            if (formMotivoBaja.ShowDialog() == DialogResult.OK)
            {
                string motivo = formMotivoBaja.Motivo.Trim();

                // ✅ VALIDACIÓN CRÍTICA: Rechazar motivos inválidos o sin razón real
                if (string.IsNullOrWhiteSpace(motivo))
                {
                    MessageBox.Show("No se puede dar de baja sin especificar un motivo.", "Motivo requerido");
                    return;
                }

                // Validar que el motivo sea real (no frases como "porque sí", "me dio la gana", etc.)
                if (!EsMotivoValidoParaBaja(motivo))
                {
                    MessageBox.Show(
                        "El motivo ingresado no es válido. Debe indicar una razón real y justificada.\n\n" +
                        "Motivos válidos: daño, caducidad, robo, obsolescencia, etc.",
                        "Motivo sin justificación válida"
                    );
                    return;
                }

                try
                {
                    EmpleadoID = SesionActual.EmpleadoId;

                    int filas = _presenter.InhabilitarProducto(
                        ProductoIDOriginal,   //  usa el ID cargado en búsqueda
                        EmpleadoID,
                        motivo
                    );

                    MessageBox.Show(filas > 0 ? "Producto dado de baja correctamente." : "No se pudo dar de baja el producto.");

                    if (filas > 0)
                    {
                        // Registrar auditoría
                        _auditoria.RegistrarAccion(
                            seccion: "Inventario",
                            accion: "ELIMINAR",
                            registroId: ProductoIDOriginal,
                            usuario: SesionActual.NombreEmpleado,
                            rolUsuario: SesionActual.Rol,
                            usuarioAutorizador: SesionActual.UsuarioAutorizador,
                            detalle: $"Producto: {CodigoOriginal} - {NombreOriginal}",
                            descripcion: motivo
                        );
                    }

                    LimpiarDespuesDeAccion();
                }
                catch (InvalidOperationException ex)
                {
                    // Capturar específicamente las excepciones de validación de motivo
                    MessageBox.Show(
                        ex.Message,
                        "Motivo no válido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al dar de baja el producto: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Valida motivos de ACTUALIZACIÓN - MÁS FLEXIBLE
        /// Permite motivos operacionales sin palabras clave específicas
        /// </summary>
        /// <summary>
        /// Valida motivos de BAJA/ELIMINACIÓN - MÁS RESTRICTIVO
        /// Requiere razones específicas y válidas
        /// </summary>
        private bool EsMotivoValidoParaBaja(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                return false;

            motivo = motivo.Trim();

            // Mínimo 10 caracteres
            if (motivo.Length < 10)
                return false;

            var palabras = motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            if (palabras.Length < 2)
                return false;

            var motivoLower = motivo.ToLower();

            // 1️⃣ RECHAZAR FRASES CLARAMENTE SIN RAZÓN VÁLIDA
            var frasesAbsurdas = new[]
            {
                "porque sí", "porque si", "me dio la gana", "me dio gana",
                "sin razón", "sin motivo", "sin justificación", "sin justificacion",
                "al azar", "capricho", "aleatoriamente", "simplemente", "solo"
            };

            foreach (var frase in frasesAbsurdas)
            {
                if (motivoLower.Contains(frase))
                    return false;
            }

            // 2️⃣ DETECTAR EVASIONES INTELIGENTES - Culpar a personas 
            var personasYAcciones = new[]
            {
                "porque el administrador", "porque la administrador", "porque el jefe", "porque la jefa",
                "porque el gerente", "porque el director", "porque la directora",
                "porque alguien", "porque él", "porque ella", "porque yo",
                "porque el dueño", "porque la empresa", "porque la compañía"
            };

            foreach (var patron in personasYAcciones)
            {
                if (motivoLower.Contains(patron))
                {
                    // Si culpa a una persona, debe tener razón objetiva
                    var razonObjetiva = new[] { "daño", "roto", "caducado", "vencido", "robo", "perdida",
                        "obsoleto", "error", "defecto", "fallo", "politica", "norma", "auditoría",
                        "stock", "inventario", "control" };
                    bool tieneRazon = false;
                    foreach (var r in razonObjetiva)
                    {
                        if (motivoLower.Contains(r)) { tieneRazon = true; break; }
                    }
                    if (!tieneRazon) return false;
                }
            }

            // Detectar acciones vacías
            var accionesVacias = new[] { "quiso", "decidió", "decidio", "se le dio la gana", "le vino en gana", "se le antojó", "se le antojo" };
            bool tienePersona = false;
            foreach (var personas in new[] { "administrador", "jefe", "gerente", "director", "dueño", "él", "ella", "yo" })
            {
                if (motivoLower.Contains(personas)) { tienePersona = true; break; }
            }
            if (tienePersona)
            {
                foreach (var accion in accionesVacias)
                {
                    if (motivoLower.Contains(accion)) return false;
                }
            }

            // 3️⃣ VERIFICAR QUE TENGA MOTIVOS VÁLIDOS
            var motivosValidos = new[]
            {
                "daño", "dañado", "roto", "mojado", "oxidado", "deterioro",
                "caducado", "caducidad", "vencido", "podrido", "fermentado",
                "robo", "robado", "perdida", "perdido", "extraviado",
                "obsoleto", "antiguo", "sin movimiento", "viejo",
                "stock", "inventario", "agotado", "exceso",
                "retiro", "cambio", "devolución", "devuelto",
                "error", "equivocación", "confusión", "defecto", "avería",
                "inspección", "revisión", "auditoría", "control",
                "mantenimiento", "limpieza", "higiene", "desinfección"
            };

            bool tieneMotivoValido = false;
            foreach (var m in motivosValidos)
            {
                if (motivoLower.Contains(m)) { tieneMotivoValido = true; break; }
            }

            if (!tieneMotivoValido)
                return false;

            // 4️⃣ DETECTAR PALABRAS INCOMPLETAS
            foreach (var palabra in palabras)
            {
                if (palabra.Length < 3)
                {
                    var palabrasCortas = new[] { "de", "en", "el", "la", "un", "a", "y", "o", "si", "no", "es", "lo", "se", "le", "al", "su", "da", "x" };
                    if (!palabrasCortas.Contains(palabra.ToLower()))
                        return false;
                }

                if (palabra.Length > 3 && (palabra.EndsWith("cad") || palabra.EndsWith("dañ") ||
                    palabra.EndsWith("rust") || palabra.EndsWith("prod")) && palabra.Length < 8)
                {
                    if (!palabra.Equals("stock"))
                        return false;
                }
            }

            // 5️⃣ DETECTAR SPAM - Cambios erráticos de mayúscula
            int cambiosMayuscula = 0;
            for (int i = 1; i < motivo.Length; i++)
            {
                bool esMayusc = char.IsUpper(motivo[i]);
                bool anteriorMayusc = char.IsUpper(motivo[i - 1]);
                if (esMayusc != anteriorMayusc && char.IsLetter(motivo[i]) && char.IsLetter(motivo[i - 1]))
                    cambiosMayuscula++;
            }

            if (cambiosMayuscula > 3)
                return false;

            return true;
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
                    // Obtener el valor real de Activo (true/false)
                    object? activoVal = dataGridInventario.Rows[e.RowIndex].Cells["Activo"].Value;
                    bool esActivo = false;

                    if (activoVal != null && activoVal != DBNull.Value)
                    {
                        if (bool.TryParse(activoVal.ToString(), out bool activo))
                        {
                            esActivo = activo;
                        }
                    }

                    // Si el producto está inactivo (Activo = false) mostrar "Inactivo"
                    if (!esActivo)
                    {
                        e.Value = "Inactivo";
                        e.FormattingApplied = true;
                        return;
                    }

                    // Si está activo, verificar stock
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

                    e.Value = estado;
                    e.FormattingApplied = true;
                }
            }
            catch
            {
                // no propagar excepción de formateo
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

        private void dataGridInventario_SelectionChanged(object? sender, EventArgs e)
        {
            // Solo cargar campos si hay múltiples resultados (resultado de búsqueda)
            if (!HayMultiplesResultados)
                return;

            if (dataGridInventario.SelectedRows.Count > 0)
            {
                var row = dataGridInventario.SelectedRows[0];
                if (row.DataBoundItem is DataRowView drv)
                {
                    CargarDatosEdicion(drv.Row);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

}













