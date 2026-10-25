using System;
using System.IO;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Utility para limpiar y resetear la BD SQLite
    /// Se ejecuta automáticamente en el startup
    /// </summary>
    public static class LimpiezaBDSQLite
    {
        public static void EliminarBDAnterior()
        {
            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string dbPath = Path.Combine(appDataPath, "Comideria", "comideria_offline.db");

                if (File.Exists(dbPath))
                {
                    Console.WriteLine("🗑️  Eliminando BD SQLite anterior...");
                    File.Delete(dbPath);
                    Console.WriteLine("✓ BD SQLite eliminada");
                }

                // También eliminar archivos temp de SQLite
                string[] tempFiles = { 
                    dbPath + "-wal", 
                    dbPath + "-shm" 
                };

                foreach (var tempFile in tempFiles)
                {
                    if (File.Exists(tempFile))
                    {
                        try
                        {
                            File.Delete(tempFile);
                        }
                        catch { /* ignorar si no se puede eliminar */ }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error al limpiar BD: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina el caché temporal de SQLite al restaurarse la conexión en vivo.
        /// El archivo local solo sirve como respaldo temporal y debe quedar limpio al volver a SQL Server.
        /// </summary>
        public static void LimpiarBDTemporal()
        {
            EliminarBDAnterior();

            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string folder = Path.Combine(appDataPath, "Comideria");
                if (Directory.Exists(folder))
                {
                    foreach (var file in Directory.GetFiles(folder, "comideria_offline*.db*", SearchOption.TopDirectoryOnly))
                    {
                        try { File.Delete(file); }
                        catch { }
                    }
                }
            }
            catch
            {
                // Ignorar errores de limpieza final
            }
        }
    }
}
