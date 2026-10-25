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

        // Debe configurarse antes de crear cualquier control para evitar errores del hilo de UI.
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (sender, e) => ManejarExcepcionGlobal(e.Exception, "WinForms");
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            if (e.ExceptionObject is Exception ex)
            {
                ManejarExcepcionGlobal(ex, "Dominio");
            }
        };
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            ManejarExcepcionGlobal(e.Exception, "Tarea");
            e.SetObserved();
        };

        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("🔄 SINCRONIZACIÓN COMPLETA SQL SERVER → SQLite");
        Console.WriteLine("═══════════════════════════════════════════════════════\n");

        // SQLite es un caché temporal para continuar operando mientras SQL está caído.
        // No se debe borrar al iniciar si SQL está vivo; se limpia al restablecerse la conexión.

        // PASO 0: Verificar estructura de BD SQLite
        Console.WriteLine("🔍 Verificando estructura de BD SQLite...");
        var verificador = new VerificadorEstructuraSQLite();
        verificador.VerificarYReparar();

        // PASO 1: Verificar conexión a SQL Server al iniciar
        Console.WriteLine("🔍 Verificando conectividad al servidor...");
        bool sqlServerDisponible = ConnectionStateManager.VerificarConexionAlServidor();
        ConnectionStateManager.IniciarMonitoreoConexion();

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
                try
                {
                    var sincronizacion = new SincronizacionCompletaService();
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await sincronizacion.SincronizarCompleto();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Advertencia: no se pudo sincronizar SQLite tras login: {ex.Message}");
                        }
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Advertencia: no se pudo sincronizar SQLite tras login: {ex.Message}");
                }

                // Si login fue exitoso y hay sesión, abrir MenuPrincipal
                ConnectionStateManager.IniciarMonitoreoConexion();
                Application.Run(new MenuPrincipal());
            }
        }
        finally
        {
            inicioSesion.Dispose();
        }
    }

    private static void ManejarExcepcionGlobal(Exception ex, string origen)
    {
        try
        {
            if (ex is Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"[{origen}] Error de SQL detectado: {sqlEx.Number} - {sqlEx.Message}");
                ConnectionStateManager.VerificarConexionAlServidor();
                if (Application.OpenForms.Count > 0)
                {
                    MessageBox.Show(
                        "Se detectó una caída del servidor SQL Server. La aplicación continuará en modo OFFLINE temporal para no cerrar la sesión y sincronizará cuando se restablezca la conexión.",
                        "Modo Offline Temporal",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                return;
            }

            Console.WriteLine($"[{origen}] Excepción no controlada: {ex.Message}");
            if (Application.OpenForms.Count > 0)
            {
                MessageBox.Show(
                    $"Se produjo un error de la aplicación. La sesión se mantendrá activa y se continuará con el modo de recuperación del sistema.\n\nDetalle: {ex.Message}",
                    "Error del sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        catch
        {
            // No detener la aplicación ni la sesión por un error del manejador.
        }
    }
}
