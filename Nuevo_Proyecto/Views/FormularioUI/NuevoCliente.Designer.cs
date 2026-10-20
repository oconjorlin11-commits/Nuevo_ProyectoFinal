namespace Nuevo_Proyecto.Models.Views
{
    partial class NuevoCliente
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
            btnCancelarClient = new Button();
            btnGuardarClient = new Button();
            label5 = new Label();
            label3 = new Label();
            txtTelefonoClient = new TextBox();
            label2 = new Label();
            txtNombreClient = new TextBox();
            label1 = new Label();
            txtCodigoClient = new TextBox();
            label4 = new Label();
            txtDireccionClient = new TextBox();
            pictureBox1 = new PictureBox();
            checboxActico = new CheckBox();
            cmboxNota = new ComboBox();
            panel1 = new Panel();
            label6 = new Label();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnCancelarClient
            // 
            btnCancelarClient.BackColor = Color.DarkRed;
            btnCancelarClient.FlatStyle = FlatStyle.Flat;
            btnCancelarClient.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelarClient.ForeColor = Color.FromArgb(250, 247, 241);
            btnCancelarClient.Image = Properties.Resources.x3;
            btnCancelarClient.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarClient.Location = new Point(320, 584);
            btnCancelarClient.Margin = new Padding(2, 2, 2, 2);
            btnCancelarClient.Name = "btnCancelarClient";
            btnCancelarClient.Size = new Size(151, 52);
            btnCancelarClient.TabIndex = 50;
            btnCancelarClient.Text = "Cancelar";
            btnCancelarClient.UseVisualStyleBackColor = false;
            btnCancelarClient.Click += btnCancelarClient_Click;
            // 
            // btnGuardarClient
            // 
            btnGuardarClient.BackColor = Color.DarkGreen;
            btnGuardarClient.FlatStyle = FlatStyle.Flat;
            btnGuardarClient.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarClient.ForeColor = Color.FromArgb(250, 247, 241);
            btnGuardarClient.Image = Properties.Resources.lista_de_verificacion__1_3;
            btnGuardarClient.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarClient.Location = new Point(11, 584);
            btnGuardarClient.Margin = new Padding(2, 2, 2, 2);
            btnGuardarClient.Name = "btnGuardarClient";
            btnGuardarClient.Size = new Size(151, 52);
            btnGuardarClient.TabIndex = 49;
            btnGuardarClient.Text = "Guardar ";
            btnGuardarClient.UseVisualStyleBackColor = false;
            btnGuardarClient.Click += btnGuardarClient_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(59, 420);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(70, 28);
            label5.TabIndex = 43;
            label5.Text = "Notas:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(59, 360);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(101, 28);
            label3.TabIndex = 42;
            label3.Text = "Direccion:";
            // 
            // txtTelefonoClient
            // 
            txtTelefonoClient.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtTelefonoClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtTelefonoClient.Location = new Point(196, 300);
            txtTelefonoClient.Margin = new Padding(2, 2, 2, 2);
            txtTelefonoClient.Name = "txtTelefonoClient";
            txtTelefonoClient.PlaceholderText = "Ej: 1234567890";
            txtTelefonoClient.Size = new Size(236, 31);
            txtTelefonoClient.TabIndex = 41;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(59, 300);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(96, 28);
            label2.TabIndex = 40;
            label2.Text = "Telefono:";
            // 
            // txtNombreClient
            // 
            txtNombreClient.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtNombreClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreClient.Location = new Point(196, 240);
            txtNombreClient.Margin = new Padding(2, 2, 2, 2);
            txtNombreClient.Name = "txtNombreClient";
            txtNombreClient.PlaceholderText = "Ej: Juan Pérez";
            txtNombreClient.Size = new Size(236, 31);
            txtNombreClient.TabIndex = 39;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(59, 240);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(92, 28);
            label1.TabIndex = 38;
            label1.Text = "Nombre:";
            // 
            // txtCodigoClient
            // 
            txtCodigoClient.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtCodigoClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoClient.Location = new Point(196, 180);
            txtCodigoClient.Margin = new Padding(2, 2, 2, 2);
            txtCodigoClient.Name = "txtCodigoClient";
            txtCodigoClient.PlaceholderText = "CLI-000";
            txtCodigoClient.Size = new Size(236, 31);
            txtCodigoClient.TabIndex = 37;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(59, 180);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(82, 28);
            label4.TabIndex = 36;
            label4.Text = "Codigo:";
            // 
            // txtDireccionClient
            // 
            txtDireccionClient.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            txtDireccionClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtDireccionClient.Location = new Point(196, 360);
            txtDireccionClient.Margin = new Padding(2, 2, 2, 2);
            txtDireccionClient.Name = "txtDireccionClient";
            txtDireccionClient.PlaceholderText = "Ej: Calle Principal 123, Apto 4B";
            txtDireccionClient.Size = new Size(236, 31);
            txtDireccionClient.TabIndex = 51;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.users_clients_group_16774;
            pictureBox1.Location = new Point(16, 18);
            pictureBox1.Margin = new Padding(2, 2, 2, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(85, 78);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 52;
            pictureBox1.TabStop = false;
            // 
            // checboxActico
            // 
            checboxActico.AutoSize = true;
            checboxActico.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            checboxActico.Location = new Point(196, 480);
            checboxActico.Margin = new Padding(2, 2, 2, 2);
            checboxActico.Name = "checboxActico";
            checboxActico.Size = new Size(83, 29);
            checboxActico.TabIndex = 55;
            checboxActico.Text = "Activo";
            checboxActico.UseVisualStyleBackColor = true;
            // 
            // cmboxNota
            // 
            cmboxNota.BackColor = Color.White;
            cmboxNota.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            cmboxNota.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxNota.FormattingEnabled = true;
            cmboxNota.Location = new Point(196, 420);
            cmboxNota.Margin = new Padding(2, 2, 2, 2);
            cmboxNota.Name = "cmboxNota";
            cmboxNota.Size = new Size(236, 33);
            cmboxNota.TabIndex = 56;
            cmboxNota.Text = "Tipos de Clientes";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(250, 247, 241);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(452, 122);
            panel1.TabIndex = 57;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(153, 40, 35);
            label6.Location = new Point(133, 18);
            label6.Name = "label6";
            label6.Size = new Size(124, 25);
            label6.TabIndex = 53;
            label6.Text = "Nuevo Cliente";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic);
            label7.Location = new Point(133, 46);
            label7.Name = "label7";
            label7.Size = new Size(287, 50);
            label7.TabIndex = 54;
            label7.Text = "Ingrese los datos del nuevo cliente \r\nen los campos vacíos";
            // 
            // NuevoCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(482, 659);
            Controls.Add(panel1);
            Controls.Add(cmboxNota);
            Controls.Add(checboxActico);
            Controls.Add(txtDireccionClient);
            Controls.Add(btnCancelarClient);
            Controls.Add(btnGuardarClient);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(txtTelefonoClient);
            Controls.Add(label2);
            Controls.Add(txtNombreClient);
            Controls.Add(label1);
            Controls.Add(txtCodigoClient);
            Controls.Add(label4);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2, 2, 2, 2);
            Name = "NuevoCliente";
            Text = "NuevoCliente";
            Load += NuevoCliente_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void cmboxNota_SelectedIndexChanged(object sender, EventArgs e)
        {
            // No action required on designer-side; event exists for future logic in the form class.
        }

        private void checboxActico_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void txtDireccionClient_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void txtTelefonoClient_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Button btnCancelarClient;
        private Button btnGuardarClient;
        private Label label5;
        private Label label3;
        private TextBox txtTelefonoClient;
        private Label label2;
        private TextBox txtNombreClient;
        private Label label1;
        private TextBox txtCodigoClient;
        private Label label4;
        private TextBox txtDireccionClient;
        private PictureBox pictureBox1;
        private CheckBox checboxActico;
        private ComboBox cmboxNota;
        private Panel panel1;
        private Label label6;
        private Label label7;
    }
}