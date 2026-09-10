using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Nuevo_Proyecto.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;



namespace Nuevo_Proyecto.Models.Views
{
    public partial class Reportes : Form
    {

        private SelectQuery selectQuery; // tu clase que hereda de DataConnection


        public Reportes()
        {
            InitializeComponent();
            selectQuery = new SelectQuery();

        }

        private void ExportarMovimientosAExcel()
        {
            if (dataGridReportes.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Guardar reporte de inventario",
                FileName = "ReporteInventario.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Reporte Inventario");

                        // Encabezados
                        for (int i = 0; i < dataGridReportes.Columns.Count; i++)
                        {
                            ws.Cell(1, i + 1).Value = dataGridReportes.Columns[i].HeaderText;
                        }

                        // Datos
                        for (int i = 0; i < dataGridReportes.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridReportes.Columns.Count; j++)
                            {
                                ws.Cell(i + 2, j + 1).Value = dataGridReportes.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        // Ajustar columnas automáticamente
                        ws.Columns().AdjustToContents();

                        // Guardar en la ruta seleccionada
                        wb.SaveAs(saveFileDialog.FileName);

                        MessageBox.Show("Reporte exportado correctamente a: " + saveFileDialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel: " + ex.Message);
                }
            }
        }

        private void CargarReportes(DateTime? desde, DateTime? hasta)
        {
            // Obtener datos desde la vista con filtro de fechas
            DataTable dt = selectQuery.ObtenerMovimientosInventario(desde, hasta);
            dataGridReportes.DataSource = dt;

            // Ajustar encabezados de columnas principales
            if (dataGridReportes.Columns.Contains("MovimientoID"))
                dataGridReportes.Columns["MovimientoID"].HeaderText = "ID Movimiento";

            if (dataGridReportes.Columns.Contains("Fecha"))
                dataGridReportes.Columns["Fecha"].HeaderText = "Fecha";

            if (dataGridReportes.Columns.Contains("Tipo"))
                dataGridReportes.Columns["Tipo"].HeaderText = "Tipo";

            if (dataGridReportes.Columns.Contains("Cantidad"))
                dataGridReportes.Columns["Cantidad"].HeaderText = "Cantidad";

            if (dataGridReportes.Columns.Contains("StockAnterior"))
                dataGridReportes.Columns["StockAnterior"].HeaderText = "Stock Anterior";

            if (dataGridReportes.Columns.Contains("StockNuevo"))
                dataGridReportes.Columns["StockNuevo"].HeaderText = "Stock Nuevo";

            if (dataGridReportes.Columns.Contains("Observacion"))
                dataGridReportes.Columns["Observacion"].HeaderText = "Observación";

            if (dataGridReportes.Columns.Contains("NombreProducto"))
                dataGridReportes.Columns["NombreProducto"].HeaderText = "Producto";

            if (dataGridReportes.Columns.Contains("NombreEmpleado"))
                dataGridReportes.Columns["NombreEmpleado"].HeaderText = "Empleado";

            if (dataGridReportes.Columns.Contains("PrecioVenta"))
            {
                dataGridReportes.Columns["PrecioVenta"].HeaderText = "Precio Venta";
                dataGridReportes.Columns["PrecioVenta"].DefaultCellStyle.Format = "C2";
            }

            if (dataGridReportes.Columns.Contains("ValorInventario"))
            {
                dataGridReportes.Columns["ValorInventario"].HeaderText = "Valor Inventario";
                dataGridReportes.Columns["ValorInventario"].DefaultCellStyle.Format = "C2";
            }

            // Ajustar encabezados de métricas de facturación (solo pagadas)
            if (dataGridReportes.Columns.Contains("CantidadFacturasEmitidas"))
                dataGridReportes.Columns["CantidadFacturasEmitidas"].HeaderText = "Facturas Emitidas (Pagadas)";

            if (dataGridReportes.Columns.Contains("CantidadProductosDescontados"))
                dataGridReportes.Columns["CantidadProductosDescontados"].HeaderText = "Productos Descontados (Pagadas)";
        }

        private void groupBoxDatosPedido_Enter(object sender, EventArgs e)
        {

        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            CargarReportes(null, null); CargarReportes(null, null);
        }

        private void btnExportaReportes_Click(object sender, EventArgs e)
        {
            ExportarMovimientosAPdf();
        }

        private void btnConsultarReport_Click(object sender, EventArgs e)
        {
            DateTime? desde = dateDesde.Value.Date;
            DateTime? hasta = dateHasta.Value.Date;

            CargarReportes(desde, hasta);
        }

        private void ExportarMovimientosAPdf()
        {
            if (dataGridReportes.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos PDF (*.pdf)|*.pdf|Archivos Excel (*.xlsx)|*.xlsx",
                Title = "Guardar Reporte de Inventario",
                FileName = $"ReporteInventario_{DateTime.Now:yyyyMMdd}.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(saveFileDialog.FileName).ToLowerInvariant();

                if (extension == ".xlsx")
                {
                    try
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add("Reporte Inventario");
                            for (int i = 0; i < dataGridReportes.Columns.Count; i++)
                            {
                                ws.Cell(1, i + 1).Value = dataGridReportes.Columns[i].HeaderText;
                            }

                            for (int i = 0; i < dataGridReportes.Rows.Count; i++)
                            {
                                for (int j = 0; j < dataGridReportes.Columns.Count; j++)
                                {
                                    ws.Cell(i + 2, j + 1).Value = dataGridReportes.Rows[i].Cells[j].Value?.ToString();
                                }
                            }

                            ws.Columns().AdjustToContents();
                            wb.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Reporte exportado correctamente a: " + saveFileDialog.FileName, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                try
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        // Landscape (horizontal) para que quepan todas las columnas
                        iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 20f, 20f, 20f, 20f);
                        PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        var fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                        var fontSub = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.DarkGray);
                        var fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.White);
                        var fontData = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                        doc.Add(new iTextSharp.text.Paragraph("ASADOS LA FLACA", fontTitulo) { Alignment = iTextSharp.text.Element.ALIGN_CENTER });
                        doc.Add(new iTextSharp.text.Paragraph("Reporte de Movimientos de Inventario", fontSub) { Alignment = iTextSharp.text.Element.ALIGN_CENTER, SpacingAfter = 10f });

                        PdfPTable table = new PdfPTable(dataGridReportes.Columns.Count)
                        {
                            WidthPercentage = 100f
                        };

                        for (int i = 0; i < dataGridReportes.Columns.Count; i++)
                        {
                            PdfPCell cell = new PdfPCell(new iTextSharp.text.Phrase(dataGridReportes.Columns[i].HeaderText, fontHeader))
                            {
                                BackgroundColor = new BaseColor(153, 40, 35),
                                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                                Padding = 4f
                            };
                            table.AddCell(cell);
                        }

                        for (int i = 0; i < dataGridReportes.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridReportes.Columns.Count; j++)
                            {
                                string val = dataGridReportes.Rows[i].Cells[j].Value?.ToString() ?? "";
                                PdfPCell cell = new PdfPCell(new iTextSharp.text.Phrase(val, fontData))
                                {
                                    Padding = 3f,
                                    HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                                };
                                table.AddCell(cell);
                            }
                        }

                        doc.Add(table);
                        doc.Close();
                    }

                    DialogResult dr = MessageBox.Show("Reporte exportado a PDF correctamente.\n\n¿Desea abrir el archivo ahora?", "Éxito", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                    }
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show($"El archivo no se pudo guardar porque está en uso por otra aplicación.\n\nDetalle: {ioEx.Message}", "Archivo en uso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
