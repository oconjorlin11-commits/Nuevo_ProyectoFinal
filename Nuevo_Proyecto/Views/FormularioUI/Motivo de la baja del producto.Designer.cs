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
            btnCancelar = new Button();
            btnAceptar = new Button();
            label2 = new Label();
            TxtAutorizadoPor = new TextBox();
            TxtIngreseElMotivo = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Red;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(514, 566);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 40);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.LimeGreen;
            btnAceptar.ForeColor = Color.White;
            btnAceptar.Location = new Point(364, 566);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(120, 40);
            btnAceptar.TabIndex = 10;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += BtnAceptar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(197, 266);
            label2.Name = "label2";
            label2.Size = new Size(448, 32);
            label2.TabIndex = 9;
            label2.Text = "Escriba el motivo de la Baja del Producto";
            // 
            // TxtAutorizadoPor
            // 
            TxtAutorizadoPor.Location = new Point(180, 172);
            TxtAutorizadoPor.Name = "TxtAutorizadoPor";
            TxtAutorizadoPor.ReadOnly = true;
            TxtAutorizadoPor.Size = new Size(432, 39);
            TxtAutorizadoPor.TabIndex = 8;
            // 
            // TxtIngreseElMotivo
            // 
            TxtIngreseElMotivo.Location = new Point(91, 346);
            TxtIngreseElMotivo.MaxLength = 2000;
            TxtIngreseElMotivo.Multiline = true;
            TxtIngreseElMotivo.Name = "TxtIngreseElMotivo";
            TxtIngreseElMotivo.ScrollBars = ScrollBars.Both;
            TxtIngreseElMotivo.Size = new Size(611, 200);
            TxtIngreseElMotivo.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(310, 101);
            label1.Name = "label1";
            label1.Size = new Size(173, 32);
            label1.TabIndex = 6;
            label1.Text = "Autorizado por";
            // 
            // Motivo_de_la_baja_del_producto
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 713);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(label2);
            Controls.Add(TxtAutorizadoPor);
            Controls.Add(TxtIngreseElMotivo);
            Controls.Add(label1);
            Name = "Motivo_de_la_baja_del_producto";
            Text = "Motivo_de_la_baja_del_producto";
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
    }
}