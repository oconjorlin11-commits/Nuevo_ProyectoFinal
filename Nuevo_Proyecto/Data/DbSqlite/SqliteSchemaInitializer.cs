using System.Diagnostics;
using Microsoft.Data.Sqlite;

namespace Nuevo_Proyecto.Data.DbSqlite;

/// <summary>
/// Crea la base SQLite local y su esquema adaptado a Comideria (Nuevo_Proyecto).
/// </summary>
public static class SqliteSchemaInitializer
{
    private const string SchemaFileName = "ComideriaSqlite_Persistente.sql";
    private const string DatabaseFileName = "ComideriaLocal.db";

    public static bool TryInitialize()
    {
        try
        {
            string directory = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data",
                "DbSqlite");

            Directory.CreateDirectory(directory);

            string schemaPath = Path.Combine(directory, SchemaFileName);
            string databasePath = Path.Combine(directory, DatabaseFileName);

            if (!File.Exists(schemaPath))
            {
                throw new FileNotFoundException(
                    "No se encontró el esquema SQLite.",
                    schemaPath);
            }

            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = databasePath,
                Mode = SqliteOpenMode.ReadWriteCreate,
                ForeignKeys = true
            };

            using var connection = new SqliteConnection(
                connectionStringBuilder.ToString());
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = File.ReadAllText(schemaPath);
            command.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"No fue posible crear la base SQLite local: {ex}");
            return false;
        }
    }
}

