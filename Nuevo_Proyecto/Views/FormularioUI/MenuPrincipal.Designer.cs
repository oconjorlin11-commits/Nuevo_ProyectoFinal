namespace Nuevo_Proyecto.Models.Views
{
    partial class MenuPrincipal
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
            pnlIcono = new Panel();
            pictureBoxIcono = new PictureBox();
            btnSalir = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            btnReportes = new Button();
            btnInventario = new Button();
            btnEmpleados = new Button();
            btnClientes = new Button();
            btnFacturacion = new Button();
            btnInicio = new Button();
            pnlBarra = new Panel();
            pictureBoxUsuario = new PictureBox();
            lblUsuario = new Label();
            dateTimePicker1 = new DateTimePicker();
            label1 = new Label();
            pnlPrincipal = new Panel();
            pnlIcono.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcono).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            pnlBarra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUsuario).BeginInit();
            SuspendLayout();
            // 
            // pnlIcono
            // 
            pnlIcono.Controls.Add(pictureBoxIcono);
            pnlIcono.Dock = DockStyle.Top;
            pnlIcono.Location = new Point(0, 0);
            pnlIcono.Name = "pnlIcono";
            pnlIcono.Size = new Size(305, 145);
            pnlIcono.TabIndex = 0;
            // 
            // pictureBoxIcono
            // 
            pictureBoxIcono.Image = Properties.Resources.Copilot_20260806_000247;
            pictureBoxIcono.Location = new Point(45, 3);
            pictureBoxIcono.Name = "pictureBoxIcono";
            pictureBoxIcono.Size = new Size(231, 142);
            pictureBoxIcono.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxIcono.TabIndex = 0;
            pictureBoxIcono.TabStop = false;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(153, 40, 35);
            btnSalir.Dock = DockStyle.Bottom;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnSalir.ForeColor = Color.FromArgb(250, 247, 241);
            btnSalir.Image = Properties.Resources._4213459_common_door_exit_logout_out_signout_115411;
            btnSalir.ImageAlign = ContentAlignment.MiddleLeft;
            btnSalir.Location = new Point(0, 890);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(305, 80);
            btnSalir.TabIndex = 6;
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Firebrick;
            panel2.Controls.Add(btnSalir);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(pnlIcono);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(305, 970);
            panel2.TabIndex = 7;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnReportes);
            panel3.Controls.Add(btnInventario);
            panel3.Controls.Add(btnEmpleados);
            panel3.Controls.Add(btnClientes);
            panel3.Controls.Add(btnFacturacion);
            panel3.Controls.Add(btnInicio);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 145);
            panel3.Name = "panel3";
            panel3.Size = new Size(305, 520);
            panel3.TabIndex = 1;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.FromArgb(153, 40, 35);
            btnReportes.Dock = DockStyle.Top;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReportes.ForeColor = Color.FromArgb(250, 247, 241);
            btnReportes.Image = Properties.Resources.custom_reports256_24920;
            btnReportes.ImageAlign = ContentAlignment.MiddleLeft;
            btnReportes.Location = new Point(0, 400);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(305, 80);
            btnReportes.TabIndex = 11;
            btnReportes.Text = "Reportes";
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnInventario
            // 
            btnInventario.BackColor = Color.FromArgb(153, 40, 35);
            btnInventario.Dock = DockStyle.Top;
            btnInventario.FlatAppearance.BorderSize = 0;
            btnInventario.FlatStyle = FlatStyle.Flat;
            btnInventario.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInventario.ForeColor = Color.FromArgb(250, 247, 241);
            btnInventario.Image = Properties.Resources.business_inventory_maintenance_product_box_boxes_2326;
            btnInventario.ImageAlign = ContentAlignment.MiddleLeft;
            btnInventario.Location = new Point(0, 320);
            btnInventario.Name = "btnInventario";
            btnInventario.Size = new Size(305, 80);
            btnInventario.TabIndex = 10;
            btnInventario.Text = "Inventario";
            btnInventario.UseVisualStyleBackColor = false;
            btnInventario.Click += btnInventario_Click;
            // 
            // btnEmpleados
            // 
            btnEmpleados.BackColor = Color.FromArgb(153, 40, 35);
            btnEmpleados.Dock = DockStyle.Top;
            btnEmpleados.FlatAppearance.BorderSize = 0;
            btnEmpleados.FlatStyle = FlatStyle.Flat;
            btnEmpleados.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEmpleados.ForeColor = Color.FromArgb(250, 247, 241);
            btnEmpleados.Image = Properties.Resources.businessapplication_edit_male_user_thepencil_theclient_negocio_2321;
            btnEmpleados.ImageAlign = ContentAlignment.MiddleLeft;
            btnEmpleados.Location = new Point(0, 240);
            btnEmpleados.Name = "btnEmpleados";
            btnEmpleados.Size = new Size(305, 80);
            btnEmpleados.TabIndex = 9;
            btnEmpleados.Text = "Empleados";
            btnEmpleados.UseVisualStyleBackColor = false;
            btnEmpleados.Click += btnEmpleados_Click;
            // 
            // btnClientes
            // 
            btnClientes.BackColor = Color.FromArgb(153, 40, 35);
            btnClientes.Dock = DockStyle.Top;
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClientes.ForeColor = Color.FromArgb(250, 247, 241);
            btnClientes.Image = Properties.Resources.users_clients_group_16774;
            btnClientes.ImageAlign = ContentAlignment.MiddleLeft;
            btnClientes.Location = new Point(0, 160);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(305, 80);
            btnClientes.TabIndex = 8;
            btnClientes.Text = "Clientes";
            btnClientes.UseVisualStyleBackColor = false;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnFacturacion
            // 
            btnFacturacion.BackColor = Color.FromArgb(153, 40, 35);
            btnFacturacion.Dock = DockStyle.Top;
            btnFacturacion.FlatAppearance.BorderSize = 0;
            btnFacturacion.FlatStyle = FlatStyle.Flat;
            btnFacturacion.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFacturacion.ForeColor = Color.FromArgb(250, 247, 241);
            btnFacturacion.Image = Properties.Resources._269672_receipt_and_card_icon;
            btnFacturacion.ImageAlign = ContentAlignment.MiddleLeft;
            btnFacturacion.Location = new Point(0, 80);
            btnFacturacion.Name = "btnFacturacion";
            btnFacturacion.Size = new Size(305, 80);
            btnFacturacion.TabIndex = 7;
            btnFacturacion.Text = "Facturacion";
            btnFacturacion.UseVisualStyleBackColor = false;
            btnFacturacion.Click += btnFacturacion_Click;
            // 
            // btnInicio
            // 
            btnInicio.BackColor = Color.FromArgb(153, 40, 35);
            btnInicio.Dock = DockStyle.Top;
            btnInicio.FlatAppearance.BorderSize = 0;
            btnInicio.FlatStyle = FlatStyle.Flat;
            btnInicio.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInicio.ForeColor = Color.FromArgb(250, 247, 241);
            btnInicio.Image = Properties.Resources.kfm_home_150961;
            btnInicio.ImageAlign = ContentAlignment.MiddleLeft;
            btnInicio.Location = new Point(0, 0);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(305, 80);
            btnInicio.TabIndex = 6;
            btnInicio.Text = "Inicio";
            btnInicio.UseVisualStyleBackColor = false;
            btnInicio.Click += btnInicio_Click;
            // 
            // pnlBarra
            // 
            pnlBarra.BackColor = Color.FromArgb(245, 237, 225);
            pnlBarra.Controls.Add(pictureBoxUsuario);
            pnlBarra.Controls.Add(lblUsuario);
            pnlBarra.Controls.Add(dateTimePicker1);
            pnlBarra.Controls.Add(label1);
            pnlBarra.Dock = DockStyle.Top;
            pnlBarra.Location = new Point(305, 0);
            pnlBarra.Name = "pnlBarra";
            pnlBarra.Size = new Size(1378, 85);
            pnlBarra.TabIndex = 8;
            // 
            // pictureBoxUsuario
            // 
            pictureBoxUsuario.Dock = DockStyle.Right;
            pictureBoxUsuario.Image = Properties.Resources.customer_person_people_man_you_1625;
            pictureBoxUsuario.Location = new Point(1271, 0);
            pictureBoxUsuario.Name = "pictureBoxUsuario";
            pictureBoxUsuario.Size = new Size(107, 85);
            pictureBoxUsuario.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxUsuario.TabIndex = 3;
            pictureBoxUsuario.TabStop = false;
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsuario.ForeColor = Color.FromArgb(38, 38, 38);
            lblUsuario.Location = new Point(1154, 31);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(97, 32);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dateTimePicker1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dateTimePicker1.Location = new Point(765, 26);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(300, 39);
            dateTimePicker1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(16, 26);
            label1.Name = "label1";
            label1.Size = new Size(489, 38);
            label1.TabIndex = 0;
            label1.Text = "Sistema De Facturacion e Inventario";
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.FromArgb(232, 221, 206);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            pnlPrincipal.Location = new Point(305, 85);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(1378, 885);
            pnlPrincipal.TabIndex = 9;
            // 
            // MenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1683, 970);
            Controls.Add(pnlPrincipal);
            Controls.Add(pnlBarra);
            Controls.Add(panel2);
            Name = "MenuPrincipal";
            Text = "MenuPrincipal";
            Load += MenuPrincipal_Load;
            pnlIcono.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcono).EndInit();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            pnlBarra.ResumeLayout(false);
            pnlBarra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUsuario).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlIcono;
        private PictureBox pictureBoxIcono;
        private Button button7;
        private Button btnSalir;
        private Panel panel2;
        private Panel panel3;
        private Button btnReportes;
        private Button btnInventario;
        private Button btnEmpleados;
        private Button btnClientes;
        private Button btnFacturacion;
        private Button btnInicio;
        private Panel pnlBarra;
        private Panel pnlPrincipal;
        private Label lblUsuario;
        private DateTimePicker dateTimePicker1;
        private Label label1;
        private PictureBox pictureBoxUsuario;
    }
}