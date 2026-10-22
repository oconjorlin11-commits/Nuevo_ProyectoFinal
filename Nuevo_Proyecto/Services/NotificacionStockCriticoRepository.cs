using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using System;
using System.Data;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Repositorio para acceder a las notificaciones de stock crítico
    /// Estas notificaciones son generadas automáticamente por el trigger trg_StockCritico
    /// en la tabla NotificacionesStockCritico de la base de datos
    /// </summary>
    public class NotificacionStockCriticoRepository
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;

        public NotificacionStockCriticoRepository(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
        }

        /// <summary>
        /// Obtiene todas las notificaciones de stock crítico con información del producto
        /// </summary>
        public List<NotificacionStockCriticoDto> ObtenerNotificacionesCriticas()
        {
            using (var db = _factory.CreateDbContext())
            {
                var notificaciones = db.Database.SqlQuery<NotificacionStockCriticoDto>(
                    $@"SELECT 
                        n.NotificacionID,
                        n.ProductoID,
                        p.Nombre AS NombreProducto,
                        n.StockActual,
                        n.StockMinimo,
                        n.FechaRegistro
                    FROM NotificacionesStockCritico n
                    INNER JOIN Productos p ON n.ProductoID = p.ProductoID
                    ORDER BY n.FechaRegistro DESC"
                ).ToList();

                return notificaciones;
            }
        }

        /// <summary>
        /// Obtiene el conteo de notificaciones críticas activas
        /// </summary>
        public int ObtenerConteoCriticas()
        {
            using (var db = _factory.CreateDbContext())
            {
                return db.Database.SqlQuery<int>(
                    $@"SELECT COUNT(*) FROM NotificacionesStockCritico"
                ).FirstOrDefault();
            }
        }

        /// <summary>
        /// Obtiene las notificaciones más recientes (últimas N)
        /// </summary>
        public List<NotificacionStockCriticoDto> ObtenerNotificacionesRecientes(int cantidad = 10)
        {
            using (var db = _factory.CreateDbContext())
            {
                var notificaciones = db.Database.SqlQuery<NotificacionStockCriticoDto>(
                    $@"SELECT TOP {cantidad}
                        n.NotificacionID,
                        n.ProductoID,
                        p.Nombre AS NombreProducto,
                        n.StockActual,
                        n.StockMinimo,
                        n.FechaRegistro
                    FROM NotificacionesStockCritico n
                    INNER JOIN Productos p ON n.ProductoID = p.ProductoID
                    ORDER BY n.FechaRegistro DESC"
                ).ToList();

                return notificaciones;
            }
        }

        /// <summary>
        /// Limpia una notificación específica (cuando se ha resuelto el issue)
        /// </summary>
        public bool LimpiarNotificacion(int notificacionId)
        {
            try
            {
                using (var db = _factory.CreateDbContext())
                {
                    db.Database.ExecuteSql(
                        $@"DELETE FROM NotificacionesStockCritico WHERE NotificacionID = {notificacionId}"
                    );
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Limpia todas las notificaciones de un producto específico
        /// </summary>
        public bool LimpiarNotificacionesPorProducto(int productoId)
        {
            try
            {
                using (var db = _factory.CreateDbContext())
                {
                    db.Database.ExecuteSql(
                        $@"DELETE FROM NotificacionesStockCritico WHERE ProductoID = {productoId}"
                    );
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
