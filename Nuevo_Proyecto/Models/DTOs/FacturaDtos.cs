namespace Nuevo_Proyecto.Models.DTOs
{
    public class FacturaResumenDto
    {
        public string Numero { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Empleado { get; set; } = string.Empty;
        public string FormaPago { get; set; } = string.Empty;
        public string? Estado { get; set; }
    }

    public class FacturaDetalleDto
    {
        public string Numero { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string Empleado { get; set; } = string.Empty;
        public string FormaPago { get; set; } = string.Empty;
        public string? Estado { get; set; }
        public string Producto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class LineaFacturaDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }

    /// <summary>
    /// Datos que envía la vista para facturar. Los precios y totales NO viajan:
    /// los calcula el repositorio con los precios vigentes de la base de datos.
    /// </summary>
    public class NuevaFacturaDto
    {
        public int? ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public int FormaPagoId { get; set; }
        public string? Observacion { get; set; }
        public List<LineaFacturaDto> Lineas { get; set; } = new();
    }

    public class ResultadoFacturaDto
    {
        public int FacturaId { get; set; }
        public string Numero { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}

namespace Nuevo_Proyecto.Models.DTOs
{
    /// <summary>Ventas vigentes (excluye facturas anuladas) en un período.</summary>
    public class ResumenVentasDto
    {
        public decimal Total { get; set; }
        public int CantidadFacturas { get; set; }
    }
}
