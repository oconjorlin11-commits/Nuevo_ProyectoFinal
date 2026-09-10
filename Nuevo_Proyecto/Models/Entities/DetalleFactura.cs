using System;
using System.Collections.Generic;


namespace Nuevo_Proyecto.Models.Entities
{
    public partial class DetalleFactura
    {
        public int DetalleId { get; set; }
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal subtotal { get; set; }

        public virtual Facturas Factura { get; set; } = null!;
        public virtual Productos Producto { get; set; } = null!;
    }
}
