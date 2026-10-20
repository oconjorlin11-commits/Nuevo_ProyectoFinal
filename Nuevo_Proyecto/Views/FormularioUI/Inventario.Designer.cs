namespace Nuevo_Proyecto.Models.Views
{
    partial class Inventario
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventario));
            btnEliminarInvent = new Button();
            dataGridInventario = new DataGridView();
            txtBucarInvet = new TextBox();
            label4 = new Label();
            label1 = new Label();
            label2 = new Label();
            cmboxCategoriaInve = new ComboBox();
            btnEditarInventar = new Button();
            btnNuevoProduct = new Button();
            txtProductosInven = new TextBox();
            label3 = new Label();
            txtCodigoInvent = new TextBox();
            label5 = new Label();
            cmboxCategorias = new ComboBox();
            label8 = new Label();
            cmboxUnidad = new ComboBox();
            label6 = new Label();
            txtPrecioVentas = new TextBox();
            label9 = new Label();
            txtMinimo = new TextBox();
            label10 = new Label();
            txtStock = new TextBox();
            label11 = new Label();
            checkBoxInventario = new CheckBox();
            pictureBox1 = new PictureBox();
            imageList1 = new ImageList(components);
            label7 = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridInventario).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnEliminarInvent
            // 
            btnEliminarInvent.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEliminarInvent.BackColor = Color.DarkRed;
            btnEliminarInvent.FlatStyle = FlatStyle.Flat;
            btnEliminarInvent.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminarInvent.ForeColor = Color.FromArgb(250, 247, 241);
            btnEliminarInvent.Image = Properties.Resources._32officeicons_3_89720__1_;
            btnEliminarInvent.ImageAlign = ContentAlignment.MiddleLeft;
            btnEliminarInvent.Location = new Point(920, 622);
            btnEliminarInvent.Margin = new Padding(2);
            btnEliminarInvent.Name = "btnEliminarInvent";
            btnEliminarInvent.Size = new Size(151, 52);
            btnEliminarInvent.TabIndex = 26;
            btnEliminarInvent.Text = "Eliminar";
            btnEliminarInvent.UseVisualStyleBackColor = false;
            btnEliminarInvent.Click += btnEliminarInvent_Click;
            // 
            // dataGridInventario
            // 
            dataGridInventario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridInventario.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridInventario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridInventario.Location = new Point(21, 376);
            dataGridInventario.Margin = new Padding(2);
            dataGridInventario.Name = "dataGridInventario";
            dataGridInventario.RowHeadersWidth = 62;
            dataGridInventario.Size = new Size(1057, 233);
            dataGridInventario.TabIndex = 25;
            // 
            // txtBucarInvet
            // 
            txtBucarInvet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBucarInvet.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtBucarInvet.ForeColor = Color.FromArgb(38, 38, 38);
            txtBucarInvet.Location = new Point(813, 31);
            txtBucarInvet.Margin = new Padding(2);
            txtBucarInvet.Name = "txtBucarInvet";
            txtBucarInvet.PlaceholderText = "Buscar";
            txtBucarInvet.Size = new Size(265, 31);
            txtBucarInvet.TabIndex = 24;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(733, 32);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(77, 28);
            label4.TabIndex = 23;
            label4.Text = "Buscar:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(153, 40, 35);
            label1.Location = new Point(90, 32);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(132, 32);
            label1.TabIndex = 22;
            label1.Text = "Inventario";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(409, 37);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(103, 28);
            label2.TabIndex = 29;
            label2.Text = "Categoria:";
            // 
            // cmboxCategoriaInve
            // 
            cmboxCategoriaInve.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmboxCategoriaInve.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            cmboxCategoriaInve.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCategoriaInve.FormattingEnabled = true;
            cmboxCategoriaInve.Location = new Point(516, 31);
            cmboxCategoriaInve.Margin = new Padding(2);
            cmboxCategoriaInve.Name = "cmboxCategoriaInve";
            cmboxCategoriaInve.Size = new Size(204, 33);
            cmboxCategoriaInve.TabIndex = 30;
            cmboxCategoriaInve.Text = "Buscar por Categoria";
            // 
            // btnEditarInventar
            // 
            btnEditarInventar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditarInventar.BackColor = Color.DarkGreen;
            btnEditarInventar.FlatStyle = FlatStyle.Flat;
            btnEditarInventar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditarInventar.ForeColor = Color.FromArgb(250, 247, 241);
            btnEditarInventar.Image = Properties.Resources.edit_validated256_25237;
            btnEditarInventar.ImageAlign = ContentAlignment.MiddleLeft;
            btnEditarInventar.Location = new Point(720, 622);
            btnEditarInventar.Margin = new Padding(2);
            btnEditarInventar.Name = "btnEditarInventar";
            btnEditarInventar.Size = new Size(151, 52);
            btnEditarInventar.TabIndex = 33;
            btnEditarInventar.Text = "Editar";
            btnEditarInventar.UseVisualStyleBackColor = false;
            btnEditarInventar.Click += btnEditarInventar_Click;
            // 
            // btnNuevoProduct
            // 
            btnNuevoProduct.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNuevoProduct.BackColor = Color.Green;
            btnNuevoProduct.FlatStyle = FlatStyle.Flat;
            btnNuevoProduct.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNuevoProduct.ForeColor = Color.FromArgb(250, 247, 241);
            btnNuevoProduct.Image = (Image)resources.GetObject("btnNuevoProduct.Image");
            btnNuevoProduct.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevoProduct.Location = new Point(507, 622);
            btnNuevoProduct.Margin = new Padding(2);
            btnNuevoProduct.Name = "btnNuevoProduct";
            btnNuevoProduct.Size = new Size(151, 52);
            btnNuevoProduct.TabIndex = 34;
            btnNuevoProduct.Text = "Nuevo Prod.";
            btnNuevoProduct.TextAlign = ContentAlignment.MiddleRight;
            btnNuevoProduct.UseVisualStyleBackColor = false;
            btnNuevoProduct.Click += btnNuevoProduct_Click;
            // 
            // txtProductosInven
            // 
            txtProductosInven.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtProductosInven.ForeColor = Color.FromArgb(38, 38, 38);
            txtProductosInven.Location = new Point(123, 99);
            txtProductosInven.Margin = new Padding(2);
            txtProductosInven.Name = "txtProductosInven";
            txtProductosInven.PlaceholderText = "Nombre Producto";
            txtProductosInven.Size = new Size(168, 31);
            txtProductosInven.TabIndex = 38;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(10, 99);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(109, 28);
            label3.TabIndex = 37;
            label3.Text = "Productos:";
            // 
            // txtCodigoInvent
            // 
            txtCodigoInvent.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtCodigoInvent.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoInvent.Location = new Point(123, 37);
            txtCodigoInvent.Margin = new Padding(2);
            txtCodigoInvent.Name = "txtCodigoInvent";
            txtCodigoInvent.PlaceholderText = "000";
            txtCodigoInvent.Size = new Size(168, 31);
            txtCodigoInvent.TabIndex = 36;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(10, 37);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(82, 28);
            label5.TabIndex = 35;
            label5.Text = "Codigo:";
            // 
            // cmboxCategorias
            // 
            cmboxCategorias.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            cmboxCategorias.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCategorias.FormattingEnabled = true;
            cmboxCategorias.Location = new Point(123, 164);
            cmboxCategorias.Margin = new Padding(2);
            cmboxCategorias.Name = "cmboxCategorias";
            cmboxCategorias.Size = new Size(168, 33);
            cmboxCategorias.TabIndex = 42;
            cmboxCategorias.Text = "Todas";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(38, 38, 38);
            label8.Location = new Point(10, 164);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(103, 28);
            label8.TabIndex = 41;
            label8.Text = "Categoria:";
            // 
            // cmboxUnidad
            // 
            cmboxUnidad.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            cmboxUnidad.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxUnidad.FormattingEnabled = true;
            cmboxUnidad.Location = new Point(468, 164);
            cmboxUnidad.Margin = new Padding(2);
            cmboxUnidad.Name = "cmboxUnidad";
            cmboxUnidad.Size = new Size(168, 33);
            cmboxUnidad.TabIndex = 46;
            cmboxUnidad.Text = "Todas";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(324, 164);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(82, 28);
            label6.TabIndex = 45;
            label6.Text = "Unidad:";
            // 
            // txtPrecioVentas
            // 
            txtPrecioVentas.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtPrecioVentas.ForeColor = Color.FromArgb(38, 38, 38);
            txtPrecioVentas.Location = new Point(468, 37);
            txtPrecioVentas.Margin = new Padding(2);
            txtPrecioVentas.Name = "txtPrecioVentas";
            txtPrecioVentas.PlaceholderText = "C$ 000.00";
            txtPrecioVentas.Size = new Size(168, 31);
            txtPrecioVentas.TabIndex = 44;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(38, 38, 38);
            label9.Location = new Point(324, 40);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(140, 28);
            label9.TabIndex = 43;
            label9.Text = "Precio Ventas:";
            // 
            // txtMinimo
            // 
            txtMinimo.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtMinimo.ForeColor = Color.FromArgb(38, 38, 38);
            txtMinimo.Location = new Point(468, 99);
            txtMinimo.Margin = new Padding(2);
            txtMinimo.Name = "txtMinimo";
            txtMinimo.PlaceholderText = "10";
            txtMinimo.Size = new Size(168, 31);
            txtMinimo.TabIndex = 50;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.FromArgb(38, 38, 38);
            label10.Location = new Point(324, 102);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(87, 28);
            label10.TabIndex = 49;
            label10.Text = "Minimo:";
            // 
            // txtStock
            // 
            txtStock.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtStock.ForeColor = Color.FromArgb(38, 38, 38);
            txtStock.Location = new Point(743, 37);
            txtStock.Margin = new Padding(2);
            txtStock.Name = "txtStock";
            txtStock.PlaceholderText = "00";
            txtStock.Size = new Size(168, 31);
            txtStock.TabIndex = 48;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.FromArgb(38, 38, 38);
            label11.Location = new Point(672, 40);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(67, 28);
            label11.TabIndex = 47;
            label11.Text = "Stock:";
            // 
            // checkBoxInventario
            // 
            checkBoxInventario.AutoSize = true;
            checkBoxInventario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            checkBoxInventario.Location = new Point(672, 99);
            checkBoxInventario.Margin = new Padding(2);
            checkBoxInventario.Name = "checkBoxInventario";
            checkBoxInventario.Size = new Size(94, 32);
            checkBoxInventario.TabIndex = 51;
            checkBoxInventario.Text = "Estado";
            checkBoxInventario.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(21, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(64, 64);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 56;
            pictureBox1.TabStop = false;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            label7.ForeColor = Color.FromArgb(38, 38, 38);
            label7.Location = new Point(21, 336);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(188, 28);
            label7.TabIndex = 57;
            label7.Text = "Lista de Inventario";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.FromArgb(250, 247, 241);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtCodigoInvent);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(checkBoxInventario);
            groupBox1.Controls.Add(txtProductosInven);
            groupBox1.Controls.Add(txtStock);
            groupBox1.Controls.Add(txtMinimo);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(cmboxUnidad);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cmboxCategorias);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(txtPrecioVentas);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            groupBox1.ForeColor = Color.FromArgb(38, 38, 38);
            groupBox1.Location = new Point(21, 105);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1057, 216);
            groupBox1.TabIndex = 58;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del Inventario";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // Inventario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1098, 689);
            Controls.Add(groupBox1);
            Controls.Add(label7);
            Controls.Add(pictureBox1);
            Controls.Add(btnNuevoProduct);
            Controls.Add(btnEditarInventar);
            Controls.Add(cmboxCategoriaInve);
            Controls.Add(label2);
            Controls.Add(btnEliminarInvent);
            Controls.Add(dataGridInventario);
            Controls.Add(txtBucarInvet);
            Controls.Add(label4);
            Controls.Add(label1);
            Cursor = Cursors.Hand;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "Inventario";
            Text = "Inventario";
            Load += Inventario_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridInventario).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnEliminarInvent;
        private DataGridView dataGridInventario;
        private TextBox txtBucarInvet;
        private Label label4;
        private Label label1;
        private Label label2;
        private ComboBox cmboxCategoriaInve;
        private Button btnEditarInventar;
        private Button btnNuevoProduct;
        private TextBox txtProductosInven;
        private Label label3;
        private TextBox txtCodigoInvent;
        private Label label5;
        private ComboBox cmboxCategorias;
        private Label label8;
        private ComboBox cmboxUnidad;
        private Label label6;
        private TextBox txtPrecioVentas;
        private Label label9;
        private TextBox txtMinimo;
        private Label label10;
        private TextBox txtStock;
        private Label label11;
        private CheckBox checkBoxInventario;
        private PictureBox pictureBox1;
        private ImageList imageList1;
        private Label label7;
        private GroupBox groupBox1;
    }
}