namespace Nuevo_Proyecto.Models.Views
{
    partial class Empleados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Empleados));
            btnEditarEmpl = new Button();
            btnEliminarEmpl = new Button();
            btnNuevoEmpleado = new Button();
            dataGridEmpleados = new DataGridView();
            txtBuscarEmpl = new TextBox();
            label4 = new Label();
            label1 = new Label();
            txtCodigoEmpl = new TextBox();
            label2 = new Label();
            txtCedulaEmpl = new TextBox();
            label3 = new Label();
            txtNombreEmpl = new TextBox();
            label5 = new Label();
            txtTelefonoEmpl = new TextBox();
            label6 = new Label();
            txtSalarioEmpl = new TextBox();
            label7 = new Label();
            label8 = new Label();
            cmboxCargoEmpl = new ComboBox();
            label9 = new Label();
            dateTimePickerEmpleado = new DateTimePicker();
            checkBoxEmpleado = new CheckBox();
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleados).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnEditarEmpl
            // 
            btnEditarEmpl.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditarEmpl.BackColor = Color.Green;
            btnEditarEmpl.FlatStyle = FlatStyle.Flat;
            btnEditarEmpl.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditarEmpl.ForeColor = Color.FromArgb(250, 247, 241);
            btnEditarEmpl.Image = Properties.Resources.edit_validated256_25237;
            btnEditarEmpl.ImageAlign = ContentAlignment.MiddleLeft;
            btnEditarEmpl.Location = new Point(507, 622);
            btnEditarEmpl.Margin = new Padding(2);
            btnEditarEmpl.Name = "btnEditarEmpl";
            btnEditarEmpl.Size = new Size(151, 52);
            btnEditarEmpl.TabIndex = 21;
            btnEditarEmpl.Text = "Editar";
            btnEditarEmpl.UseVisualStyleBackColor = false;
            btnEditarEmpl.Click += btnEditarEmpl_Click;
            // 
            // btnEliminarEmpl
            // 
            btnEliminarEmpl.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEliminarEmpl.BackColor = Color.DarkRed;
            btnEliminarEmpl.FlatStyle = FlatStyle.Flat;
            btnEliminarEmpl.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminarEmpl.ForeColor = Color.FromArgb(250, 247, 241);
            btnEliminarEmpl.Image = Properties.Resources._32officeicons_3_89720__1_;
            btnEliminarEmpl.ImageAlign = ContentAlignment.MiddleLeft;
            btnEliminarEmpl.Location = new Point(720, 622);
            btnEliminarEmpl.Margin = new Padding(2);
            btnEliminarEmpl.Name = "btnEliminarEmpl";
            btnEliminarEmpl.Size = new Size(151, 52);
            btnEliminarEmpl.TabIndex = 20;
            btnEliminarEmpl.Text = "Eliminar";
            btnEliminarEmpl.UseVisualStyleBackColor = false;
            btnEliminarEmpl.Click += btnEliminarEmpl_Click;
            // 
            // btnNuevoEmpleado
            // 
            btnNuevoEmpleado.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNuevoEmpleado.BackColor = Color.DarkGray;
            btnNuevoEmpleado.FlatStyle = FlatStyle.Flat;
            btnNuevoEmpleado.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNuevoEmpleado.ForeColor = Color.FromArgb(250, 247, 241);
            btnNuevoEmpleado.Image = Properties.Resources.new_add_user_16734__1_;
            btnNuevoEmpleado.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevoEmpleado.Location = new Point(920, 622);
            btnNuevoEmpleado.Margin = new Padding(2);
            btnNuevoEmpleado.Name = "btnNuevoEmpleado";
            btnNuevoEmpleado.Size = new Size(151, 52);
            btnNuevoEmpleado.TabIndex = 19;
            btnNuevoEmpleado.Text = "Nuevo Emple";
            btnNuevoEmpleado.TextAlign = ContentAlignment.MiddleRight;
            btnNuevoEmpleado.UseVisualStyleBackColor = false;
            btnNuevoEmpleado.Click += btnNuevoEmpleado_Click;
            // 
            // dataGridEmpleados
            // 
            dataGridEmpleados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridEmpleados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridEmpleados.BackgroundColor = Color.DarkGray;
            dataGridEmpleados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridEmpleados.Cursor = Cursors.Hand;
            dataGridEmpleados.Location = new Point(21, 376);
            dataGridEmpleados.Margin = new Padding(2);
            dataGridEmpleados.Name = "dataGridEmpleados";
            dataGridEmpleados.RowHeadersWidth = 62;
            dataGridEmpleados.Size = new Size(1057, 233);
            dataGridEmpleados.TabIndex = 18;
            // 
            // txtBuscarEmpl
            // 
            txtBuscarEmpl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscarEmpl.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtBuscarEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtBuscarEmpl.Location = new Point(813, 31);
            txtBuscarEmpl.Margin = new Padding(2);
            txtBuscarEmpl.Name = "txtBuscarEmpl";
            txtBuscarEmpl.PlaceholderText = "Codigo,Nombre ";
            txtBuscarEmpl.Size = new Size(265, 31);
            txtBuscarEmpl.TabIndex = 17;
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
            label4.TabIndex = 16;
            label4.Text = "Buscar:";
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
            label1.Size = new Size(145, 32);
            label1.TabIndex = 15;
            label1.Text = " Empleados";
            // 
            // txtCodigoEmpl
            // 
            txtCodigoEmpl.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtCodigoEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoEmpl.Location = new Point(106, 37);
            txtCodigoEmpl.Margin = new Padding(2);
            txtCodigoEmpl.Name = "txtCodigoEmpl";
            txtCodigoEmpl.PlaceholderText = "EMP-000";
            txtCodigoEmpl.Size = new Size(188, 31);
            txtCodigoEmpl.TabIndex = 23;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(10, 37);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(82, 28);
            label2.TabIndex = 22;
            label2.Text = "Codigo:";
            // 
            // txtCedulaEmpl
            // 
            txtCedulaEmpl.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtCedulaEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtCedulaEmpl.Location = new Point(106, 164);
            txtCedulaEmpl.Margin = new Padding(2);
            txtCedulaEmpl.Name = "txtCedulaEmpl";
            txtCedulaEmpl.PlaceholderText = "000-112608-1001Q";
            txtCedulaEmpl.Size = new Size(188, 31);
            txtCedulaEmpl.TabIndex = 25;
            txtCedulaEmpl.TextChanged += txtCedulaEmpl_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(10, 164);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(79, 28);
            label3.TabIndex = 24;
            label3.Text = "Cedula:";
            // 
            // txtNombreEmpl
            // 
            txtNombreEmpl.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtNombreEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreEmpl.Location = new Point(106, 99);
            txtNombreEmpl.Margin = new Padding(2);
            txtNombreEmpl.Name = "txtNombreEmpl";
            txtNombreEmpl.PlaceholderText = "Nombre y Apellidos";
            txtNombreEmpl.Size = new Size(188, 31);
            txtNombreEmpl.TabIndex = 27;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(10, 99);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(92, 28);
            label5.TabIndex = 26;
            label5.Text = "Nombre:";
            // 
            // txtTelefonoEmpl
            // 
            txtTelefonoEmpl.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtTelefonoEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtTelefonoEmpl.Location = new Point(424, 40);
            txtTelefonoEmpl.Margin = new Padding(2);
            txtTelefonoEmpl.Name = "txtTelefonoEmpl";
            txtTelefonoEmpl.PlaceholderText = "5518-3045";
            txtTelefonoEmpl.Size = new Size(188, 31);
            txtTelefonoEmpl.TabIndex = 29;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(324, 40);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(96, 28);
            label6.TabIndex = 28;
            label6.Text = "Telefono:";
            // 
            // txtSalarioEmpl
            // 
            txtSalarioEmpl.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtSalarioEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtSalarioEmpl.Location = new Point(424, 164);
            txtSalarioEmpl.Margin = new Padding(2);
            txtSalarioEmpl.Name = "txtSalarioEmpl";
            txtSalarioEmpl.PlaceholderText = "C$000.00";
            txtSalarioEmpl.Size = new Size(188, 31);
            txtSalarioEmpl.TabIndex = 31;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(38, 38, 38);
            label7.Location = new Point(324, 164);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(77, 28);
            label7.TabIndex = 30;
            label7.Text = "Salario:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(38, 38, 38);
            label8.Location = new Point(324, 102);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(70, 28);
            label8.TabIndex = 32;
            label8.Text = "Cargo:";
            // 
            // cmboxCargoEmpl
            // 
            cmboxCargoEmpl.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            cmboxCargoEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCargoEmpl.FormattingEnabled = true;
            cmboxCargoEmpl.Location = new Point(424, 99);
            cmboxCargoEmpl.Margin = new Padding(2);
            cmboxCargoEmpl.Name = "cmboxCargoEmpl";
            cmboxCargoEmpl.Size = new Size(188, 33);
            cmboxCargoEmpl.TabIndex = 34;
            cmboxCargoEmpl.Text = "Cargo del Empleado";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(38, 38, 38);
            label9.Location = new Point(650, 40);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(144, 28);
            label9.TabIndex = 35;
            label9.Text = "Fecha Ingreso:";
            // 
            // dateTimePickerEmpleado
            // 
            dateTimePickerEmpleado.CalendarFont = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            dateTimePickerEmpleado.CalendarTitleForeColor = Color.FromArgb(38, 38, 38);
            dateTimePickerEmpleado.Font = new Font("Segoe UI", 12F);
            dateTimePickerEmpleado.Location = new Point(798, 35);
            dateTimePickerEmpleado.Margin = new Padding(2);
            dateTimePickerEmpleado.Name = "dateTimePickerEmpleado";
            dateTimePickerEmpleado.Size = new Size(252, 34);
            dateTimePickerEmpleado.TabIndex = 37;
            // 
            // checkBoxEmpleado
            // 
            checkBoxEmpleado.AutoSize = true;
            checkBoxEmpleado.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            checkBoxEmpleado.Location = new Point(650, 101);
            checkBoxEmpleado.Margin = new Padding(2);
            checkBoxEmpleado.Name = "checkBoxEmpleado";
            checkBoxEmpleado.Size = new Size(90, 32);
            checkBoxEmpleado.TabIndex = 38;
            checkBoxEmpleado.Text = "Activo";
            checkBoxEmpleado.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.FromArgb(250, 247, 241);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(checkBoxEmpleado);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(dateTimePickerEmpleado);
            groupBox1.Controls.Add(txtCodigoEmpl);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cmboxCargoEmpl);
            groupBox1.Controls.Add(txtCedulaEmpl);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtSalarioEmpl);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(txtNombreEmpl);
            groupBox1.Controls.Add(txtTelefonoEmpl);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            groupBox1.ForeColor = Color.FromArgb(38, 38, 38);
            groupBox1.Location = new Point(21, 105);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1057, 216);
            groupBox1.TabIndex = 39;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del Empleado";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(21, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(64, 64);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 40;
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
            label10.Size = new Size(217, 28);
            label10.TabIndex = 41;
            label10.Text = "Listado de Empleados";
            // 
            // Empleados
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1098, 689);
            Controls.Add(label10);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox1);
            Controls.Add(btnEditarEmpl);
            Controls.Add(btnEliminarEmpl);
            Controls.Add(btnNuevoEmpleado);
            Controls.Add(dataGridEmpleados);
            Controls.Add(txtBuscarEmpl);
            Controls.Add(label4);
            Controls.Add(label1);
            Cursor = Cursors.Hand;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "Empleados";
            Text = "Empleados";
            Load += Empleados_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleados).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnEditarEmpl;
        private Button btnEliminarEmpl;
        private Button btnNuevoEmpleado;
        private DataGridView dataGridEmpleados;
        private TextBox txtBuscarEmpl;
        private Label label4;
        private Label label1;
        private TextBox txtCodigoEmpl;
        private Label label2;
        private TextBox txtCedulaEmpl;
        private Label label3;
        private TextBox txtNombreEmpl;
        private Label label5;
        private TextBox txtTelefonoEmpl;
        private Label label6;
        private TextBox txtSalarioEmpl;
        private Label label7;
        private Label label8;
        private ComboBox cmboxCargoEmpl;
        private Label label9;
        private DateTimePicker dateTimePickerEmpleado;
        private CheckBox checkBoxEmpleado;
        private GroupBox groupBox1;
        private PictureBox pictureBox1;
        private Label label10;
    }
}