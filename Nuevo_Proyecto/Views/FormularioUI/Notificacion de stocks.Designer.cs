namespace Nuevo_Proyecto.Views.FormularioUI
{
    partial class Notificacion_de_stocks
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
            label1 = new Label();
            DgvProductosEnStocksCritico = new DataGridView();
            BtnCerrar = new Button();
            label2 = new Label();
            LblResultado = new Label();
            ((System.ComponentModel.ISupportInitialize)DgvProductosEnStocksCritico).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(257, 81);
            label1.Name = "label1";
            label1.Size = new Size(395, 32);
            label1.TabIndex = 0;
            label1.Text = "Alerta de productos con bajos stock";
            // 
            // DgvProductosEnStocksCritico
            // 
            DgvProductosEnStocksCritico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DgvProductosEnStocksCritico.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DgvProductosEnStocksCritico.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvProductosEnStocksCritico.Location = new Point(34, 192);
            DgvProductosEnStocksCritico.Name = "DgvProductosEnStocksCritico";
            DgvProductosEnStocksCritico.RowHeadersWidth = 82;
            DgvProductosEnStocksCritico.Size = new Size(795, 379);
            DgvProductosEnStocksCritico.TabIndex = 1;
            // 
            // BtnCerrar
            // 
            BtnCerrar.Location = new Point(679, 676);
            BtnCerrar.Name = "BtnCerrar";
            BtnCerrar.Size = new Size(150, 46);
            BtnCerrar.TabIndex = 2;
            BtnCerrar.Text = "Cerar Aviso";
            BtnCerrar.UseVisualStyleBackColor = true;
            BtnCerrar.Click += BtnCerrar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 616);
            label2.Name = "label2";
            label2.Size = new Size(539, 32);
            label2.TabIndex = 3;
            label2.Text = "hay tantos productos a punto de acabarse el stok";
            // 
            // LblResultado
            // 
            LblResultado.AutoSize = true;
            LblResultado.Location = new Point(751, 604);
            LblResultado.Name = "LblResultado";
            LblResultado.Size = new Size(27, 32);
            LblResultado.TabIndex = 4;
            LblResultado.Text = "1";
            // 
            // Notificacion_de_stocks
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(901, 758);
            Controls.Add(LblResultado);
            Controls.Add(label2);
            Controls.Add(BtnCerrar);
            Controls.Add(DgvProductosEnStocksCritico);
            Controls.Add(label1);
            Name = "Notificacion_de_stocks";
            Text = "Notificacion_de_stocks";
            Load += Notificacion_de_stocks_Load;
            ((System.ComponentModel.ISupportInitialize)DgvProductosEnStocksCritico).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView DgvProductosEnStocksCritico;
        private Button BtnCerrar;
        private Label label2;
        private Label LblResultado;
    }
}