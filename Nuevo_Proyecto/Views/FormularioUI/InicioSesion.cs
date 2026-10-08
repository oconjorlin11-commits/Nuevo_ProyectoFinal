using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;
using Nuevo_Proyecto.Views.FormularioUI;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class InicioSesion : Form, ILoginView
    {
        private readonly LoginPresenter _presenter;
        private bool _contrasenaVisible = false;

        public event EventHandler? IngresarClicked;

        public InicioSesion()
        {
            InitializeComponent();

            // Después de InitializeComponent(), ya existen los controles
            txtUsuario.PlaceholderText = "Usuario";
            txtContraseña.PlaceholderText = "Contraseña";
            txtContraseña.UseSystemPasswordChar = true;  // Ocultar por defecto

            AcceptButton = button2;   // Enter = Ingresar

            // Event handlers para los botones
            BtnOcultarContraseña.Click += BtnOcultarContraseña_Click;
            BtnResetearOMostrarConstraseña.Click += BtnResetearOMostrarConstraseña_Click;

            _presenter = new LoginPresenter(this);
        }

        // ---- ILoginView
        public string Usuario { get => txtUsuario.Text; set => txtUsuario.Text = value; }
        public string Contrasena { get => txtContraseña.Text; set => txtContraseña.Text = value; }

        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBox.Show(message, titulo, MessageBoxButtons.OK,
                            esError ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        public void ResetFields()
        {
            txtUsuario.Clear();
            txtContraseña.Clear();
            _contrasenaVisible = false;
            txtContraseña.UseSystemPasswordChar = true;
        }

        public void CompletarLogin()
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        // ---- Eventos
        private void button2_Click(object sender, EventArgs e) => IngresarClicked?.Invoke(this, EventArgs.Empty);

        private void pictureBox1_Click(object sender, EventArgs e) { }

        /// <summary>
        /// Toggle para mostrar/ocultar la contraseña
        /// </summary>
        private void BtnOcultarContraseña_Click(object sender, EventArgs e)
        {
            _contrasenaVisible = !_contrasenaVisible;
            txtContraseña.UseSystemPasswordChar = !_contrasenaVisible;
            BtnOcultarContraseña.Text = _contrasenaVisible ? "👁️" : "👁‍🗨️";
        }

        /// <summary>
        /// Abre el diálogo para recuperar contraseña (requiere credenciales admin)
        /// </summary>
        private void BtnResetearOMostrarConstraseña_Click(object sender, EventArgs e)
        {
            var dialogoRecuperacion = new Olvide_la_mi_usuario();
            if (dialogoRecuperacion.ShowDialog(this) == DialogResult.OK)
            {
                // El diálogo se encarga de mostrar la contraseña
            }
        }
    }
}
