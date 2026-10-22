using System;

namespace Nuevo_Proyecto.Models.DTOs
{
    /// <summary>
    /// DTO para representar una notificación de stock crítico
    /// Basado en la tabla NotificacionesStockCritico que es actualizada por el trigger trg_StockCritico
    /// </summary>
    public class NotificacionStockCriticoDto
    {
        public int NotificacionID { get; set; }
        public int ProductoID { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public override string ToString()
        {
            return $"{NombreProducto} - Stock: {StockActual}/{StockMinimo}";
        }
    }
}
