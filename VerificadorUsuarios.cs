using Microsoft.Data.Sqlite;
using System;
using System.IO;

class VerificadorUsuarios
{
    static void Main()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string dbPath = Path.Combine(appDataPath, "Comideria", "comideria_offline.db");

        Console.WriteLine($"📍 Base de datos: {dbPath}");
        Console.WriteLine($"📊 Existe: {File.Exists(dbPath)}");

        if (!File.Exists(dbPath))
        {
            Console.WriteLine("✗ La BD SQLite NO existe. Se creará automáticamente al iniciar la app.");
            return;
        }

        try
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                Console.WriteLine("✓ Conectado a BD SQLite");

                // Verificar si tabla Usuarios existe
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT COUNT(*) 
                        FROM sqlite_master 
                        WHERE type='table' AND name='Usuarios'";

                    var result = (long)command.ExecuteScalar();

                    if (result > 0)
                    {
                        Console.WriteLine("✓ Tabla 'Usuarios' EXISTE");

                        // Contar registros
                        using (var countCmd = connection.CreateCommand())
                        {
                            countCmd.CommandText = "SELECT COUNT(*) FROM Usuarios";
                            var count = (long)countCmd.ExecuteScalar();
                            Console.WriteLine($"  - Registros: {count}");
                        }

                        // Mostrar esquema
                        using (var schemaCmd = connection.CreateCommand())
                        {
                            schemaCmd.CommandText = "PRAGMA table_info(Usuarios)";
                            using (var reader = schemaCmd.ExecuteReader())
                            {
                                Console.WriteLine("  - Columnas:");
                                while (reader.Read())
                                {
                                    var colName = reader.GetString(1);
                                    var colType = reader.GetString(2);
                                    Console.WriteLine($"    • {colName} ({colType})");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("✗ Tabla 'Usuarios' NO EXISTE");
                        Console.WriteLine("  Esto se creará automáticamente cuando loguees desde SQL Server.");
                    }
                }

                // Listar todas las tablas
                Console.WriteLine("\n📋 Todas las tablas en SQLite:");
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"  • {reader.GetString(0)}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }

        Console.WriteLine("\n✓ Verificación completada.");
    }
}
