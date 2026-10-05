namespace Nuevo_Proyecto.Models.DTOs
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string? Nota { get; set; }
        public bool Activo { get; set; } = true;
    }
}
