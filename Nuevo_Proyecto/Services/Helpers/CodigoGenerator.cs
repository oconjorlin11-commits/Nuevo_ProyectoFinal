using System.Text.RegularExpressions;

namespace Nuevo_Proyecto.Services.Helpers
{
    /// <summary>
    /// Generador único de códigos. Reemplaza la lógica de regex que estaba copiada
    /// en ClientePresenter, EmpleadoPresenter, ProductosPresenter y FacturacionPresenter.
    /// </summary>
    public static class CodigoGenerator
    {
        /// <summary>
        /// Códigos tipo CLI-001, EMP-000, CLI-001-000 (grupos de 3 dígitos, base 1000).
        /// Si no hay ningún código previo devuelve <paramref name="codigoInicial"/>.
        /// </summary>
        public static string SiguienteAgrupado(string prefijo, IEnumerable<string?> existentes, string codigoInicial)
        {
            var conPrefijo = new Regex($"^{Regex.Escape(prefijo)}-(\\d{{3}}(?:-\\d{{3}})*)$", RegexOptions.IgnoreCase);
            var soloGrupos = new Regex("^(\\d{3}(?:-\\d{3})*)$");

            long max = 0;
            bool hayAlguno = false;

            foreach (var raw in existentes)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;
                var txt = raw.Trim();
                long valor;

                var m = conPrefijo.Match(txt);
                if (!m.Success) m = soloGrupos.Match(txt);

                if (m.Success)
                {
                    valor = 0;
                    foreach (var g in m.Groups[1].Value.Split('-'))
                        valor = valor * 1000 + int.Parse(g);
                }
                else
                {
                    var digitos = new string(txt.Where(char.IsDigit).ToArray());
                    if (digitos.Length == 0 || digitos.Length > 17 || !long.TryParse(digitos, out valor)) continue;
                }

                hayAlguno = true;
                if (valor > max) max = valor;
            }

            if (!hayAlguno) return codigoInicial;

            return prefijo + "-" + FormatearGrupos(max + 1);
        }

        /// <summary>
        /// Códigos tipo F0001 / P0001. Compara el número (no el texto), así F0010 > F0009 y F10000 > F9999.
        /// </summary>
        public static string SiguienteSecuencial(string prefijo, IEnumerable<string?> existentes, int digitos = 4)
        {
            var rx = new Regex($"^{Regex.Escape(prefijo)}(\\d+)$", RegexOptions.IgnoreCase);
            long max = 0;

            foreach (var raw in existentes)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;
                var m = rx.Match(raw.Trim());
                if (m.Success && long.TryParse(m.Groups[1].Value, out var n) && n > max) max = n;
            }

            return prefijo + (max + 1).ToString("D" + digitos);
        }

        private static string FormatearGrupos(long valor)
        {
            var partes = new List<string>();
            while (valor > 0)
            {
                partes.Add(((int)(valor % 1000)).ToString("D3"));
                valor /= 1000;
            }
            if (partes.Count == 0) partes.Add("000");
            partes.Reverse();
            return string.Join("-", partes);
        }
    }
}
