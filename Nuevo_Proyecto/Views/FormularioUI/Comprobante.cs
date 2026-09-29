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

            // Columnas de detalle
            StringBuilder productos = new StringBuilder();
            StringBuilder cantidades = new StringBuilder();
            StringBuilder precios = new StringBuilder();
            StringBuilder subtotales = new StringBuilder();

            decimal total = 0;

            foreach (DataRow row in dt.Rows)
            {
                string prod = row["Producto"]?.ToString() ?? "";
                string cant = row["Cantidad"]?.ToString() ?? "0";
                decimal precio = row["Precio"] != DBNull.Value ? Convert.ToDecimal(row["Precio"]) : 0m;
                decimal subtotal = row["Subtotal"] != DBNull.Value ? Convert.ToDecimal(row["Subtotal"]) : 0m;

                total += subtotal;

                productos.AppendLine(prod);
                cantidades.AppendLine(cant);
                precios.AppendLine(precio.ToString("N2"));
                subtotales.AppendLine(subtotal.ToString("N2"));
            }

            lblProductoFinal.Text = productos.ToString();
            lblCantFinal.Text = cantidades.ToString();
            lblPrecioFinal.Text = precios.ToString();
            lblSubTotalFinal.Text = subtotales.ToString();

            // Total general
            lblTotal.Text = $"TOTAL A PAGAR: C$ {total:N2}";
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            // Reutiliza la exportación a PDF para que el usuario pueda visualizarlo e imprimirlo
            btnExportar_Click(sender, e);

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
        Paragraph pTitulo = new Paragraph("ASADOS LA FLACA", fontTitulo) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 2f };
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

        // 4. TOTAL
        Paragraph pSep3 = new Paragraph(new string('-', 42), fontNormal) { Alignment = Element.ALIGN_CENTER, SpacingBefore = 6f, SpacingAfter = 6f };
        cardCell.AddElement(pSep3);

        Paragraph pTotal = new Paragraph($"TOTAL A PAGAR: C$ {totalCalculado:N2}", fontTotal) { Alignment = Element.ALIGN_RIGHT, SpacingAfter = 6f };
        cardCell.AddElement(pTotal);

        Paragraph pSep4 = new Paragraph(new string('=', 42), fontNormal) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 8f };
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

    }
}
