using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Views.FormularioUI;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

      


        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            // Datos de la sesión y permisos según el rol
            lblUsuario.Text = SesionActual.NombreEmpleado;

            // Mostrar saludo de bienvenida
            string rol = SesionActual.EsAdministrador ? "Administrador" : SesionActual.Usuario?.Cargo ?? "Usuario";
            MessageBox.Show(
                $"¡Bienvenido {SesionActual.NombreEmpleado}!\n\nRol: {rol}",
                "Inicio de Sesión Exitoso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Desbloquear acceso para todos los usuarios a Empleados y Reportes
            btnEmpleados.Enabled = true; // SesionActual.EsAdministrador;
            btnReportes.Enabled = true; // SesionActual.EsAdministrador;

            // Pantalla de inicio al abrir
            CargarFormulario(new InicioMenu());
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlBotones_Paint(object sender, PaintEventArgs e)
        {

        }

        // Un solo lugar para mostrar un formulario dentro del panel principal
        // (antes este bloque estaba copiado 6 veces).
        private void CargarFormulario(Form formulario)
        {
            foreach (Control actual in pnlPrincipal.Controls.OfType<Form>().ToList())
            {
                pnlPrincipal.Controls.Remove(actual);
                actual.Dispose();
            }

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            pnlPrincipal.Controls.Add(formulario);
            formulario.Show();
        }

        private void btnInicio_Click(object sender, EventArgs e) => CargarFormulario(new InicioMenu());

        private void btnFacturacion_Click(object sender, EventArgs e) => CargarFormulario(new Facturacion());

        private void btnClientes_Click(object sender, EventArgs e) => CargarFormulario(new Clientescs());

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            if (!VerificarAcceso())
                return;
            CargarFormulario(new Empleados());
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            if (!VerificarAcceso())
                return;
            CargarFormulario(new Inventario());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (!VerificarAcceso())
                return;
            CargarFormulario(new Reportes());
        }

        private bool VerificarAcceso()
        {
            // Si es administrador, permitir acceso directo
            if (SesionActual.EsAdministrador)
                return true;

            // Mostrar dialogo de verificacion de permisos
            var verificacion = new VerificacionPermisos(SesionActual.NombreUsuario);
            if (verificacion.ShowDialog(this) == DialogResult.OK)
            {
                return true;
            }

            return false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
          "¿Desea salir del sistema?",
           "Confirmar salida",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question
  );

            if (result == DialogResult.Yes)
            {
                SesionActual.Cerrar();
                Application.Exit();
            }
        }
    }
}
