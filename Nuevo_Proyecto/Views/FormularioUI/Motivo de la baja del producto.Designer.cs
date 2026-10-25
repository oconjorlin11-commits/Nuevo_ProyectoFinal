namespace Nuevo_Proyecto.Views.FormularioUI
{
    partial class Motivo_de_la_baja_del_producto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Motivo_de_la_baja_del_producto));
            btnCancelar = new Button();
            btnAceptar = new Button();
            label2 = new Label();
            TxtAutorizadoPor = new TextBox();
            TxtIngreseElMotivo = new TextBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.DarkRed;
            btnCancelar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.FromArgb(250, 247, 241);
            btnCancelar.Image = Properties.Resources.x2;
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(509, 618);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(245, 83);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.DarkGreen;
            btnAceptar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAceptar.ForeColor = Color.FromArgb(250, 247, 241);
            btnAceptar.Image = Properties.Resources.lista_de_verificacion__1_2;
            btnAceptar.ImageAlign = ContentAlignment.MiddleLeft;
            btnAceptar.Location = new Point(20, 619);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(245, 83);
            btnAceptar.TabIndex = 10;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += BtnAceptar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(20, 262);
            label2.Name = "label2";
            label2.Size = new Size(545, 40);
            label2.TabIndex = 9;
            label2.Text = "Escriba el motivo de la Baja del Producto";
            // 
            // TxtAutorizadoPor
            // 
            TxtAutorizadoPor.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            TxtAutorizadoPor.ForeColor = Color.FromArgb(38, 38, 38);
            TxtAutorizadoPor.Location = new Point(172, 146);
            TxtAutorizadoPor.Name = "TxtAutorizadoPor";
            TxtAutorizadoPor.ReadOnly = true;
            TxtAutorizadoPor.Size = new Size(431, 46);
            TxtAutorizadoPor.TabIndex = 8;
            // 
            // TxtIngreseElMotivo
            // 
            TxtIngreseElMotivo.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            TxtIngreseElMotivo.ForeColor = Color.FromArgb(38, 38, 38);
            TxtIngreseElMotivo.Location = new Point(21, 326);
            TxtIngreseElMotivo.MaxLength = 2000;
            TxtIngreseElMotivo.Multiline = true;
            TxtIngreseElMotivo.Name = "TxtIngreseElMotivo";
            TxtIngreseElMotivo.PlaceholderText = "Ejemplo: Robos etc.....";
            TxtIngreseElMotivo.ScrollBars = ScrollBars.Both;
            TxtIngreseElMotivo.Size = new Size(732, 262);
            TxtIngreseElMotivo.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic);
            label1.Location = new Point(289, 72);
            label1.Name = "label1";
            label1.Size = new Size(211, 40);
            label1.TabIndex = 6;
            label1.Text = "Autorizado por";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(26, 29);
            pictureBox1.Margin = new Padding(5, 5, 5, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 142);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // Motivo_de_la_baja_del_producto
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(774, 718);
            Controls.Add(pictureBox1);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(label2);
            Controls.Add(TxtAutorizadoPor);
            Controls.Add(TxtIngreseElMotivo);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Motivo_de_la_baja_del_producto";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancelar;
        private Button btnAceptar;
        private Label label2;
        private TextBox TxtAutorizadoPor;
        private TextBox TxtIngreseElMotivo;
        private Label label1;
        private PictureBox pictureBox1;
    }
}