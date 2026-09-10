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
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleados).BeginInit();
            SuspendLayout();
            // 
            // btnEditarEmpl
            // 
            btnEditarEmpl.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditarEmpl.BackColor = Color.Green;
            btnEditarEmpl.FlatStyle = FlatStyle.Flat;
            btnEditarEmpl.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditarEmpl.ForeColor = Color.FromArgb(250, 247, 241);
            btnEditarEmpl.Image = (Image)resources.GetObject("btnEditarEmpl.Image");
            btnEditarEmpl.ImageAlign = ContentAlignment.MiddleLeft;
            btnEditarEmpl.Location = new Point(673, 782);
            btnEditarEmpl.Name = "btnEditarEmpl";
            btnEditarEmpl.Size = new Size(189, 65);
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
            btnEliminarEmpl.Image = (Image)resources.GetObject("btnEliminarEmpl.Image");
            btnEliminarEmpl.ImageAlign = ContentAlignment.MiddleLeft;
            btnEliminarEmpl.Location = new Point(918, 782);
            btnEliminarEmpl.Name = "btnEliminarEmpl";
            btnEliminarEmpl.Size = new Size(189, 65);
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
            btnNuevoEmpleado.Location = new Point(1158, 782);
            btnNuevoEmpleado.Name = "btnNuevoEmpleado";
            btnNuevoEmpleado.Size = new Size(189, 65);
            btnNuevoEmpleado.TabIndex = 19;
            btnNuevoEmpleado.Text = "Nuevo Emple";
            btnNuevoEmpleado.TextAlign = ContentAlignment.MiddleRight;
            btnNuevoEmpleado.UseVisualStyleBackColor = false;
            btnNuevoEmpleado.Click += btnNuevoEmpleado_Click;
            // 
            // dataGridEmpleados
            // 
            dataGridEmpleados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridEmpleados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridEmpleados.Location = new Point(26, 313);
            dataGridEmpleados.Name = "dataGridEmpleados";
            dataGridEmpleados.RowHeadersWidth = 62;
            dataGridEmpleados.Size = new Size(1321, 425);
            dataGridEmpleados.TabIndex = 18;
            // 
            // txtBuscarEmpl
            // 
            txtBuscarEmpl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscarEmpl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscarEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtBuscarEmpl.Location = new Point(1017, 38);
            txtBuscarEmpl.Name = "txtBuscarEmpl";
            txtBuscarEmpl.Size = new Size(330, 39);
            txtBuscarEmpl.TabIndex = 17;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(918, 41);
            label4.Name = "label4";
            label4.Size = new Size(93, 32);
            label4.TabIndex = 16;
            label4.Text = "Buscar:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(153, 40, 35);
            label1.Location = new Point(26, 38);
            label1.Name = "label1";
            label1.Size = new Size(167, 38);
            label1.TabIndex = 15;
            label1.Text = " Empleados";
            // 
            // txtCodigoEmpl
            // 
            txtCodigoEmpl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigoEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoEmpl.Location = new Point(80, 138);
            txtCodigoEmpl.Name = "txtCodigoEmpl";
            txtCodigoEmpl.Size = new Size(209, 39);
            txtCodigoEmpl.TabIndex = 23;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(136, 103);
            label2.Name = "label2";
            label2.Size = new Size(91, 32);
            label2.TabIndex = 22;
            label2.Text = "Codigo";
            // 
            // txtCedulaEmpl
            // 
            txtCedulaEmpl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCedulaEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtCedulaEmpl.Location = new Point(80, 240);
            txtCedulaEmpl.Name = "txtCedulaEmpl";
            txtCedulaEmpl.Size = new Size(209, 39);
            txtCedulaEmpl.TabIndex = 25;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(136, 205);
            label3.Name = "label3";
            label3.Size = new Size(89, 32);
            label3.TabIndex = 24;
            label3.Text = "Cedula";
            // 
            // txtNombreEmpl
            // 
            txtNombreEmpl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombreEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreEmpl.Location = new Point(342, 138);
            txtNombreEmpl.Name = "txtNombreEmpl";
            txtNombreEmpl.Size = new Size(209, 39);
            txtNombreEmpl.TabIndex = 27;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(398, 103);
            label5.Name = "label5";
            label5.Size = new Size(103, 32);
            label5.TabIndex = 26;
            label5.Text = "Nombre";
            // 
            // txtTelefonoEmpl
            // 
            txtTelefonoEmpl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTelefonoEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtTelefonoEmpl.Location = new Point(342, 240);
            txtTelefonoEmpl.Name = "txtTelefonoEmpl";
            txtTelefonoEmpl.Size = new Size(209, 39);
            txtTelefonoEmpl.TabIndex = 29;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(398, 205);
            label6.Name = "label6";
            label6.Size = new Size(107, 32);
            label6.TabIndex = 28;
            label6.Text = "Telefono";
            // 
            // txtSalarioEmpl
            // 
            txtSalarioEmpl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSalarioEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtSalarioEmpl.Location = new Point(594, 240);
            txtSalarioEmpl.Name = "txtSalarioEmpl";
            txtSalarioEmpl.Size = new Size(209, 39);
            txtSalarioEmpl.TabIndex = 31;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(38, 38, 38);
            label7.Location = new Point(650, 205);
            label7.Name = "label7";
            label7.Size = new Size(88, 32);
            label7.TabIndex = 30;
            label7.Text = "Salario";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(38, 38, 38);
            label8.Location = new Point(650, 103);
            label8.Name = "label8";
            label8.Size = new Size(79, 32);
            label8.TabIndex = 32;
            label8.Text = "Cargo";
            // 
            // cmboxCargoEmpl
            // 
            cmboxCargoEmpl.Font = new Font("Segoe UI", 12F);
            cmboxCargoEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxCargoEmpl.FormattingEnabled = true;
            cmboxCargoEmpl.Location = new Point(594, 138);
            cmboxCargoEmpl.Name = "cmboxCargoEmpl";
            cmboxCargoEmpl.Size = new Size(209, 40);
            cmboxCargoEmpl.TabIndex = 34;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(38, 38, 38);
            label9.Location = new Point(877, 103);
            label9.Name = "label9";
            label9.Size = new Size(165, 32);
            label9.TabIndex = 35;
            label9.Text = "Fecha Ingreso";
            // 
            // dateTimePickerEmpleado
            // 
            dateTimePickerEmpleado.CalendarTitleForeColor = Color.FromArgb(38, 38, 38);
            dateTimePickerEmpleado.Font = new Font("Segoe UI", 12F);
            dateTimePickerEmpleado.Location = new Point(851, 138);
            dateTimePickerEmpleado.Name = "dateTimePickerEmpleado";
            dateTimePickerEmpleado.Size = new Size(209, 39);
            dateTimePickerEmpleado.TabIndex = 37;
            dateTimePickerEmpleado.ValueChanged += dateTimePickerEmpleado_ValueChanged;
            // 
            // checkBoxEmpleado
            // 
            checkBoxEmpleado.AutoSize = true;
            checkBoxEmpleado.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            checkBoxEmpleado.Location = new Point(877, 240);
            checkBoxEmpleado.Name = "checkBoxEmpleado";
            checkBoxEmpleado.Size = new Size(108, 36);
            checkBoxEmpleado.TabIndex = 38;
            checkBoxEmpleado.Text = "Activo";
            checkBoxEmpleado.UseVisualStyleBackColor = true;
            // 
            // Empleados
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1373, 885);
            Controls.Add(checkBoxEmpleado);
            Controls.Add(dateTimePickerEmpleado);
            Controls.Add(label9);
            Controls.Add(cmboxCargoEmpl);
            Controls.Add(label8);
            Controls.Add(txtSalarioEmpl);
            Controls.Add(label7);
            Controls.Add(txtTelefonoEmpl);
            Controls.Add(label6);
            Controls.Add(txtNombreEmpl);
            Controls.Add(label5);
            Controls.Add(txtCedulaEmpl);
            Controls.Add(label3);
            Controls.Add(txtCodigoEmpl);
            Controls.Add(label2);
            Controls.Add(btnEditarEmpl);
            Controls.Add(btnEliminarEmpl);
            Controls.Add(btnNuevoEmpleado);
            Controls.Add(dataGridEmpleados);
            Controls.Add(txtBuscarEmpl);
            Controls.Add(label4);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Empleados";
            Text = "Empleados";
            Load += Empleados_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleados).EndInit();
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
    }
}