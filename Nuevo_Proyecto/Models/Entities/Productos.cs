using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuevo_Proyecto.Models.Entities
{
    public partial class Productos
    {
        public int ProductoId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int CategoriaId { get; set; }
        public int UnidadId { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal PrecioVentas { get; set; }
        public bool? Activo { get; set; }

        public virtual Categoria Categoria { get; set; } = null!;

        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();
        public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
        public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();

        public virtual Unidade Unidade { get; set; } = null!;
    }
}
