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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnCancelarProduct
            // 
            btnCancelarProduct.BackColor = Color.DarkRed;
            btnCancelarProduct.FlatStyle = FlatStyle.Flat;
            btnCancelarProduct.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelarProduct.ForeColor = Color.FromArgb(250, 247, 241);
            btnCancelarProduct.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarProduct.Location = new Point(629, 1188);
            btnCancelarProduct.Margin = new Padding(4);
            btnCancelarProduct.Name = "btnCancelarProduct";
            btnCancelarProduct.Size = new Size(246, 83);
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
            btnGuardarProduc.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarProduc.Location = new Point(87, 1188);
            btnGuardarProduc.Margin = new Padding(4);
            btnGuardarProduc.Name = "btnGuardarProduc";
            btnGuardarProduc.Size = new Size(246, 83);
            btnGuardarProduc.TabIndex = 49;
            btnGuardarProduc.Text = "Guardar ";
            btnGuardarProduc.UseVisualStyleBackColor = false;
            btnGuardarProduc.Click += btnGuardarProduc_Click;
            // 
            // checkBoxProductosActi
            // 
            checkBoxProductosActi.AutoSize = true;
            checkBoxProductosActi.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxProductosActi.Location = new Point(369, 942);
            checkBoxProductosActi.Margin = new Padding(4);
            checkBoxProductosActi.Name = "checkBoxProductosActi";
            checkBoxProductosActi.Size = new Size(308, 49);
            checkBoxProductosActi.TabIndex = 48;
            checkBoxProductosActi.Text = "Productos Activos";
            checkBoxProductosActi.UseVisualStyleBackColor = true;
            // 
            // cmboxUnidadProduct
            // 
            cmboxUnidadProduct.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmboxUnidadProduct.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxUnidadProduct.FormattingEnabled = true;
            cmboxUnidadProduct.Location = new Point(369, 466);
            cmboxUnidadProduct.Margin = new Padding(4);
            cmboxUnidadProduct.Name = "cmboxUnidadProduct";
            cmboxUnidadProduct.Size = new Size(381, 53);
            cmboxUnidadProduct.TabIndex = 46;
            // 
            // txtPrecioProduct
            // 
            txtPrecioProduct.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrecioProduct.ForeColor = Color.FromArgb(38, 38, 38);
            txtPrecioProduct.Location = new Point(369, 654);
            txtPrecioProduct.Margin = new Padding(4);
            txtPrecioProduct.Name = "txtPrecioProduct";
            txtPrecioProduct.Size = new Size(381, 50);
            txtPrecioProduct.TabIndex = 44;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(83, 658);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(289, 45);
            label5.TabIndex = 43;
            label5.Text = "Precio/Ventas(C$):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(83, 470);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(133, 45);
            label3.TabIndex = 42;
            label3.Text = "Unidad:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(83, 369);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(169, 45);
            label2.TabIndex = 40;
            label2.Text = "Categoria:";
            // 
            // txtNombreProduct
            // 
            txtNombreProduct.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombreProduct.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreProduct.Location = new Point(369, 256);
            txtNombreProduct.Margin = new Padding(4);
            txtNombreProduct.Name = "txtNombreProduct";
            txtNombreProduct.Size = new Size(381, 50);
            txtNombreProduct.TabIndex = 39;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(83, 259);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(148, 45);
            label1.TabIndex = 38;
            label1.Text = "Nombre:";
            // 
            // txtCodigoProduc
            // 
            txtCodigoProduc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigoProduc.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoProduc.Location = new Point(369, 158);
            txtCodigoProduc.Margin = new Padding(4);
            txtCodigoProduc.Name = "txtCodigoProduc";
            txtCodigoProduc.Size = new Size(381, 50);
            txtCodigoProduc.TabIndex = 37;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(83, 162);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(132, 45);
            label4.TabIndex = 36;
            label4.Text = "Codigo:";
            // 
            // cmboxCategoriaProduc
            // 
            cmboxCategoriaProduc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmboxCategoriaProduc.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCategoriaProduc.FormattingEnabled = true;
            cmboxCategoriaProduc.Location = new Point(369, 365);
            cmboxCategoriaProduc.Margin = new Padding(4);
            cmboxCategoriaProduc.Name = "cmboxCategoriaProduc";
            cmboxCategoriaProduc.Size = new Size(381, 53);
            cmboxCategoriaProduc.TabIndex = 51;
            // 
            // txtStockInicial
            // 
            txtStockInicial.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStockInicial.ForeColor = Color.FromArgb(38, 38, 38);
            txtStockInicial.Location = new Point(369, 748);
            txtStockInicial.Margin = new Padding(4);
            txtStockInicial.Name = "txtStockInicial";
            txtStockInicial.Size = new Size(381, 50);
            txtStockInicial.TabIndex = 53;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(83, 751);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(204, 45);
            label6.TabIndex = 52;
            label6.Text = "Stock/Inicial:";
            // 
            // txtStockMinimo
            // 
            txtStockMinimo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStockMinimo.ForeColor = Color.FromArgb(38, 38, 38);
            txtStockMinimo.Location = new Point(369, 840);
            txtStockMinimo.Margin = new Padding(4);
            txtStockMinimo.Name = "txtStockMinimo";
            txtStockMinimo.Size = new Size(381, 50);
            txtStockMinimo.TabIndex = 55;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(38, 38, 38);
            label7.Location = new Point(83, 844);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(232, 45);
            label7.TabIndex = 54;
            label7.Text = "Stock/Minimo:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.business_inventory_maintenance_product_box_boxes_2326;
            pictureBox1.Location = new Point(0, 1);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(114, 96);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 56;
            pictureBox1.TabStop = false;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescripcion.ForeColor = Color.FromArgb(38, 38, 38);
            txtDescripcion.Location = new Point(369, 559);
            txtDescripcion.Margin = new Padding(4);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(381, 50);
            txtDescripcion.TabIndex = 60;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(38, 38, 38);
            label9.Location = new Point(83, 563);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(197, 45);
            label9.TabIndex = 59;
            label9.Text = "Descripcion:";
            // 
            // NuevoProducto
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(968, 1325);
            Controls.Add(txtDescripcion);
            Controls.Add(label9);
            Controls.Add(pictureBox1);
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
            Margin = new Padding(4);
            Name = "NuevoProducto";
            Text = "NuevoProducto";
            Load += NuevoProducto_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
    }
}