namespace Nuevo_Proyecto.Models.DTOs
{
    public class ProductoNuevoDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public int UnidadId { get; set; }
        public string? Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Activo { get; set; } = true;
        public int StockInicial { get; set; }
        public int StockMinimo { get; set; }
    }
}
