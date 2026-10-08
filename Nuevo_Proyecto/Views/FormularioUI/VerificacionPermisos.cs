using System;
using System.Windows.Forms;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Views.FormularioUI
{
    public partial class VerificacionPermisos : Form
    {
        private readonly IAuthService _auth;
        private string _usuarioActual;

        public VerificacionPermisos(string usuarioActual)
        {
            _usuarioActual = usuarioActual;
            _auth = new AuthService();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Verificacion de Permisos";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(650, 400);
            this.BackColor = Color.White;

            // Etiqueta de instruccion
            var lblInstruccion = new Label
            {
                Text = $"Usuario actual: {_usuarioActual}\n\nPara acceder a esta seccion, por favor ingrese las credenciales de un administrador.",
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 100,
                Padding = new Padding(40),
                TextAlign = ContentAlignment.TopLeft,
                Font = new Font("Segoe UI", 12, FontStyle.Regular)
            };

            // Usuario Admin
            var lblUsuario = new Label
            {
                Text = "Usuario Admin:",
                Left = 40,
                Top = 120,
                Width = 200,
                Height = 40,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            var txtUsuarioAdmin = new TextBox
            {
                Name = "TxtUsuarioAdmin",
                Left = 260,
                Top = 120,
                Width = 320,
                Height = 35,
                Font = new Font("Segoe UI", 11)
            };

            // Contrasena Admin
            var lblContrasena = new Label
            {
                Text = "Contrasena Admin:",
                Left = 40,
                Top = 170,
                Width = 200,
                Height = 40,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            var txtContrasenaAdmin = new TextBox
            {
                Name = "TxtContrasenaAdmin",
                Left = 260,
                Top = 170,
                Width = 320,
                Height = 35,
                UseSystemPasswordChar = true,
                Font = new Font("Segoe UI", 11)
            };

            // Botones
            var btnAceptar = new Button
            {
                Text = "Aceptar",
                Left = 260,
                Top = 230,
                Width = 140,
                Height = 45,
                BackColor = Color.FromArgb(153, 40, 35),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                Left = 440,
                Top = 230,
                Width = 140,
                Height = 45,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            btnAceptar.Click += (sender, e) => VerificarCredenciales(txtUsuarioAdmin.Text, txtContrasenaAdmin.Text);
            btnCancelar.Click += (sender, e) => 
            { 
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.Add(lblInstruccion);
            this.Controls.Add(lblUsuario);
            this.Controls.Add(txtUsuarioAdmin);
            this.Controls.Add(lblContrasena);
            this.Controls.Add(txtContrasenaAdmin);
            this.Controls.Add(btnAceptar);
            this.Controls.Add(btnCancelar);

            // Configurar Enter en textbox como boton aceptar
            this.AcceptButton = btnAceptar;
            this.CancelButton = btnCancelar;
        }

        private void VerificarCredenciales(string usuarioAdmin, string contrasenaAdmin)
        {
            try
            {
                // Validar campos
                if (string.IsNullOrWhiteSpace(usuarioAdmin))
                {
                    MessageBox.Show("Por favor, ingrese el usuario del administrador.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(contrasenaAdmin))
                {
                    MessageBox.Show("Por favor, ingrese la contrasena del administrador.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Autenticar credenciales admin
                var resultadoAuth = _auth.Autenticar(usuarioAdmin, contrasenaAdmin);

                if (!resultadoAuth.Exitoso || resultadoAuth.Sesion == null)
                {
                    MessageBox.Show("Las credenciales proporcionadas no son validas.", "Autenticacion Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar que es administrador
                if (!resultadoAuth.Sesion.Rol.Contains("Admin", StringComparison.OrdinalIgnoreCase) &&
                    !resultadoAuth.Sesion.Cargo.Contains("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("El usuario proporcionado no tiene permisos de administrador.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Credenciales validas y es administrador
                MessageBox.Show($"Bienvenido {_usuarioActual}. Acceso permitido.", "Verificacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
