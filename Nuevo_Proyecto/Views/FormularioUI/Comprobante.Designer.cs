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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Comprobante));
            btnExportar = new Button();
            btnImprimir = new Button();
            pnlComprobante = new Panel();
            pnlFactura = new Panel();
            lblGracias = new Label();
            lblTotal = new Label();
            lblSubTotalFinal = new Label();
            lblPrecioFinal = new Label();
            lblCantFinal = new Label();
            lblProductoFinal = new Label();
            lblSubTotal = new Label();
            lblPrecio = new Label();
            lblCantidad = new Label();
            lblProducto = new Label();
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
            button1 = new Button();
            pnlComprobante.SuspendLayout();
            pnlFactura.SuspendLayout();
            SuspendLayout();
            // 
            // btnExportar
            // 
            btnExportar.BackColor = Color.DarkRed;
            btnExportar.FlatStyle = FlatStyle.Flat;
            btnExportar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.ForeColor = Color.FromArgb(250, 247, 241);
            btnExportar.Image = (Image)resources.GetObject("btnExportar.Image");
            btnExportar.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportar.Location = new Point(529, 940);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(189, 65);
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
            btnImprimir.Image = (Image)resources.GetObject("btnImprimir.Image");
            btnImprimir.ImageAlign = ContentAlignment.MiddleLeft;
            btnImprimir.Location = new Point(30, 940);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(189, 65);
            btnImprimir.TabIndex = 51;
            btnImprimir.Text = "Imprimir";
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // pnlComprobante
            // 
            pnlComprobante.BackColor = Color.FromArgb(232, 221, 206);
            pnlComprobante.Controls.Add(pnlFactura);
            pnlComprobante.Dock = DockStyle.Top;
            pnlComprobante.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            pnlComprobante.Location = new Point(0, 0);
            pnlComprobante.Name = "pnlComprobante";
            pnlComprobante.Size = new Size(745, 917);
            pnlComprobante.TabIndex = 54;
            pnlComprobante.Paint += pnlComprobante_Paint;
            // 
            // pnlFactura
            // 
            pnlFactura.BackColor = Color.FromArgb(250, 247, 241);
            pnlFactura.Controls.Add(lblGracias);
            pnlFactura.Controls.Add(lblTotal);
            pnlFactura.Controls.Add(lblSubTotalFinal);
            pnlFactura.Controls.Add(lblPrecioFinal);
            pnlFactura.Controls.Add(lblCantFinal);
            pnlFactura.Controls.Add(lblProductoFinal);
            pnlFactura.Controls.Add(lblSubTotal);
            pnlFactura.Controls.Add(lblPrecio);
            pnlFactura.Controls.Add(lblCantidad);
            pnlFactura.Controls.Add(lblProducto);
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
            pnlFactura.Location = new Point(31, 32);
            pnlFactura.Name = "pnlFactura";
            pnlFactura.Size = new Size(688, 860);
            pnlFactura.TabIndex = 0;
            pnlFactura.Paint += pnlFactura_Paint;
            // 
            // lblGracias
            // 
            lblGracias.AutoSize = true;
            lblGracias.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblGracias.Location = new Point(260, 735);
            lblGracias.Name = "lblGracias";
            lblGracias.Size = new Size(346, 56);
            lblGracias.TabIndex = 26;
            lblGracias.Text = "\"Gracias por su compra. \r\n¡Esperamos atenderle Nuevamente!\"\r\n";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTotal.Location = new Point(468, 695);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(127, 28);
            lblTotal.TabIndex = 25;
            lblTotal.Text = "Total a Pagar";
            // 
            // lblSubTotalFinal
            // 
            lblSubTotalFinal.AutoSize = true;
            lblSubTotalFinal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSubTotalFinal.Location = new Point(545, 584);
            lblSubTotalFinal.Name = "lblSubTotalFinal";
            lblSubTotalFinal.Size = new Size(90, 28);
            lblSubTotalFinal.TabIndex = 24;
            lblSubTotalFinal.Text = "SubTotal";
            // 
            // lblPrecioFinal
            // 
            lblPrecioFinal.AutoSize = true;
            lblPrecioFinal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPrecioFinal.Location = new Point(419, 584);
            lblPrecioFinal.Name = "lblPrecioFinal";
            lblPrecioFinal.Size = new Size(68, 28);
            lblPrecioFinal.TabIndex = 23;
            lblPrecioFinal.Text = "Precio";
            // 
            // lblCantFinal
            // 
            lblCantFinal.AutoSize = true;
            lblCantFinal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCantFinal.Location = new Point(271, 584);
            lblCantFinal.Name = "lblCantFinal";
            lblCantFinal.Size = new Size(92, 28);
            lblCantFinal.TabIndex = 22;
            lblCantFinal.Text = "Cantidad";
            // 
            // lblProductoFinal
            // 
            lblProductoFinal.AutoSize = true;
            lblProductoFinal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblProductoFinal.Location = new Point(54, 584);
            lblProductoFinal.Name = "lblProductoFinal";
            lblProductoFinal.Size = new Size(95, 28);
            lblProductoFinal.TabIndex = 21;
            lblProductoFinal.Text = "Producto";
            // 
            // lblSubTotal
            // 
            lblSubTotal.AutoSize = true;
            lblSubTotal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSubTotal.Location = new Point(545, 505);
            lblSubTotal.Name = "lblSubTotal";
            lblSubTotal.Size = new Size(90, 28);
            lblSubTotal.TabIndex = 20;
            lblSubTotal.Text = "SubTotal";
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPrecio.Location = new Point(419, 505);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(68, 28);
            lblPrecio.TabIndex = 19;
            lblPrecio.Text = "Precio";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCantidad.Location = new Point(271, 505);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(92, 28);
            lblCantidad.TabIndex = 18;
            lblCantidad.Text = "Cantidad";
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblProducto.Location = new Point(54, 505);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(95, 28);
            lblProducto.TabIndex = 17;
            lblProducto.Text = "Producto";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label3.Location = new Point(19, 667);
            label3.Name = "label3";
            label3.Size = new Size(652, 28);
            label3.TabIndex = 16;
            label3.Text = "--------------------------------------------------------------------------------";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label2.Location = new Point(19, 545);
            label2.Name = "label2";
            label2.Size = new Size(652, 28);
            label2.TabIndex = 15;
            label2.Text = "--------------------------------------------------------------------------------";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label1.Location = new Point(19, 460);
            label1.Name = "label1";
            label1.Size = new Size(652, 28);
            label1.TabIndex = 14;
            label1.Text = "--------------------------------------------------------------------------------";
            // 
            // lblPago
            // 
            lblPago.AutoSize = true;
            lblPago.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPago.Location = new Point(270, 407);
            lblPago.Name = "lblPago";
            lblPago.Size = new Size(135, 28);
            lblPago.TabIndex = 13;
            lblPago.Text = "Metodo Pago";
            // 
            // lblAtendidopor
            // 
            lblAtendidopor.AutoSize = true;
            lblAtendidopor.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblAtendidopor.Location = new Point(270, 355);
            lblAtendidopor.Name = "lblAtendidopor";
            lblAtendidopor.Size = new Size(96, 28);
            lblAtendidopor.TabIndex = 12;
            lblAtendidopor.Text = "Atendido";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCliente.Location = new Point(270, 304);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(75, 28);
            lblCliente.TabIndex = 11;
            lblCliente.Text = "Cliente";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblFecha.Location = new Point(270, 255);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(64, 28);
            lblFecha.TabIndex = 10;
            lblFecha.Text = "Fecha";
            // 
            // lblNumeroFact
            // 
            lblNumeroFact.AutoSize = true;
            lblNumeroFact.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblNumeroFact.Location = new Point(270, 203);
            lblNumeroFact.Name = "lblNumeroFact";
            lblNumeroFact.Size = new Size(96, 28);
            lblNumeroFact.TabIndex = 9;
            lblNumeroFact.Text = "N.Factura";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label9.Location = new Point(103, 255);
            label9.Name = "label9";
            label9.Size = new Size(69, 28);
            label9.TabIndex = 8;
            label9.Text = "Fecha:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label8.Location = new Point(103, 407);
            label8.Name = "label8";
            label8.Size = new Size(140, 28);
            label8.TabIndex = 7;
            label8.Text = "Metodo Pago:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label7.Location = new Point(103, 203);
            label7.Name = "label7";
            label7.Size = new Size(101, 28);
            label7.TabIndex = 6;
            label7.Text = "N.Factura:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label6.Location = new Point(103, 304);
            label6.Name = "label6";
            label6.Size = new Size(80, 28);
            label6.TabIndex = 5;
            label6.Text = "Cliente:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label5.Location = new Point(103, 355);
            label5.Name = "label5";
            label5.Size = new Size(101, 28);
            label5.TabIndex = 4;
            label5.Text = "Atendido:";
            // 
            // lblLineal
            // 
            lblLineal.AutoSize = true;
            lblLineal.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblLineal.Location = new Point(19, 162);
            lblLineal.Name = "lblLineal";
            lblLineal.Size = new Size(652, 28);
            lblLineal.TabIndex = 3;
            lblLineal.Text = "--------------------------------------------------------------------------------";
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDireccion.Location = new Point(260, 106);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(165, 56);
            lblDireccion.TabIndex = 2;
            lblDireccion.Text = "Rivas, Nicaragua \r\nTel: 8472 4904";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSubtitulo.Location = new Point(159, 72);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(355, 28);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Fritangas, Asados y Bebidas Naturales";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTitulo.Location = new Point(270, 33);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(139, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Asado la Flaca";
            // 
            // button1
            // 
            button1.BackColor = Color.Green;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(250, 247, 241);
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(291, 940);
            button1.Name = "button1";
            button1.Size = new Size(189, 65);
            button1.TabIndex = 55;
            button1.Text = "Export-PDF";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnExportar_Click;
            // 
            // Comprobante
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(745, 1036);
            Controls.Add(button1);
            Controls.Add(pnlComprobante);
            Controls.Add(btnExportar);
            Controls.Add(btnImprimir);
            Name = "Comprobante";
            Text = "Comprobante";
            pnlComprobante.ResumeLayout(false);
            pnlFactura.ResumeLayout(false);
            pnlFactura.PerformLayout();
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
        private Label lblSubTotal;
        private Label lblPrecio;
        private Label lblCantidad;
        private Label lblProducto;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label lblGracias;
        private Label lblTotal;
    }
}