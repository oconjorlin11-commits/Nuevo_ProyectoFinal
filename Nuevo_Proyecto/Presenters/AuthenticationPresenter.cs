using System.Diagnostics;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class AuthenticationPresenter
    {
        private readonly IAuthenticationView _view;
        private readonly IAuthService _authenticationService;

        public AuthenticationPresenter(
            IAuthenticationView view,
            IAuthService authenticationService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        public async Task AuthenticateAsync()
        {
            try
            {
                var request = new AuthenticationRequestDto
                {
                    UserName = _view.UserName,
                    Password = _view.Password
                };

                AuthenticationResultDto result = await _authenticationService.AuthenticateAsync(request);

                if (!result.IsAuthenticated || result.User is null)
                {
                    _view.ShowAuthenticationError(result.Message);
                    return;
                }

                _view.ShowAuthenticatedUser(result.User);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                _view.ShowAuthenticationError("No fue posible procesar la autenticación.");
            }
        }
    }
}

