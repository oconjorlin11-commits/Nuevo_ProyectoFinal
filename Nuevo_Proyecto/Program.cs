using Nuevo_Proyecto.Models.Views;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;

namespace Nuevo_Proyecto;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Inicializar controles visuales INMEDIATAMENTE para evitar flash de pantalla negra
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        ApplicationConfiguration.Initialize();

        // PASO -1: LIMPIAR BD SQLITE (Eliminar versión anterior)
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("🔄 SINCRONIZACIÓN COMPLETA SQL SERVER → SQLite");
        Console.WriteLine("═══════════════════════════════════════════════════════\n");
        LimpiezaBDSQLite.EliminarBDAnterior();

        // PASO 0: Verificar estructura de BD SQLite
        Console.WriteLine("🔍 Verificando estructura de BD SQLite...");
        var verificador = new VerificadorEstructuraSQLite();
        verificador.VerificarYReparar();

        // PASO 1: Verificar conexión a SQL Server al iniciar
        Console.WriteLine("🔍 Verificando conectividad al servidor...");
        bool sqlServerDisponible = ConnectionStateManager.VerificarConexionAlServidor();

        if (!sqlServerDisponible)
        {
            Console.WriteLine("⚠️  SQL Server no está disponible.");
            Console.WriteLine("✓ La aplicación funcionará en MODO OFFLINE usando SQLite.");
            Console.WriteLine("  Las operaciones se guardarán en una cola y se sincronizarán");
            Console.WriteLine("  cuando se restablezca la conexión.\n");
        }
        else
        {
            Console.WriteLine("✓ Conexión a SQL Server exitosa.\n");

            // SINCRONIZACIÓN COMPLETA: Si hay SQL disponible, sincronizar TODA la BD
            Console.WriteLine("📡 SQL Server disponible - iniciando sincronización...\n");
            try
            {
                var sincronizacion = new SincronizacionCompletaService();
                // Ejecutar sincronización completa de forma síncrona para garantizar que termine antes de login
                var tarea = sincronizacion.SincronizarCompleto();
                tarea.Wait(TimeSpan.FromSeconds(60)); // Esperar máximo 60 segundos
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Advertencia en sincronización: {ex.Message}\n");
            }
        }

        // Mostrar InicioSesion como diálogo modal
        InicioSesion inicioSesion = new InicioSesion();
        try
        {
            if (inicioSesion.ShowDialog() == DialogResult.OK && SesionActual.HaySesion)
            {
                // Si login fue exitoso y hay sesión, abrir MenuPrincipal
                Application.Run(new MenuPrincipal());
            }
        }
        finally
        {
            inicioSesion.Dispose();
        }
    }
}
