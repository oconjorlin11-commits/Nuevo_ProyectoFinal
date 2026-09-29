using System;
using System.Collections.Generic;

namespace Nuevo_Proyecto.Models.Entities
{
    public partial class Facturas
    {
        public int FacturaId { get; set; }
        // Numero es un código alfanumérico en la base de datos (ej. "F0001")
        public string? Numero { get; set; }
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int FormaPagoId { get; set; }
        public string? Observacion { get; set; } = null!;
        // Estado textual (ej. "ACTIVA")
        public string? Estado { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public int? EstadoId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();
        public virtual Empleado Empleado { get; set; } = null!;
        public virtual Estado? EstadoNavigation { get; set; }
        public virtual FormaPago FormaPago { get; set; } = null!;

    }
}
