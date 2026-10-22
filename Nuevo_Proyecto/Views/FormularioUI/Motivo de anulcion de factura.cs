using System;
using System.Windows.Forms;

namespace Nuevo_Proyecto.Views.FormularioUI
{
    public partial class Motivo_de_anulcion_de_factura : Form
    {
        public string Motivo { get; private set; } = string.Empty;

        public Motivo_de_anulcion_de_factura()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Valida motivos de ANULACIÓN DE FACTURA
        /// Menos estricto que eliminación, pero requiere justificación válida
        /// </summary>
        private bool EsMotivoValidoParaAnulacion(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                return false;

            motivo = motivo.Trim();
            var motivoLower = motivo.ToLower();

            // Mínimo 10 caracteres (menos estricto que eliminación de 15)
            if (motivo.Length < 10)
            {
                MessageBox.Show(
                    "El motivo debe tener al menos 10 caracteres.",
                    "Motivo incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verificar que tenga al menos 2 palabras
            int cantidadPalabras = motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries).Length;
            if (cantidadPalabras < 2)
            {
                MessageBox.Show(
                    "El motivo debe tener al menos 2 palabras.",
                    "Motivo incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // VALIDACIÓN: Rechazar frases triviales
            var frasesAbsurdas = new[]
            {
                "porque sí", "porque si", "me dio la gana", "sin razón", "sin motivo",
                "al azar", "capricho", "no lo quiero", "así nomas", "me da la gana",
                "me da", "azar", "porque", "prueba", "test"
            };

            foreach (var frase in frasesAbsurdas)
            {
                if (motivoLower.Contains(frase))
                {
                    MessageBox.Show(
                        "El motivo ingresado no es válido. Debe indicar una razón real y justificada.\n\n" +
                        "Motivos válidos: cliente solicita, error en factura, duplicado, " +
                        "datos incorrectos, devolución de producto, anulación autorizada, etc.",
                        "Motivo sin justificación válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Detectar palabras incompletas
            foreach (var palabra in motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries))
            {
                if (palabra.Length < 2)
                {
                    var palabrasCortas = new[]
                    {
                        "de", "en", "el", "la", "un", "a", "y", "o", "si", "no",
                        "es", "lo", "se", "le", "al", "su", "ya", "por", "x"
                    };
                    if (!palabrasCortas.Contains(palabra.ToLower()))
                    {
                        MessageBox.Show(
                            "El motivo contiene palabras incompletas o inválidas.",
                            "Motivo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            // Detectar spam (cambios erráticos de mayúscula)
            int cambiosMayuscula = 0;
            for (int i = 1; i < motivo.Length; i++)
            {
                bool esMayusc = char.IsUpper(motivo[i]);
                bool anteriorMayusc = char.IsUpper(motivo[i - 1]);
                if (esMayusc != anteriorMayusc && char.IsLetter(motivo[i]) && char.IsLetter(motivo[i - 1]))
                    cambiosMayuscula++;
            }

            if (cambiosMayuscula > 5)
            {
                MessageBox.Show(
                    "El motivo contiene formato inválido (mayúsculas erráticas).",
                    "Motivo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Rechazar si contiene pocos caracteres alfabéticos
            int letraCount = 0;
            foreach (char c in motivo)
            {
                if (char.IsLetter(c)) letraCount++;
            }
            if (letraCount < motivo.Length * 0.6)
            {
                MessageBox.Show(
                    "El motivo debe contener principalmente texto, no números ni caracteres especiales.",
                    "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            string motivo = TxtIngreseElMotivo.Text.Trim();

            if (!EsMotivoValidoParaAnulacion(motivo))
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
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que no desea enviar un motivo?",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        public new DialogResult ShowDialog()
        {
            return base.ShowDialog();
        }
    }
}
