namespace Nuevo_Proyecto.Models.Views
{
    partial class InicioMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            panel1 = new Panel();
            grboxUltimasFacturas = new GroupBox();
            dataGriUltimasFacturas = new DataGridView();
            grboxProductosStockBajo = new GroupBox();
            dataGriProductosBajos = new DataGridView();
            grboxValorInventario = new GroupBox();
            grboxFacturaSemanales = new GroupBox();
            grboxVentasSemanales = new GroupBox();
            grboxVentasHoy = new GroupBox();
            panel1.SuspendLayout();
            grboxUltimasFacturas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGriUltimasFacturas).BeginInit();
            grboxProductosStockBajo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGriProductosBajos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(153, 40, 35);
            label1.Location = new Point(33, 24);
            label1.Name = "label1";
            label1.Size = new Size(305, 38);
            label1.TabIndex = 0;
            label1.Text = "Resumen Del Negocio";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(grboxUltimasFacturas);
            panel1.Controls.Add(grboxProductosStockBajo);
            panel1.Controls.Add(grboxValorInventario);
            panel1.Controls.Add(grboxFacturaSemanales);
            panel1.Controls.Add(grboxVentasSemanales);
            panel1.Controls.Add(grboxVentasHoy);
            panel1.Location = new Point(68, 108);
            panel1.Name = "panel1";
            panel1.Size = new Size(1210, 692);
            panel1.TabIndex = 1;
            // 
            // grboxUltimasFacturas
            // 
            grboxUltimasFacturas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grboxUltimasFacturas.BackColor = Color.FromArgb(250, 247, 241);
            grboxUltimasFacturas.Controls.Add(dataGriUltimasFacturas);
            grboxUltimasFacturas.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxUltimasFacturas.ForeColor = Color.FromArgb(38, 38, 38);
            grboxUltimasFacturas.Location = new Point(619, 292);
            grboxUltimasFacturas.Name = "grboxUltimasFacturas";
            grboxUltimasFacturas.Size = new Size(578, 397);
            grboxUltimasFacturas.TabIndex = 3;
            grboxUltimasFacturas.TabStop = false;
            grboxUltimasFacturas.Text = "Ultimas Facturas";
            grboxUltimasFacturas.Enter += groupBox6_Enter;
            // 
            // dataGriUltimasFacturas
            // 
            dataGriUltimasFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGriUltimasFacturas.Location = new Point(0, 52);
            dataGriUltimasFacturas.Name = "dataGriUltimasFacturas";
            dataGriUltimasFacturas.RowHeadersWidth = 62;
            dataGriUltimasFacturas.Size = new Size(578, 345);
            dataGriUltimasFacturas.TabIndex = 1;
            dataGriUltimasFacturas.CellContentClick += dataGriUltimasFacturas_CellContentClick;
            // 
            // grboxProductosStockBajo
            // 
            grboxProductosStockBajo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grboxProductosStockBajo.BackColor = Color.FromArgb(250, 247, 241);
            grboxProductosStockBajo.Controls.Add(dataGriProductosBajos);
            grboxProductosStockBajo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxProductosStockBajo.ForeColor = Color.FromArgb(38, 38, 38);
            grboxProductosStockBajo.Location = new Point(16, 292);
            grboxProductosStockBajo.Name = "grboxProductosStockBajo";
            grboxProductosStockBajo.Size = new Size(578, 397);
            grboxProductosStockBajo.TabIndex = 2;
            grboxProductosStockBajo.TabStop = false;
            grboxProductosStockBajo.Text = "Productos Stock Bajos";
            // 
            // dataGriProductosBajos
            // 
            dataGriProductosBajos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGriProductosBajos.Location = new Point(0, 52);
            dataGriProductosBajos.Name = "dataGriProductosBajos";
            dataGriProductosBajos.RowHeadersWidth = 62;
            dataGriProductosBajos.Size = new Size(578, 348);
            dataGriProductosBajos.TabIndex = 0;
            dataGriProductosBajos.CellContentClick += dataGriProductosBajos_CellContentClick;
            // 
            // grboxValorInventario
            // 
            grboxValorInventario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grboxValorInventario.BackColor = Color.FromArgb(250, 247, 241);
            grboxValorInventario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxValorInventario.ForeColor = Color.FromArgb(38, 38, 38);
            grboxValorInventario.Location = new Point(920, 26);
            grboxValorInventario.Name = "grboxValorInventario";
            grboxValorInventario.Size = new Size(277, 165);
            grboxValorInventario.TabIndex = 1;
            grboxValorInventario.TabStop = false;
            grboxValorInventario.Text = "Valor Inventario";
            // 
            // grboxFacturaSemanales
            // 
            grboxFacturaSemanales.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grboxFacturaSemanales.BackColor = Color.FromArgb(250, 247, 241);
            grboxFacturaSemanales.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxFacturaSemanales.ForeColor = Color.FromArgb(38, 38, 38);
            grboxFacturaSemanales.Location = new Point(620, 26);
            grboxFacturaSemanales.Name = "grboxFacturaSemanales";
            grboxFacturaSemanales.Size = new Size(277, 165);
            grboxFacturaSemanales.TabIndex = 1;
            grboxFacturaSemanales.TabStop = false;
            grboxFacturaSemanales.Text = "Facturas Semanales";
            // 
            // grboxVentasSemanales
            // 
            grboxVentasSemanales.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grboxVentasSemanales.BackColor = Color.FromArgb(250, 247, 241);
            grboxVentasSemanales.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxVentasSemanales.ForeColor = Color.FromArgb(38, 38, 38);
            grboxVentasSemanales.Location = new Point(317, 26);
            grboxVentasSemanales.Name = "grboxVentasSemanales";
            grboxVentasSemanales.Size = new Size(277, 165);
            grboxVentasSemanales.TabIndex = 1;
            grboxVentasSemanales.TabStop = false;
            grboxVentasSemanales.Text = "Ventas Semanales";
            // 
            // grboxVentasHoy
            // 
            grboxVentasHoy.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grboxVentasHoy.BackColor = Color.FromArgb(250, 247, 241);
            grboxVentasHoy.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxVentasHoy.ForeColor = Color.FromArgb(38, 38, 38);
            grboxVentasHoy.Location = new Point(16, 26);
            grboxVentasHoy.Name = "grboxVentasHoy";
            grboxVentasHoy.Size = new Size(277, 165);
            grboxVentasHoy.TabIndex = 0;
            grboxVentasHoy.TabStop = false;
            grboxVentasHoy.Text = "Ventas de Hoy";
            // 
            // InicioMenu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1373, 885);
            Controls.Add(panel1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "InicioMenu";
            Text = "InicioMenu";
            Load += InicioMenu_Load;
            panel1.ResumeLayout(false);
            grboxUltimasFacturas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGriUltimasFacturas).EndInit();
            grboxProductosStockBajo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGriProductosBajos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void dataGriProductosBajos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dataGriUltimasFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private GroupBox grboxVentasHoy;
        private GroupBox grboxUltimasFacturas;
        private GroupBox grboxProductosStockBajo;
        private GroupBox grboxValorInventario;
        private GroupBox grboxFacturaSemanales;
        private GroupBox grboxVentasSemanales;
        private DataGridView dataGriUltimasFacturas;
        private DataGridView dataGriProductosBajos;
    }
}