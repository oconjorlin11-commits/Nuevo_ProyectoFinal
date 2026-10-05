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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevoCliente));
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
            label6 = new Label();
            cmboxAutizado = new ComboBox();
            checboxActico = new CheckBox();
            cmboxNota = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnCancelarClient
            // 
            btnCancelarClient.BackColor = Color.DarkRed;
            btnCancelarClient.FlatStyle = FlatStyle.Flat;
            btnCancelarClient.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelarClient.ForeColor = Color.FromArgb(250, 247, 241);
            // btnCancelarClient.Image = (Image)resources.GetObject("btnCancelarClient.Image");
            btnCancelarClient.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarClient.Location = new Point(486, 726);
            btnCancelarClient.Name = "btnCancelarClient";
            btnCancelarClient.Size = new Size(189, 65);
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
            // btnGuardarClient.Image = (Image)resources.GetObject("btnGuardarClient.Image");
            btnGuardarClient.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarClient.Location = new Point(69, 726);
            btnGuardarClient.Name = "btnGuardarClient";
            btnGuardarClient.Size = new Size(189, 65);
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
            label5.Location = new Point(128, 475);
            label5.Name = "label5";
            label5.Size = new Size(84, 32);
            label5.TabIndex = 43;
            label5.Text = "Notas:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(128, 399);
            label3.Name = "label3";
            label3.Size = new Size(121, 32);
            label3.TabIndex = 42;
            label3.Text = "Direccion:";
            // 
            // txtTelefonoClient
            // 
            txtTelefonoClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTelefonoClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtTelefonoClient.Location = new Point(299, 320);
            txtTelefonoClient.Name = "txtTelefonoClient";
            txtTelefonoClient.Size = new Size(294, 39);
            txtTelefonoClient.TabIndex = 41;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(128, 320);
            label2.Name = "label2";
            label2.Size = new Size(113, 32);
            label2.TabIndex = 40;
            label2.Text = "Telefono:";
            // 
            // txtNombreClient
            // 
            txtNombreClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombreClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreClient.Location = new Point(299, 246);
            txtNombreClient.Name = "txtNombreClient";
            txtNombreClient.Size = new Size(294, 39);
            txtNombreClient.TabIndex = 39;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(128, 249);
            label1.Name = "label1";
            label1.Size = new Size(109, 32);
            label1.TabIndex = 38;
            label1.Text = "Nombre:";
            // 
            // txtCodigoClient
            // 
            txtCodigoClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigoClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoClient.Location = new Point(299, 170);
            txtCodigoClient.Name = "txtCodigoClient";
            txtCodigoClient.Size = new Size(294, 39);
            txtCodigoClient.TabIndex = 37;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(128, 173);
            label4.Name = "label4";
            label4.Size = new Size(97, 32);
            label4.TabIndex = 36;
            label4.Text = "Codigo:";
            // 
            // txtDireccionClient
            // 
            txtDireccionClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDireccionClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtDireccionClient.Location = new Point(299, 396);
            txtDireccionClient.Name = "txtDireccionClient";
            txtDireccionClient.Size = new Size(294, 39);
            txtDireccionClient.TabIndex = 51;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.users_clients_group_16774;
            pictureBox1.Location = new Point(1, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(106, 98);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 52;
            pictureBox1.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(128, 545);
            label6.Name = "label6";
            label6.Size = new Size(140, 32);
            label6.TabIndex = 53;
            label6.Text = "Autorizado:";
            // 
            // cmboxAutizado
            // 
            cmboxAutizado.BackColor = Color.White;
            cmboxAutizado.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxAutizado.FormattingEnabled = true;
            cmboxAutizado.Location = new Point(299, 544);
            cmboxAutizado.Name = "cmboxAutizado";
            cmboxAutizado.Size = new Size(294, 33);
            cmboxAutizado.TabIndex = 54;
            // 
            // checboxActico
            // 
            checboxActico.AutoSize = true;
            checboxActico.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            checboxActico.Location = new Point(299, 623);
            checboxActico.Name = "checboxActico";
            checboxActico.Size = new Size(108, 36);
            checboxActico.TabIndex = 55;
            checboxActico.Text = "Activo";
            checboxActico.UseVisualStyleBackColor = true;
            // 
            // cmboxNota
            // 
            cmboxNota.BackColor = Color.White;
            cmboxNota.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxNota.FormattingEnabled = true;
            cmboxNota.Location = new Point(299, 474);
            cmboxNota.Name = "cmboxNota";
            cmboxNota.Size = new Size(294, 33);
            cmboxNota.TabIndex = 56;
            // 
            // NuevoCliente
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(745, 866);
            Controls.Add(cmboxNota);
            Controls.Add(checboxActico);
            Controls.Add(cmboxAutizado);
            Controls.Add(label6);
            Controls.Add(pictureBox1);
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
            Name = "NuevoCliente";
            Text = "NuevoCliente";
            Load += NuevoCliente_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private Label label6;
        private ComboBox cmboxAutizado;
        private CheckBox checboxActico;
        private ComboBox cmboxNota;
    }
}