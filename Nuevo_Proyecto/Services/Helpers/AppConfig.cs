using Microsoft.Extensions.Configuration;

namespace Nuevo_Proyecto.Services.Helpers
{
    /// <summary>
    /// Lectura centralizada de appsettings.json (antes estaba repetida dentro del DbContext).
    /// </summary>
    public static class AppConfig
    {
        private static readonly Lazy<IConfigurationRoot> _config = new(() =>
            new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build());

        public static string ConnectionString =>
            _config.Value.GetConnectionString("Dev_ComideriaDbConnection")
            ?? _config.Value.GetConnectionString("ConexionDB")
            ?? throw new InvalidOperationException(
                "No se encontró la cadena de conexión 'Dev_ComideriaDbConnection' en appsettings.json.");

        /// <summary>
        /// true  = la aplicación descuenta el stock al facturar (y lo devuelve al anular).
        /// false = el descuento lo hace la base de datos (trigger); la app no lo toca.
        /// </summary>
        public static bool DescontarStockEnApp =>
            bool.TryParse(_config.Value["Facturacion:DescontarStockEnApp"], out var val) ? val : true;

        public static string NombreNegocio =>
            _config.Value["Negocio:Nombre"] ?? "ASADOS LA FLACA";
    }
}
