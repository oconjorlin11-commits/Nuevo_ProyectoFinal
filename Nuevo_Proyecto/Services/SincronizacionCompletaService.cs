using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Data.DbSqlite;
using Nuevo_Proyecto.Models.Entities;
using System;
using System.Diagnostics;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Servicio que sincroniza TODAS las tablas críticas de SQL Server a SQLite
    /// Se ejecuta automáticamente al inicio si hay conexión SQL disponible
    /// Garantiza operación completa en modo OFFLINE
    /// </summary>
    public class SincronizacionCompletaService
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _sqlFactory;
        private readonly ComideriaSqliteContextFactory _sqliteFactory;
        private readonly Stopwatch _timer = new();

        public SincronizacionCompletaService(
            IDbContextFactory<Dev_ComideriaDbContext>? sqlFactory = null,
            ComideriaSqliteContextFactory? sqliteFactory = null)
        {
            _sqlFactory = sqlFactory ?? new ComideriaContextFactory();
            _sqliteFactory = sqliteFactory ?? new ComideriaSqliteContextFactory();
        }

        /// <summary>
        /// Sincroniza TODAS las tablas de SQL Server a SQLite
        /// Se ejecuta automáticamente al iniciar si SQL está disponible
        /// </summary>
        public async Task<bool> SincronizarCompleto()
        {
            if (ConnectionStateManager.UsandoSqlite)
            {
                Console.WriteLine("⚠️ SQLite temporal activo - omitiendo sincronización completa");
                return false;
            }

            if (!ConnectionStateManager.VerificarConexionAlServidor())
            {
                Console.WriteLine("⚠️ SQL Server no disponible. No se hace sincronización completa.");
                return false;
            }

            _timer.Restart();
            Console.WriteLine("\n🔄 INICIANDO SINCRONIZACIÓN COMPLETA SQL → SQLite...");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            try
            {
                LimpiezaBDSQLite.EliminarBDAnterior();

                using (var sqlDb = _sqlFactory.CreateDbContext())
                using (var sqliteDb = _sqliteFactory.CreateDbContext())
                {
                    Console.WriteLine("🗑️  Limpiando BD SQLite anterior...");
                    LimpiarBDSQLite(sqliteDb);

                    await SincronizarTabla<Cliente>(sqlDb, sqliteDb, "Clientes", c => c.Activo ?? true);
                    await SincronizarTabla<Empleado>(sqlDb, sqliteDb, "Empleados", e => e.Activo ?? true);
                    await SincronizarTabla<Categoria>(sqlDb, sqliteDb, "Categorias", null);
                    await SincronizarTabla<Productos>(sqlDb, sqliteDb, "Productos", null);
                    await SincronizarTabla<Unidade>(sqlDb, sqliteDb, "Unidades", null);
                    await SincronizarTabla<FormaPago>(sqlDb, sqliteDb, "FormaPagos", null);
                    await SincronizarTabla<Inventario>(sqlDb, sqliteDb, "Inventario", null);
                    await SincronizarTabla<Facturas>(sqlDb, sqliteDb, "Facturas", null);
                    await SincronizarTabla<DetalleFactura>(sqlDb, sqliteDb, "DetalleFacturas", null);
                    await SincronizarUsuarios(sqlDb, sqliteDb);

                    _timer.Stop();
                    Console.WriteLine("\n═══════════════════════════════════════════════════════");
                    Console.WriteLine($"✅ SINCRONIZACIÓN COMPLETADA EN {_timer.ElapsedMilliseconds}ms");
                    Console.WriteLine("✓ BD SQLite lista para operación OFFLINE\n");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _timer.Stop();
                Console.WriteLine($"\n❌ ERROR EN SINCRONIZACIÓN: {ex.Message}");
                Console.WriteLine("⚠️  La aplicación continuará con el caché existente o apagada.");
                return false;
            }
        }

        /// <summary>
        /// Sincroniza una tabla genérica de SQL a SQLite
        /// </summary>
        private async Task SincronizarTabla<T>(
            Dev_ComideriaDbContext sqlDb,
            ComideriaSqliteContext sqliteDb,
            string nombreTabla,
            Func<T, bool>? filtro = null) where T : class
        {
            try
            {
                Console.Write($"  📊 Sincronizando {nombreTabla}... ");

                // Obtener datos de SQL Server
                var dbSet = sqlDb.Set<T>();
                var datos = await dbSet.ToListAsync();

                if (filtro != null)
                    datos = datos.Where(filtro).ToList();

                if (datos.Count == 0)
                {
                    Console.WriteLine("(vacío)");
                    return;
                }

                // Limpiar tabla en SQLite
                var sqliteDbSet = sqliteDb.Set<T>();
                sqliteDbSet.RemoveRange(sqliteDbSet);

                // Insertar en SQLite
                await sqliteDbSet.AddRangeAsync(datos);
                await sqliteDb.SaveChangesAsync();

                Console.WriteLine($"✓ {datos.Count} registros");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Sincroniza usuarios usando SQL query directo (compatible con ambas BDs)
        /// </summary>
        private async Task SincronizarUsuarios(Dev_ComideriaDbContext sqlDb, ComideriaSqliteContext sqliteDb)
        {
            try
            {
                Console.Write($"  👤 Sincronizando Usuarios (CRÍTICO)... ");

                // Obtener usuarios de SQL Server
                var usuariosSql = await sqlDb.Database.SqlQuery<Usuario>($@"
                    SELECT u.UsuarioId,
                           u.NombreUsuario,
                           u.[Contraseña] AS Contrasena,
                           u.RolSistema,
                           u.EmpleadoID AS EmpleadoId,
                           u.Activo,
                           u.UltimoAcceso
                    FROM Usuarios u
                    WHERE u.Activo = 1").ToListAsync();

                if (usuariosSql.Count == 0)
                {
                    Console.WriteLine("(NO HAY USUARIOS ACTIVOS)");
                    return;
                }

                // Limpiar usuarios en SQLite
                var usuariosExistentes = sqliteDb.Usuarios.ToList();
                if (usuariosExistentes.Count > 0)
                    sqliteDb.Usuarios.RemoveRange(usuariosExistentes);

                // Insertar usuarios en SQLite
                await sqliteDb.Usuarios.AddRangeAsync(usuariosSql);
                await sqliteDb.SaveChangesAsync();

                Console.WriteLine($"✓ {usuariosSql.Count} usuarios (OFFLINE HABILITADO)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Limpia completamente la BD SQLite
        /// </summary>
        private void LimpiarBDSQLite(ComideriaSqliteContext context)
        {
            try
            {
                // Obtener todas las tablas
                context.Usuarios.RemoveRange(context.Usuarios);
                context.Clientes.RemoveRange(context.Clientes);
                context.Empleados.RemoveRange(context.Empleados);
                context.Categorias.RemoveRange(context.Categorias);
                context.Productos.RemoveRange(context.Productos);
                context.Unidades.RemoveRange(context.Unidades);
                context.FormaPagos.RemoveRange(context.FormaPagos);
                context.Inventario.RemoveRange(context.Inventario);
                context.Facturas.RemoveRange(context.Facturas);
                context.DetalleFacturas.RemoveRange(context.DetalleFacturas);

                context.SaveChanges();
                Console.WriteLine("✓ BD SQLite limpiada");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error al limpiar: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina el archivo de BD SQLite para forzar recreación completa
        /// </summary>
        public void EliminarYRecreaDB()
        {
            try
            {
                using (var context = _sqliteFactory.CreateDbContext())
                {
                    Console.WriteLine("🗑️  Eliminando BD SQLite anterior...");
                    context.Database.EnsureDeleted();

                    Console.WriteLine("📝 Recreando estructura de BD SQLite...");
                    context.Database.EnsureCreated();

                    Console.WriteLine("✓ BD SQLite recreada");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }
    }
}
