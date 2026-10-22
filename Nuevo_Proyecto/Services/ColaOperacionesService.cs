using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Data.DbSqlite;
using Nuevo_Proyecto.Data.DbSqlite.Entities;
using System.Text.Json;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Servicio para guardar operaciones en la cola SQLite cuando falla la conexión a SQL Server
    /// Las operaciones se procesan automáticamente cuando se restablece la conexión
    /// </summary>
    public class ColaOperacionesService
    {
        private readonly ComideriaSqliteContextFactory _sqliteFactory;

        public ColaOperacionesService(ComideriaSqliteContextFactory? sqliteFactory = null)
        {
            _sqliteFactory = sqliteFactory ?? new ComideriaSqliteContextFactory();
        }

        /// <summary>
        /// Registra una operación en la cola para sincronizar cuando se restablezca la conexión
        /// </summary>
        public int RegistrarOperacion(
            string nombreEntidad,
            string accion,
            string identificadorEntidad,
            object payload,
            string? claveIdempotencia = null)
        {
            try
            {
                using (var sqlite = _sqliteFactory.CreateDbContext())
                {
                    claveIdempotencia ??= $"{nombreEntidad}_{accion}_{identificadorEntidad}_{DateTime.UtcNow:yyyyMMddHHmmss}";

                    // Verificar si ya existe una operación con la misma clave de idempotencia
                    var existente = sqlite.ColaOperaciones
                        .FirstOrDefault(x => x.ClaveIdempotencia == claveIdempotencia && x.Estado == "Pendiente");

                    if (existente != null)
                    {
                        // No duplicar si ya existe la misma operación pendiente
                        return existente.Secuencia;
                    }

                    var operacion = new ColaOperacione
                    {
                        OperacionId = Guid.NewGuid().ToString(),
                        ClaveIdempotencia = claveIdempotencia,
                        NombreEntidad = nombreEntidad,
                        Accion = accion,
                        IdentificadorEntidad = identificadorEntidad,
                        PayloadJson = JsonSerializer.Serialize(payload),
                        Estado = "Pendiente",
                        Intentos = 0,
                        CreadaUtc = DateTime.UtcNow.ToString("o"),
                        ProximoIntentoUtc = DateTime.UtcNow.AddSeconds(5).ToString("o")
                    };

                    sqlite.ColaOperaciones.Add(operacion);
                    sqlite.SaveChanges();

                    Console.WriteLine($"✓ Operación registrada en cola: {operacion.OperacionId}");
                    return operacion.Secuencia;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error registrando operación en cola: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las operaciones pendientes ordenadas por secuencia
        /// </summary>
        public List<ColaOperacione> ObtenerOperacionesPendientes()
        {
            using (var sqlite = _sqliteFactory.CreateDbContext())
            {
                return sqlite.ColaOperaciones
                    .Where(x => x.Estado == "Pendiente")
                    .OrderBy(x => x.Secuencia)
                    .ToList();
            }
        }

        /// <summary>
        /// Obtiene el conteo de operaciones por estado
        /// </summary>
        public Dictionary<string, int> ObtenerEstadisticas()
        {
            using (var sqlite = _sqliteFactory.CreateDbContext())
            {
                return sqlite.ColaOperaciones
                    .GroupBy(x => x.Estado)
                    .Select(g => new { Estado = g.Key, Cantidad = g.Count() })
                    .ToDictionary(x => x.Estado, x => x.Cantidad);
            }
        }

        /// <summary>
        /// Limpia operaciones confirmadas más antiguas que X días
        /// </summary>
        public int LimpiarOperacionesAntiguas(int diasAntiguedad = 30)
        {
            using (var sqlite = _sqliteFactory.CreateDbContext())
            {
                var fechaLimite = DateTime.UtcNow.AddDays(-diasAntiguedad);
                var operacionesAntIguas = sqlite.ColaOperaciones
                    .Where(x => 
                        x.Estado == "Confirmada" && 
                        DateTime.Parse(x.ConfirmadaUtc!) < fechaLimite
                    )
                    .ToList();

                if (operacionesAntIguas.Count > 0)
                {
                    sqlite.ColaOperaciones.RemoveRange(operacionesAntIguas);
                    sqlite.SaveChanges();
                    Console.WriteLine($"✓ Limpiadas {operacionesAntIguas.Count} operaciones antiguas de la cola");
                }

                return operacionesAntIguas.Count;
            }
        }

        /// <summary>
        /// Reinicia una operación fallida para intentar de nuevo
        /// </summary>
        public bool ReintenlarOperacion(int secuencia)
        {
            try
            {
                using (var sqlite = _sqliteFactory.CreateDbContext())
                {
                    var operacion = sqlite.ColaOperaciones.Find(secuencia);
                    if (operacion != null && operacion.Estado == "Error")
                    {
                        operacion.Estado = "Pendiente";
                        operacion.Intentos = 0;
                        operacion.ProximoIntentoUtc = DateTime.UtcNow.AddSeconds(5).ToString("o");
                        operacion.UltimoError = null;

                        sqlite.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error reintentando operación: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Obtiene una operación específica
        /// </summary>
        public ColaOperacione? ObtenerOperacion(int secuencia)
        {
            using (var sqlite = _sqliteFactory.CreateDbContext())
            {
                return sqlite.ColaOperaciones.Find(secuencia);
            }
        }
    }
}
