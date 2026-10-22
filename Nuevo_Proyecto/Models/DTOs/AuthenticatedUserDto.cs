namespace Nuevo_Proyecto.Models.DTOs
{
    public class AuthenticatedUserDto
    {
        public int UserId { get; set; }

        public int EmployeeId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string EmployeeName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public IReadOnlyCollection<string> Roles { get; set; } = Array.Empty<string>();
    }
}

