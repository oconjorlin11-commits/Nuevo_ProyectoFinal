using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IAuthenticationView
    {
        string UserName { get; }

        string Password { get; }

        void ShowAuthenticationError(string message);

        void ShowAuthenticatedUser(AuthenticatedUserDto user);
    }
}

