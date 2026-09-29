using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nuevo_Proyecto.Models.Entities
{
    [Table("Unidades")]
    public partial class Unidade
    {
        public int UnidadId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();

    }
}
