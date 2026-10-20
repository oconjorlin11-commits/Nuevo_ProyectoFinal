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
            groupBox1 = new GroupBox();
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
            groupBox1.SuspendLayout();
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
            label1.Location = new Point(21, 23);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(266, 32);
            label1.TabIndex = 0;
            label1.Text = "Resumen Del Negocio";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(grboxProductosStockBajo);
            panel1.Controls.Add(grboxValorInventario);
            panel1.Controls.Add(grboxFacturaSemanales);
            panel1.Controls.Add(grboxVentasSemanales);
            panel1.Controls.Add(grboxVentasHoy);
            panel1.Location = new Point(8, 66);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1082, 615);
            panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.FromArgb(250, 247, 241);
            groupBox1.Controls.Add(dataGriUltimasFacturas);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            groupBox1.Location = new Point(15, 431);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1055, 162);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ultimas Facturas";
            // 
            // dataGriUltimasFacturas
            // 
            dataGriUltimasFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGriUltimasFacturas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGriUltimasFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGriUltimasFacturas.Dock = DockStyle.Fill;
            dataGriUltimasFacturas.Location = new Point(3, 30);
            dataGriUltimasFacturas.Margin = new Padding(2);
            dataGriUltimasFacturas.Name = "dataGriUltimasFacturas";
            dataGriUltimasFacturas.RowHeadersWidth = 62;
            dataGriUltimasFacturas.Size = new Size(1049, 129);
            dataGriUltimasFacturas.TabIndex = 1;
            dataGriUltimasFacturas.CellContentClick += dataGriUltimasFacturas_CellContentClick;
            // 
            // grboxProductosStockBajo
            // 
            grboxProductosStockBajo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grboxProductosStockBajo.BackColor = Color.FromArgb(250, 247, 241);
            grboxProductosStockBajo.Controls.Add(dataGriProductosBajos);
            grboxProductosStockBajo.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            grboxProductosStockBajo.ForeColor = Color.FromArgb(38, 38, 38);
            grboxProductosStockBajo.Location = new Point(13, 234);
            grboxProductosStockBajo.Margin = new Padding(2);
            grboxProductosStockBajo.Name = "grboxProductosStockBajo";
            grboxProductosStockBajo.Padding = new Padding(2);
            grboxProductosStockBajo.Size = new Size(1059, 169);
            grboxProductosStockBajo.TabIndex = 2;
            grboxProductosStockBajo.TabStop = false;
            grboxProductosStockBajo.Text = "Productos Stock Bajos ";
            // 
            // dataGriProductosBajos
            // 
            dataGriProductosBajos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGriProductosBajos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGriProductosBajos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGriProductosBajos.Dock = DockStyle.Fill;
            dataGriProductosBajos.Location = new Point(2, 29);
            dataGriProductosBajos.Margin = new Padding(2);
            dataGriProductosBajos.Name = "dataGriProductosBajos";
            dataGriProductosBajos.RowHeadersWidth = 62;
            dataGriProductosBajos.Size = new Size(1055, 138);
            dataGriProductosBajos.TabIndex = 0;
            dataGriProductosBajos.CellContentClick += dataGriProductosBajos_CellContentClick;
            // 
            // grboxValorInventario
            // 
            grboxValorInventario.BackColor = Color.FromArgb(250, 247, 241);
            grboxValorInventario.Controls.Add(LblValorInventario);
            grboxValorInventario.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            grboxValorInventario.ForeColor = Color.FromArgb(38, 38, 38);
            grboxValorInventario.Location = new Point(813, 21);
            grboxValorInventario.Margin = new Padding(2);
            grboxValorInventario.Name = "grboxValorInventario";
            grboxValorInventario.Padding = new Padding(2);
            grboxValorInventario.Size = new Size(266, 175);
            grboxValorInventario.TabIndex = 1;
            grboxValorInventario.TabStop = false;
            grboxValorInventario.Text = "Valor Inventario";
            // 
            // LblValorInventario
            // 
            LblValorInventario.AutoSize = true;
            LblValorInventario.Location = new Point(60, 56);
            LblValorInventario.Margin = new Padding(2, 0, 2, 0);
            LblValorInventario.Name = "LblValorInventario";
            LblValorInventario.Size = new Size(70, 28);
            LblValorInventario.TabIndex = 3;
            LblValorInventario.Text = "label5";
            // 
            // grboxFacturaSemanales
            // 
            grboxFacturaSemanales.BackColor = Color.FromArgb(250, 247, 241);
            grboxFacturaSemanales.Controls.Add(LblFacturasSemanales);
            grboxFacturaSemanales.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            grboxFacturaSemanales.ForeColor = Color.FromArgb(38, 38, 38);
            grboxFacturaSemanales.Location = new Point(543, 21);
            grboxFacturaSemanales.Margin = new Padding(2);
            grboxFacturaSemanales.Name = "grboxFacturaSemanales";
            grboxFacturaSemanales.Padding = new Padding(2);
            grboxFacturaSemanales.Size = new Size(266, 175);
            grboxFacturaSemanales.TabIndex = 1;
            grboxFacturaSemanales.TabStop = false;
            grboxFacturaSemanales.Text = "Facturas Semanales";
            // 
            // LblFacturasSemanales
            // 
            LblFacturasSemanales.AutoSize = true;
            LblFacturasSemanales.Location = new Point(75, 63);
            LblFacturasSemanales.Margin = new Padding(2, 0, 2, 0);
            LblFacturasSemanales.Name = "LblFacturasSemanales";
            LblFacturasSemanales.Size = new Size(70, 28);
            LblFacturasSemanales.TabIndex = 2;
            LblFacturasSemanales.Text = "label4";
            // 
            // grboxVentasSemanales
            // 
            grboxVentasSemanales.BackColor = Color.FromArgb(250, 247, 241);
            grboxVentasSemanales.Controls.Add(VentasSemanales);
            grboxVentasSemanales.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            grboxVentasSemanales.ForeColor = Color.FromArgb(38, 38, 38);
            grboxVentasSemanales.Location = new Point(273, 21);
            grboxVentasSemanales.Margin = new Padding(2);
            grboxVentasSemanales.Name = "grboxVentasSemanales";
            grboxVentasSemanales.Padding = new Padding(2);
            grboxVentasSemanales.Size = new Size(266, 175);
            grboxVentasSemanales.TabIndex = 1;
            grboxVentasSemanales.TabStop = false;
            grboxVentasSemanales.Text = "Ventas Semanales";
            // 
            // VentasSemanales
            // 
            VentasSemanales.AutoSize = true;
            VentasSemanales.Location = new Point(68, 63);
            VentasSemanales.Margin = new Padding(2, 0, 2, 0);
            VentasSemanales.Name = "VentasSemanales";
            VentasSemanales.Size = new Size(70, 28);
            VentasSemanales.TabIndex = 1;
            VentasSemanales.Text = "label3";
            // 
            // grboxVentasHoy
            // 
            grboxVentasHoy.BackColor = Color.FromArgb(250, 247, 241);
            grboxVentasHoy.Controls.Add(LblVentasHoy);
            grboxVentasHoy.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            grboxVentasHoy.ForeColor = Color.FromArgb(38, 38, 38);
            grboxVentasHoy.Location = new Point(3, 21);
            grboxVentasHoy.Margin = new Padding(2);
            grboxVentasHoy.Name = "grboxVentasHoy";
            grboxVentasHoy.Padding = new Padding(2);
            grboxVentasHoy.Size = new Size(266, 175);
            grboxVentasHoy.TabIndex = 0;
            grboxVentasHoy.TabStop = false;
            grboxVentasHoy.Text = "Ventas de Hoy";
            // 
            // LblVentasHoy
            // 
            LblVentasHoy.AutoSize = true;
            LblVentasHoy.Location = new Point(70, 63);
            LblVentasHoy.Margin = new Padding(2, 0, 2, 0);
            LblVentasHoy.Name = "LblVentasHoy";
            LblVentasHoy.Size = new Size(70, 28);
            LblVentasHoy.TabIndex = 0;
            LblVentasHoy.Text = "label2";
            // 
            // InicioMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1098, 689);
            Controls.Add(panel1);
            Controls.Add(label1);
            Cursor = Cursors.Hand;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "InicioMenu";
            Text = "InicioMenu";
            Load += InicioMenu_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
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
        private GroupBox groupBox1;
    }
}