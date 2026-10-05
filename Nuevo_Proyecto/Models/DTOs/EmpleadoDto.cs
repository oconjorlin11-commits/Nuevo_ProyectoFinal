namespace Nuevo_Proyecto.Models.DTOs
{
    public class EmpleadoDto
    {
        public int EmpleadoId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Cargo { get; set; }
        public decimal Salario { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public bool Activo { get; set; } = true;
    }
}
