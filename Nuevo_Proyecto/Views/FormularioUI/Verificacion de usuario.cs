using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Data;

namespace Nuevo_Proyecto.Views.FormularioUI
{
    public partial class Verificacion_de_usuario : Form
    {
        private readonly IAuthService _auth;
        public string AdminNombreVerificado { get; private set; } = string.Empty;

        public Verificacion_de_usuario()
        {
            InitializeComponent();
            _auth = new AuthService();

            // Ocultar todos los labels y lineas inicialmente
            OcultarResultados();

            // Configurar eventos de botones
            var btnVerificar = new Button
            {
                Text = "Verificar",
                Left = 357,
                Top = 250,
                Width = 180,
                Height = 50,
                BackColor = Color.FromArgb(153, 40, 35),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                Left = 587,
                Top = 250,
                Width = 180,
                Height = 50,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            btnVerificar.Click += BtnVerificar_Click;
            btnCancelar.Click += BtnCancelar_Click;

            this.Controls.Add(btnVerificar);
            this.Controls.Add(btnCancelar);
        }

        private void OcultarResultados()
        {
            // Ocultar label de verificacion
            LblVerificacion.Visible = false;

            // Ocultar label de guia
            LblverUsuario.Visible = false;

            // Ocultar label de respuesta
            LblRespUsuario.Visible = false;

            // Ocultar lineas
            LblLineas1.Visible = false;
            LblLineas3.Visible = false;
            LblLineas4.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            label6.Visible = false;
        }

        private void MostrarResultadosExitosos()
        {
            // Mostrar label de verificacion en verde
            LblVerificacion.Visible = true;
            LblVerificacion.Text = "Verificacion Exitosa";
            LblVerificacion.ForeColor = Color.Green;

            // Mostrar label de guia en verde
            LblverUsuario.Visible = true;
            LblverUsuario.ForeColor = Color.Green;

            // Mostrar label de respuesta
            LblRespUsuario.Visible = true;
            LblRespUsuario.ForeColor = Color.Green;

            // Mostrar lineas en verde
            LblLineas1.Visible = true;
            LblLineas1.ForeColor = Color.Green;
            LblLineas3.Visible = true;
            LblLineas3.ForeColor = Color.Green;
            LblLineas4.Visible = true;
            LblLineas4.ForeColor = Color.Green;
            label2.Visible = true;
            label2.ForeColor = Color.Green;
            label4.Visible = true;
            label4.ForeColor = Color.Green;
            label6.Visible = true;
            label6.ForeColor = Color.Green;
        }

        private void MostrarErrorBusqueda()
        {
            // Ocultar todo excepto el label de verificacion
            OcultarResultados();

            // Mostrar solo el label de verificacion en rojo
            LblVerificacion.Visible = true;
            LblVerificacion.Text = "No se encontro el usuario";
            LblVerificacion.ForeColor = Color.Red;
        }

        private async void BtnVerificar_Click(object? sender, EventArgs e)
        {
            await BuscarUsuarioAsync();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private async Task BuscarUsuarioAsync()
        {
            try
            {
                // Validar campos
                if (string.IsNullOrWhiteSpace(TxtUsuarioEspecial.Text))
                {
                    MessageBox.Show("Por favor, ingrese el Usuario.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtUsuarioEspecial.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtContraseñaEspecial.Text))
                {
                    MessageBox.Show("Por favor, ingrese la Contrasena.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtContraseñaEspecial.Focus();
                    return;
                }

                // Validar credenciales del usuario
                var resultadoAuth = _auth.Autenticar(TxtUsuarioEspecial.Text, TxtContraseñaEspecial.Text);

                if (!resultadoAuth.Exitoso || resultadoAuth.Sesion == null)
                {
                    MostrarErrorBusqueda();
                    return;
                }

                // Usuario encontrado y autenticado correctamente
                LblRespUsuario.Text = resultadoAuth.Sesion.NombreUsuario ?? "N/A";

                // 👉 Guardar el nombre del admin verificado
                AdminNombreVerificado = resultadoAuth.Sesion.NombreEmpleado ?? resultadoAuth.Sesion.NombreUsuario ?? "Desconocido";

                // Mostrar resultados en verde
                MostrarResultadosExitosos();

                // Cerrar el diálogo con resultado OK después de un breve tiempo para que el usuario vea el resultado exitoso
                await Task.Delay(1500);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OcultarResultados();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void GbVerificacion_Enter(object sender, EventArgs e)
        {

        }

        private void Verificacion_de_usuario_Load(object sender, EventArgs e)
        {

        }
    }
}
