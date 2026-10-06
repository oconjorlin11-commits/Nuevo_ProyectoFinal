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
            groupBoxReporteVentas = new GroupBox();
            dateHasta = new DateTimePicker();
            dateDesde = new DateTimePicker();
            dataGridReportes = new DataGridView();
            btnConsultarReport = new Button();
            label1 = new Label();
            label4 = new Label();
            btnExportaReportes = new Button();
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
            groupBoxReporteVentas.Location = new Point(30, 31);
            groupBoxReporteVentas.Margin = new Padding(4, 4, 4, 4);
            groupBoxReporteVentas.Name = "groupBoxReporteVentas";
            groupBoxReporteVentas.Padding = new Padding(4, 4, 4, 4);
            groupBoxReporteVentas.Size = new Size(1720, 936);
            groupBoxReporteVentas.TabIndex = 12;
            groupBoxReporteVentas.TabStop = false;
            groupBoxReporteVentas.Text = "Reportes De Ventas";
            groupBoxReporteVentas.Enter += groupBoxDatosPedido_Enter;
            // 
            // dateHasta
            // 
            dateHasta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dateHasta.CalendarForeColor = Color.FromArgb(38, 38, 38);
            dateHasta.Font = new Font("Segoe UI", 12F);
            dateHasta.Location = new Point(821, 45);
            dateHasta.Margin = new Padding(4, 4, 4, 4);
            dateHasta.Name = "dateHasta";
            dateHasta.Size = new Size(366, 50);
            dateHasta.TabIndex = 37;
            // 
            // dateDesde
            // 
            dateDesde.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dateDesde.CalendarForeColor = Color.FromArgb(38, 38, 38);
            dateDesde.Font = new Font("Segoe UI", 12F);
            dateDesde.Location = new Point(321, 45);
            dateDesde.Margin = new Padding(4, 4, 4, 4);
            dateDesde.Name = "dateDesde";
            dateDesde.Size = new Size(353, 50);
            dateDesde.TabIndex = 36;
            // 
            // dataGridReportes
            // 
            dataGridReportes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridReportes.Dock = DockStyle.Bottom;
            dataGridReportes.Location = new Point(4, 145);
            dataGridReportes.Margin = new Padding(4, 4, 4, 4);
            dataGridReportes.Name = "dataGridReportes";
            dataGridReportes.RowHeadersWidth = 62;
            dataGridReportes.Size = new Size(1712, 787);
            dataGridReportes.TabIndex = 34;
            // 
            // btnConsultarReport
            // 
            btnConsultarReport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConsultarReport.BackColor = Color.SlateGray;
            btnConsultarReport.FlatStyle = FlatStyle.Flat;
            btnConsultarReport.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConsultarReport.ForeColor = Color.FromArgb(250, 247, 241);
            btnConsultarReport.ImageAlign = ContentAlignment.MiddleLeft;
            btnConsultarReport.Location = new Point(1230, 33);
            btnConsultarReport.Margin = new Padding(4, 4, 4, 4);
            btnConsultarReport.Name = "btnConsultarReport";
            btnConsultarReport.Size = new Size(246, 83);
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
            label1.Location = new Point(701, 50);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(112, 45);
            label1.TabIndex = 27;
            label1.Text = "Hasta:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(38, 38, 38);
            label4.Location = new Point(185, 50);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(118, 45);
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
            btnExportaReportes.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportaReportes.Location = new Point(30, 1011);
            btnExportaReportes.Margin = new Padding(4, 4, 4, 4);
            btnExportaReportes.Name = "btnExportaReportes";
            btnExportaReportes.Size = new Size(246, 83);
            btnExportaReportes.TabIndex = 56;
            btnExportaReportes.Text = "Export-PDF";
            btnExportaReportes.UseVisualStyleBackColor = false;
            btnExportaReportes.Click += btnExportaReportes_Click;
            // 
            // Reportes
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 221, 206);
            ClientSize = new Size(1785, 1133);
            Controls.Add(btnExportaReportes);
            Controls.Add(groupBoxReporteVentas);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 4, 4, 4);
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