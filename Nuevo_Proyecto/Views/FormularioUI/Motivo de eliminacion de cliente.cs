using System;
using System.Windows.Forms;

namespace Nuevo_Proyecto.Views.FormularioUI
{
    public partial class Motivo_de_eliminacion_de_cliente : Form
    {
        public string Motivo { get; private set; } = string.Empty;

        public Motivo_de_eliminacion_de_cliente()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Valida motivos de ELIMINACIÓN - MÁS ESTRICTO
        /// Solo acepta motivos reales, justificados y bien escritos
        /// </summary>
        private bool EsMotivoValidoParaEliminacion(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                return false;

            motivo = motivo.Trim();
            var motivoLower = motivo.ToLower();

            // Mínimo 15 caracteres (más estricto que actualización)
            if (motivo.Length < 15)
            {
                MessageBox.Show(
                    "El motivo debe tener al menos 15 caracteres para una eliminación permanente.",
                    "Motivo incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verificar que tenga al menos 3 palabras (más estricto)
            int cantidadPalabras = motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries).Length;
            if (cantidadPalabras < 3)
            {
                MessageBox.Show(
                    "El motivo debe tener al menos 3 palabras.",
                    "Motivo incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // VALIDACIÓN: Rechazar frases triviales y sin justificación
            var frasesAbsurdas = new[]
            {
                "porque sí", "porque si", "me dio la gana", "sin razón", "sin motivo",
                "al azar", "capricho", "no lo quiero", "así nomas", "me da la gana",
                "me da", "azar"
            };

            foreach (var frase in frasesAbsurdas)
            {
                if (motivoLower.Contains(frase))
                {
                    MessageBox.Show(
                        "El motivo ingresado no es válido. Debe indicar una razón real y justificada.\n\n" +
                        "Motivos válidos: cliente duplicado, datos incorrectos, solicitud del cliente, " +
                        "cliente inactivo prolongado, información fraudulenta, etc.",
                        "Motivo sin justificación válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Rechazar si culpa a persona SIN razón objetiva
            if (motivoLower.Contains("porque el") || motivoLower.Contains("porque la"))
            {
                var razonesObjetivas = new[] { 
                    "ingreso", "error", "corregir", "corregí", "ajust", "cambio",
                    "actualiz", "revisar", "corrección", "control", "verificación",
                    "solicit", "duplicado", "fraude", "inactivo", "periodo", "tiempo"
                };
                bool tieneRazon = false;
                foreach (var r in razonesObjetivas)
                {
                    if (motivoLower.Contains(r)) 
                    { 
                        tieneRazon = true; 
                        break; 
                    }
                }
                if (!tieneRazon)
                {
                    MessageBox.Show(
                        "No puede atribuir la eliminación a una persona sin razón objetiva.",
                        "Motivo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Detectar palabras incompletas
            foreach (var palabra in motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries))
            {
                if (palabra.Length < 3)
                {
                    var palabrasCortas = new[] { 
                        "de", "en", "el", "la", "un", "a", "y", "o", "si", "no", 
                        "es", "lo", "se", "le", "al", "su", "ya", "por"
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

            if (cambiosMayuscula > 3)
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
            if (letraCount < motivo.Length * 0.7)
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

            if (!EsMotivoValidoParaEliminacion(motivo))
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
