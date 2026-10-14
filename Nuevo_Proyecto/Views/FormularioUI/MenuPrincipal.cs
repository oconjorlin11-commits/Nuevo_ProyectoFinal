using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Views.FormularioUI;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class MenuPrincipal : Form
    {
        private string _seccionActual = ""; // Para rastrear qué sección se está accediendo

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
            _seccionActual = "Empleados";
            if (!VerificarAcceso())
                return;
            CargarFormulario(new Empleados());
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            _seccionActual = "Inventario";
            if (!VerificarAcceso())
                return;
            CargarFormulario(new Inventario());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            _seccionActual = "Reportes";
            if (!VerificarAcceso())
                return;
            CargarFormulario(new Reportes());
        }

        private bool VerificarAcceso()
        {
            // Si es administrador, permitir acceso directo
            if (SesionActual.EsAdministrador)
                return true;

            // Mostrar mensaje inicial explicando que necesita permiso de administrador
            DialogResult resultado = MessageBox.Show(
                "Para poder acceder a esta sección primero necesita tener un permiso de administrador.\n\n" +
                "¿Desea ingresar las credenciales de un administrador?",
                "Permiso Requerido",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (resultado != DialogResult.Yes)
                return false;

            // Mostrar dialogo de verificacion de usuario
            var verificacion = new Verificacion_de_usuario();
            if (verificacion.ShowDialog(this) == DialogResult.OK)
            {
                // 👉 Guardar el nombre del admin que fue verificado en la sesión
                SesionActual.UsuarioAutorizador = verificacion.AdminNombreVerificado;

                // Registrar la auditoría cuando se otorga acceso temporal
                var auditoriaService = new AuditoriaService();
                auditoriaService.RegistrarAuditoria(
                    seccion: _seccionActual,
                    accion: "ACCESO_TEMPORAL",
                    usuario: SesionActual.NombreUsuario ?? "Desconocido",
                    rolUsuario: SesionActual.Usuario?.Cargo ?? "Usuario",
                    descripcion: $"Acceso temporal a {_seccionActual} otorgado por usuario autorizado ({SesionActual.UsuarioAutorizador})"
                );

                // Mostrar mensaje de acceso temporal
                MessageBox.Show(
                    "Tienes solo permiso para ver esta sección temporalmente.\n\n" +
                    "Ya después se te quitará el acceso.",
                    "Acceso Temporal",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
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
