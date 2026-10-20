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
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridClientes).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(153, 40, 35);
            label1.Location = new Point(90, 32);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(104, 32);
            label1.TabIndex = 1;
            label1.Text = "Clientes";
            label1.Click += label1_Click;
            // 
            // txtBuscarClient
            // 
            txtBuscarClient.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscarClient.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtBuscarClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtBuscarClient.Location = new Point(813, 31);
            txtBuscarClient.Margin = new Padding(2);
            txtBuscarClient.Name = "txtBuscarClient";
            txtBuscarClient.PlaceholderText = "Codigo, Nombre";
            txtBuscarClient.Size = new Size(265, 31);
            txtBuscarClient.TabIndex = 10;
            txtBuscarClient.TextChanged += txtBuscarClientes_TextChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(733, 32);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(77, 28);
            label4.TabIndex = 9;
            label4.Text = "Buscar:";
            // 
            // dataGridClientes
            // 
            dataGridClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridClientes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridClientes.BackgroundColor = Color.DarkGray;
            dataGridClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridClientes.Location = new Point(21, 376);
            dataGridClientes.Margin = new Padding(2);
            dataGridClientes.Name = "dataGridClientes";
            dataGridClientes.RowHeadersWidth = 62;
            dataGridClientes.Size = new Size(1057, 233);
            dataGridClientes.TabIndex = 11;
            dataGridClientes.SelectionChanged += dataGridClientes_SelectionChanged;
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
            btnNuevoClien.Location = new Point(920, 622);
            btnNuevoClien.Margin = new Padding(2);
            btnNuevoClien.Name = "btnNuevoClien";
            btnNuevoClien.Size = new Size(158, 52);
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
            btnEliminarClie.Image = Properties.Resources._32officeicons_3_89720__1_;
            btnEliminarClie.ImageAlign = ContentAlignment.MiddleLeft;
            btnEliminarClie.Location = new Point(720, 622);
            btnEliminarClie.Margin = new Padding(2);
            btnEliminarClie.Name = "btnEliminarClie";
            btnEliminarClie.Size = new Size(158, 52);
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
            btnEditarClien.Image = Properties.Resources.edit_validated256_25237;
            btnEditarClien.ImageAlign = ContentAlignment.MiddleLeft;
            btnEditarClien.Location = new Point(507, 622);
            btnEditarClien.Margin = new Padding(2);
            btnEditarClien.Name = "btnEditarClien";
            btnEditarClien.Size = new Size(158, 52);
            btnEditarClien.TabIndex = 14;
            btnEditarClien.Text = "Editar";
            btnEditarClien.UseVisualStyleBackColor = false;
            btnEditarClien.Click += btnEditarClien_Click;
            // 
            // checboxClient
            // 
            checboxClient.AutoSize = true;
            checboxClient.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            checboxClient.Location = new Point(699, 92);
            checboxClient.Margin = new Padding(2);
            checboxClient.Name = "checboxClient";
            checboxClient.Size = new Size(90, 32);
            checboxClient.TabIndex = 53;
            checboxClient.Text = "Activo";
            checboxClient.UseVisualStyleBackColor = true;
            // 
            // cmboxNotasClient
            // 
            cmboxNotasClient.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            cmboxNotasClient.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxNotasClient.FormattingEnabled = true;
            cmboxNotasClient.Location = new Point(773, 38);
            cmboxNotasClient.Margin = new Padding(2);
            cmboxNotasClient.Name = "cmboxNotasClient";
            cmboxNotasClient.Size = new Size(188, 33);
            cmboxNotasClient.TabIndex = 50;
            cmboxNotasClient.Text = "Seleccione una Opcion";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(38, 38, 38);
            label8.Location = new Point(699, 41);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(70, 28);
            label8.TabIndex = 49;
            label8.Text = "Notas:";
            // 
            // txtDireccionClient
            // 
            txtDireccionClient.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtDireccionClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtDireccionClient.Location = new Point(456, 98);
            txtDireccionClient.Margin = new Padding(2);
            txtDireccionClient.Multiline = true;
            txtDireccionClient.Name = "txtDireccionClient";
            txtDireccionClient.PlaceholderText = "Colonia Miguel Bonilla, Casa 127 Mangua.";
            txtDireccionClient.Size = new Size(188, 93);
            txtDireccionClient.TabIndex = 46;
            txtDireccionClient.TextChanged += txtDireccionClient_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(351, 101);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(101, 28);
            label6.TabIndex = 45;
            label6.Text = "Direccion:";
            // 
            // txtNombreClient
            // 
            txtNombreClient.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtNombreClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreClient.Location = new Point(122, 95);
            txtNombreClient.Margin = new Padding(2);
            txtNombreClient.Name = "txtNombreClient";
            txtNombreClient.PlaceholderText = "Nombre cliente";
            txtNombreClient.Size = new Size(188, 31);
            txtNombreClient.TabIndex = 44;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(26, 98);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(92, 28);
            label5.TabIndex = 43;
            label5.Text = "Nombre:";
            // 
            // txtTelefonoClient
            // 
            txtTelefonoClient.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtTelefonoClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtTelefonoClient.Location = new Point(456, 41);
            txtTelefonoClient.Margin = new Padding(2);
            txtTelefonoClient.Name = "txtTelefonoClient";
            txtTelefonoClient.PlaceholderText = "8890-5430";
            txtTelefonoClient.Size = new Size(188, 31);
            txtTelefonoClient.TabIndex = 42;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(356, 44);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(96, 28);
            label3.TabIndex = 41;
            label3.Text = "Telefono:";
            // 
            // txtCodigoClient
            // 
            txtCodigoClient.BackColor = SystemColors.Window;
            txtCodigoClient.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtCodigoClient.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoClient.Location = new Point(122, 38);
            txtCodigoClient.Margin = new Padding(2);
            txtCodigoClient.Name = "txtCodigoClient";
            txtCodigoClient.PlaceholderText = "CLI-000";
            txtCodigoClient.Size = new Size(188, 31);
            txtCodigoClient.TabIndex = 40;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(26, 41);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(82, 28);
            label2.TabIndex = 39;
            label2.Text = "Codigo:";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.FromArgb(250, 247, 241);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cmboxNotasClient);
            groupBox1.Controls.Add(checboxClient);
            groupBox1.Controls.Add(txtCodigoClient);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(txtNombreClient);
            groupBox1.Controls.Add(txtDireccionClient);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtTelefonoClient);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            groupBox1.Location = new Point(21, 105);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1057, 216);
            groupBox1.TabIndex = 54;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos de los Clientes";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(21, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(64, 64);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 55;
            pictureBox1.TabStop = false;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            label10.ForeColor = Color.FromArgb(38, 38, 38);
            label10.Location = new Point(21, 336);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(189, 28);
            label10.TabIndex = 56;
            label10.Text = "Listado de Clientes";
            // 
            // Clientescs
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1098, 689);
            Controls.Add(label10);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox1);
            Controls.Add(btnEditarClien);
            Controls.Add(btnEliminarClie);
            Controls.Add(btnNuevoClien);
            Controls.Add(dataGridClientes);
            Controls.Add(txtBuscarClient);
            Controls.Add(label4);
            Controls.Add(label1);
            Cursor = Cursors.Hand;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "Clientescs";
            Text = "Clientescs";
            Load += Clientescs_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridClientes).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private Label label8;
        private TextBox txtDireccionClient;
        private Label label6;
        private TextBox txtNombreClient;
        private Label label5;
        private TextBox txtTelefonoClient;
        private Label label3;
        private TextBox txtCodigoClient;
        private Label label2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private GroupBox groupBox1;
        private PictureBox pictureBox1;
        private Label label10;
    }
}