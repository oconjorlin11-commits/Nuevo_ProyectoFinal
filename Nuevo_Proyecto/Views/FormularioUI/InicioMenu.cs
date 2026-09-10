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
    public partial class InicioMenu : Form
    {
        public InicioMenu()
        {
            InitializeComponent();
        }

        // Refresca todo el tablero del menu

        public void RefrescarMenu()
        {
          
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void InicioMenu_Load(object sender, EventArgs e)
        {

        }

        private void ConfigurarDataGriProductosBajos()
        {
            dataGriProductosBajos.AutoGenerateColumns = false;
            dataGriProductosBajos.Columns.Clear();

            dataGriProductosBajos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Codigo",
                HeaderText = "Código",
                DataPropertyName = "Codigo"
            });

            dataGriProductosBajos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Producto",
                DataPropertyName = "Nombre"
            });

            dataGriProductosBajos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock Actual",
                DataPropertyName = "Stock"
            });

            dataGriProductosBajos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockMinimo",
                HeaderText = "Stock Mínimo",
                DataPropertyName = "StockMinimo"
            });

            dataGriProductosBajos.ReadOnly = true;
            dataGriProductosBajos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGriProductosBajos.MultiSelect = false;

        }

        private void ConfigurarDataGriUltimasFacturas()
        {
            dataGriUltimasFacturas.AutoGenerateColumns = false;
            dataGriUltimasFacturas.Columns.Clear();

            dataGriUltimasFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Numero",
                HeaderText = "Código Factura",
                DataPropertyName = "Numero"
            });

            dataGriUltimasFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha",
                DataPropertyName = "Fecha"
            });

            dataGriUltimasFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                DefaultCellStyle = { Format = "C2" } // formato moneda
            });

            dataGriUltimasFacturas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Empleado",
                HeaderText = "Empleado",
                DataPropertyName = "Empleado"
            });

            dataGriUltimasFacturas.ReadOnly = true;
            dataGriUltimasFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGriUltimasFacturas.MultiSelect = false;
        }
    }
}
