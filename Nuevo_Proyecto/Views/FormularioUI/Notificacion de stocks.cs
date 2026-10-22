using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;

namespace Nuevo_Proyecto.Views.FormularioUI
{
    public partial class Notificacion_de_stocks : Form
    {
        private readonly NotificacionStockCriticoRepository _repositorio;

        public Notificacion_de_stocks()
        {
            InitializeComponent();
            _repositorio = new NotificacionStockCriticoRepository();
        }

        private void Notificacion_de_stocks_Load(object sender, EventArgs e)
        {
            CargarProductosEnStockCritico();
        }

        private void CargarProductosEnStockCritico()
        {
            try
            {
                var notificaciones = _repositorio.ObtenerNotificacionesCriticas();

                // Limpiar DataGridView
                DgvProductosEnStocksCritico.DataSource = null;
                DgvProductosEnStocksCritico.Rows.Clear();

                if (notificaciones.Count > 0)
                {
                    // Configurar las columnas del DataGridView
                    DgvProductosEnStocksCritico.AutoGenerateColumns = false;

                    // Limpiar cualquier columna anterior
                    DgvProductosEnStocksCritico.Columns.Clear();

                    // Agregar columnas
                    DgvProductosEnStocksCritico.Columns.Add("ProductoID", "ID Producto");
                    DgvProductosEnStocksCritico.Columns.Add("NombreProducto", "Nombre del Producto");
                    DgvProductosEnStocksCritico.Columns.Add("StockActual", "Stock Actual");
                    DgvProductosEnStocksCritico.Columns.Add("StockMinimo", "Stock Mínimo");
                    DgvProductosEnStocksCritico.Columns.Add("FechaRegistro", "Fecha de Registro");

                    // Poblar filas
                    foreach (var notificacion in notificaciones)
                    {
                        DgvProductosEnStocksCritico.Rows.Add(
                            notificacion.ProductoID,
                            notificacion.NombreProducto,
                            notificacion.StockActual,
                            notificacion.StockMinimo,
                            notificacion.FechaRegistro.ToString("yyyy-MM-dd HH:mm")
                        );
                    }

                    // Actualizar etiqueta de conteo
                    LblResultado.Text = notificaciones.Count.ToString();
                }
                else
                {
                    LblResultado.Text = "0";
                    MensajeHelper.Informacion("No hay productos con stock crítico en este momento.");
                }
            }
            catch (Exception ex)
            {
                MensajeHelper.ErrorSistema($"Error al cargar notificaciones de stock: {ex.Message}");
                LblResultado.Text = "0";
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
