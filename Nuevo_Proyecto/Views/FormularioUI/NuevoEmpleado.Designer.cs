namespace Nuevo_Proyecto.Models.Views
{
    partial class NuevoEmpleado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevoEmpleado));
            txtCodigoEmple = new TextBox();
            label4 = new Label();
            txtNombreEmple = new TextBox();
            label1 = new Label();
            txtTelefonoEmple = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtSalarioEmpleado = new TextBox();
            label5 = new Label();
            label6 = new Label();
            comboxCargoEmpleado = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            checkEmpleadoAct = new CheckBox();
            btnGuardarEmple = new Button();
            btnCancelarEmple = new Button();
            pictureBox1 = new PictureBox();
            cmboxAutizadoEmple = new ComboBox();
            label7 = new Label();
            txtCedulaEmpl = new TextBox();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtCodigoEmple
            // 
            txtCodigoEmple.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigoEmple.ForeColor = Color.FromArgb(38, 38, 38);
            txtCodigoEmple.Location = new Point(284, 91);
            txtCodigoEmple.Name = "txtCodigoEmple";
            txtCodigoEmple.Size = new Size(294, 39);
            txtCodigoEmple.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(113, 94);
            label4.Name = "label4";
            label4.Size = new Size(97, 32);
            label4.TabIndex = 9;
            label4.Text = "Codigo:";
            label4.Click += label4_Click;
            // 
            // txtNombreEmple
            // 
            txtNombreEmple.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombreEmple.ForeColor = Color.FromArgb(38, 38, 38);
            txtNombreEmple.Location = new Point(284, 167);
            txtNombreEmple.Name = "txtNombreEmple";
            txtNombreEmple.Size = new Size(294, 39);
            txtNombreEmple.TabIndex = 12;
            txtNombreEmple.TextChanged += txtNombreEmple_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(113, 170);
            label1.Name = "label1";
            label1.Size = new Size(109, 32);
            label1.TabIndex = 11;
            label1.Text = "Nombre:";
            // 
            // txtTelefonoEmple
            // 
            txtTelefonoEmple.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTelefonoEmple.ForeColor = Color.FromArgb(38, 38, 38);
            txtTelefonoEmple.Location = new Point(284, 304);
            txtTelefonoEmple.Name = "txtTelefonoEmple";
            txtTelefonoEmple.Size = new Size(294, 39);
            txtTelefonoEmple.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(113, 304);
            label2.Name = "label2";
            label2.Size = new Size(113, 32);
            label2.TabIndex = 13;
            label2.Text = "Telefono:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(38, 38, 38);
            label3.Location = new Point(113, 383);
            label3.Name = "label3";
            label3.Size = new Size(85, 32);
            label3.TabIndex = 15;
            label3.Text = "Cargo:";
            // 
            // txtSalarioEmpleado
            // 
            txtSalarioEmpleado.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSalarioEmpleado.ForeColor = Color.FromArgb(38, 38, 38);
            txtSalarioEmpleado.Location = new Point(284, 456);
            txtSalarioEmpleado.Name = "txtSalarioEmpleado";
            txtSalarioEmpleado.Size = new Size(294, 39);
            txtSalarioEmpleado.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(38, 38, 38);
            label5.Location = new Point(113, 459);
            label5.Name = "label5";
            label5.Size = new Size(145, 32);
            label5.TabIndex = 17;
            label5.Text = "Salario (C$):";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(38, 38, 38);
            label6.Location = new Point(113, 538);
            label6.Name = "label6";
            label6.Size = new Size(171, 32);
            label6.TabIndex = 19;
            label6.Text = "Fecha Ingreso:";
            // 
            // comboxCargoEmpleado
            // 
            comboxCargoEmpleado.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboxCargoEmpleado.ForeColor = Color.FromArgb(38, 38, 38);
            comboxCargoEmpleado.FormattingEnabled = true;
            comboxCargoEmpleado.Location = new Point(284, 380);
            comboxCargoEmpleado.Name = "comboxCargoEmpleado";
            comboxCargoEmpleado.Size = new Size(294, 40);
            comboxCargoEmpleado.TabIndex = 31;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.Location = new Point(284, 533);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(294, 39);
            dateTimePicker1.TabIndex = 32;
            // 
            // checkEmpleadoAct
            // 
            checkEmpleadoAct.AutoSize = true;
            checkEmpleadoAct.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkEmpleadoAct.Location = new Point(284, 672);
            checkEmpleadoAct.Name = "checkEmpleadoAct";
            checkEmpleadoAct.Size = new Size(219, 36);
            checkEmpleadoAct.TabIndex = 33;
            checkEmpleadoAct.Text = "Empleado Activo";
            checkEmpleadoAct.UseVisualStyleBackColor = true;
            // 
            // btnGuardarEmple
            // 
            btnGuardarEmple.BackColor = Color.DarkGreen;
            btnGuardarEmple.FlatStyle = FlatStyle.Flat;
            btnGuardarEmple.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarEmple.ForeColor = Color.FromArgb(250, 247, 241);
            btnGuardarEmple.Image = (Image)resources.GetObject("btnGuardarEmple.Image");
            btnGuardarEmple.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarEmple.Location = new Point(69, 741);
            btnGuardarEmple.Name = "btnGuardarEmple";
            btnGuardarEmple.Size = new Size(189, 65);
            btnGuardarEmple.TabIndex = 34;
            btnGuardarEmple.Text = "Guardar ";
            btnGuardarEmple.UseVisualStyleBackColor = false;
            btnGuardarEmple.Click += btnGuardarEmple_Click;
            // 
            // btnCancelarEmple
            // 
            btnCancelarEmple.BackColor = Color.DarkRed;
            btnCancelarEmple.FlatStyle = FlatStyle.Flat;
            btnCancelarEmple.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelarEmple.ForeColor = Color.FromArgb(250, 247, 241);
            btnCancelarEmple.Image = (Image)resources.GetObject("btnCancelarEmple.Image");
            btnCancelarEmple.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarEmple.Location = new Point(486, 741);
            btnCancelarEmple.Name = "btnCancelarEmple";
            btnCancelarEmple.Size = new Size(189, 65);
            btnCancelarEmple.TabIndex = 35;
            btnCancelarEmple.Text = "Cancelar";
            btnCancelarEmple.UseVisualStyleBackColor = false;
            btnCancelarEmple.Click += btnCancelarEmple_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.businessapplication_edit_male_user_thepencil_theclient_negocio_2321;
            pictureBox1.Location = new Point(1, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(106, 98);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 53;
            pictureBox1.TabStop = false;
            // 
            // cmboxAutizadoEmple
            // 
            cmboxAutizadoEmple.BackColor = Color.White;
            cmboxAutizadoEmple.ForeColor = Color.FromArgb(38, 38, 38);
            cmboxAutizadoEmple.FormattingEnabled = true;
            cmboxAutizadoEmple.Location = new Point(284, 609);
            cmboxAutizadoEmple.Name = "cmboxAutizadoEmple";
            cmboxAutizadoEmple.Size = new Size(294, 33);
            cmboxAutizadoEmple.TabIndex = 56;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(38, 38, 38);
            label7.Location = new Point(113, 610);
            label7.Name = "label7";
            label7.Size = new Size(140, 32);
            label7.TabIndex = 55;
            label7.Text = "Autorizado:";
            // 
            // txtCedulaEmpl
            // 
            txtCedulaEmpl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCedulaEmpl.ForeColor = Color.FromArgb(38, 38, 38);
            txtCedulaEmpl.Location = new Point(284, 235);
            txtCedulaEmpl.Name = "txtCedulaEmpl";
            txtCedulaEmpl.Size = new Size(294, 39);
            txtCedulaEmpl.TabIndex = 58;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(38, 38, 38);
            label8.Location = new Point(113, 238);
            label8.Name = "label8";
            label8.Size = new Size(95, 32);
            label8.TabIndex = 57;
            label8.Text = "Cedula:";
            // 
            // NuevoEmpleado
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(745, 866);
            Controls.Add(txtCedulaEmpl);
            Controls.Add(label8);
            Controls.Add(cmboxAutizadoEmple);
            Controls.Add(label7);
            Controls.Add(pictureBox1);
            Controls.Add(btnCancelarEmple);
            Controls.Add(btnGuardarEmple);
            Controls.Add(checkEmpleadoAct);
            Controls.Add(dateTimePicker1);
            Controls.Add(comboxCargoEmpleado);
            Controls.Add(label6);
            Controls.Add(txtSalarioEmpleado);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(txtTelefonoEmple);
            Controls.Add(label2);
            Controls.Add(txtNombreEmple);
            Controls.Add(label1);
            Controls.Add(txtCodigoEmple);
            Controls.Add(label4);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "NuevoEmpleado";
            Text = "NuevoEmpleado";
            Load += NuevoEmpleado_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void txtNombreEmple_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private TextBox txtCodigoEmple;
        private Label label4;
        private TextBox txtNombreEmple;
        private Label label1;
        private TextBox txtTelefonoEmple;
        private Label label2;
        private Label label3;
        private TextBox txtSalarioEmpleado;
        private Label label5;
        private Label label6;
        private ComboBox comboxCargoEmpleado;
        private DateTimePicker dateTimePicker1;
        private CheckBox checkEmpleadoAct;
        private Button btnGuardarEmple;
        private Button btnCancelarEmple;
        private PictureBox pictureBox1;
        private ComboBox cmboxAutizadoEmple;
        private Label label7;
        private TextBox txtCedulaEmpl;
        private Label label8;
    }
}