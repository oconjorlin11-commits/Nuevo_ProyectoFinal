namespace Nuevo_Proyecto.Models.Views
{
    partial class NuevoProducto
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
            btnCancelarProduct = new Button();
            btnGuardarProduc = new Button();
            checkBoxProductosActi = new CheckBox();
            cmboxUnidadProduct = new ComboBox();
            txtPrecioProduct = new TextBox();
            label5 = new Label();
            label3 = new Label();
            label2 = new Label();
            txtNombreProduct = new TextBox();
            label1 = new Label();
            txtCodigoProduc = new TextBox();
            label4 = new Label();
            cmboxCategoriaProduc = new ComboBox();
            txtStockInicial = new TextBox();
            label6 = new Label();
            txtStockMinimo = new TextBox();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            txtDescripcion = new TextBox();
            label9 = new Label();
            panel1 = new Panel();
            label8 = new Label();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnCancelarProduct
            // 
            btnCancelarProduct.BackColor = Color.DarkRed;
            btnCancelarProduct.FlatStyle = FlatStyle.Flat;
            btnCancelarProduct.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelarProduct.ForeColor = Color.FromArgb(250, 247, 241);
            btnCancelarProduct.Image = Properties.Resources.x5;
            btnCancelarProduct.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarProduct.Location = new Point(313, 680);
            btnCancelarProduct.Margin = new Padding(2, 2, 2, 2);
            btnCancelarProduct.Name = "btnCancelarProduct";
            btnCancelarProduct.Size = new Size(151, 52);
            btnCancelarProduct.TabIndex = 50;
            btnCancelarProduct.Text = "Cancelar";
            btnCancelarProduct.UseVisualStyleBackColor = false;
            btnCancelarProduct.Click += btnCancelarProduct_Click;
            // 
            // btnGuardarProduc
            // 
            btnGuardarProduc.BackColor = Color.DarkGreen;
            btnGuardarProduc.FlatStyle = FlatStyle.Flat;
            btnGuardarProduc.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarProduc.ForeColor = Color.FromArgb(250, 247, 241);
            btnGuardarProduc.Image = Properties.Resources.lista_de_verificacion__1_5;
            btnGuardarProduc.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarProduc.Location = new Point(11, 680);
            btnGuardarProduc.Margin = new Padding(2, 2, 2, 2);
            btnGuardarProduc.Name = "btnGuardarProduc";
            btnGuardarProduc.Size = new Size(151, 52);
            btnGuardarProduc.TabIndex = 49;
            btnGuardarProduc.Text = "Guardar ";
            btnGuardarProduc.UseVisualStyleBackColor = false;
            btnGuardarProduc.Click += btnGuardarProduc_Click;
            // 
            // checkBoxProductosActi
            // 
            checkBoxProductosActi.AutoSize = true;
            checkBoxProductosActi.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            checkBoxProductosActi.Location = new Point(196, 640);
            checkBoxProductosActi.Margin = new Padding(2, 2, 2, 2);
            checkBoxProductosActi.Name = "checkBoxProductosActi";
            checkBoxProductosActi.Size = new Size(172, 29);
            checkBoxProductosActi.TabIndex = 48;
            checkBoxProductosActi.Text = "Productos Activos";
            checkBoxProductosActi.UseVisualStyleBackColor = true;
            // 
            // cmboxUnidadProduct
            // 
            cmboxUnidadProduct.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            cmboxUnidadProduct.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxUnidadProduct.FormattingEnabled = true;
            cmboxUnidadProduct.Location = new Point(196, 340);
            cmboxUnidadProduct.Margin = new Padding(2, 2, 2, 2);
            cmboxUnidadProduct.Name = "cmboxUnidadProduct";
            cmboxUnidadProduct.Size = new Size(236, 33);
            cmboxUnidadProduct.TabIndex = 46;
            cmboxUnidadProduct.Text = "Und";
            // 
            // txtPrecioProduct
            // 
            txtPrecioProduct.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtPrecioProduct.ForeColor = Color.FromArgb(38, 38, 38);
            txtPrecioProduct.Location = new Point(196, 460);
            txtPrecioProduct.Margin = new Padding(2, 2, 2, 2);
            txtPrecioProduct.Name = "txtPrecioProduct";
            txtPrecioProduct.PlaceholderText = "C$ 000.00";
            txtPrecioProduct.Size = new Size(236, 31);
            txtPrecioProduct.TabIndex = 44;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(19, 460);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(179, 28);
            label5.TabIndex = 43;
            label5.Text = "Precio/Ventas(C$):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(59, 340);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(82, 28);
            label3.TabIndex = 42;
            label3.Text = "Unidad:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(59, 280);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(103, 28);
            label2.TabIndex = 40;
            label2.Text = "Categoria:";
            // 
            // txtNombreProduct
            // 
            txtNombreProduct.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtNombreProduct.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreProduct.Location = new Point(196, 220);
            txtNombreProduct.Margin = new Padding(2, 2, 2, 2);
            txtNombreProduct.Name = "txtNombreProduct";
            txtNombreProduct.PlaceholderText = "Nombre del Producto";
            txtNombreProduct.Size = new Size(236, 31);
            txtNombreProduct.TabIndex = 39;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(59, 220);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(92, 28);
            label1.TabIndex = 38;
            label1.Text = "Nombre:";
            // 
            // txtCodigoProduc
            // 
            txtCodigoProduc.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtCodigoProduc.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoProduc.Location = new Point(196, 160);
            txtCodigoProduc.Margin = new Padding(2, 2, 2, 2);
            txtCodigoProduc.Name = "txtCodigoProduc";
            txtCodigoProduc.PlaceholderText = "PDR-000";
            txtCodigoProduc.Size = new Size(236, 31);
            txtCodigoProduc.TabIndex = 37;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(59, 160);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(82, 28);
            label4.TabIndex = 36;
            label4.Text = "Codigo:";
            // 
            // cmboxCategoriaProduc
            // 
            cmboxCategoriaProduc.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            cmboxCategoriaProduc.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCategoriaProduc.FormattingEnabled = true;
            cmboxCategoriaProduc.Location = new Point(196, 280);
            cmboxCategoriaProduc.Margin = new Padding(2, 2, 2, 2);
            cmboxCategoriaProduc.Name = "cmboxCategoriaProduc";
            cmboxCategoriaProduc.Size = new Size(236, 33);
            cmboxCategoriaProduc.TabIndex = 51;
            cmboxCategoriaProduc.Text = "Todas";
            // 
            // txtStockInicial
            // 
            txtStockInicial.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtStockInicial.ForeColor = Color.FromArgb(38, 38, 38);
            txtStockInicial.Location = new Point(196, 520);
            txtStockInicial.Margin = new Padding(2, 2, 2, 2);
            txtStockInicial.Name = "txtStockInicial";
            txtStockInicial.PlaceholderText = "000";
            txtStockInicial.Size = new Size(236, 31);
            txtStockInicial.TabIndex = 53;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(19, 520);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(126, 28);
            label6.TabIndex = 52;
            label6.Text = "Stock/Inicial:";
            // 
            // txtStockMinimo
            // 
            txtStockMinimo.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtStockMinimo.ForeColor = Color.FromArgb(38, 38, 38);
            txtStockMinimo.Location = new Point(196, 580);
            txtStockMinimo.Margin = new Padding(2, 2, 2, 2);
            txtStockMinimo.Name = "txtStockMinimo";
            txtStockMinimo.PlaceholderText = "00";
            txtStockMinimo.Size = new Size(236, 31);
            txtStockMinimo.TabIndex = 55;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(38, 38, 38);
            label7.Location = new Point(19, 580);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(144, 28);
            label7.TabIndex = 54;
            label7.Text = "Stock/Minimo:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.business_inventory_maintenance_product_box_boxes_2326;
            pictureBox1.Location = new Point(16, 18);
            pictureBox1.Margin = new Padding(2, 2, 2, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(70, 78);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 56;
            pictureBox1.TabStop = false;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtDescripcion.ForeColor = Color.FromArgb(38, 38, 38);
            txtDescripcion.Location = new Point(196, 400);
            txtDescripcion.Margin = new Padding(2, 2, 2, 2);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "En que consiste el producto";
            txtDescripcion.Size = new Size(236, 31);
            txtDescripcion.TabIndex = 60;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(38, 38, 38);
            label9.Location = new Point(59, 400);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(122, 28);
            label9.TabIndex = 59;
            label9.Text = "Descripcion:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(250, 247, 241);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(452, 122);
            panel1.TabIndex = 61;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            label8.Location = new Point(133, 46);
            label8.Name = "label8";
            label8.Size = new Size(305, 50);
            label8.TabIndex = 54;
            label8.Text = "Ingrese los datos del nuevo producto \r\nen los campos vacíos";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.FromArgb(153, 40, 35);
            label10.Location = new Point(133, 18);
            label10.Name = "label10";
            label10.Size = new Size(141, 25);
            label10.TabIndex = 53;
            label10.Text = "Nuevo Producto";
            // 
            // NuevoProducto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(476, 742);
            Controls.Add(panel1);
            Controls.Add(txtDescripcion);
            Controls.Add(label9);
            Controls.Add(txtStockMinimo);
            Controls.Add(label7);
            Controls.Add(txtStockInicial);
            Controls.Add(label6);
            Controls.Add(cmboxCategoriaProduc);
            Controls.Add(btnCancelarProduct);
            Controls.Add(btnGuardarProduc);
            Controls.Add(checkBoxProductosActi);
            Controls.Add(cmboxUnidadProduct);
            Controls.Add(txtPrecioProduct);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtNombreProduct);
            Controls.Add(label1);
            Controls.Add(txtCodigoProduc);
            Controls.Add(label4);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2, 2, 2, 2);
            Name = "NuevoProducto";
            Text = "NuevoProducto";
            Load += NuevoProducto_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancelarProduct;
        private Button btnGuardarProduc;
        private CheckBox checkBoxProductosActi;
        private ComboBox cmboxUnidadProduct;
        private TextBox txtPrecioProduct;
        private Label label5;
        private Label label3;
        private Label label2;
        private TextBox txtNombreProduct;
        private Label label1;
        private TextBox txtCodigoProduc;
        private Label label4;
        private ComboBox cmboxCategoriaProduc;
        private TextBox txtStockInicial;
        private Label label6;
        private TextBox txtStockMinimo;
        private Label label7;
        private PictureBox pictureBox1;
        private TextBox txtDescripcion;
        private Label label9;
        private Panel panel1;
        private Label label8;
        private Label label10;
    }
}