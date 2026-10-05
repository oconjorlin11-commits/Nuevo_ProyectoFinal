namespace Nuevo_Proyecto.Views.Helpers
{
    /// <summary>Cuadro simple para pedir un texto (p. ej. el motivo de una anulación).</summary>
    public static class PromptDialog
    {
        public static string? Pedir(string titulo, string mensaje)
        {
            using var form = new Form
            {
                Text = titulo,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(380, 130)
            };
            var lbl = new Label { Text = mensaje, Left = 12, Top = 12, Width = 356, Height = 32 };
            var txt = new TextBox { Left = 12, Top = 48, Width = 356, MaxLength = 150 };
            var ok = new Button { Text = "Aceptar", Left = 212, Top = 88, Width = 75, DialogResult = DialogResult.OK };
            var cancel = new Button { Text = "Cancelar", Left = 293, Top = 88, Width = 75, DialogResult = DialogResult.Cancel };
            form.Controls.AddRange(new Control[] { lbl, txt, ok, cancel });
            form.AcceptButton = ok;
            form.CancelButton = cancel;

            return form.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txt.Text)
                ? txt.Text.Trim()
                : null;
        }
    }
}
