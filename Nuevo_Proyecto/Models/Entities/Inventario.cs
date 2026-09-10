using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuevo_Proyecto.Models.Entities
{
    public partial class Inventario
    {
        public int InventarioId { get; set; }
        public int ProductoId { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public int ValorInventario { get; set; } = 0;

        public virtual Productos Productos { get; set; } = null!;
    }
}
