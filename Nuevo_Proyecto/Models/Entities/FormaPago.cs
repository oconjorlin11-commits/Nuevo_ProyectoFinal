using System;
using System.Collections.Generic;

namespace Nuevo_Proyecto.Models.Entities
{
    public partial class FormaPago
    {
        public int FormaPagoId { get; set; }
        public string Nombre { get; set; } = null!;
        public virtual ICollection<Facturas> Facturas { get; set; } = new List<Facturas>();
    }
}
