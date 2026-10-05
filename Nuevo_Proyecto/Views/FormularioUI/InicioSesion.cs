using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class InicioSesion : Form, ILoginView
    {
        private readonly LoginPresenter _presenter;

        public event EventHandler? IngresarClicked;

        public InicioSesion()
        {
            InitializeComponent();

            // Los textos "Usuarios" / "Contraseña" eran valores reales que había que borrar a mano
            txtUsuario.Text = string.Empty;
            txtUsuario.PlaceholderText = "Usuario";
            txtContraseña.Text = string.Empty;
            txtContraseña.PlaceholderText = "Contraseña";
            txtContraseña.UseSystemPasswordChar = true;

            AcceptButton = button2;   // Enter = Ingresar

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
        }

        public void CompletarLogin()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        // ---- eventos del diseñador
        private void button2_Click(object sender, EventArgs e) => IngresarClicked?.Invoke(this, EventArgs.Empty);

        private void pictureBox1_Click(object sender, EventArgs e) { }
    }
}
