using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using ClosedXML.Excel;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class Comprobante : Form, IFacturacionView
    {
        private string _facturaCodigo;

        private DataTable? _dtFactura;
        private FacturacionPresenter _presenter;

        public Comprobante() : this("")
        {

        }

        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        public void ResetFields()
        {
            // No aplica para comprobante; método requerido por la interfaz
        }

        // Implementación de métodos de la interfaz IFacturacionView requeridos
        public void LoadCategorias(DataTable categorias) { }
        public void LoadProductosPorCategoria(DataTable productos) { }
        public void LoadEmpleados(DataTable empleados) { }
        public void LoadClientes(DataTable clientes) { }
        public void LoadFormasPago(DataTable formasPago) { }
        public void AgregarLineaDetalle(int productoId, string nombreProducto, int cantidad, decimal precioUnitario, decimal subtotal) { }
        public void LimpiarDetalles() { }
        public int ObtenerFilasDetalles() => 0;

        // Propiedades de la interfaz
        public int? ClienteSeleccionado => null;
        public int? EmpleadoSeleccionado => null;
        public int? FormaPagoSeleccionado => null;
        public int? CategoriaSeleccionada => null;
        public int? ProductoSeleccionado => null;
        public int CantidadProducto => 0;
        public string Observacion => string.Empty;
        public decimal Total { get => 0; set { } }

        // Eventos de la interfaz
        public event EventHandler NuevoClienteClick;
        public event EventHandler AgregarProductoClick;
        public event EventHandler QuitarLineaClick;
        public event EventHandler LimpiarTodoClick;
        public event EventHandler VerImprimirClick;
        public event EventHandler GuardarFacturaClick;

        public Comprobante(string facturaCodigo)
        {
            InitializeComponent();
            _facturaCodigo = facturaCodigo;
            _presenter = new FacturacionPresenter(this);
        }

        public Comprobante(string facturaCodigo, FacturacionPresenter presenter) : this(facturaCodigo)
        {
            _presenter = presenter ?? new FacturacionPresenter(this);
        }

        private void Comprobante_Shown(object sender, EventArgs e)
        {
            // Cargar datos en tiempo real cuando el formulario se muestra
            if (!string.IsNullOrWhiteSpace(_facturaCodigo))
            {
                _dtFactura = _presenter.ObtenerDetalleFacturaPorCodigo(_facturaCodigo.Trim());
                if (_dtFactura != null && _dtFactura.Rows.Count > 0)
                {
                    CargarFacturaEnLabels(_dtFactura);
                }
                else
                {
                    MessageBox.Show($"No se encontraron datos para la factura: {_facturaCodigo}",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void CargarFacturaEnLabels(DataTable? dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show($"No se encontraron datos para la factura: {_facturaCodigo}",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Encabezado
            lblNumeroFact.Text = dt.Rows[0]["Numero"]?.ToString() ?? _facturaCodigo;

            if (dt.Rows[0]["Fecha"] != DBNull.Value && DateTime.TryParse(dt.Rows[0]["Fecha"].ToString(), out DateTime fecha))
            {
                lblFecha.Text = fecha.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }

            string cliente = dt.Rows[0]["Cliente"]?.ToString() ?? "";
            lblCliente.Text = string.IsNullOrWhiteSpace(cliente) ? "Consumidor Final" : cliente;
            lblAtendidopor.Text = dt.Rows[0]["Empleado"]?.ToString() ?? "";
            lblPago.Text = dt.Rows[0]["FormaPago"]?.ToString() ?? "";

            // Limpiar DataGridView y agregar columnas si no existen
            dgvProductos.DataSource = null;
            dgvProductos.Columns.Clear();

            // Agregar columnas
            dgvProductos.Columns.Add("Producto", "Producto");
            dgvProductos.Columns.Add("Cantidad", "Cantidad");
            dgvProductos.Columns.Add("Precio", "Precio");
            dgvProductos.Columns.Add("Subtotal", "SubTotal");

            // Configurar ancho de columnas
            dgvProductos.Columns["Producto"].Width = 400;
            dgvProductos.Columns["Cantidad"].Width = 100;
            dgvProductos.Columns["Precio"].Width = 150;
            dgvProductos.Columns["Subtotal"].Width = 150;

            // Configurar alineación
            dgvProductos.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            decimal total = 0;

            // Llenar datos
            foreach (DataRow row in dt.Rows)
            {
                string prod = row["Producto"]?.ToString() ?? "";
                string cant = row["Cantidad"]?.ToString() ?? "0";
                decimal precio = row["Precio"] != DBNull.Value ? Convert.ToDecimal(row["Precio"]) : 0m;
                decimal subtotal = row["Subtotal"] != DBNull.Value ? Convert.ToDecimal(row["Subtotal"]) : 0m;

                total += subtotal;

                dgvProductos.Rows.Add(prod, cant, precio.ToString("N2"), subtotal.ToString("N2"));
            }

            // Total general
            lblTotal.Text = $"TOTAL A PAGAR: C$ {total:N2}";

            // Aplicar estilos al DataGridView
            EstilizarDataGridView();
        }

        private void EstilizarDataGridView()
        {
            // Configurar bordes de filas alternas
            for (int i = 0; i < dgvProductos.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    dgvProductos.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(250, 247, 241);
                }
                else
                {
                    dgvProductos.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(240, 230, 220);
                }
            }

            // Autoajustar altura de filas al contenido
            dgvProductos.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Exportar a Excel
            ExportarAExcel();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Abre el PDF directamente para imprimir sin diálogo de guardar
            ExportarAPDFParaImprimir();
        }

        private void BtnExportar_Click(object sender, EventArgs e) 
        {
            try
            {
                // Asegurar que tengamos los datos de la factura cargados
                if (_dtFactura == null || _dtFactura.Rows.Count == 0)
                {
                    _dtFactura = _presenter.ObtenerDetalleFacturaPorCodigo(_facturaCodigo?.Trim() ?? "");
                    if (_dtFactura == null || _dtFactura.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay datos disponibles para exportar esta factura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CargarFacturaEnLabels(_dtFactura);
                }

                string safeCodigo = string.Join("_", (_facturaCodigo ?? "Factura").Split(Path.GetInvalidFileNameChars()));
                if (string.IsNullOrWhiteSpace(safeCodigo)) safeCodigo = "Factura";

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Guardar Comprobante de Venta";
                    saveFileDialog.FileName = $"Comprobante_{safeCodigo}.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string rutaPdf = saveFileDialog.FileName;
                        GenerarPdfComprobante(rutaPdf);

                        DialogResult dr = MessageBox.Show(
                            "Comprobante exportado a PDF correctamente.\n\n¿Desea abrir el archivo ahora?",
                            "Éxito",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (dr == DialogResult.Yes)
                        {
                            Process.Start(new ProcessStartInfo(rutaPdf) { UseShellExecute = true });
                        }
                    }
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show(
                    $"No se pudo guardar el archivo. Verifique que no esté abierto en otra aplicación.\n\nDetalle: {ioEx.Message}",
                    "Archivo en uso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al exportar el comprobante: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            
        }

        
        private void GenerarPdfComprobante(string filePath)
        {
          using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
        Document doc = new Document(PageSize.A4, 40f, 40f, 40f, 40f);
        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
        doc.Open();

        // Definición de fuentes estilo ticket
        var fontTitulo = FontFactory.GetFont(FontFactory.COURIER_BOLD, 15, BaseColor.Black);
        var fontSubtitulo = FontFactory.GetFont(FontFactory.COURIER, 10, BaseColor.DarkGray);
        var fontNormal = FontFactory.GetFont(FontFactory.COURIER, 10, BaseColor.Black);
        var fontBold = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10, BaseColor.Black);
        var fontTotal = FontFactory.GetFont(FontFactory.COURIER_BOLD, 12, BaseColor.Black);
        var fontFooter = FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 9, BaseColor.DarkGray);

        // Tarjeta central de comprobante
        PdfPTable ticketCard = new PdfPTable(1);
        ticketCard.TotalWidth = 360f;
        ticketCard.LockedWidth = true;
        ticketCard.HorizontalAlignment = Element.ALIGN_CENTER;

        PdfPCell cardCell = new PdfPCell();
        cardCell.Border = iTextSharp.text.Rectangle.BOX;
        cardCell.BorderColor = new BaseColor(180, 180, 180);
        cardCell.BorderWidth = 1f;
        cardCell.Padding = 16f;
        cardCell.BackgroundColor = BaseColor.White;

        // 1. ENCABEZADO DEL COMEDOR
        Paragraph pTitulo = new Paragraph(Nuevo_Proyecto.Services.Helpers.AppConfig.NombreNegocio, fontTitulo) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 2f };
        Paragraph pSub = new Paragraph("Fritangas, asados y bebidas naturales", fontSubtitulo) { Alignment = Element.ALIGN_CENTER };
        Paragraph pDir = new Paragraph("Rivas, Nicaragua", fontSubtitulo) { Alignment = Element.ALIGN_CENTER };
        Paragraph pTel = new Paragraph("Tel: 8888-8888", fontSubtitulo) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 6f };
        Paragraph pSep1 = new Paragraph(new string('=', 42), fontNormal) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 6f };

        cardCell.AddElement(pTitulo);
        cardCell.AddElement(pSub);
        cardCell.AddElement(pDir);
        cardCell.AddElement(pTel);
        cardCell.AddElement(pSep1);

        // 2. METADATOS DE FACTURA (Tabla de 2 columnas)
        PdfPTable metaTable = new PdfPTable(2);
        metaTable.WidthPercentage = 100f;
        metaTable.SetWidths(new float[] { 32f, 68f });

        void AddMeta(string eti, string val)
        {
            PdfPCell c1 = new PdfPCell(new Phrase(eti, fontBold)) { Border = iTextSharp.text.Rectangle.NO_BORDER, PaddingTop = 1.5f, PaddingBottom = 1.5f };
            PdfPCell c2 = new PdfPCell(new Phrase(val, fontNormal)) { Border = iTextSharp.text.Rectangle.NO_BORDER, PaddingTop = 1.5f, PaddingBottom = 1.5f };
            metaTable.AddCell(c1);
            metaTable.AddCell(c2);
        }

        AddMeta("Factura :", lblNumeroFact.Text);
        AddMeta("Fecha   :", lblFecha.Text);
        AddMeta("Cliente :", lblCliente.Text);
        AddMeta("Atendido:", lblAtendidopor.Text);
        AddMeta("Pago    :", lblPago.Text);

        cardCell.AddElement(metaTable);

        Paragraph pSep2 = new Paragraph(new string('-', 42), fontNormal) { Alignment = Element.ALIGN_CENTER, SpacingBefore = 6f, SpacingAfter = 6f };
        cardCell.AddElement(pSep2);

        // 3. DETALLE DE PRODUCTOS (Tabla de 4 columnas)
        PdfPTable prodTable = new PdfPTable(4);
        prodTable.WidthPercentage = 100f;
        prodTable.SetWidths(new float[] { 46f, 14f, 20f, 20f });

        // Encabezados
        PdfPCell thProd = new PdfPCell(new Phrase("Producto", fontBold)) { Border = iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderWidth = 1f, PaddingBottom = 4f };
        PdfPCell thCant = new PdfPCell(new Phrase("Cant", fontBold)) { Border = iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderWidth = 1f, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 4f };
        PdfPCell thPrec = new PdfPCell(new Phrase("Precio", fontBold)) { Border = iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderWidth = 1f, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingBottom = 4f };
        PdfPCell thSub = new PdfPCell(new Phrase("SubTotal", fontBold)) { Border = iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderWidth = 1f, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingBottom = 4f };

        prodTable.AddCell(thProd);
        prodTable.AddCell(thCant);
        prodTable.AddCell(thPrec);
        prodTable.AddCell(thSub);

        decimal totalCalculado = 0;

        if (_dtFactura != null && _dtFactura.Rows.Count > 0)
        {
            foreach (DataRow row in _dtFactura.Rows)
            {
                string prod = row["Producto"]?.ToString() ?? "";
                string cant = row["Cantidad"]?.ToString() ?? "0";
                decimal precio = row["Precio"] != DBNull.Value ? Convert.ToDecimal(row["Precio"]) : 0m;
                decimal subtotal = row["Subtotal"] != DBNull.Value ? Convert.ToDecimal(row["Subtotal"]) : 0m;
                totalCalculado += subtotal;

                PdfPCell cProd = new PdfPCell(new Phrase(prod, fontNormal)) { Border = iTextSharp.text.Rectangle.NO_BORDER, PaddingTop = 2.5f, PaddingBottom = 2.5f };
                PdfPCell cCant = new PdfPCell(new Phrase(cant, fontNormal)) { Border = iTextSharp.text.Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, PaddingTop = 2.5f, PaddingBottom = 2.5f };
                PdfPCell cPrec = new PdfPCell(new Phrase(precio.ToString("N2"), fontNormal)) { Border = iTextSharp.text.Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingTop = 2.5f, PaddingBottom = 2.5f };
                PdfPCell cSub = new PdfPCell(new Phrase(subtotal.ToString("N2"), fontNormal)) { Border = iTextSharp.text.Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingTop = 2.5f, PaddingBottom = 2.5f };

                prodTable.AddCell(cProd);
                prodTable.AddCell(cCant);
                prodTable.AddCell(cPrec);
                prodTable.AddCell(cSub);
            }
        }

        cardCell.AddElement(prodTable);

        // 4. TOTAL EN RECUADRO
        Paragraph pSep3 = new Paragraph(new string('-', 42), fontNormal) { Alignment = Element.ALIGN_CENTER, SpacingBefore = 6f, SpacingAfter = 6f };
        cardCell.AddElement(pSep3);

        // Crear tabla para el recuadro del total
        PdfPTable totalTable = new PdfPTable(1);
        totalTable.WidthPercentage = 80f;
        totalTable.HorizontalAlignment = Element.ALIGN_CENTER;

        PdfPCell totalCell = new PdfPCell(new Phrase($"TOTAL A PAGAR: C$ {totalCalculado:N2}", fontTotal))
        {
            Border = iTextSharp.text.Rectangle.BOX,
            BorderColor = BaseColor.Black,
            BorderWidth = 2f,
            Padding = 12f,
            HorizontalAlignment = Element.ALIGN_CENTER,
            VerticalAlignment = Element.ALIGN_CENTER,
            BackgroundColor = new BaseColor(240, 240, 240)
        };

        totalTable.AddCell(totalCell);
        cardCell.AddElement(totalTable);

        Paragraph pSep4 = new Paragraph(new string('=', 42), fontNormal) { Alignment = Element.ALIGN_CENTER, SpacingBefore = 8f, SpacingAfter = 8f };
        cardCell.AddElement(pSep4);

        // 5. MENSAJE FINAL
        string mensajeGracias = string.IsNullOrWhiteSpace(lblGracias.Text)
            ? "Gracias por su compra lo esperamos\ncon un cochon la próxima vez que vuelva"
            : lblGracias.Text;

        Paragraph pGracias = new Paragraph(mensajeGracias, fontFooter) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 4f };
        cardCell.AddElement(pGracias);

        ticketCard.AddCell(cardCell);
        doc.Add(ticketCard);

        doc.Close();
        }
    }
    



        private void pnlComprobante_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlFactura_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FromComprobante_Load_1(object sender, EventArgs e) 
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_facturaCodigo))
                {
                    // Usar el presenter para obtener los datos en lugar de SelectQuery
                    _dtFactura = (_presenter ?? new FacturacionPresenter(this)).ObtenerDetalleFacturaPorCodigo(_facturaCodigo.Trim());
                    CargarFacturaEnLabels(_dtFactura);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el comprobante: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exporta la factura completa a Excel con formato profesional
        /// </summary>
        private void ExportarAExcel()
        {
            try
            {
                if (_dtFactura == null || _dtFactura.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos de factura para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string safeCodigo = string.Join("_", (_facturaCodigo ?? "Factura").Split(Path.GetInvalidFileNameChars()));
                if (string.IsNullOrWhiteSpace(safeCodigo)) safeCodigo = "Factura";

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
                    saveFileDialog.Title = "Guardar Factura en Excel";
                    saveFileDialog.FileName = $"Factura_{safeCodigo}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string rutaExcel = saveFileDialog.FileName;
                        GenerarExcelComprobante(rutaExcel);

                        DialogResult dr = MessageBox.Show(
                            "Factura exportada a Excel correctamente.\n\n¿Desea abrir el archivo ahora?",
                            "Éxito",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (dr == DialogResult.Yes)
                        {
                            Process.Start(new ProcessStartInfo(rutaExcel) { UseShellExecute = true });
                        }
                    }
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show(
                    $"No se pudo guardar el archivo. Verifique que no esté abierto en otra aplicación.\n\nDetalle: {ioEx.Message}",
                    "Archivo en uso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al exportar a Excel: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Genera un archivo Excel profesional con los datos de la factura
        /// </summary>
        private void GenerarExcelComprobante(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Factura");

                // Configurar ancho de columnas
                worksheet.Column(1).Width = 30;
                worksheet.Column(2).Width = 15;
                worksheet.Column(3).Width = 15;
                worksheet.Column(4).Width = 15;

                int row = 1;

                // ============ ENCABEZADO ============
                var titleCell = worksheet.Cell(row, 1);
                titleCell.Value = Nuevo_Proyecto.Services.Helpers.AppConfig.NombreNegocio;
                titleCell.Style.Font.Bold = true;
                titleCell.Style.Font.FontSize = 14;
                worksheet.Range(row, 1, row, 4).Merge();
                row++;

                var subtitleCell = worksheet.Cell(row, 1);
                subtitleCell.Value = "Fritangas, Asados y Bebidas Naturales";
                subtitleCell.Style.Font.FontSize = 11;
                worksheet.Range(row, 1, row, 4).Merge();
                row++;

                var directionCell = worksheet.Cell(row, 1);
                directionCell.Value = "Rivas, Nicaragua - Tel: 8472-4904";
                directionCell.Style.Font.FontSize = 10;
                worksheet.Range(row, 1, row, 4).Merge();
                row += 2;

                // ============ DATOS DE FACTURA ============
                worksheet.Cell(row, 1).Value = "N. Factura:";
                worksheet.Cell(row, 1).Style.Font.Bold = true;
                worksheet.Cell(row, 2).Value = lblNumeroFact.Text;
                worksheet.Cell(row, 3).Value = "Fecha:";
                worksheet.Cell(row, 3).Style.Font.Bold = true;
                worksheet.Cell(row, 4).Value = lblFecha.Text;
                row++;

                worksheet.Cell(row, 1).Value = "Cliente:";
                worksheet.Cell(row, 1).Style.Font.Bold = true;
                worksheet.Cell(row, 2).Value = lblCliente.Text;
                worksheet.Cell(row, 3).Value = "Atendido:";
                worksheet.Cell(row, 3).Style.Font.Bold = true;
                worksheet.Cell(row, 4).Value = lblAtendidopor.Text;
                row++;

                worksheet.Cell(row, 1).Value = "Forma de Pago:";
                worksheet.Cell(row, 1).Style.Font.Bold = true;
                worksheet.Cell(row, 2).Value = lblPago.Text;
                row += 2;

                // ============ TABLA DE PRODUCTOS ============
                var headerRow = row;
                var headerCell1 = worksheet.Cell(row, 1);
                headerCell1.Value = "Producto";
                headerCell1.Style.Font.Bold = true;
                headerCell1.Style.Fill.BackgroundColor = XLColor.LightGray;

                var headerCell2 = worksheet.Cell(row, 2);
                headerCell2.Value = "Cantidad";
                headerCell2.Style.Font.Bold = true;
                headerCell2.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerCell2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                var headerCell3 = worksheet.Cell(row, 3);
                headerCell3.Value = "Precio";
                headerCell3.Style.Font.Bold = true;
                headerCell3.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerCell3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                var headerCell4 = worksheet.Cell(row, 4);
                headerCell4.Value = "Subtotal";
                headerCell4.Style.Font.Bold = true;
                headerCell4.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerCell4.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                row++;

                // Poblar productos
                decimal totalGeneral = 0;
                foreach (DataRow dataRow in _dtFactura.Rows)
                {
                    string producto = dataRow["Producto"]?.ToString() ?? "";
                    int cantidad = dataRow["Cantidad"] != DBNull.Value ? Convert.ToInt32(dataRow["Cantidad"]) : 0;
                    decimal precio = dataRow["Precio"] != DBNull.Value ? Convert.ToDecimal(dataRow["Precio"]) : 0m;
                    decimal subtotal = dataRow["Subtotal"] != DBNull.Value ? Convert.ToDecimal(dataRow["Subtotal"]) : 0m;

                    worksheet.Cell(row, 1).Value = producto;
                    worksheet.Cell(row, 2).Value = cantidad;
                    worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    worksheet.Cell(row, 3).Value = precio;
                    worksheet.Cell(row, 3).Style.NumberFormat.Format = "#,##0.00";
                    worksheet.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    worksheet.Cell(row, 4).Value = subtotal;
                    worksheet.Cell(row, 4).Style.NumberFormat.Format = "#,##0.00";
                    worksheet.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    totalGeneral += subtotal;
                    row++;
                }

                // ============ TOTALES ============
                row++;
                var totalLabelCell = worksheet.Cell(row, 3);
                totalLabelCell.Value = "TOTAL A PAGAR:";
                totalLabelCell.Style.Font.Bold = true;
                totalLabelCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                var totalCell = worksheet.Cell(row, 4);
                totalCell.Value = totalGeneral;
                totalCell.Style.Font.Bold = true;
                totalCell.Style.NumberFormat.Format = "#,##0.00";
                totalCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                totalCell.Style.Fill.BackgroundColor = XLColor.Yellow;

                // Guardar archivo
                workbook.SaveAs(filePath);
            }
        }

        private void ExportarAPDFParaImprimir()
        {
            try
            {
                // Asegurar que tengamos los datos de la factura cargados
                if (_dtFactura == null || _dtFactura.Rows.Count == 0)
                {
                    _dtFactura = _presenter.ObtenerDetalleFacturaPorCodigo(_facturaCodigo?.Trim() ?? "");
                    if (_dtFactura == null || _dtFactura.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay datos disponibles para imprimir esta factura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CargarFacturaEnLabels(_dtFactura);
                }

                // Generar PDF temporal
                string tempPath = System.IO.Path.GetTempPath();
                string safeCodigo = string.Join("_", (_facturaCodigo ?? "Factura").Split(System.IO.Path.GetInvalidFileNameChars()));
                if (string.IsNullOrWhiteSpace(safeCodigo)) safeCodigo = "Factura";

                string rutaPdf = System.IO.Path.Combine(tempPath, $"Comprobante_{safeCodigo}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                // Generar PDF
                GenerarPdfComprobante(rutaPdf);

                // Abrir PDF con el visor predeterminado del sistema
                Process.Start(new ProcessStartInfo(rutaPdf) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al preparar la impresión.\n\nDetalle: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

    }
}
