using System.Security.Cryptography;

namespace Nuevo_Proyecto.Services.Helpers
{
    /// <summary>PBKDF2-SHA256 con sal aleatoria. Formato guardado: PBKDF2$iteraciones$sal$hash (Base64).</summary>
    public static class PasswordHasher
    {
        private const int Iteraciones = 120_000;
        private const int TamSal = 16;
        private const int TamHash = 32;
        private const string Prefijo = "PBKDF2";

        public static string Hash(string contrasena)
        {
            ArgumentException.ThrowIfNullOrEmpty(contrasena);
            var sal = RandomNumberGenerator.GetBytes(TamSal);
            var hash = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, TamHash);
            return $"{Prefijo}${Iteraciones}${Convert.ToBase64String(sal)}${Convert.ToBase64String(hash)}";
        }

        public static bool Verificar(string contrasena, string? guardado)
        {
            if (string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(guardado)) return false;

            var partes = guardado.Split('$');
            if (partes.Length != 4 || partes[0] != Prefijo || !int.TryParse(partes[1], out var iter)) return false;

            try
            {
                var sal = Convert.FromBase64String(partes[2]);
                var esperado = Convert.FromBase64String(partes[3]);
                var calculado = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, iter, HashAlgorithmName.SHA256, esperado.Length);
                return CryptographicOperations.FixedTimeEquals(calculado, esperado);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool EsHash(string? valor) => valor != null && valor.StartsWith(Prefijo + "$");
    }
}
