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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnCancelarClient
            // 
            btnCancelarClient.BackColor = Color.DarkRed;
            btnCancelarClient.FlatStyle = FlatStyle.Flat;
            btnCancelarClient.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelarClient.ForeColor = Color.FromArgb(250, 247, 241);
            btnCancelarClient.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarClient.Location = new Point(632, 929);
            btnCancelarClient.Margin = new Padding(4, 4, 4, 4);
            btnCancelarClient.Name = "btnCancelarClient";
            btnCancelarClient.Size = new Size(246, 83);
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
            btnGuardarClient.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarClient.Location = new Point(90, 929);
            btnGuardarClient.Margin = new Padding(4, 4, 4, 4);
            btnGuardarClient.Name = "btnGuardarClient";
            btnGuardarClient.Size = new Size(246, 83);
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
            label5.Location = new Point(166, 608);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(115, 45);
            label5.TabIndex = 43;
            label5.Text = "Notas:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(166, 511);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(164, 45);
            label3.TabIndex = 42;
            label3.Text = "Direccion:";
            // 
            // txtTelefonoClient
            // 
            txtTelefonoClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTelefonoClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtTelefonoClient.Location = new Point(389, 410);
            txtTelefonoClient.Margin = new Padding(4, 4, 4, 4);
            txtTelefonoClient.Name = "txtTelefonoClient";
            txtTelefonoClient.PlaceholderText = "Ej: 1234567890";
            txtTelefonoClient.Size = new Size(381, 50);
            txtTelefonoClient.TabIndex = 41;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(166, 410);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(153, 45);
            label2.TabIndex = 40;
            label2.Text = "Telefono:";
            // 
            // txtNombreClient
            // 
            txtNombreClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombreClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreClient.Location = new Point(389, 315);
            txtNombreClient.Margin = new Padding(4, 4, 4, 4);
            txtNombreClient.Name = "txtNombreClient";
            txtNombreClient.PlaceholderText = "Ej: Juan Pérez";
            txtNombreClient.Size = new Size(381, 50);
            txtNombreClient.TabIndex = 39;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(166, 319);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(148, 45);
            label1.TabIndex = 38;
            label1.Text = "Nombre:";
            // 
            // txtCodigoClient
            // 
            txtCodigoClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigoClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoClient.Location = new Point(389, 218);
            txtCodigoClient.Margin = new Padding(4, 4, 4, 4);
            txtCodigoClient.Name = "txtCodigoClient";
            txtCodigoClient.Size = new Size(381, 50);
            txtCodigoClient.TabIndex = 37;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(166, 221);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(132, 45);
            label4.TabIndex = 36;
            label4.Text = "Codigo:";
            // 
            // txtDireccionClient
            // 
            txtDireccionClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDireccionClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtDireccionClient.Location = new Point(389, 507);
            txtDireccionClient.Margin = new Padding(4, 4, 4, 4);
            txtDireccionClient.Name = "txtDireccionClient";
            txtDireccionClient.PlaceholderText = "Ej: Calle Principal 123, Apto 4B";
            txtDireccionClient.Size = new Size(381, 50);
            txtDireccionClient.TabIndex = 51;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.users_clients_group_16774;
            pictureBox1.Location = new Point(1, 0);
            pictureBox1.Margin = new Padding(4, 4, 4, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(138, 125);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 52;
            pictureBox1.TabStop = false;
            // 
            // checboxActico
            // 
            checboxActico.AutoSize = true;
            checboxActico.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            checboxActico.Location = new Point(389, 797);
            checboxActico.Margin = new Padding(4, 4, 4, 4);
            checboxActico.Name = "checboxActico";
            checboxActico.Size = new Size(143, 49);
            checboxActico.TabIndex = 55;
            checboxActico.Text = "Activo";
            checboxActico.UseVisualStyleBackColor = true;
            // 
            // cmboxNota
            // 
            cmboxNota.BackColor = Color.White;
            cmboxNota.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxNota.FormattingEnabled = true;
            cmboxNota.Location = new Point(389, 607);
            cmboxNota.Margin = new Padding(4, 4, 4, 4);
            cmboxNota.Name = "cmboxNota";
            cmboxNota.Size = new Size(381, 40);
            cmboxNota.TabIndex = 56;
            // 
            // NuevoCliente
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(968, 1108);
            Controls.Add(cmboxNota);
            Controls.Add(checboxActico);
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
            Margin = new Padding(4, 4, 4, 4);
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
        private CheckBox checboxActico;
        private ComboBox cmboxNota;
    }
}