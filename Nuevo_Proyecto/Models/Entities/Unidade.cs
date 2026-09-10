using System;
using System.Collections.Generic;


namespace Nuevo_Proyecto.Models.Entities
{
    public partial class Unidade
    {
        public int UnidadeId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();

    }
}
