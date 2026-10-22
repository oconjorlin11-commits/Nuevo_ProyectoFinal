using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data.DbSqlite;

namespace Nuevo_Proyecto.Data
{
    /// <summary>
    /// Factory para crear instancias del contexto SQLite
    /// Usado para sincronización offline y caché local
    /// </summary>
    public class ComideriaSqliteContextFactory : IDbContextFactory<ComideriaSqliteContext>
    {
        private readonly string _sqlitePath;

        public ComideriaSqliteContextFactory()
        {
            // Por defecto guardar en AppData local
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string comercia = Path.Combine(appDataPath, "Comideria");

            // Crear carpeta si no existe
            if (!Directory.Exists(comercia))
            {
                Directory.CreateDirectory(comercia);
            }

            _sqlitePath = Path.Combine(comercia, "comideria_offline.db");
        }

        public ComideriaSqliteContextFactory(string customPath)
        {
            _sqlitePath = customPath;

            // Crear carpeta si no existe
            string? directory = Path.GetDirectoryName(customPath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public ComideriaSqliteContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ComideriaSqliteContext>()
                .UseSqlite($"Data Source={_sqlitePath};")
                .Options;

            var context = new ComideriaSqliteContext(options);

            // Crear tablas si no existen
            try
            {
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crítico al crear tablas SQLite: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;  // Propagar el error para que no se oculte
            }

            return context;
        }
    }
}
