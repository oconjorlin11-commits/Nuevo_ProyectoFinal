using System;
using System.Collections.Generic;


namespace Nuevo_Proyecto.Models.Entities
{
    public partial class Cliente
    {

        public int ClienteId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string Direccion { get; set; } = null!;
        public string Nota { get; set; } = null!;
        public bool? Activo { get; set; }

        public virtual ICollection<Facturas> Facturas { get; set; } = new List<Facturas>();
    }
}
