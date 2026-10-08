namespace Nuevo_Proyecto.Models.DTOs
{
    /// <summary>Producto + existencias (lo que muestran Inventario, Facturación y el dashboard).</summary>
    public class ProductoInventarioDto
    {
        public int ProductoId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public int UnidadId { get; set; }
        public string Unidad { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public bool Activo { get; set; }
        public string Observacion { get; set; } = string.Empty;

        public bool StockBajo => Stock <= StockMinimo;
        public decimal ValorInventario => PrecioVenta * Stock;
    }
}
