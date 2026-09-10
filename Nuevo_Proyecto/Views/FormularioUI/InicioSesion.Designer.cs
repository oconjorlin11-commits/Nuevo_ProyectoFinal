namespace Nuevo_Proyecto.Models.Views
{
    partial class InicioSesion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InicioSesion));
            panel1 = new Panel();
            button3 = new Button();
            button2 = new Button();
            checkBox1 = new CheckBox();
            button1 = new Button();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            txtContraseña = new TextBox();
            txtUsuario = new TextBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(250, 247, 241);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(txtContraseña);
            panel1.Controls.Add(txtUsuario);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Cursor = Cursors.Hand;
            panel1.Location = new Point(775, 29);
            panel1.Name = "panel1";
            panel1.Size = new Size(731, 907);
            panel1.TabIndex = 0;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(250, 247, 241);
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button3.ForeColor = Color.FromArgb(153, 40, 35);
            button3.Location = new Point(202, 739);
            button3.Name = "button3";
            button3.Size = new Size(360, 55);
            button3.TabIndex = 9;
            button3.Text = "¿Olvido su Contaseña?";
            button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(153, 40, 35);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button2.ForeColor = Color.FromArgb(250, 247, 241);
            button2.Location = new Point(202, 633);
            button2.Name = "button2";
            button2.Size = new Size(360, 55);
            button2.TabIndex = 8;
            button2.Text = "Ingresar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkBox1.ForeColor = Color.FromArgb(38, 38, 38);
            checkBox1.Location = new Point(202, 554);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(279, 36);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Recordar Contraseña";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(245, 237, 225);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(515, 457);
            button1.Name = "button1";
            button1.Size = new Size(47, 45);
            button1.TabIndex = 6;
            button1.UseVisualStyleBackColor = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(142, 457);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(54, 45);
            pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(142, 348);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(54, 45);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // txtContraseña
            // 
            txtContraseña.BackColor = Color.FromArgb(245, 237, 225);
            txtContraseña.BorderStyle = BorderStyle.FixedSingle;
            txtContraseña.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtContraseña.ForeColor = Color.FromArgb(153, 40, 35);
            txtContraseña.Location = new Point(202, 457);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(313, 45);
            txtContraseña.TabIndex = 3;
            txtContraseña.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.FromArgb(245, 237, 225);
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtUsuario.ForeColor = Color.FromArgb(153, 40, 35);
            txtUsuario.Location = new Point(202, 348);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(360, 45);
            txtUsuario.TabIndex = 2;
            txtUsuario.Text = "Usuarios";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(284, 239);
            label1.Name = "label1";
            label1.Size = new Size(194, 38);
            label1.TabIndex = 1;
            label1.Text = "Inicio Sesion";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Copilot_20260806_000247;
            pictureBox1.Location = new Point(284, 52);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(194, 162);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // InicioSesion
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1531, 1026);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "InicioSesion";
            Text = "InicioSesion";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label1;
        private TextBox txtUsuario;
        private TextBox txtContraseña;
        private Button button1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private Button button2;
        private CheckBox checkBox1;
        private Button button3;
    }
}