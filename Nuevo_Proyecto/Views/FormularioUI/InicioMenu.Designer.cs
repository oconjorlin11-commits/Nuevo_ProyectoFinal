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
            LblValorInventario = new Label();
            grboxFacturaSemanales = new GroupBox();
            LblFacturasSemanales = new Label();
            grboxVentasSemanales = new GroupBox();
            VentasSemanales = new Label();
            grboxVentasHoy = new GroupBox();
            LblVentasHoy = new Label();
            panel1.SuspendLayout();
            grboxUltimasFacturas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGriUltimasFacturas).BeginInit();
            grboxProductosStockBajo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGriProductosBajos).BeginInit();
            grboxValorInventario.SuspendLayout();
            grboxFacturaSemanales.SuspendLayout();
            grboxVentasSemanales.SuspendLayout();
            grboxVentasHoy.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(153, 40, 35);
            label1.Location = new Point(43, 31);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(416, 51);
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
            panel1.Location = new Point(13, 106);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1759, 1014);
            panel1.TabIndex = 1;
            // 
            // grboxUltimasFacturas
            // 
            grboxUltimasFacturas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grboxUltimasFacturas.BackColor = Color.FromArgb(250, 247, 241);
            grboxUltimasFacturas.Controls.Add(dataGriUltimasFacturas);
            grboxUltimasFacturas.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxUltimasFacturas.ForeColor = Color.FromArgb(38, 38, 38);
            grboxUltimasFacturas.Location = new Point(915, 374);
            grboxUltimasFacturas.Margin = new Padding(4);
            grboxUltimasFacturas.Name = "grboxUltimasFacturas";
            grboxUltimasFacturas.Padding = new Padding(4);
            grboxUltimasFacturas.Size = new Size(827, 636);
            grboxUltimasFacturas.TabIndex = 3;
            grboxUltimasFacturas.TabStop = false;
            grboxUltimasFacturas.Text = "Ultimas Facturas";
            grboxUltimasFacturas.Enter += groupBox6_Enter;
            // 
            // dataGriUltimasFacturas
            // 
            dataGriUltimasFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGriUltimasFacturas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGriUltimasFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGriUltimasFacturas.Location = new Point(0, 67);
            dataGriUltimasFacturas.Margin = new Padding(4);
            dataGriUltimasFacturas.Name = "dataGriUltimasFacturas";
            dataGriUltimasFacturas.RowHeadersWidth = 62;
            dataGriUltimasFacturas.Size = new Size(827, 573);
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
            grboxProductosStockBajo.Location = new Point(21, 374);
            grboxProductosStockBajo.Margin = new Padding(4);
            grboxProductosStockBajo.Name = "grboxProductosStockBajo";
            grboxProductosStockBajo.Padding = new Padding(4);
            grboxProductosStockBajo.Size = new Size(856, 636);
            grboxProductosStockBajo.TabIndex = 2;
            grboxProductosStockBajo.TabStop = false;
            grboxProductosStockBajo.Text = "Productos Stock Bajos";
            // 
            // dataGriProductosBajos
            // 
            dataGriProductosBajos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGriProductosBajos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGriProductosBajos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGriProductosBajos.Location = new Point(0, 67);
            dataGriProductosBajos.Margin = new Padding(4);
            dataGriProductosBajos.Name = "dataGriProductosBajos";
            dataGriProductosBajos.RowHeadersWidth = 62;
            dataGriProductosBajos.Size = new Size(856, 569);
            dataGriProductosBajos.TabIndex = 0;
            dataGriProductosBajos.CellContentClick += dataGriProductosBajos_CellContentClick;
            // 
            // grboxValorInventario
            // 
            grboxValorInventario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grboxValorInventario.BackColor = Color.FromArgb(250, 247, 241);
            grboxValorInventario.Controls.Add(LblValorInventario);
            grboxValorInventario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxValorInventario.ForeColor = Color.FromArgb(38, 38, 38);
            grboxValorInventario.Location = new Point(1382, 33);
            grboxValorInventario.Margin = new Padding(4);
            grboxValorInventario.Name = "grboxValorInventario";
            grboxValorInventario.Padding = new Padding(4);
            grboxValorInventario.Size = new Size(360, 311);
            grboxValorInventario.TabIndex = 1;
            grboxValorInventario.TabStop = false;
            grboxValorInventario.Text = "Valor Inventario";
            // 
            // LblValorInventario
            // 
            LblValorInventario.AutoSize = true;
            LblValorInventario.Location = new Point(98, 89);
            LblValorInventario.Name = "LblValorInventario";
            LblValorInventario.Size = new Size(107, 45);
            LblValorInventario.TabIndex = 3;
            LblValorInventario.Text = "label5";
            // 
            // grboxFacturaSemanales
            // 
            grboxFacturaSemanales.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grboxFacturaSemanales.BackColor = Color.FromArgb(250, 247, 241);
            grboxFacturaSemanales.Controls.Add(LblFacturasSemanales);
            grboxFacturaSemanales.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxFacturaSemanales.ForeColor = Color.FromArgb(38, 38, 38);
            grboxFacturaSemanales.Location = new Point(992, 33);
            grboxFacturaSemanales.Margin = new Padding(4);
            grboxFacturaSemanales.Name = "grboxFacturaSemanales";
            grboxFacturaSemanales.Padding = new Padding(4);
            grboxFacturaSemanales.Size = new Size(360, 311);
            grboxFacturaSemanales.TabIndex = 1;
            grboxFacturaSemanales.TabStop = false;
            grboxFacturaSemanales.Text = "Facturas Semanales";
            // 
            // LblFacturasSemanales
            // 
            LblFacturasSemanales.AutoSize = true;
            LblFacturasSemanales.Location = new Point(122, 101);
            LblFacturasSemanales.Name = "LblFacturasSemanales";
            LblFacturasSemanales.Size = new Size(107, 45);
            LblFacturasSemanales.TabIndex = 2;
            LblFacturasSemanales.Text = "label4";
            // 
            // grboxVentasSemanales
            // 
            grboxVentasSemanales.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grboxVentasSemanales.BackColor = Color.FromArgb(250, 247, 241);
            grboxVentasSemanales.Controls.Add(VentasSemanales);
            grboxVentasSemanales.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxVentasSemanales.ForeColor = Color.FromArgb(38, 38, 38);
            grboxVentasSemanales.Location = new Point(412, 33);
            grboxVentasSemanales.Margin = new Padding(4);
            grboxVentasSemanales.Name = "grboxVentasSemanales";
            grboxVentasSemanales.Padding = new Padding(4);
            grboxVentasSemanales.Size = new Size(360, 311);
            grboxVentasSemanales.TabIndex = 1;
            grboxVentasSemanales.TabStop = false;
            grboxVentasSemanales.Text = "Ventas Semanales";
            // 
            // VentasSemanales
            // 
            VentasSemanales.AutoSize = true;
            VentasSemanales.Location = new Point(111, 101);
            VentasSemanales.Name = "VentasSemanales";
            VentasSemanales.Size = new Size(107, 45);
            VentasSemanales.TabIndex = 1;
            VentasSemanales.Text = "label3";
            // 
            // grboxVentasHoy
            // 
            grboxVentasHoy.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grboxVentasHoy.BackColor = Color.FromArgb(250, 247, 241);
            grboxVentasHoy.Controls.Add(LblVentasHoy);
            grboxVentasHoy.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grboxVentasHoy.ForeColor = Color.FromArgb(38, 38, 38);
            grboxVentasHoy.Location = new Point(21, 33);
            grboxVentasHoy.Margin = new Padding(4);
            grboxVentasHoy.Name = "grboxVentasHoy";
            grboxVentasHoy.Padding = new Padding(4);
            grboxVentasHoy.Size = new Size(360, 311);
            grboxVentasHoy.TabIndex = 0;
            grboxVentasHoy.TabStop = false;
            grboxVentasHoy.Text = "Ventas de Hoy";
            // 
            // LblVentasHoy
            // 
            LblVentasHoy.AutoSize = true;
            LblVentasHoy.Location = new Point(114, 101);
            LblVentasHoy.Name = "LblVentasHoy";
            LblVentasHoy.Size = new Size(107, 45);
            LblVentasHoy.TabIndex = 0;
            LblVentasHoy.Text = "label2";
            // 
            // InicioMenu
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1785, 1133);
            Controls.Add(panel1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "InicioMenu";
            Text = "InicioMenu";
            Load += InicioMenu_Load;
            panel1.ResumeLayout(false);
            grboxUltimasFacturas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGriUltimasFacturas).EndInit();
            grboxProductosStockBajo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGriProductosBajos).EndInit();
            grboxValorInventario.ResumeLayout(false);
            grboxValorInventario.PerformLayout();
            grboxFacturaSemanales.ResumeLayout(false);
            grboxFacturaSemanales.PerformLayout();
            grboxVentasSemanales.ResumeLayout(false);
            grboxVentasSemanales.PerformLayout();
            grboxVentasHoy.ResumeLayout(false);
            grboxVentasHoy.PerformLayout();
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
        private Label LblValorInventario;
        private Label LblFacturasSemanales;
        private Label VentasSemanales;
        private Label LblVentasHoy;
    }
}