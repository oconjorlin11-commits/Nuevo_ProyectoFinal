using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class LoginPresenter
    {
        private const int MaxIntentos = 5;

        private readonly ILoginView _view;
        private readonly IAuthService _auth;
        private int _intentosFallidos;

        public LoginPresenter(ILoginView view, IAuthService? auth = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _auth = auth ?? new AuthService();
            _view.IngresarClicked += OnIngresarClicked;
        }

        private void OnIngresarClicked(object? sender, EventArgs e)
        {
            if (_intentosFallidos >= MaxIntentos)
            {
                _view.showMessage("Demasiados intentos fallidos. Cierre la aplicación e inténtelo de nuevo.", "Acceso bloqueado", true);
                return;
            }

            var resultado = _auth.Autenticar(_view.Usuario, _view.Contrasena);

            if (!resultado.Exitoso || resultado.Sesion == null)
            {
                _intentosFallidos++;
                _view.showMessage(resultado.Mensaje, "Acceso denegado", true);
                _view.Contrasena = string.Empty;
                return;
            }

            SesionActual.Iniciar(resultado.Sesion);
            _view.CompletarLogin();
        }
    }
}
