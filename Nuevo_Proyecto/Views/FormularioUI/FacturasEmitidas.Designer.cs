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
            panel1 = new Panel();
            dataGridFacturasEmitidas = new DataGridView();
            txtBuscarFact = new TextBox();
            label2 = new Label();
            btnVerComprob = new Button();
            button2 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridFacturasEmitidas).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(250, 247, 241);
            panel1.Controls.Add(dataGridFacturasEmitidas);
            panel1.Location = new Point(16, 109);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1725, 851);
            panel1.TabIndex = 0;
            // 
            // dataGridFacturasEmitidas
            // 
            dataGridFacturasEmitidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridFacturasEmitidas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridFacturasEmitidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridFacturasEmitidas.Dock = DockStyle.Fill;
            dataGridFacturasEmitidas.Location = new Point(0, 0);
            dataGridFacturasEmitidas.Margin = new Padding(4, 4, 4, 4);
            dataGridFacturasEmitidas.Name = "dataGridFacturasEmitidas";
            dataGridFacturasEmitidas.RowHeadersWidth = 62;
            dataGridFacturasEmitidas.Size = new Size(1725, 851);
            dataGridFacturasEmitidas.TabIndex = 0;
            // 
            // txtBuscarFact
            // 
            txtBuscarFact.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscarFact.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscarFact.ForeColor = Color.FromArgb(38, 38, 38);
            txtBuscarFact.Location = new Point(1074, 47);
            txtBuscarFact.Margin = new Padding(4, 4, 4, 4);
            txtBuscarFact.Name = "txtBuscarFact";
            txtBuscarFact.Size = new Size(307, 50);
            txtBuscarFact.TabIndex = 26;
            txtBuscarFact.TextChanged += txtBuscarFact_TextChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(38, 38, 38);
            label2.Location = new Point(931, 51);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(125, 45);
            label2.TabIndex = 25;
            label2.Text = "Buscar:";
            // 
            // btnVerComprob
            // 
            btnVerComprob.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerComprob.BackColor = Color.DarkBlue;
            btnVerComprob.FlatStyle = FlatStyle.Flat;
            btnVerComprob.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVerComprob.ForeColor = Color.FromArgb(250, 247, 241);
            btnVerComprob.ImageAlign = ContentAlignment.MiddleLeft;
            btnVerComprob.Location = new Point(16, 973);
            btnVerComprob.Margin = new Padding(4, 4, 4, 4);
            btnVerComprob.Name = "btnVerComprob";
            btnVerComprob.Size = new Size(246, 83);
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
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(326, 973);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new Size(246, 83);
            button2.TabIndex = 34;
            button2.Text = "Anular Factura";
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // FacturasEmitidas
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1756, 1061);
            Controls.Add(button2);
            Controls.Add(btnVerComprob);
            Controls.Add(txtBuscarFact);
            Controls.Add(label2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(4, 4, 4, 4);
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
        private TextBox txtBuscarFact;
        private Label label2;
        private Button btnVerComprob;
        private Button button2;
    }
}