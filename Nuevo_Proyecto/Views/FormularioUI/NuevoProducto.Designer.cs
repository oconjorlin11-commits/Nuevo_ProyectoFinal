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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevoProducto));
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
            CmboxEmpleado = new ComboBox();
            label8 = new Label();
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
            // btnCancelarProduct.Image = (Image)resources.GetObject("btnCancelarProduct.Image");
            btnCancelarProduct.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarProduct.Location = new Point(484, 928);
            btnCancelarProduct.Name = "btnCancelarProduct";
            btnCancelarProduct.Size = new Size(189, 65);
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
            // btnGuardarProduc.Image = (Image)resources.GetObject("btnGuardarProduc.Image");
            btnGuardarProduc.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarProduc.Location = new Point(67, 928);
            btnGuardarProduc.Name = "btnGuardarProduc";
            btnGuardarProduc.Size = new Size(189, 65);
            btnGuardarProduc.TabIndex = 49;
            btnGuardarProduc.Text = "Guardar ";
            btnGuardarProduc.UseVisualStyleBackColor = false;
            btnGuardarProduc.Click += btnGuardarProduc_Click;
            // 
            // checkBoxProductosActi
            // 
            checkBoxProductosActi.AutoSize = true;
            checkBoxProductosActi.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxProductosActi.Location = new Point(284, 736);
            checkBoxProductosActi.Name = "checkBoxProductosActi";
            checkBoxProductosActi.Size = new Size(229, 36);
            checkBoxProductosActi.TabIndex = 48;
            checkBoxProductosActi.Text = "Productos Activos";
            checkBoxProductosActi.UseVisualStyleBackColor = true;
            // 
            // cmboxUnidadProduct
            // 
            cmboxUnidadProduct.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmboxUnidadProduct.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxUnidadProduct.FormattingEnabled = true;
            cmboxUnidadProduct.Location = new Point(284, 364);
            cmboxUnidadProduct.Name = "cmboxUnidadProduct";
            cmboxUnidadProduct.Size = new Size(294, 40);
            cmboxUnidadProduct.TabIndex = 46;
            // 
            // txtPrecioProduct
            // 
            txtPrecioProduct.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrecioProduct.ForeColor = Color.FromArgb(38, 38, 38);
            txtPrecioProduct.Location = new Point(284, 511);
            txtPrecioProduct.Name = "txtPrecioProduct";
            txtPrecioProduct.Size = new Size(294, 39);
            txtPrecioProduct.TabIndex = 44;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(64, 514);
            label5.Name = "label5";
            label5.Size = new Size(213, 32);
            label5.TabIndex = 43;
            label5.Text = "Precio/Ventas(C$):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(64, 367);
            label3.Name = "label3";
            label3.Size = new Size(98, 32);
            label3.TabIndex = 42;
            label3.Text = "Unidad:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(64, 288);
            label2.Name = "label2";
            label2.Size = new Size(126, 32);
            label2.TabIndex = 40;
            label2.Text = "Categoria:";
            // 
            // txtNombreProduct
            // 
            txtNombreProduct.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombreProduct.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreProduct.Location = new Point(284, 152);
            txtNombreProduct.Name = "txtNombreProduct";
            txtNombreProduct.Size = new Size(294, 39);
            txtNombreProduct.TabIndex = 39;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(64, 155);
            label1.Name = "label1";
            label1.Size = new Size(109, 32);
            label1.TabIndex = 38;
            label1.Text = "Nombre:";
            // 
            // txtCodigoProduc
            // 
            txtCodigoProduc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigoProduc.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoProduc.Location = new Point(284, 76);
            txtCodigoProduc.Name = "txtCodigoProduc";
            txtCodigoProduc.Size = new Size(294, 39);
            txtCodigoProduc.TabIndex = 37;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(64, 79);
            label4.Name = "label4";
            label4.Size = new Size(97, 32);
            label4.TabIndex = 36;
            label4.Text = "Codigo:";
            // 
            // cmboxCategoriaProduc
            // 
            cmboxCategoriaProduc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmboxCategoriaProduc.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCategoriaProduc.FormattingEnabled = true;
            cmboxCategoriaProduc.Location = new Point(284, 285);
            cmboxCategoriaProduc.Name = "cmboxCategoriaProduc";
            cmboxCategoriaProduc.Size = new Size(294, 40);
            cmboxCategoriaProduc.TabIndex = 51;
            // 
            // txtStockInicial
            // 
            txtStockInicial.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStockInicial.ForeColor = Color.FromArgb(38, 38, 38);
            txtStockInicial.Location = new Point(284, 584);
            txtStockInicial.Name = "txtStockInicial";
            txtStockInicial.Size = new Size(294, 39);
            txtStockInicial.TabIndex = 53;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(64, 587);
            label6.Name = "label6";
            label6.Size = new Size(152, 32);
            label6.TabIndex = 52;
            label6.Text = "Stock/Inicial:";
            // 
            // txtStockMinimo
            // 
            txtStockMinimo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStockMinimo.ForeColor = Color.FromArgb(38, 38, 38);
            txtStockMinimo.Location = new Point(284, 656);
            txtStockMinimo.Name = "txtStockMinimo";
            txtStockMinimo.Size = new Size(294, 39);
            txtStockMinimo.TabIndex = 55;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(38, 38, 38);
            label7.Location = new Point(64, 659);
            label7.Name = "label7";
            label7.Size = new Size(172, 32);
            label7.TabIndex = 54;
            label7.Text = "Stock/Minimo:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.business_inventory_maintenance_product_box_boxes_2326;
            pictureBox1.Location = new Point(0, 1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 75);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 56;
            pictureBox1.TabStop = false;
            // 
            // CmboxEmpleado
            // 
            CmboxEmpleado.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CmboxEmpleado.ForeColor = Color.FromArgb(38, 38, 38);
            CmboxEmpleado.FormattingEnabled = true;
            CmboxEmpleado.Location = new Point(284, 217);
            CmboxEmpleado.Name = "CmboxEmpleado";
            CmboxEmpleado.Size = new Size(294, 40);
            CmboxEmpleado.TabIndex = 58;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(38, 38, 38);
            label8.Location = new Point(64, 220);
            label8.Name = "label8";
            label8.Size = new Size(127, 32);
            label8.TabIndex = 57;
            label8.Text = "Empleado:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescripcion.ForeColor = Color.FromArgb(38, 38, 38);
            txtDescripcion.Location = new Point(284, 437);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(294, 39);
            txtDescripcion.TabIndex = 60;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(38, 38, 38);
            label9.Location = new Point(64, 440);
            label9.Name = "label9";
            label9.Size = new Size(145, 32);
            label9.TabIndex = 59;
            label9.Text = "Descripcion:";
            // 
            // NuevoProducto
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(745, 1035);
            Controls.Add(txtDescripcion);
            Controls.Add(label9);
            Controls.Add(CmboxEmpleado);
            Controls.Add(label8);
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
        private ComboBox CmboxEmpleado;
        private Label label8;
        private TextBox txtDescripcion;
        private Label label9;
    }
}