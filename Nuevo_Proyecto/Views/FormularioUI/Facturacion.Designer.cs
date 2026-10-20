namespace Nuevo_Proyecto.Models.Views
{
    partial class Facturacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Facturacion));
            panel1 = new Panel();
            txtTotal = new TextBox();
            label7 = new Label();
            btnGuardarfact = new Button();
            btnLimpiarAll = new Button();
            btnImprimir = new Button();
            btnQuitarLinea = new Button();
            groupBox2 = new GroupBox();
            dataGridDetallesFacturas = new DataGridView();
            groupBox1 = new GroupBox();
            btnAgregarProduc = new Button();
            numericUpCantidad = new NumericUpDown();
            label5 = new Label();
            label6 = new Label();
            cmboxProductos = new ComboBox();
            cmboxCategorias = new ComboBox();
            label8 = new Label();
            groupBoxDatosPedido = new GroupBox();
            txtObseravciones = new TextBox();
            label4 = new Label();
            cmboxAtendidoPor = new ComboBox();
            label3 = new Label();
            btnNuevoCliente = new Button();
            comboBox2 = new ComboBox();
            label2 = new Label();
            cmboxClientes = new ComboBox();
            label1 = new Label();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridDetallesFacturas).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpCantidad).BeginInit();
            groupBoxDatosPedido.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(txtTotal);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(btnGuardarfact);
            panel1.Controls.Add(btnLimpiarAll);
            panel1.Controls.Add(btnImprimir);
            panel1.Controls.Add(btnQuitarLinea);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(groupBoxDatosPedido);
            panel1.Cursor = Cursors.Hand;
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1102, 689);
            panel1.TabIndex = 11;
            panel1.Paint += panel1_Paint;
            // 
            // txtTotal
            // 
            txtTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtTotal.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTotal.ForeColor = Color.FromArgb(153, 40, 35);
            txtTotal.Location = new Point(949, 566);
            txtTotal.Margin = new Padding(2);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(130, 34);
            txtTotal.TabIndex = 19;
            txtTotal.Text = "C$000.00";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(38, 38, 38);
            label7.Location = new Point(867, 569);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(74, 28);
            label7.TabIndex = 18;
            label7.Text = "TOTAL:";
            // 
            // btnGuardarfact
            // 
            btnGuardarfact.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardarfact.BackColor = Color.DarkGreen;
            btnGuardarfact.Cursor = Cursors.Hand;
            btnGuardarfact.FlatAppearance.BorderSize = 0;
            btnGuardarfact.FlatStyle = FlatStyle.Flat;
            btnGuardarfact.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarfact.ForeColor = Color.FromArgb(250, 247, 241);
            btnGuardarfact.Image = Properties.Resources.diskette_save_saveas_1514__1_;
            btnGuardarfact.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarfact.Location = new Point(922, 626);
            btnGuardarfact.Margin = new Padding(2);
            btnGuardarfact.Name = "btnGuardarfact";
            btnGuardarfact.Size = new Size(158, 52);
            btnGuardarfact.TabIndex = 17;
            btnGuardarfact.Text = "Guardar Fact.";
            btnGuardarfact.TextAlign = ContentAlignment.MiddleRight;
            btnGuardarfact.UseVisualStyleBackColor = false;
            btnGuardarfact.Click += btnGuardarfact_Click;
            // 
            // btnLimpiarAll
            // 
            btnLimpiarAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLimpiarAll.BackColor = Color.Firebrick;
            btnLimpiarAll.Cursor = Cursors.Hand;
            btnLimpiarAll.FlatStyle = FlatStyle.Flat;
            btnLimpiarAll.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiarAll.ForeColor = Color.FromArgb(250, 247, 241);
            btnLimpiarAll.Image = (Image)resources.GetObject("btnLimpiarAll.Image");
            btnLimpiarAll.ImageAlign = ContentAlignment.MiddleLeft;
            btnLimpiarAll.Location = new Point(621, 626);
            btnLimpiarAll.Margin = new Padding(2);
            btnLimpiarAll.Name = "btnLimpiarAll";
            btnLimpiarAll.Size = new Size(158, 52);
            btnLimpiarAll.TabIndex = 16;
            btnLimpiarAll.Text = "Limpiar Todo";
            btnLimpiarAll.TextAlign = ContentAlignment.MiddleRight;
            btnLimpiarAll.UseVisualStyleBackColor = false;
            btnLimpiarAll.Click += btnLimpiarAll_Click;
            // 
            // btnImprimir
            // 
            btnImprimir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnImprimir.BackColor = Color.DarkBlue;
            btnImprimir.FlatStyle = FlatStyle.Flat;
            btnImprimir.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnImprimir.ForeColor = Color.FromArgb(250, 247, 241);
            btnImprimir.Image = Properties.Resources.actions_document_print_15785;
            btnImprimir.ImageAlign = ContentAlignment.MiddleLeft;
            btnImprimir.Location = new Point(309, 626);
            btnImprimir.Margin = new Padding(2);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(158, 52);
            btnImprimir.TabIndex = 15;
            btnImprimir.Text = "Ver/Imprimir";
            btnImprimir.TextAlign = ContentAlignment.MiddleRight;
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // btnQuitarLinea
            // 
            btnQuitarLinea.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnQuitarLinea.BackColor = Color.DarkRed;
            btnQuitarLinea.FlatStyle = FlatStyle.Flat;
            btnQuitarLinea.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuitarLinea.ForeColor = Color.FromArgb(250, 247, 241);
            btnQuitarLinea.Image = (Image)resources.GetObject("btnQuitarLinea.Image");
            btnQuitarLinea.ImageAlign = ContentAlignment.MiddleLeft;
            btnQuitarLinea.Location = new Point(26, 626);
            btnQuitarLinea.Margin = new Padding(2);
            btnQuitarLinea.Name = "btnQuitarLinea";
            btnQuitarLinea.Size = new Size(158, 52);
            btnQuitarLinea.TabIndex = 14;
            btnQuitarLinea.Text = "Quitar Linea";
            btnQuitarLinea.TextAlign = ContentAlignment.MiddleRight;
            btnQuitarLinea.UseVisualStyleBackColor = false;
            btnQuitarLinea.Click += btnQuitarLinea_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.BackColor = Color.FromArgb(250, 247, 241);
            groupBox2.Controls.Add(dataGridDetallesFacturas);
            groupBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            groupBox2.ForeColor = Color.FromArgb(38, 38, 38);
            groupBox2.Location = new Point(21, 311);
            groupBox2.Margin = new Padding(2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2);
            groupBox2.Size = new Size(1058, 239);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Detalle de Factura";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // dataGridDetallesFacturas
            // 
            dataGridDetallesFacturas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridDetallesFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridDetallesFacturas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridDetallesFacturas.BackgroundColor = Color.DarkGray;
            dataGridDetallesFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridDetallesFacturas.Location = new Point(5, 31);
            dataGridDetallesFacturas.Margin = new Padding(2);
            dataGridDetallesFacturas.Name = "dataGridDetallesFacturas";
            dataGridDetallesFacturas.RowHeadersWidth = 62;
            dataGridDetallesFacturas.Size = new Size(1049, 193);
            dataGridDetallesFacturas.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.FromArgb(250, 247, 241);
            groupBox1.Controls.Add(btnAgregarProduc);
            groupBox1.Controls.Add(numericUpCantidad);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cmboxProductos);
            groupBox1.Controls.Add(cmboxCategorias);
            groupBox1.Controls.Add(label8);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            groupBox1.ForeColor = Color.FromArgb(38, 38, 38);
            groupBox1.Location = new Point(21, 179);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(1058, 112);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Agregar Productos";
            // 
            // btnAgregarProduc
            // 
            btnAgregarProduc.BackColor = Color.Green;
            btnAgregarProduc.FlatStyle = FlatStyle.Flat;
            btnAgregarProduc.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregarProduc.ForeColor = Color.FromArgb(250, 247, 241);
            btnAgregarProduc.Image = Properties.Resources.shopping_basket_add256_24909;
            btnAgregarProduc.ImageAlign = ContentAlignment.MiddleLeft;
            btnAgregarProduc.Location = new Point(889, 32);
            btnAgregarProduc.Margin = new Padding(2);
            btnAgregarProduc.Name = "btnAgregarProduc";
            btnAgregarProduc.Size = new Size(158, 52);
            btnAgregarProduc.TabIndex = 9;
            btnAgregarProduc.Text = "Agregar";
            btnAgregarProduc.UseVisualStyleBackColor = false;
            btnAgregarProduc.Click += btnAgregarProduc_Click;
            // 
            // numericUpCantidad
            // 
            numericUpCantidad.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            numericUpCantidad.Location = new Point(790, 44);
            numericUpCantidad.Margin = new Padding(1);
            numericUpCantidad.Name = "numericUpCantidad";
            numericUpCantidad.Size = new Size(68, 34);
            numericUpCantidad.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(690, 44);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(97, 28);
            label5.TabIndex = 7;
            label5.Text = "Cantidad:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(358, 44);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(109, 28);
            label6.TabIndex = 5;
            label6.Text = "Productos:";
            // 
            // cmboxProductos
            // 
            cmboxProductos.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            cmboxProductos.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxProductos.FormattingEnabled = true;
            cmboxProductos.Location = new Point(471, 42);
            cmboxProductos.Margin = new Padding(2);
            cmboxProductos.Name = "cmboxProductos";
            cmboxProductos.Size = new Size(204, 36);
            cmboxProductos.TabIndex = 3;
            cmboxProductos.Text = "Todos";
            // 
            // cmboxCategorias
            // 
            cmboxCategorias.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            cmboxCategorias.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCategorias.FormattingEnabled = true;
            cmboxCategorias.Location = new Point(139, 42);
            cmboxCategorias.Margin = new Padding(2);
            cmboxCategorias.Name = "cmboxCategorias";
            cmboxCategorias.Size = new Size(204, 36);
            cmboxCategorias.TabIndex = 1;
            cmboxCategorias.Text = "Todas";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(38, 38, 38);
            label8.Location = new Point(26, 44);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(112, 28);
            label8.TabIndex = 0;
            label8.Text = "Categorias:";
            // 
            // groupBoxDatosPedido
            // 
            groupBoxDatosPedido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxDatosPedido.BackColor = Color.FromArgb(250, 247, 241);
            groupBoxDatosPedido.Controls.Add(txtObseravciones);
            groupBoxDatosPedido.Controls.Add(label4);
            groupBoxDatosPedido.Controls.Add(cmboxAtendidoPor);
            groupBoxDatosPedido.Controls.Add(label3);
            groupBoxDatosPedido.Controls.Add(btnNuevoCliente);
            groupBoxDatosPedido.Controls.Add(comboBox2);
            groupBoxDatosPedido.Controls.Add(label2);
            groupBoxDatosPedido.Controls.Add(cmboxClientes);
            groupBoxDatosPedido.Controls.Add(label1);
            groupBoxDatosPedido.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            groupBoxDatosPedido.ForeColor = Color.FromArgb(38, 38, 38);
            groupBoxDatosPedido.Location = new Point(21, 9);
            groupBoxDatosPedido.Margin = new Padding(2);
            groupBoxDatosPedido.Name = "groupBoxDatosPedido";
            groupBoxDatosPedido.Padding = new Padding(2);
            groupBoxDatosPedido.Size = new Size(1058, 157);
            groupBoxDatosPedido.TabIndex = 11;
            groupBoxDatosPedido.TabStop = false;
            groupBoxDatosPedido.Text = "Datos del Pedido";
            // 
            // txtObseravciones
            // 
            txtObseravciones.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtObseravciones.ForeColor = Color.FromArgb(38, 38, 38);
            txtObseravciones.Location = new Point(756, 104);
            txtObseravciones.Margin = new Padding(2);
            txtObseravciones.Name = "txtObseravciones";
            txtObseravciones.PlaceholderText = "Observaciones";
            txtObseravciones.Size = new Size(236, 34);
            txtObseravciones.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(619, 104);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(130, 28);
            label4.TabIndex = 7;
            label4.Text = "Observacion:";
            // 
            // cmboxAtendidoPor
            // 
            cmboxAtendidoPor.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            cmboxAtendidoPor.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxAtendidoPor.FormattingEnabled = true;
            cmboxAtendidoPor.Location = new Point(756, 42);
            cmboxAtendidoPor.Margin = new Padding(2);
            cmboxAtendidoPor.Name = "cmboxAtendidoPor";
            cmboxAtendidoPor.Size = new Size(236, 36);
            cmboxAtendidoPor.TabIndex = 6;
            cmboxAtendidoPor.Text = "Cajeros";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(619, 44);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(139, 28);
            label3.TabIndex = 5;
            label3.Text = "Atendido por:";
            // 
            // btnNuevoCliente
            // 
            btnNuevoCliente.BackColor = Color.DarkGray;
            btnNuevoCliente.FlatStyle = FlatStyle.Flat;
            btnNuevoCliente.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNuevoCliente.ForeColor = Color.FromArgb(250, 247, 241);
            btnNuevoCliente.Image = Properties.Resources.new_add_user_16734__1_;
            btnNuevoCliente.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevoCliente.Location = new Point(434, 32);
            btnNuevoCliente.Margin = new Padding(2);
            btnNuevoCliente.Name = "btnNuevoCliente";
            btnNuevoCliente.Size = new Size(158, 52);
            btnNuevoCliente.TabIndex = 4;
            btnNuevoCliente.Text = "Nuevo Cliente";
            btnNuevoCliente.TextAlign = ContentAlignment.MiddleRight;
            btnNuevoCliente.UseVisualStyleBackColor = false;
            btnNuevoCliente.Click += button1_Click;
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            comboBox2.ForeColor = Color.FromArgb(38, 38, 38);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(191, 98);
            comboBox2.Margin = new Padding(2);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(204, 36);
            comboBox2.TabIndex = 3;
            comboBox2.Text = "Formas de pagó";
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(26, 100);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(154, 28);
            label2.TabIndex = 2;
            label2.Text = "Forma de Pago:";
            // 
            // cmboxClientes
            // 
            cmboxClientes.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            cmboxClientes.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxClientes.FormattingEnabled = true;
            cmboxClientes.Location = new Point(191, 42);
            cmboxClientes.Margin = new Padding(2);
            cmboxClientes.Name = "cmboxClientes";
            cmboxClientes.Size = new Size(204, 36);
            cmboxClientes.TabIndex = 1;
            cmboxClientes.Text = "Nombre";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(26, 44);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(80, 28);
            label1.TabIndex = 0;
            label1.Text = "Cliente:";
            // 
            // Facturacion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1102, 689);
            Controls.Add(panel1);
            Cursor = Cursors.Hand;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "Facturacion";
            Text = "Facturacion";
            Load += Detalle_Factura_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridDetallesFacturas).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpCantidad).EndInit();
            groupBoxDatosPedido.ResumeLayout(false);
            groupBoxDatosPedido.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox txtTotal;
        private Label label7;
        private Button btnGuardarfact;
        private Button btnLimpiarAll;
        private Button btnImprimir;
        private Button btnQuitarLinea;
        private GroupBox groupBox2;
        private DataGridView dataGridDetallesFacturas;
        private GroupBox groupBox1;
        private Button btnAgregarProduc;
        private NumericUpDown numericUpCantidad;
        private Label label5;
        private Label label6;
        private ComboBox cmboxProductos;
        private ComboBox cmboxCategorias;
        private Label label8;
        private GroupBox groupBoxDatosPedido;
        private TextBox txtObseravciones;
        private Label label4;
        private ComboBox cmboxAtendidoPor;
        private Label label3;
        private Button btnNuevoCliente;
        private ComboBox comboBox2;
        private Label label2;
        private ComboBox cmboxClientes;
        private Label label1;
    }
}