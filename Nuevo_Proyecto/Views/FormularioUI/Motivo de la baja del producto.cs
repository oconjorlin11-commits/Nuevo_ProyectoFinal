using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nuevo_Proyecto.Services.Helpers;
using System.Linq;

namespace Nuevo_Proyecto.Views.FormularioUI
{
    public partial class Motivo_de_la_baja_del_producto : Form
    {
        public string Motivo { get; private set; } = string.Empty;
        public string AutorizadoPor { get; private set; } = string.Empty;

        public Motivo_de_la_baja_del_producto()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarAutorizadoPor();
        }

        private void CargarAutorizadoPor()
        {
            // Mostrar el nombre del administrador autorizador
            string autorizador = SesionActual.UsuarioAutorizador ?? SesionActual.NombreEmpleado ?? "Desconocido";
            TxtAutorizadoPor.Text = autorizador;
            TxtAutorizadoPor.ReadOnly = true;
            AutorizadoPor = autorizador;
        }

        /// <summary>
        /// Valida que el motivo de baja sea coherente y con razón válida
        /// </summary>
        private bool EsMotivoValido(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                return false;

            motivo = motivo.Trim();

            // Mínimo 15 caracteres para asegurar que es un motivo real
            if (motivo.Length < 15)
            {
                MessageBox.Show("El motivo de baja debe tener al menos 15 caracteres. Sea más descriptivo.",
                    "Motivo incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verificar que tenga al menos 2 palabras (es mucho más flexible)
            int cantidadPalabras = motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries).Length;
            if (cantidadPalabras < 2)
            {
                MessageBox.Show("El motivo debe tener al menos 2 palabras.",
                    "Motivo incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var motivoLower = motivo.ToLower();

            // ✅ VALIDACIÓN: Rechazar SOLO motivos sin razón válida
            // Palabras/frases que indican falta de razón válida
            var frasesInvalidas = new[]
            {
                "porque sí", "porque si", "me dio la gana", "me dio gana", "solo porque", 
                "sin razón", "sin motivo", "sin justificación", "sin justificacion", "al azar", 
                "capricho", "just because", "for no reason", "aleatoria", "aleatoriamente",
                "como me vino", "me vino en gana"
            };

            foreach (var frase in frasesInvalidas)
            {
                if (motivoLower.Contains(frase))
                {
                    MessageBox.Show(
                        "El motivo de baja no es válido. Debe indicar una razón real.",
                        "Motivo sin justificación válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
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
