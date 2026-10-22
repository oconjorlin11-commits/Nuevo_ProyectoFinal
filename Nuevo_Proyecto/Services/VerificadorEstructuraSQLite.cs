using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Data.DbSqlite;
using System;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Utilitario para verificar y reparar la estructura de BD SQLite
    /// Verifica si la tabla Usuarios existe y la crea si es necesaria
    /// </summary>
    public class VerificadorEstructuraSQLite
    {
        private readonly ComideriaSqliteContextFactory _contextFactory;

        public VerificadorEstructuraSQLite(ComideriaSqliteContextFactory? contextFactory = null)
        {
            _contextFactory = contextFactory ?? new ComideriaSqliteContextFactory();
        }

        /// <summary>
        /// Verifica la estructura de BD SQLite y repara si es necesario
        /// </summary>
        public void VerificarYReparar()
        {
            try
            {
                Console.WriteLine("🔍 Verificando estructura de BD SQLite...");

                using (var context = _contextFactory.CreateDbContext())
                {
                    // Verificar si tabla Usuarios existe
                    bool usuariosExists = ExisteTabla(context, "Usuarios");

                    if (usuariosExists)
                    {
                        Console.WriteLine("✓ Tabla 'Usuarios' existe");

                        // Contar registros
                        try
                        {
                            var count = context.Usuarios.Count();
                            Console.WriteLine($"  - Registros en Usuarios: {count}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"  ⚠️ Error al contar registros: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("✗ Tabla 'Usuarios' NO EXISTE");
                        Console.WriteLine("  → Se creará automáticamente cuando loguees desde SQL Server");
                    }

                    // Verificar otras tablas críticas
                    string[] tablasEsperadas = { "Empleados", "Clientes", "Productos", "Categorias", "FormaPagos" };
                    Console.WriteLine("\n📋 Estado de tablas:");
                    foreach (var tabla in tablasEsperadas)
                    {
                        bool existe = ExisteTabla(context, tabla);
                        string estado = existe ? "✓" : "✗";
                        Console.WriteLine($"  {estado} {tabla}");
                    }
                }

                Console.WriteLine("\n✓ Verificación completada\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error en verificación: {ex.Message}\n");
            }
        }

        /// <summary>
        /// Verifica si una tabla existe en SQLite
        /// </summary>
        private bool ExisteTabla(DbContext context, string nombreTabla)
        {
            try
            {
                var result = context.Database.SqlQueryRaw<int>(
                    $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{nombreTabla}'")
                    .FirstOrDefault();

                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Limpia y recrea la BD SQLite completamente
        /// ⚠️ CUIDADO: Borra todos los datos locales
        /// </summary>
        public void RecreaDBCompleta()
        {
            Console.WriteLine("⚠️ Eliminando y recreando BD SQLite...");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    // Eliminar y recrear
                    context.Database.EnsureDeleted();
                    Console.WriteLine("✓ BD anterior eliminada");

                    context.Database.EnsureCreated();
                    Console.WriteLine("✓ BD nueva creada");
                }

                Console.WriteLine("✓ BD SQLite recreada exitosamente\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error al recrear BD: {ex.Message}\n");
            }
        }
    }
}
