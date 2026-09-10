namespace Nuevo_Proyecto.Models.Views
{
    partial class FacturasEmitidas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacturasEmitidas));
            panel1 = new Panel();
            dataGridFacturasEmitidas = new DataGridView();
            label4 = new Label();
            label1 = new Label();
            txtBuscarFact = new TextBox();
            label2 = new Label();
            btnBucarFacturas = new Button();
            btnVerComprob = new Button();
            button2 = new Button();
            dateTimeDesde = new DateTimePicker();
            dateTimeHasta = new DateTimePicker();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridFacturasEmitidas).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(250, 247, 241);
            panel1.Controls.Add(dataGridFacturasEmitidas);
            panel1.Location = new Point(12, 103);
            panel1.Name = "panel1";
            panel1.Size = new Size(1327, 651);
            panel1.TabIndex = 0;
            // 
            // dataGridFacturasEmitidas
            // 
            dataGridFacturasEmitidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridFacturasEmitidas.Dock = DockStyle.Fill;
            dataGridFacturasEmitidas.Location = new Point(0, 0);
            dataGridFacturasEmitidas.Name = "dataGridFacturasEmitidas";
            dataGridFacturasEmitidas.RowHeadersWidth = 62;
            dataGridFacturasEmitidas.Size = new Size(1327, 651);
            dataGridFacturasEmitidas.TabIndex = 0;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(110, 40);
            label4.Name = "label4";
            label4.Size = new Size(87, 32);
            label4.TabIndex = 25;
            label4.Text = "Desde:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(412, 40);
            label1.Name = "label1";
            label1.Size = new Size(83, 32);
            label1.TabIndex = 25;
            label1.Text = "Hasta:";
            // 
            // txtBuscarFact
            // 
            txtBuscarFact.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscarFact.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscarFact.ForeColor = Color.FromArgb(38, 38, 38);
            txtBuscarFact.Location = new Point(826, 37);
            txtBuscarFact.Name = "txtBuscarFact";
            txtBuscarFact.Size = new Size(237, 39);
            txtBuscarFact.TabIndex = 26;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(716, 40);
            label2.Name = "label2";
            label2.Size = new Size(93, 32);
            label2.TabIndex = 25;
            label2.Text = "Buscar:";
            // 
            // btnBucarFacturas
            // 
            btnBucarFacturas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBucarFacturas.BackColor = Color.SlateGray;
            btnBucarFacturas.FlatStyle = FlatStyle.Flat;
            btnBucarFacturas.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBucarFacturas.ForeColor = Color.FromArgb(250, 247, 241);
            btnBucarFacturas.Image = (Image)resources.GetObject("btnBucarFacturas.Image");
            btnBucarFacturas.ImageAlign = ContentAlignment.MiddleLeft;
            btnBucarFacturas.Location = new Point(1083, 25);
            btnBucarFacturas.Name = "btnBucarFacturas";
            btnBucarFacturas.Size = new Size(189, 65);
            btnBucarFacturas.TabIndex = 32;
            btnBucarFacturas.Text = "Buscar";
            btnBucarFacturas.UseVisualStyleBackColor = false;
            btnBucarFacturas.Click += btnBucarFacturas_Click;
            // 
            // btnVerComprob
            // 
            btnVerComprob.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerComprob.BackColor = Color.DarkBlue;
            btnVerComprob.FlatStyle = FlatStyle.Flat;
            btnVerComprob.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVerComprob.ForeColor = Color.FromArgb(250, 247, 241);
            btnVerComprob.Image = (Image)resources.GetObject("btnVerComprob.Image");
            btnVerComprob.ImageAlign = ContentAlignment.MiddleLeft;
            btnVerComprob.Location = new Point(12, 760);
            btnVerComprob.Name = "btnVerComprob";
            btnVerComprob.Size = new Size(189, 65);
            btnVerComprob.TabIndex = 33;
            btnVerComprob.Text = "Ver-Comprob";
            btnVerComprob.TextAlign = ContentAlignment.MiddleRight;
            btnVerComprob.UseVisualStyleBackColor = false;
            btnVerComprob.Click += btnVerComprob_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.BackColor = Color.DarkRed;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.FromArgb(250, 247, 241);
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(251, 760);
            button2.Name = "button2";
            button2.Size = new Size(189, 65);
            button2.TabIndex = 34;
            button2.Text = "Anular Factura";
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // dateTimeDesde
            // 
            dateTimeDesde.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dateTimeDesde.CalendarForeColor = Color.FromArgb(38, 38, 38);
            dateTimeDesde.Font = new Font("Segoe UI", 12F);
            dateTimeDesde.Location = new Point(203, 35);
            dateTimeDesde.Name = "dateTimeDesde";
            dateTimeDesde.Size = new Size(203, 39);
            dateTimeDesde.TabIndex = 35;
            // 
            // dateTimeHasta
            // 
            dateTimeHasta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dateTimeHasta.CalendarForeColor = Color.FromArgb(38, 38, 38);
            dateTimeHasta.Font = new Font("Segoe UI", 12F);
            dateTimeHasta.Location = new Point(501, 35);
            dateTimeHasta.Name = "dateTimeHasta";
            dateTimeHasta.Size = new Size(203, 39);
            dateTimeHasta.TabIndex = 36;
            // 
            // FacturasEmitidas
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1351, 829);
            Controls.Add(dateTimeHasta);
            Controls.Add(dateTimeDesde);
            Controls.Add(button2);
            Controls.Add(btnVerComprob);
            Controls.Add(btnBucarFacturas);
            Controls.Add(txtBuscarFact);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "FacturasEmitidas";
            Text = "FacturasEmitidas";
            Load += FacturasEmitidas_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridFacturasEmitidas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridFacturasEmitidas;
        private Label label4;
        private Label label1;
        private TextBox txtBuscarFact;
        private Label label2;
        private Button btnBucarFacturas;
        private Button btnVerComprob;
        private Button button2;
        private DateTimePicker dateTimeDesde;
        private DateTimePicker dateTimeHasta;
    }
}