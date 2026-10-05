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
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1791, 1133);
            panel1.TabIndex = 11;
            panel1.Paint += panel1_Paint;
            // 
            // txtTotal
            // 
            txtTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtTotal.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTotal.ForeColor = Color.FromArgb(153, 40, 35);
            txtTotal.Location = new Point(1544, 936);
            txtTotal.Margin = new Padding(4, 4, 4, 4);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(208, 50);
            txtTotal.TabIndex = 19;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(38, 38, 38);
            label7.Location = new Point(1409, 940);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(121, 45);
            label7.TabIndex = 18;
            label7.Text = "TOTAL:";
            // 
            // btnGuardarfact
            // 
            btnGuardarfact.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardarfact.BackColor = Color.DarkGreen;
            btnGuardarfact.FlatStyle = FlatStyle.Flat;
            btnGuardarfact.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarfact.ForeColor = Color.FromArgb(250, 247, 241);
            // btnGuardarfact.Image = (Image)resources.GetObject("btnGuardarfact.Image");
            btnGuardarfact.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarfact.Location = new Point(1478, 1005);
            btnGuardarfact.Margin = new Padding(4, 4, 4, 4);
            btnGuardarfact.Name = "btnGuardarfact";
            btnGuardarfact.Size = new Size(246, 83);
            btnGuardarfact.TabIndex = 17;
            btnGuardarfact.Text = "Guardar Fact.";
            btnGuardarfact.UseVisualStyleBackColor = false;
            btnGuardarfact.Click += btnGuardarfact_Click;
            // 
            // btnLimpiarAll
            // 
            btnLimpiarAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLimpiarAll.BackColor = Color.Firebrick;
            btnLimpiarAll.FlatStyle = FlatStyle.Flat;
            btnLimpiarAll.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiarAll.ForeColor = Color.FromArgb(250, 247, 241);
            // btnLimpiarAll.Image = (Image)resources.GetObject("btnLimpiarAll.Image");
            btnLimpiarAll.ImageAlign = ContentAlignment.MiddleLeft;
            btnLimpiarAll.Location = new Point(1030, 1005);
            btnLimpiarAll.Margin = new Padding(4, 4, 4, 4);
            btnLimpiarAll.Name = "btnLimpiarAll";
            btnLimpiarAll.Size = new Size(246, 83);
            btnLimpiarAll.TabIndex = 16;
            btnLimpiarAll.Text = "Limpiar Todo";
            btnLimpiarAll.UseVisualStyleBackColor = false;
            btnLimpiarAll.Click += btnLimpiarAll_Click;
            // 
            // btnImprimir
            // 
            btnImprimir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnImprimir.BackColor = Color.DarkBlue;
            btnImprimir.FlatStyle = FlatStyle.Flat;
            btnImprimir.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnImprimir.ForeColor = Color.FromArgb(250, 247, 241);
            // btnImprimir.Image = (Image)resources.GetObject("btnImprimir.Image");
            btnImprimir.ImageAlign = ContentAlignment.MiddleLeft;
            btnImprimir.Location = new Point(555, 1005);
            btnImprimir.Margin = new Padding(4, 4, 4, 4);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(246, 83);
            btnImprimir.TabIndex = 15;
            btnImprimir.Text = "Ver/Imprimir";
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // btnQuitarLinea
            // 
            btnQuitarLinea.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnQuitarLinea.BackColor = Color.DarkRed;
            btnQuitarLinea.FlatStyle = FlatStyle.Flat;
            btnQuitarLinea.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuitarLinea.ForeColor = Color.FromArgb(250, 247, 241);
            // btnQuitarLinea.Image = (Image)resources.GetObject("btnQuitarLinea.Image");
            btnQuitarLinea.ImageAlign = ContentAlignment.MiddleLeft;
            btnQuitarLinea.Location = new Point(99, 1005);
            btnQuitarLinea.Margin = new Padding(4, 4, 4, 4);
            btnQuitarLinea.Name = "btnQuitarLinea";
            btnQuitarLinea.Size = new Size(246, 83);
            btnQuitarLinea.TabIndex = 14;
            btnQuitarLinea.Text = "Quitar Linea";
            btnQuitarLinea.UseVisualStyleBackColor = false;
            btnQuitarLinea.Click += btnQuitarLinea_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.BackColor = Color.FromArgb(250, 247, 241);
            groupBox2.Controls.Add(dataGridDetallesFacturas);
            groupBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.ForeColor = Color.FromArgb(153, 40, 35);
            groupBox2.Location = new Point(34, 497);
            groupBox2.Margin = new Padding(4, 4, 4, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 4, 4, 4);
            groupBox2.Size = new Size(1720, 413);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Detalle de Factura";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // dataGridDetallesFacturas
            // 
            dataGridDetallesFacturas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridDetallesFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridDetallesFacturas.Location = new Point(8, 49);
            dataGridDetallesFacturas.Margin = new Padding(4, 4, 4, 4);
            dataGridDetallesFacturas.Name = "dataGridDetallesFacturas";
            dataGridDetallesFacturas.RowHeadersWidth = 62;
            dataGridDetallesFacturas.Size = new Size(1704, 339);
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
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.ForeColor = Color.FromArgb(153, 40, 35);
            groupBox1.Location = new Point(34, 287);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            groupBox1.Size = new Size(1720, 179);
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
            // btnAgregarProduc.Image = (Image)resources.GetObject("btnAgregarProduc.Image");
            btnAgregarProduc.ImageAlign = ContentAlignment.MiddleLeft;
            btnAgregarProduc.Location = new Point(1444, 51);
            btnAgregarProduc.Margin = new Padding(4, 4, 4, 4);
            btnAgregarProduc.Name = "btnAgregarProduc";
            btnAgregarProduc.Size = new Size(246, 83);
            btnAgregarProduc.TabIndex = 9;
            btnAgregarProduc.Text = "Agregar";
            btnAgregarProduc.UseVisualStyleBackColor = false;
            btnAgregarProduc.Click += btnAgregarProduc_Click;
            // 
            // numericUpCantidad
            // 
            numericUpCantidad.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpCantidad.Location = new Point(1283, 68);
            numericUpCantidad.Margin = new Padding(4, 4, 4, 4);
            numericUpCantidad.Name = "numericUpCantidad";
            numericUpCantidad.Size = new Size(116, 50);
            numericUpCantidad.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(1122, 70);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(159, 45);
            label5.TabIndex = 7;
            label5.Text = "Cantidad:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(582, 70);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(176, 45);
            label6.TabIndex = 5;
            label6.Text = "Productos:";
            // 
            // cmboxProductos
            // 
            cmboxProductos.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmboxProductos.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxProductos.FormattingEnabled = true;
            cmboxProductos.Location = new Point(758, 67);
            cmboxProductos.Margin = new Padding(4, 4, 4, 4);
            cmboxProductos.Name = "cmboxProductos";
            cmboxProductos.Size = new Size(329, 53);
            cmboxProductos.TabIndex = 3;
            // 
            // cmboxCategorias
            // 
            cmboxCategorias.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmboxCategorias.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCategorias.FormattingEnabled = true;
            cmboxCategorias.Location = new Point(226, 67);
            cmboxCategorias.Margin = new Padding(4, 4, 4, 4);
            cmboxCategorias.Name = "cmboxCategorias";
            cmboxCategorias.Size = new Size(329, 53);
            cmboxCategorias.TabIndex = 1;
            cmboxCategorias.Text = "Todas";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(38, 38, 38);
            label8.Location = new Point(42, 70);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(183, 45);
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
            groupBoxDatosPedido.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxDatosPedido.ForeColor = Color.FromArgb(153, 40, 35);
            groupBoxDatosPedido.Location = new Point(34, 15);
            groupBoxDatosPedido.Margin = new Padding(4, 4, 4, 4);
            groupBoxDatosPedido.Name = "groupBoxDatosPedido";
            groupBoxDatosPedido.Padding = new Padding(4, 4, 4, 4);
            groupBoxDatosPedido.Size = new Size(1720, 251);
            groupBoxDatosPedido.TabIndex = 11;
            groupBoxDatosPedido.TabStop = false;
            groupBoxDatosPedido.Text = "Datos del Pedido";
            // 
            // txtObseravciones
            // 
            txtObseravciones.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtObseravciones.ForeColor = Color.FromArgb(38, 38, 38);
            txtObseravciones.Location = new Point(1228, 166);
            txtObseravciones.Margin = new Padding(4, 4, 4, 4);
            txtObseravciones.Name = "txtObseravciones";
            txtObseravciones.Size = new Size(381, 50);
            txtObseravciones.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(1006, 166);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(208, 45);
            label4.TabIndex = 7;
            label4.Text = "Observacion:";
            // 
            // cmboxAtendidoPor
            // 
            cmboxAtendidoPor.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmboxAtendidoPor.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxAtendidoPor.FormattingEnabled = true;
            cmboxAtendidoPor.Location = new Point(1228, 67);
            cmboxAtendidoPor.Margin = new Padding(4, 4, 4, 4);
            cmboxAtendidoPor.Name = "cmboxAtendidoPor";
            cmboxAtendidoPor.Size = new Size(381, 53);
            cmboxAtendidoPor.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(1006, 70);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(221, 45);
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
            btnNuevoCliente.Location = new Point(706, 51);
            btnNuevoCliente.Margin = new Padding(4, 4, 4, 4);
            btnNuevoCliente.Name = "btnNuevoCliente";
            btnNuevoCliente.Size = new Size(246, 83);
            btnNuevoCliente.TabIndex = 4;
            btnNuevoCliente.Text = "Nuevo Cliente";
            btnNuevoCliente.TextAlign = ContentAlignment.MiddleRight;
            btnNuevoCliente.UseVisualStyleBackColor = false;
            btnNuevoCliente.Click += button1_Click;
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.ForeColor = Color.FromArgb(38, 38, 38);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(311, 156);
            comboBox2.Margin = new Padding(4, 4, 4, 4);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(329, 53);
            comboBox2.TabIndex = 3;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(42, 160);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(247, 45);
            label2.TabIndex = 2;
            label2.Text = "Forma de Pago:";
            // 
            // cmboxClientes
            // 
            cmboxClientes.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmboxClientes.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxClientes.FormattingEnabled = true;
            cmboxClientes.Location = new Point(311, 67);
            cmboxClientes.Margin = new Padding(4, 4, 4, 4);
            cmboxClientes.Name = "cmboxClientes";
            cmboxClientes.Size = new Size(329, 53);
            cmboxClientes.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(42, 70);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(129, 45);
            label1.TabIndex = 0;
            label1.Text = "Cliente:";
            // 
            // Facturacion
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1791, 1133);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 4, 4, 4);
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