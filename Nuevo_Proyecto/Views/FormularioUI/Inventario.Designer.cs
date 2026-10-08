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
            ((System.ComponentModel.ISupportInitialize)dataGridInventario).BeginInit();
            SuspendLayout();
            // 
            // btnEliminarInvent
            // 
            btnEliminarInvent.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEliminarInvent.BackColor = Color.DarkGray;
            btnEliminarInvent.FlatStyle = FlatStyle.Flat;
            btnEliminarInvent.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminarInvent.ForeColor = Color.FromArgb(250, 247, 241);
            btnEliminarInvent.ImageAlign = ContentAlignment.MiddleLeft;
            btnEliminarInvent.Location = new Point(1505, 1001);
            btnEliminarInvent.Margin = new Padding(4);
            btnEliminarInvent.Name = "btnEliminarInvent";
            btnEliminarInvent.Size = new Size(246, 83);
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
            dataGridInventario.Location = new Point(34, 401);
            dataGridInventario.Margin = new Padding(4);
            dataGridInventario.Name = "dataGridInventario";
            dataGridInventario.RowHeadersWidth = 62;
            dataGridInventario.Size = new Size(1717, 544);
            dataGridInventario.TabIndex = 25;
            // 
            // txtBucarInvet
            // 
            txtBucarInvet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBucarInvet.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBucarInvet.ForeColor = Color.FromArgb(38, 38, 38);
            txtBucarInvet.Location = new Point(1422, 45);
            txtBucarInvet.Margin = new Padding(4);
            txtBucarInvet.Name = "txtBucarInvet";
            txtBucarInvet.Size = new Size(329, 50);
            txtBucarInvet.TabIndex = 24;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(1249, 51);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(125, 45);
            label4.TabIndex = 23;
            label4.Text = "Buscar:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(153, 40, 35);
            label1.Location = new Point(34, 49);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(206, 51);
            label1.TabIndex = 22;
            label1.Text = "Inventario";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(719, 50);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(169, 45);
            label2.TabIndex = 29;
            label2.Text = "Categoria:";
            // 
            // cmboxCategoriaInve
            // 
            cmboxCategoriaInve.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmboxCategoriaInve.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmboxCategoriaInve.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCategoriaInve.FormattingEnabled = true;
            cmboxCategoriaInve.Location = new Point(890, 46);
            cmboxCategoriaInve.Margin = new Padding(4);
            cmboxCategoriaInve.Name = "cmboxCategoriaInve";
            cmboxCategoriaInve.Size = new Size(329, 53);
            cmboxCategoriaInve.TabIndex = 30;
            // 
            // btnEditarInventar
            // 
            btnEditarInventar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditarInventar.BackColor = Color.Green;
            btnEditarInventar.FlatStyle = FlatStyle.Flat;
            btnEditarInventar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditarInventar.ForeColor = Color.FromArgb(250, 247, 241);
            btnEditarInventar.ImageAlign = ContentAlignment.MiddleLeft;
            btnEditarInventar.Location = new Point(334, 1001);
            btnEditarInventar.Margin = new Padding(4);
            btnEditarInventar.Name = "btnEditarInventar";
            btnEditarInventar.Size = new Size(246, 83);
            btnEditarInventar.TabIndex = 33;
            btnEditarInventar.Text = "Editar";
            btnEditarInventar.UseVisualStyleBackColor = false;
            btnEditarInventar.Click += btnEditarInventar_Click;
            // 
            // btnNuevoProduct
            // 
            btnNuevoProduct.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNuevoProduct.BackColor = Color.DarkGray;
            btnNuevoProduct.FlatStyle = FlatStyle.Flat;
            btnNuevoProduct.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNuevoProduct.ForeColor = Color.FromArgb(250, 247, 241);
            btnNuevoProduct.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevoProduct.Location = new Point(34, 1001);
            btnNuevoProduct.Margin = new Padding(4);
            btnNuevoProduct.Name = "btnNuevoProduct";
            btnNuevoProduct.Size = new Size(246, 83);
            btnNuevoProduct.TabIndex = 34;
            btnNuevoProduct.Text = "Nuevo Prod.";
            btnNuevoProduct.UseVisualStyleBackColor = false;
            btnNuevoProduct.Click += btnNuevoProduct_Click;
            // 
            // txtProductosInven
            // 
            txtProductosInven.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtProductosInven.ForeColor = Color.FromArgb(38, 38, 38);
            txtProductosInven.Location = new Point(88, 316);
            txtProductosInven.Margin = new Padding(4);
            txtProductosInven.Name = "txtProductosInven";
            txtProductosInven.Size = new Size(270, 50);
            txtProductosInven.TabIndex = 38;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(143, 271);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(168, 45);
            label3.TabIndex = 37;
            label3.Text = "Productos";
            // 
            // txtCodigoInvent
            // 
            txtCodigoInvent.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigoInvent.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoInvent.Location = new Point(88, 186);
            txtCodigoInvent.Margin = new Padding(4);
            txtCodigoInvent.Name = "txtCodigoInvent";
            txtCodigoInvent.Size = new Size(270, 50);
            txtCodigoInvent.TabIndex = 36;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(161, 141);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(124, 45);
            label5.TabIndex = 35;
            label5.Text = "Codigo";
            // 
            // cmboxCategorias
            // 
            cmboxCategorias.Font = new Font("Segoe UI", 12F);
            cmboxCategorias.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCategorias.FormattingEnabled = true;
            cmboxCategorias.Location = new Point(391, 186);
            cmboxCategorias.Margin = new Padding(4);
            cmboxCategorias.Name = "cmboxCategorias";
            cmboxCategorias.Size = new Size(270, 53);
            cmboxCategorias.TabIndex = 42;
            cmboxCategorias.Text = "Todas";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(38, 38, 38);
            label8.Location = new Point(446, 141);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(175, 45);
            label8.TabIndex = 41;
            label8.Text = "Categorias";
            // 
            // cmboxUnidad
            // 
            cmboxUnidad.Font = new Font("Segoe UI", 12F);
            cmboxUnidad.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxUnidad.FormattingEnabled = true;
            cmboxUnidad.Location = new Point(697, 184);
            cmboxUnidad.Margin = new Padding(4);
            cmboxUnidad.Name = "cmboxUnidad";
            cmboxUnidad.Size = new Size(270, 53);
            cmboxUnidad.TabIndex = 46;
            cmboxUnidad.Text = "Todas";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(779, 141);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(125, 45);
            label6.TabIndex = 45;
            label6.Text = "Unidad";
            // 
            // txtPrecioVentas
            // 
            txtPrecioVentas.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrecioVentas.ForeColor = Color.FromArgb(38, 38, 38);
            txtPrecioVentas.Location = new Point(385, 315);
            txtPrecioVentas.Margin = new Padding(4);
            txtPrecioVentas.Name = "txtPrecioVentas";
            txtPrecioVentas.Size = new Size(270, 50);
            txtPrecioVentas.TabIndex = 44;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(38, 38, 38);
            label9.Location = new Point(417, 271);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(217, 45);
            label9.TabIndex = 43;
            label9.Text = "Precio Ventas";
            // 
            // txtMinimo
            // 
            txtMinimo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMinimo.ForeColor = Color.FromArgb(38, 38, 38);
            txtMinimo.Location = new Point(697, 316);
            txtMinimo.Margin = new Padding(4);
            txtMinimo.Name = "txtMinimo";
            txtMinimo.Size = new Size(270, 50);
            txtMinimo.TabIndex = 50;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.FromArgb(38, 38, 38);
            label10.Location = new Point(770, 271);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(132, 45);
            label10.TabIndex = 49;
            label10.Text = "Minimo";
            // 
            // txtStock
            // 
            txtStock.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStock.ForeColor = Color.FromArgb(38, 38, 38);
            txtStock.Location = new Point(1009, 187);
            txtStock.Margin = new Padding(4);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(270, 50);
            txtStock.TabIndex = 48;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.FromArgb(38, 38, 38);
            label11.Location = new Point(1082, 142);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(99, 45);
            label11.TabIndex = 47;
            label11.Text = "Stock";
            // 
            // checkBoxInventario
            // 
            checkBoxInventario.AutoSize = true;
            checkBoxInventario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            checkBoxInventario.Location = new Point(1022, 315);
            checkBoxInventario.Margin = new Padding(4);
            checkBoxInventario.Name = "checkBoxInventario";
            checkBoxInventario.Size = new Size(150, 49);
            checkBoxInventario.TabIndex = 51;
            checkBoxInventario.Text = "Estado";
            checkBoxInventario.UseVisualStyleBackColor = true;
            // 
            // Inventario
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1785, 1133);
            Controls.Add(checkBoxInventario);
            Controls.Add(txtMinimo);
            Controls.Add(label10);
            Controls.Add(txtStock);
            Controls.Add(label11);
            Controls.Add(cmboxUnidad);
            Controls.Add(label6);
            Controls.Add(txtPrecioVentas);
            Controls.Add(label9);
            Controls.Add(cmboxCategorias);
            Controls.Add(label8);
            Controls.Add(txtProductosInven);
            Controls.Add(label3);
            Controls.Add(txtCodigoInvent);
            Controls.Add(label5);
            Controls.Add(btnNuevoProduct);
            Controls.Add(btnEditarInventar);
            Controls.Add(cmboxCategoriaInve);
            Controls.Add(label2);
            Controls.Add(btnEliminarInvent);
            Controls.Add(dataGridInventario);
            Controls.Add(txtBucarInvet);
            Controls.Add(label4);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Inventario";
            Text = "Inventario";
            Load += Inventario_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridInventario).EndInit();
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
    }
}