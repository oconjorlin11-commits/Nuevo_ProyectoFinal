namespace Nuevo_Proyecto.Models.DTOs
{
    public class ClientesDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Nota { get; set; }
        public bool Activo { get; set; }
    }
}

