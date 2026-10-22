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

            // Mostrar indicador de modo offline si aplica
            if (ConnectionStateManager.UsandoSqlite)
            {
                Text = "Sistema de Comidería - ⚠️ MODO OFFLINE - Funcionamiento Limitado";
                lblUsuario.Text += " [OFFLINE]";
                lblUsuario.ForeColor = System.Drawing.Color.Red;
            }

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

            // Verificar y mostrar notificaciones de stock crítico SOLO si está online
            // En modo offline no intentar triggers del servidor
            if (!ConnectionStateManager.UsandoSqlite)
            {
                VerificarStockCritico();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Modo offline: se omitieron verificaciones de triggers de servidor");
            }

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
            foreach (Form actual in pnlPrincipal.Controls.OfType<Form>().ToList())
            {
                // Usar Close() para disparar el evento FormClosing correctamente
                // Esto permite que se muestre el mensaje de advertencia si hay cambios sin guardar
                actual.Close();

                // Solo remover si Close() no fue cancelado (FormClosing no canceló con e.Cancel = true)
                if (pnlPrincipal.Controls.Contains(actual))
                {
                    // Si Close() fue cancelado, el usuario eligió no cerrar el formulario
                    // Cancelar la carga del nuevo formulario
                    return;
                }

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

        /// <summary>
        /// Verifica si hay productos con stock crítico y muestra notificación en grande
        /// Utiliza el trigger trg_StockCritico que actualiza NotificacionesStockCritico automáticamente
        /// </summary>
        private void VerificarStockCritico()
        {
            try
            {
                // Doble verificación: no ejecutar si estamos en modo offline
                if (ConnectionStateManager.UsandoSqlite)
                {
                    System.Diagnostics.Debug.WriteLine("Saltar verificación de stock en modo offline");
                    return;
                }

                var repositorio = new NotificacionStockCriticoRepository();
                var notificaciones = repositorio.ObtenerNotificacionesCriticas();

                if (notificaciones.Count > 0)
                {
                    // Mostrar aviso estandarizado primero
                    MensajeHelper.Advertencia(
                        $"⚠️ En el inventario hay {notificaciones.Count} producto(s) a punto de vencerse o con stock bajo.\n\n" +
                        "Vamos a revisar cuáles son para tomar las acciones necesarias.",
                        "Alerta de Inventario Crítico"
                    );

                    // Mostrar formulario de alerta en grande con DataGrid
                    Notificacion_de_stocks alerta = new Notificacion_de_stocks();
                    alerta.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                // No detener la aplicación si hay error en verificación de stock
                System.Diagnostics.Debug.WriteLine($"Error verificando stock crítico: {ex.Message}");
            }
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
