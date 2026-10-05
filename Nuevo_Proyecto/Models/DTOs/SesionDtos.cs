namespace Nuevo_Proyecto.Models.DTOs
{
    public class SesionUsuarioDto
    {
        public int EmpleadoId { get; set; }
        public string CodigoEmpleado { get; set; } = string.Empty;
        public string NombreEmpleado { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
    }

    public class ResultadoLoginDto
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public SesionUsuarioDto? Sesion { get; set; }
    }
}
