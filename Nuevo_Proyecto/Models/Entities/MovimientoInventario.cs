using System;
using System.Collections.Generic;


namespace Nuevo_Proyecto.Models.Entities
{
    public partial class MovimientoInventario
    {
        public int MovimientoId { get; set; }
        public int ProductoId { get; set; } 
        public DateTime Fecha { get; set; } 
        public string? TipoMovimiento { get; set; } = null!;
        public int Cantidad { get; set; }
        public int StockAnterior { get; set; }
        public int StockNuevo { get; set; }
        public int EmpleadoId { get; set; } = 1;
        public string? Observacion { get; set; } = null!;

        public virtual Empleado Empleado { get; set; } = null!;
        public virtual Productos Producto { get; set; } = null!;




    }
}
