namespace Nuevo_Proyecto.Models.Views
{
    partial class Clientescs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Clientescs));
            label1 = new Label();
            txtBuscarClient = new TextBox();
            label4 = new Label();
            dataGridClientes = new DataGridView();
            btnNuevoClien = new Button();
            btnEliminarClie = new Button();
            btnEditarClien = new Button();
            checboxClient = new CheckBox();
            cmboxNotasClient = new ComboBox();
            label8 = new Label();
            txtDireccionClient = new TextBox();
            label6 = new Label();
            txtNombreClient = new TextBox();
            label5 = new Label();
            txtTelefonoClient = new TextBox();
            label3 = new Label();
            txtCodigoClient = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridClientes).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(153, 40, 35);
            label1.Location = new Point(28, 34);
            label1.Name = "label1";
            label1.Size = new Size(120, 38);
            label1.TabIndex = 1;
            label1.Text = "Clientes";
            label1.Click += label1_Click;
            // 
            // txtBuscarClient
            // 
            txtBuscarClient.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscarClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscarClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtBuscarClient.Location = new Point(1019, 36);
            txtBuscarClient.Name = "txtBuscarClient";
            txtBuscarClient.Size = new Size(330, 39);
            txtBuscarClient.TabIndex = 10;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(920, 39);
            label4.Name = "label4";
            label4.Size = new Size(93, 32);
            label4.TabIndex = 9;
            label4.Text = "Buscar:";
            // 
            // dataGridClientes
            // 
            dataGridClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridClientes.Location = new Point(26, 313);
            dataGridClientes.Name = "dataGridClientes";
            dataGridClientes.RowHeadersWidth = 62;
            dataGridClientes.Size = new Size(1321, 425);
            dataGridClientes.TabIndex = 11;
            // 
            // btnNuevoClien
            // 
            btnNuevoClien.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNuevoClien.BackColor = Color.DarkGray;
            btnNuevoClien.FlatStyle = FlatStyle.Flat;
            btnNuevoClien.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNuevoClien.ForeColor = Color.FromArgb(250, 247, 241);
            btnNuevoClien.Image = Properties.Resources.new_add_user_16734__1_;
            btnNuevoClien.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevoClien.Location = new Point(1160, 778);
            btnNuevoClien.Name = "btnNuevoClien";
            btnNuevoClien.Size = new Size(189, 65);
            btnNuevoClien.TabIndex = 12;
            btnNuevoClien.Text = "Nuevo Cliente";
            btnNuevoClien.TextAlign = ContentAlignment.MiddleRight;
            btnNuevoClien.UseVisualStyleBackColor = false;
            btnNuevoClien.Click += btnNuevoClien_Click;
            // 
            // btnEliminarClie
            // 
            btnEliminarClie.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEliminarClie.BackColor = Color.DarkRed;
            btnEliminarClie.FlatStyle = FlatStyle.Flat;
            btnEliminarClie.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminarClie.ForeColor = Color.FromArgb(250, 247, 241);
            btnEliminarClie.Image = (Image)resources.GetObject("btnEliminarClie.Image");
            btnEliminarClie.ImageAlign = ContentAlignment.MiddleLeft;
            btnEliminarClie.Location = new Point(920, 778);
            btnEliminarClie.Name = "btnEliminarClie";
            btnEliminarClie.Size = new Size(189, 65);
            btnEliminarClie.TabIndex = 13;
            btnEliminarClie.Text = "Eliminar";
            btnEliminarClie.UseVisualStyleBackColor = false;
            btnEliminarClie.Click += btnEliminarClie_Click;
            // 
            // btnEditarClien
            // 
            btnEditarClien.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditarClien.BackColor = Color.Green;
            btnEditarClien.FlatStyle = FlatStyle.Flat;
            btnEditarClien.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditarClien.ForeColor = Color.FromArgb(250, 247, 241);
            btnEditarClien.Image = (Image)resources.GetObject("btnEditarClien.Image");
            btnEditarClien.ImageAlign = ContentAlignment.MiddleLeft;
            btnEditarClien.Location = new Point(675, 778);
            btnEditarClien.Name = "btnEditarClien";
            btnEditarClien.Size = new Size(189, 65);
            btnEditarClien.TabIndex = 14;
            btnEditarClien.Text = "Editar";
            btnEditarClien.UseVisualStyleBackColor = false;
            btnEditarClien.Click += btnEditarClien_Click;
            // 
            // checboxClient
            // 
            checboxClient.AutoSize = true;
            checboxClient.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            checboxClient.Location = new Point(626, 239);
            checboxClient.Name = "checboxClient";
            checboxClient.Size = new Size(108, 36);
            checboxClient.TabIndex = 53;
            checboxClient.Text = "Activo";
            checboxClient.UseVisualStyleBackColor = true;
            // 
            // cmboxNotasClient
            // 
            cmboxNotasClient.Font = new Font("Segoe UI", 12F);
            cmboxNotasClient.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxNotasClient.FormattingEnabled = true;
            cmboxNotasClient.Location = new Point(600, 137);
            cmboxNotasClient.Name = "cmboxNotasClient";
            cmboxNotasClient.Size = new Size(209, 40);
            cmboxNotasClient.TabIndex = 50;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(38, 38, 38);
            label8.Location = new Point(656, 102);
            label8.Name = "label8";
            label8.Size = new Size(78, 32);
            label8.TabIndex = 49;
            label8.Text = "Notas";
            // 
            // txtDireccionClient
            // 
            txtDireccionClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDireccionClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtDireccionClient.Location = new Point(348, 239);
            txtDireccionClient.Name = "txtDireccionClient";
            txtDireccionClient.Size = new Size(209, 39);
            txtDireccionClient.TabIndex = 46;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(404, 204);
            label6.Name = "label6";
            label6.Size = new Size(115, 32);
            label6.TabIndex = 45;
            label6.Text = "Direccion";
            // 
            // txtNombreClient
            // 
            txtNombreClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombreClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreClient.Location = new Point(348, 137);
            txtNombreClient.Name = "txtNombreClient";
            txtNombreClient.Size = new Size(209, 39);
            txtNombreClient.TabIndex = 44;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(404, 102);
            label5.Name = "label5";
            label5.Size = new Size(103, 32);
            label5.TabIndex = 43;
            label5.Text = "Nombre";
            // 
            // txtTelefonoClient
            // 
            txtTelefonoClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTelefonoClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtTelefonoClient.Location = new Point(86, 239);
            txtTelefonoClient.Name = "txtTelefonoClient";
            txtTelefonoClient.Size = new Size(209, 39);
            txtTelefonoClient.TabIndex = 42;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(142, 204);
            label3.Name = "label3";
            label3.Size = new Size(107, 32);
            label3.TabIndex = 41;
            label3.Text = "Telefono";
            // 
            // txtCodigoClient
            // 
            txtCodigoClient.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigoClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoClient.Location = new Point(86, 137);
            txtCodigoClient.Name = "txtCodigoClient";
            txtCodigoClient.Size = new Size(209, 39);
            txtCodigoClient.TabIndex = 40;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(142, 102);
            label2.Name = "label2";
            label2.Size = new Size(91, 32);
            label2.TabIndex = 39;
            label2.Text = "Codigo";
            // 
            // Clientescs
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1373, 885);
            Controls.Add(checboxClient);
            Controls.Add(cmboxNotasClient);
            Controls.Add(label8);
            Controls.Add(txtDireccionClient);
            Controls.Add(label6);
            Controls.Add(txtNombreClient);
            Controls.Add(label5);
            Controls.Add(txtTelefonoClient);
            Controls.Add(label3);
            Controls.Add(txtCodigoClient);
            Controls.Add(label2);
            Controls.Add(btnEditarClien);
            Controls.Add(btnEliminarClie);
            Controls.Add(btnNuevoClien);
            Controls.Add(dataGridClientes);
            Controls.Add(txtBuscarClient);
            Controls.Add(label4);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Clientescs";
            Text = "Clientescs";
            Load += Clientescs_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtBuscarClient;
        private Label label4;
        private DataGridView dataGridClientes;
        private Button btnNuevoClien;
        private Button btnEliminarClie;
        private Button btnEditarClien;
        private CheckBox checboxClient;
        private ComboBox cmboxNotasClient;
        private CheckBox checkBoxEmpleado;
        private DateTimePicker dateTimePickerEmpleado;
        private Label label9;
        private ComboBox cmboxCargoEmpl;
        private Label label8;
        private TextBox txtDireccionClient;
        private TextBox txtSalarioEmpl;
        private Label label7;
        private TextBox txtTelefonoEmpl;
        private Label label6;
        private TextBox txtNombreClient;
        private TextBox txtNombreEmpl;
        private Label label5;
        private TextBox txtTelefonoClient;
        private TextBox txtCedulaEmpl;
        private Label label3;
        private TextBox txtCodigoClient;
        private Label label2;
    }
}