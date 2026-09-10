using System;
using System.Collections.Generic;

namespace Nuevo_Proyecto.Models.Entities
{
    public partial class Estado
    {
        public int EstadoId { get; set; }
        public string NombreEstado { get; set; } = null!;

        public virtual ICollection<Facturas> Facturas { get; set; } = new List<Facturas>();

    }
}
