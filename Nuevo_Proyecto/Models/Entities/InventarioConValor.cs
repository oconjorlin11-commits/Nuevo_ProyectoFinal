using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuevo_Proyecto.Models.Entities
{
    public partial class InventarioConValor
    {
        public int MovimientoId { get; set; }
        public DateTime Fecha { get; set; }
        public string? TipoMovimiento { get; set; } = null!;

        public int Cantidad { get; set; }

        public int StockAnterior { get; set; }

        public int StockNuevo { get; set; }

        public string? Observacion { get; set; } = null!;

        public int ProductoId { get; set; }

        public string? NombreProducto { get; set; } = null!;

        public int ValorInventario { get; set; } 

        public int? CantidadFacturasEmitidas { get; set; }

        public int? CantidadProductosDescontados { get; set; }

    }
}
