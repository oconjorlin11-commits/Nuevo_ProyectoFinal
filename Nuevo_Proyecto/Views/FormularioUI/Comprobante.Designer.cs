namespace Nuevo_Proyecto.Models.Views
{
    partial class Comprobante
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            btnExportar = new Button();
            btnImprimir = new Button();
            pnlComprobante = new Panel();
            pnlFactura = new Panel();
            dgvProductos = new DataGridView();
            lblGracias = new Label();
            lblTotal = new Label();
            lblSubTotalFinal = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lblPago = new Label();
            lblAtendidopor = new Label();
            lblCliente = new Label();
            lblFecha = new Label();
            lblNumeroFact = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            lblLineal = new Label();
            lblDireccion = new Label();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            pnlBotones = new Panel();
            button1 = new Button();
            lblPrecioFinal = new Label();
            lblCantFinal = new Label();
            lblProductoFinal = new Label();
            pnlComprobante.SuspendLayout();
            pnlFactura.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // btnExportar
            // 
            btnExportar.BackColor = Color.DarkRed;
            btnExportar.FlatStyle = FlatStyle.Flat;
            btnExportar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.ForeColor = Color.FromArgb(250, 247, 241);
            btnExportar.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportar.Location = new Point(764, 16);
            btnExportar.Margin = new Padding(4);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(246, 83);
            btnExportar.TabIndex = 52;
            btnExportar.Text = "Cerrar";
            btnExportar.UseVisualStyleBackColor = false;
            btnExportar.Click += button3_Click;
            // 
            // btnImprimir
            // 
            btnImprimir.BackColor = Color.DarkGreen;
            btnImprimir.FlatStyle = FlatStyle.Flat;
            btnImprimir.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnImprimir.ForeColor = Color.FromArgb(250, 247, 241);
            btnImprimir.ImageAlign = ContentAlignment.MiddleLeft;
            btnImprimir.Location = new Point(40, 16);
            btnImprimir.Margin = new Padding(4);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(246, 83);
            btnImprimir.TabIndex = 51;
            btnImprimir.Text = "Imprimir";
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // pnlComprobante
            // 
            pnlComprobante.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlComprobante.BackColor = Color.FromArgb(232, 221, 206);
            pnlComprobante.Controls.Add(pnlFactura);
            pnlComprobante.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            pnlComprobante.Location = new Point(0, 0);
            pnlComprobante.Margin = new Padding(4);
            pnlComprobante.Name = "pnlComprobante";
            pnlComprobante.Size = new Size(1037, 1210);
            pnlComprobante.TabIndex = 54;
            pnlComprobante.Paint += pnlComprobante_Paint;
            // 
            // pnlFactura
            // 
            pnlFactura.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlFactura.AutoScroll = true;
            pnlFactura.BackColor = Color.FromArgb(250, 247, 241);
            pnlFactura.Controls.Add(dgvProductos);
            pnlFactura.Controls.Add(lblGracias);
            pnlFactura.Controls.Add(lblTotal);
            pnlFactura.Controls.Add(lblSubTotalFinal);
            pnlFactura.Controls.Add(label3);
            pnlFactura.Controls.Add(label2);
            pnlFactura.Controls.Add(label1);
            pnlFactura.Controls.Add(lblPago);
            pnlFactura.Controls.Add(lblAtendidopor);
            pnlFactura.Controls.Add(lblCliente);
            pnlFactura.Controls.Add(lblFecha);
            pnlFactura.Controls.Add(lblNumeroFact);
            pnlFactura.Controls.Add(label9);
            pnlFactura.Controls.Add(label8);
            pnlFactura.Controls.Add(label7);
            pnlFactura.Controls.Add(label6);
            pnlFactura.Controls.Add(label5);
            pnlFactura.Controls.Add(lblLineal);
            pnlFactura.Controls.Add(lblDireccion);
            pnlFactura.Controls.Add(lblSubtitulo);
            pnlFactura.Controls.Add(lblTitulo);
            pnlFactura.Location = new Point(26, 31);
            pnlFactura.Margin = new Padding(4);
            pnlFactura.Name = "pnlFactura";
            pnlFactura.Size = new Size(984, 1130);
            pnlFactura.TabIndex = 0;
            pnlFactura.Paint += pnlFactura_Paint;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvProductos.BackgroundColor = Color.FromArgb(250, 247, 241);
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(25, 618);
            dgvProductos.Margin = new Padding(4);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.RowHeadersWidth = 82;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(897, 251);
            dgvProductos.TabIndex = 19;
            // 
            // lblGracias
            // 
            lblGracias.AutoSize = true;
            lblGracias.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblGracias.Location = new Point(262, 974);
            lblGracias.Margin = new Padding(4, 0, 4, 0);
            lblGracias.Name = "lblGracias";
            lblGracias.Size = new Size(463, 74);
            lblGracias.TabIndex = 26;
            lblGracias.Text = "\"Gracias por su compra. \r\n¡Esperamos atenderle Nuevamente!\"\r\n";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTotal.Location = new Point(306, 914);
            lblTotal.Margin = new Padding(4, 0, 4, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(173, 37);
            lblTotal.TabIndex = 25;
            lblTotal.Text = "Total a Pagar";
            // 
            // lblSubTotalFinal
            // 
            lblSubTotalFinal.AutoSize = true;
            lblSubTotalFinal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSubTotalFinal.Location = new Point(708, 748);
            lblSubTotalFinal.Margin = new Padding(4, 0, 4, 0);
            lblSubTotalFinal.Name = "lblSubTotalFinal";
            lblSubTotalFinal.Size = new Size(123, 37);
            lblSubTotalFinal.TabIndex = 24;
            lblSubTotalFinal.Text = "SubTotal";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label3.Location = new Point(25, 854);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(897, 37);
            label3.TabIndex = 16;
            label3.Text = "--------------------------------------------------------------------------------";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label2.Location = new Point(25, 698);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(897, 37);
            label2.TabIndex = 15;
            label2.Text = "--------------------------------------------------------------------------------";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label1.Location = new Point(25, 589);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(897, 37);
            label1.TabIndex = 14;
            label1.Text = "--------------------------------------------------------------------------------";
            // 
            // lblPago
            // 
            lblPago.AutoSize = true;
            lblPago.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPago.Location = new Point(351, 521);
            lblPago.Margin = new Padding(4, 0, 4, 0);
            lblPago.Name = "lblPago";
            lblPago.Size = new Size(182, 37);
            lblPago.TabIndex = 13;
            lblPago.Text = "Metodo Pago";
            // 
            // lblAtendidopor
            // 
            lblAtendidopor.AutoSize = true;
            lblAtendidopor.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblAtendidopor.Location = new Point(351, 454);
            lblAtendidopor.Margin = new Padding(4, 0, 4, 0);
            lblAtendidopor.Name = "lblAtendidopor";
            lblAtendidopor.Size = new Size(130, 37);
            lblAtendidopor.TabIndex = 12;
            lblAtendidopor.Text = "Atendido";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCliente.Location = new Point(351, 389);
            lblCliente.Margin = new Padding(4, 0, 4, 0);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(102, 37);
            lblCliente.TabIndex = 11;
            lblCliente.Text = "Cliente";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblFecha.Location = new Point(351, 326);
            lblFecha.Margin = new Padding(4, 0, 4, 0);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(88, 37);
            lblFecha.TabIndex = 10;
            lblFecha.Text = "Fecha";
            // 
            // lblNumeroFact
            // 
            lblNumeroFact.AutoSize = true;
            lblNumeroFact.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblNumeroFact.Location = new Point(351, 260);
            lblNumeroFact.Margin = new Padding(4, 0, 4, 0);
            lblNumeroFact.Name = "lblNumeroFact";
            lblNumeroFact.Size = new Size(135, 37);
            lblNumeroFact.TabIndex = 9;
            lblNumeroFact.Text = "N.Factura";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label9.Location = new Point(134, 326);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(95, 37);
            label9.TabIndex = 8;
            label9.Text = "Fecha:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label8.Location = new Point(134, 521);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(189, 37);
            label8.TabIndex = 7;
            label8.Text = "Metodo Pago:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label7.Location = new Point(134, 260);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(142, 37);
            label7.TabIndex = 6;
            label7.Text = "N.Factura:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label6.Location = new Point(134, 389);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(109, 37);
            label6.TabIndex = 5;
            label6.Text = "Cliente:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label5.Location = new Point(134, 454);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(137, 37);
            label5.TabIndex = 4;
            label5.Text = "Atendido:";
            // 
            // lblLineal
            // 
            lblLineal.AutoSize = true;
            lblLineal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblLineal.Location = new Point(25, 207);
            lblLineal.Margin = new Padding(4, 0, 4, 0);
            lblLineal.Name = "lblLineal";
            lblLineal.Size = new Size(897, 37);
            lblLineal.TabIndex = 3;
            lblLineal.Text = "--------------------------------------------------------------------------------";
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDireccion.Location = new Point(338, 136);
            lblDireccion.Margin = new Padding(4, 0, 4, 0);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(227, 74);
            lblDireccion.TabIndex = 2;
            lblDireccion.Text = "Rivas, Nicaragua \r\nTel: 8472 4904";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSubtitulo.Location = new Point(207, 92);
            lblSubtitulo.Margin = new Padding(4, 0, 4, 0);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(480, 37);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Fritangas, Asados y Bebidas Naturales";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTitulo.Location = new Point(351, 42);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(190, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Asado la Flaca";
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.FromArgb(232, 221, 206);
            pnlBotones.Controls.Add(btnImprimir);
            pnlBotones.Controls.Add(button1);
            pnlBotones.Controls.Add(btnExportar);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 1210);
            pnlBotones.Margin = new Padding(4);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Size = new Size(1037, 116);
            pnlBotones.TabIndex = 56;
            // 
            // button1
            // 
            button1.BackColor = Color.DarkBlue;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(250, 247, 241);
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(390, 16);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(246, 83);
            button1.TabIndex = 55;
            button1.Text = "Export-Excel";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // lblPrecioFinal
            // 
            lblPrecioFinal.AutoSize = true;
            lblPrecioFinal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPrecioFinal.Location = new Point(545, 748);
            lblPrecioFinal.Margin = new Padding(4, 0, 4, 0);
            lblPrecioFinal.Name = "lblPrecioFinal";
            lblPrecioFinal.Size = new Size(93, 37);
            lblPrecioFinal.TabIndex = 23;
            lblPrecioFinal.Text = "Precio";
            // 
            // lblCantFinal
            // 
            lblCantFinal.AutoSize = true;
            lblCantFinal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCantFinal.Location = new Point(352, 748);
            lblCantFinal.Margin = new Padding(4, 0, 4, 0);
            lblCantFinal.Name = "lblCantFinal";
            lblCantFinal.Size = new Size(127, 37);
            lblCantFinal.TabIndex = 22;
            lblCantFinal.Text = "Cantidad";
            // 
            // lblProductoFinal
            // 
            lblProductoFinal.AutoSize = true;
            lblProductoFinal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblProductoFinal.Location = new Point(70, 748);
            lblProductoFinal.Margin = new Padding(4, 0, 4, 0);
            lblProductoFinal.Name = "lblProductoFinal";
            lblProductoFinal.Size = new Size(130, 37);
            lblProductoFinal.TabIndex = 21;
            lblProductoFinal.Text = "Producto";
            // 
            // Comprobante
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1037, 1326);
            Controls.Add(pnlBotones);
            Controls.Add(pnlComprobante);
            Margin = new Padding(4);
            MinimumSize = new Size(750, 600);
            Name = "Comprobante";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Comprobante";
            Shown += Comprobante_Shown;
            pnlComprobante.ResumeLayout(false);
            pnlFactura.ResumeLayout(false);
            pnlFactura.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private Button btnExportar;
        private Button btnImprimir;
        private Panel pnlComprobante;
        private Panel pnlFactura;
        private Panel pnlBotones;
        private DataGridView dgvProductos;
        private Button button1;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label lblLineal;
        private Label lblDireccion;
        private Label lblSubtitulo;
        private Label lblTitulo;
        private Label lblNumeroFact;
        private Label lblPago;
        private Label lblAtendidopor;
        private Label lblCliente;
        private Label lblFecha;
        private Label lblSubTotalFinal;
        private Label lblPrecioFinal;
        private Label lblCantFinal;
        private Label lblProductoFinal;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label lblGracias;
        private Label lblTotal;
    }
}