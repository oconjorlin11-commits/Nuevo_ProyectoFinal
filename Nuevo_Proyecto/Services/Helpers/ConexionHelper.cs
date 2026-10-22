using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nuevo_Proyecto.Services.Helpers
{
    public static class ConexionHelper
    {
        public const string MensajeModoOffline =
            "No hay internet o se interrumpió la conexión por lo tanto solo tienes acceso a agregar un nuevo empleado, cliente y producto pero a consultar no o editar un producto o eliminar eso ya no.";

        /// <summary>
        /// Determina si una excepción fue causada por falla de red, pérdida de conexión o indisponibilidad de SQL Server.
        /// </summary>
        public static bool IsConnectionFailure(Exception? exception)
        {
            for (Exception? current = exception; current is not null; current = current.InnerException)
            {
                if (current is TimeoutException)
                {
                    return true;
                }

                if (current is SqlException sqlException)
                {
                    if (sqlException.Errors.Cast<SqlError>().Any(error =>
                            error.Number is 0 or -2 or 53 or 64 or 233 or 10053 or 10054 or 10060 or 11001))
                    {
                        return true;
                    }

                    if (sqlException.Message.Contains("network-related", StringComparison.OrdinalIgnoreCase) ||
                        sqlException.Message.Contains("server-specific", StringComparison.OrdinalIgnoreCase) ||
                        sqlException.Message.Contains("error occurred while establishing a connection", StringComparison.OrdinalIgnoreCase) ||
                        sqlException.Message.Contains("no se pudo abrir una conexión", StringComparison.OrdinalIgnoreCase) ||
                        sqlException.Message.Contains("tiempo de espera", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Muestra el mensaje informativo estándar de modo sin conexión.
        /// </summary>
        public static void MostrarMensajeModoOffline()
        {
            MessageBox.Show(
                MensajeModoOffline,
                "Sin Conexión con el Servidor",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
    }
}

