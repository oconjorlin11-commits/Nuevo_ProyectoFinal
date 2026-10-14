using System;
using System.Windows.Forms;
using Nuevo_Proyecto.Services.Helpers;

namespace Nuevo_Proyecto.Views.FormularioUI
{
    public partial class Motivo_de_actualizacion_cliente : Form
    {
        public string Motivo { get; private set; } = string.Empty;

        public Motivo_de_actualizacion_cliente()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Valida motivos de ACTUALIZACIÓN - MÁS FLEXIBLE
        /// Permite motivos operacionales sin palabras clave específicas
        /// </summary>
        private bool EsMotivoValido(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                return false;

            motivo = motivo.Trim();

            // Mínimo 10 caracteres (flexible)
            if (motivo.Length < 10)
            {
                MessageBox.Show("El motivo debe tener al menos 10 caracteres.",
                    "Motivo incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verificar que tenga al menos 2 palabras
            int cantidadPalabras = motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries).Length;
            if (cantidadPalabras < 2)
            {
                MessageBox.Show("El motivo debe tener al menos 2 palabras.",
                    "Motivo incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            string motivo = TxtIngreseElMotivo.Text.Trim();

            if (!EsMotivoValido(motivo))
            {
                TxtIngreseElMotivo.Focus();
                return;
            }

            Motivo = motivo;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public new DialogResult ShowDialog()
        {
            return base.ShowDialog();
        }
    }
}
