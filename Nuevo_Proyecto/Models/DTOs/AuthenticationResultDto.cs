namespace Nuevo_Proyecto.Models.DTOs
{
    public class AuthenticationResultDto
    {
        public bool IsAuthenticated { get; set; }

        public AuthenticatedUserDto? User { get; set; }

        public string Message { get; set; } = string.Empty;

        public static AuthenticationResultDto Success(AuthenticatedUserDto user)
        {
            return new AuthenticationResultDto
            {
                IsAuthenticated = true,
                User = user,
                Message = "Autenticación exitosa."
            };
        }

        public static AuthenticationResultDto Failure(string message)
        {
            return new AuthenticationResultDto
            {
                IsAuthenticated = false,
                Message = message
            };
        }
    }
}

