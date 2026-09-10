using System;
using System.Collections.Generic;


namespace Nuevo_Proyecto.Models.Entities
{
    public partial class Categoria
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();
    }
}
