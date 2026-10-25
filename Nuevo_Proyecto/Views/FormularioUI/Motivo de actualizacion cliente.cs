using System;
using System.Windows.Forms;
using Nuevo_Proyecto.Services.Helpers;
using System.Linq;
using System.Text.RegularExpressions;

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
                MensajeHelper.Motivo.MotivoInvalido("Debe tener al menos 10 caracteres");
                return false;
            }

            // Verificar que tenga al menos 2 palabras
            int cantidadPalabras = motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries).Length;
            if (cantidadPalabras < 2)
            {
                MensajeHelper.Motivo.MotivoInvalido("Debe tener al menos 2 palabras");
                return false;
            }

            var motivoLower = motivo.ToLower();

            // ? VALIDACIÓN: Rechazar SOLO frases absurdas sin razón
            var frasesAbsurdas = new[]
            {
                "porque sí", "porque si", "me dio la gana", "sin razón", "sin motivo",
                "al azar", "capricho"
            };

            foreach (var frase in frasesAbsurdas)
            {
                if (motivoLower.Contains(frase))
                {
                    MensajeHelper.Motivo.MotivoInvalido("Debe indicar una razón real");
                    return false;
                }
            }

            // Rechazar si culpa a persona SIN razón objetiva
            if (motivoLower.Contains("porque el") || motivoLower.Contains("porque la"))
            {
                var razonesObjetivas = new[] { 
                    "ingreso", "error", "corregir", "corregí", "ajust", "cambio",
                    "actualiz", "revisar", "corrección", "control", "verificación", "datos"
                };
                bool tieneRazon = false;
                foreach (var r in razonesObjetivas)
                {
                    if (motivoLower.Contains(r)) { tieneRazon = true; break; }
                }
                if (!tieneRazon)
                {
                    MensajeHelper.Motivo.MotivoInvalido("No puede atribuir a una persona sin razón objetiva");
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
                        MensajeHelper.Motivo.MotivoInvalido("Contiene palabras incompletas o inválidas");
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
                MensajeHelper.Motivo.MotivoInvalido("Formato inválido (mayúsculas erráticas)");
                return false;
            }

            return true;
        }

        private void BtnAceptar_Click(object? sender, EventArgs e)
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

        private void BtnCancelar_Click(object? sender, EventArgs e)
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
