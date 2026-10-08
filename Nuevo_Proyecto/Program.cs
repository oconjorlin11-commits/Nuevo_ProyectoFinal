using Nuevo_Proyecto.Models.Views;
using Nuevo_Proyecto.Services.Helpers;

namespace Nuevo_Proyecto
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Mostrar InicioSesion como diálogo modal
            using (var inicioSesion = new InicioSesion())
            {
                if (inicioSesion.ShowDialog() == DialogResult.OK && SesionActual.HaySesion)
                {
                    // Si login fue exitoso y hay sesión, abrir MenuPrincipal
                    Application.Run(new MenuPrincipal());
                }
                // Si no hubo login exitoso, la aplicación se cierra
            }
        }
    }
}