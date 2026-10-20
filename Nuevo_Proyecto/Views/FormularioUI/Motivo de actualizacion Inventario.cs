using System;
using Nuevo_Proyecto.Services.Helpers;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nuevo_Proyecto.Views.FormularioUI
{
    public partial class Motivo_de_actualizacion_Inventario : Form
    {
        public string Motivo { get; private set; } = string.Empty;

        public Motivo_de_actualizacion_Inventario()
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

            var motivoLower = motivo.ToLower();

            // ✅ VALIDACIÓN: Rechazar SOLO frases absurdas sin razón
            var frasesAbsurdas = new[]
            {
                "porque sí", "porque si", "me dio la gana", "sin razón", "sin motivo",
                "al azar", "capricho"
            };

            foreach (var frase in frasesAbsurdas)
            {
                if (motivoLower.Contains(frase))
                {
                    MessageBox.Show(
                        "El motivo no es válido. Debe indicar una razón real.",
                        "Motivo sin justificación válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Rechazar si culpa a persona SIN razón objetiva
            if (motivoLower.Contains("porque el") || motivoLower.Contains("porque la"))
            {
                var razonesObjetivas = new[] { "ingreso", "error", "corregir", "corregí", "ajust", "cambio",
                    "actualiz", "revisar", "corrección", "control", "verificación" };
                bool tieneRazon = false;
                foreach (var r in razonesObjetivas)
                {
                    if (motivoLower.Contains(r)) { tieneRazon = true; break; }
                }
                if (!tieneRazon)
                {
                    MessageBox.Show(
                        "No puede atribuir la actualización a una persona sin razón objetiva.",
                        "Motivo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Detectar palabras incompletas
            foreach (var palabra in motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries))
            {
                if (palabra.Length < 3)
                {
                    var palabrasCortas = new[] { "de", "en", "el", "la", "un", "a", "y", "o", "si", "no", "es", "lo", "se", "le", "al", "su" };
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
