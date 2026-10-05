namespace Nuevo_Proyecto.Models.DTOs
{
    public class MovimientoInventarioDto
    {
        public int MovimientoId { get; set; }
        public DateTime Fecha { get; set; }
        public string? Tipo { get; set; }
        public int Cantidad { get; set; }
        public int StockAnterior { get; set; }
        public int StockNuevo { get; set; }
        public string? Observacion { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string NombreEmpleado { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public decimal ValorInventario { get; set; }
    }
}
