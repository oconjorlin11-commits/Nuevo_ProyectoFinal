namespace Nuevo_Proyecto.Views.FormularioUI
{
    partial class Motivo_de_actualizacion_Inventario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Motivo_de_actualizacion_Inventario));
            TxtIngreseElMotivo = new TextBox();
            label2 = new Label();
            btnAceptar = new Button();
            btnCancelar = new Button();
            panel1 = new Panel();
            label3 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // TxtIngreseElMotivo
            // 
            TxtIngreseElMotivo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TxtIngreseElMotivo.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            TxtIngreseElMotivo.ForeColor = Color.FromArgb(38, 38, 38);
            TxtIngreseElMotivo.Location = new Point(12, 204);
            TxtIngreseElMotivo.Margin = new Padding(2);
            TxtIngreseElMotivo.MaxLength = 2000;
            TxtIngreseElMotivo.Multiline = true;
            TxtIngreseElMotivo.Name = "TxtIngreseElMotivo";
            TxtIngreseElMotivo.PlaceholderText = "Ejemplo: Ingreso de nuevos Productos, Correccion de stock, Cambios de precios, etc.";
            TxtIngreseElMotivo.ScrollBars = ScrollBars.Both;
            TxtIngreseElMotivo.Size = new Size(452, 165);
            TxtIngreseElMotivo.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(12, 164);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(418, 25);
            label2.TabIndex = 3;
            label2.Text = "Escriba el motivo de la actualizacion del Inventario";
            // 
            // btnAceptar
            // 
            btnAceptar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAceptar.BackColor = Color.DarkGreen;
            btnAceptar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAceptar.ForeColor = Color.FromArgb(250, 247, 241);
            btnAceptar.Image = Properties.Resources.lista_de_verificacion__1_;
            btnAceptar.ImageAlign = ContentAlignment.MiddleLeft;
            btnAceptar.Location = new Point(12, 386);
            btnAceptar.Margin = new Padding(2);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(151, 52);
            btnAceptar.TabIndex = 4;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += BtnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.BackColor = Color.DarkRed;
            btnCancelar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.FromArgb(250, 247, 241);
            btnCancelar.Image = (Image)resources.GetObject("btnCancelar.Image");
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(313, 386);
            btnCancelar.Margin = new Padding(2);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(151, 52);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(250, 247, 241);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(452, 122);
            panel1.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            label3.Location = new Point(121, 57);
            label3.Name = "label3";
            label3.Size = new Size(325, 50);
            label3.TabIndex = 2;
            label3.Text = "Indique el motivo por el cual\r\n se realiza la actualización el inventario";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(153, 40, 35);
            label1.Location = new Point(125, 18);
            label1.Name = "label1";
            label1.Size = new Size(321, 25);
            label1.TabIndex = 1;
            label1.Text = "Motivos de Actualizacion de Inventario";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(16, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(77, 89);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Motivo_de_actualizacion_Inventario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(476, 449);
            Controls.Add(panel1);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(label2);
            Controls.Add(TxtIngreseElMotivo);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            Name = "Motivo_de_actualizacion_Inventario";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TxtIngreseElMotivo;
        private Label label2;
        private Button btnAceptar;
        private Button btnCancelar;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label3;
    }
}