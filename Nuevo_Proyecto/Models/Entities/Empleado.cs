using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nuevo_Proyecto.Models.Entities
{
    [Table("Empleados")]
    public partial class Empleado
    {
        public int EmpleadoId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string? Telefono { get; set; } = null!;
        public string? Cargo { get; set; } = null!;
        public decimal Salario { get; set; }
        public DateTime? Fechaingreso { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Facturas> Facturas { get; set; } = new List<Facturas>();
        public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();
    }
}
