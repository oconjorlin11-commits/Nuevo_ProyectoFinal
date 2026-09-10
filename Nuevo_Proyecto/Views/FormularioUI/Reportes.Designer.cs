namespace Nuevo_Proyecto.Models.Views
{
    partial class Reportes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reportes));
            groupBoxReporteVentas = new GroupBox();
            dataGridReportes = new DataGridView();
            btnConsultarReport = new Button();
            label1 = new Label();
            label4 = new Label();
            btnExportaReportes = new Button();
            dateDesde = new DateTimePicker();
            dateHasta = new DateTimePicker();
            groupBoxReporteVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridReportes).BeginInit();
            SuspendLayout();
            // 
            // groupBoxReporteVentas
            // 
            groupBoxReporteVentas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxReporteVentas.BackColor = Color.FromArgb(250, 247, 241);
            groupBoxReporteVentas.Controls.Add(dateHasta);
            groupBoxReporteVentas.Controls.Add(dateDesde);
            groupBoxReporteVentas.Controls.Add(dataGridReportes);
            groupBoxReporteVentas.Controls.Add(btnConsultarReport);
            groupBoxReporteVentas.Controls.Add(label1);
            groupBoxReporteVentas.Controls.Add(label4);
            groupBoxReporteVentas.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBoxReporteVentas.ForeColor = Color.FromArgb(153, 40, 35);
            groupBoxReporteVentas.Location = new Point(23, 24);
            groupBoxReporteVentas.Name = "groupBoxReporteVentas";
            groupBoxReporteVentas.Size = new Size(1323, 731);
            groupBoxReporteVentas.TabIndex = 12;
            groupBoxReporteVentas.TabStop = false;
            groupBoxReporteVentas.Text = "Reportes De Ventas";
            groupBoxReporteVentas.Enter += groupBoxDatosPedido_Enter;
            // 
            // dataGridReportes
            // 
            dataGridReportes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridReportes.Dock = DockStyle.Bottom;
            dataGridReportes.Location = new Point(3, 113);
            dataGridReportes.Name = "dataGridReportes";
            dataGridReportes.RowHeadersWidth = 62;
            dataGridReportes.Size = new Size(1317, 615);
            dataGridReportes.TabIndex = 34;
            // 
            // btnConsultarReport
            // 
            btnConsultarReport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConsultarReport.BackColor = Color.SlateGray;
            btnConsultarReport.FlatStyle = FlatStyle.Flat;
            btnConsultarReport.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConsultarReport.ForeColor = Color.FromArgb(250, 247, 241);
            btnConsultarReport.Image = (Image)resources.GetObject("btnConsultarReport.Image");
            btnConsultarReport.ImageAlign = ContentAlignment.MiddleLeft;
            btnConsultarReport.Location = new Point(946, 26);
            btnConsultarReport.Name = "btnConsultarReport";
            btnConsultarReport.Size = new Size(189, 65);
            btnConsultarReport.TabIndex = 33;
            btnConsultarReport.Text = "Consultar";
            btnConsultarReport.UseVisualStyleBackColor = false;
            btnConsultarReport.Click += btnConsultarReport_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(38, 38, 38);
            label1.Location = new Point(603, 41);
            label1.Name = "label1";
            label1.Size = new Size(83, 32);
            label1.TabIndex = 27;
            label1.Text = "Hasta:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(301, 41);
            label4.Name = "label4";
            label4.Size = new Size(87, 32);
            label4.TabIndex = 28;
            label4.Text = "Desde:";
            // 
            // btnExportaReportes
            // 
            btnExportaReportes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnExportaReportes.BackColor = Color.Green;
            btnExportaReportes.FlatStyle = FlatStyle.Flat;
            btnExportaReportes.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportaReportes.ForeColor = Color.FromArgb(250, 247, 241);
            btnExportaReportes.Image = (Image)resources.GetObject("btnExportaReportes.Image");
            btnExportaReportes.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportaReportes.Location = new Point(23, 790);
            btnExportaReportes.Name = "btnExportaReportes";
            btnExportaReportes.Size = new Size(189, 65);
            btnExportaReportes.TabIndex = 56;
            btnExportaReportes.Text = "Export-PDF";
            btnExportaReportes.UseVisualStyleBackColor = false;
            btnExportaReportes.Click += btnExportaReportes_Click;
            // 
            // dateDesde
            // 
            dateDesde.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dateDesde.CalendarForeColor = Color.FromArgb(38, 38, 38);
            dateDesde.Font = new Font("Segoe UI", 12F);
            dateDesde.Location = new Point(394, 36);
            dateDesde.Name = "dateDesde";
            dateDesde.Size = new Size(203, 39);
            dateDesde.TabIndex = 36;
            // 
            // dateHasta
            // 
            dateHasta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dateHasta.CalendarForeColor = Color.FromArgb(38, 38, 38);
            dateHasta.Font = new Font("Segoe UI", 12F);
            dateHasta.Location = new Point(692, 34);
            dateHasta.Name = "dateHasta";
            dateHasta.Size = new Size(203, 39);
            dateHasta.TabIndex = 37;
            // 
            // Reportes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1373, 885);
            Controls.Add(btnExportaReportes);
            Controls.Add(groupBoxReporteVentas);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Reportes";
            Text = "Reportes";
            Load += Reportes_Load;
            groupBoxReporteVentas.ResumeLayout(false);
            groupBoxReporteVentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridReportes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxReporteVentas;
        private Label label1;
        private Label label4;
        private Button btnConsultarReport;
        private DataGridView dataGridReportes;
        private Button btnExportaReportes;
        private DateTimePicker dateHasta;
        private DateTimePicker dateDesde;
    }
}