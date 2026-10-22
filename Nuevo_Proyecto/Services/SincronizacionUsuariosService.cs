using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Data.DbSqlite;
using Nuevo_Proyecto.Models.Entities;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Servicio que sincroniza datos kriticos (Usuarios, Empleados, etc.) 
    /// de SQL Server a SQLite para disponibilidad offline
    /// Se ejecuta automáticamente después de login exitoso
    /// </summary>
    public class SincronizacionUsuariosService
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _sqlFactory;
        private readonly ComideriaSqliteContextFactory _sqliteFactory;

        public SincronizacionUsuariosService(
            IDbContextFactory<Dev_ComideriaDbContext>? sqlFactory = null,
            ComideriaSqliteContextFactory? sqliteFactory = null)
        {
            _sqlFactory = sqlFactory ?? new ComideriaContextFactory();
            _sqliteFactory = sqliteFactory ?? new ComideriaSqliteContextFactory();
        }

        /// <summary>
        /// Sincroniza la tabla Usuarios de SQL Server a SQLite
        /// Se ejecuta automáticamente después de login exitoso
        /// </summary>
        public async Task SincronizarUsuarios()
        {
            try
            {
                Console.WriteLine("Iniciando sincronización de usuarios...");

                using (var sqlDb = _sqlFactory.CreateDbContext())
                using (var sqliteDb = _sqliteFactory.CreateDbContext())
                {
                    // Obtener todos los usuarios activos de SQL Server
                    var usuariosSQL = await sqlDb.Database.SqlQuery<Usuario>($@"
                        SELECT u.UsuarioId,
                               u.NombreUsuario,
                               u.[Contraseña] AS Contrasena,
                               u.RolSistema,
                               u.EmpleadoID AS EmpleadoId,
                               u.Activo,
                               u.UltimoAcceso
                        FROM Usuarios u
                        WHERE u.Activo = 1")
                        .ToListAsync();

                    if (usuariosSQL.Count == 0)
                    {
                        Console.WriteLine("No hay usuarios para sincronizar");
                        return;
                    }

                    Console.WriteLine($"Se encontraron {usuariosSQL.Count} usuarios en SQL Server");

                    // Limpiar usuarios existentes en SQLite (para actualizar)
                    var usuariosExistentes = sqliteDb.Usuarios.ToList();
                    if (usuariosExistentes.Count > 0)
                    {
                        sqliteDb.Usuarios.RemoveRange(usuariosExistentes);
                    }

                    // Insertar usuarios en SQLite
                    foreach (var usuario in usuariosSQL)
                    {
                        // Verificar que el empleado existe
                        var empleado = sqliteDb.Empleados
                            .FirstOrDefault(e => e.EmpleadoId == usuario.EmpleadoId);

                        if (empleado == null)
                        {
                            Console.WriteLine($"⚠️ Empleado {usuario.EmpleadoId} no encontrado para usuario {usuario.NombreUsuario}");
                            continue;
                        }

                        // Agregar usuario a SQLite
                        sqliteDb.Usuarios.Add(usuario);
                    }

                    // Guardar cambios
                    await sqliteDb.SaveChangesAsync();
                    Console.WriteLine($"✓ Sincronización completada: {usuariosSQL.Count} usuarios guardados en SQLite");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en sincronización de usuarios: {ex.Message}");
                // No fallar el login si la sincronización falla - el usuario puede continuar
            }
        }

        /// <summary>
        /// Sincroniza un usuario individual después de login
        /// Más eficiente que sincronizar todos los usuarios cada vez
        /// </summary>
        public async Task SincronizarUsuarioIndividual(int usuarioId, string nombreUsuario)
        {
            try
            {
                using (var sqlDb = _sqlFactory.CreateDbContext())
                using (var sqliteDb = _sqliteFactory.CreateDbContext())
                {
                    // Obtener usuario de SQL Server
                    var usuarioSQL = await sqlDb.Database.SqlQuery<Usuario>($@"
                        SELECT u.UsuarioId,
                               u.NombreUsuario,
                               u.[Contraseña] AS Contrasena,
                               u.RolSistema,
                               u.EmpleadoID AS EmpleadoId,
                               u.Activo,
                               u.UltimoAcceso
                        FROM Usuarios u
                        WHERE u.UsuarioId = {usuarioId}")
                        .FirstOrDefaultAsync();

                    if (usuarioSQL == null)
                    {
                        Console.WriteLine($"Usuario {nombreUsuario} no encontrado en SQL Server");
                        return;
                    }

                    // Verificar empleado asociado
                    var empleado = sqliteDb.Empleados
                        .FirstOrDefault(e => e.EmpleadoId == usuarioSQL.EmpleadoId);

                    if (empleado == null)
                    {
                        Console.WriteLine($"Empleado {usuarioSQL.EmpleadoId} no encontrado en SQLite");
                        return;
                    }

                    // Buscar si el usuario ya existe en SQLite
                    var usuarioExistente = sqliteDb.Usuarios
                        .FirstOrDefault(u => u.UsuarioId == usuarioId);

                    if (usuarioExistente != null)
                    {
                        // Actualizar
                        usuarioExistente.Contrasena = usuarioSQL.Contrasena;
                        usuarioExistente.RolSistema = usuarioSQL.RolSistema;
                        usuarioExistente.Activo = usuarioSQL.Activo;
                        usuarioExistente.UltimoAcceso = usuarioSQL.UltimoAcceso;
                        sqliteDb.Usuarios.Update(usuarioExistente);
                    }
                    else
                    {
                        // Crear nuevo
                        sqliteDb.Usuarios.Add(usuarioSQL);
                    }

                    await sqliteDb.SaveChangesAsync();
                    Console.WriteLine($"✓ Usuario {nombreUsuario} sincronizado exitosamente");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sincronizando usuario {nombreUsuario}: {ex.Message}");
            }
        }
    }
}
