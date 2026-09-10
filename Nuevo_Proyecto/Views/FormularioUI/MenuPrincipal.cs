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

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlBotones_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {

            // Limpiar el panel antes de cargar un nuevo formulario
            pnlPrincipal.Controls.Clear();

            // Crear instancia del formulario InicioMenu
            InicioMenu frm = new InicioMenu();

            // Configurar para que se comporte como control dentro del panel
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Agregar al panel
            pnlPrincipal.Controls.Add(frm);

            // Mostrar el formulario embebido
            frm.Show();
        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {

            // Limpiar el panel antes de cargar un nuevo formulario
            pnlPrincipal.Controls.Clear();

            // Crear instancia del formulario Facturacion
            Facturacion frm = new Facturacion();

            // Configurar para que se comporte como control dentro del panel
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Agregar al panel
            pnlPrincipal.Controls.Add(frm);

            // Mostrar el formulario embebido
            frm.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {

            // Limpiar el panel antes de cargar un nuevo formulario
            pnlPrincipal.Controls.Clear();

            // Crear instancia del formulario Clientescs
            Clientescs frm = new Clientescs();

            // Configurar para que se comporte como control dentro del panel
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Agregar al panel
            pnlPrincipal.Controls.Add(frm);

            // Mostrar el formulario embebido
            frm.Show();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {

            // Limpiar el panel antes de cargar un nuevo formulario
            pnlPrincipal.Controls.Clear();

            // Crear instancia del formulario Empleados
            Empleados frm = new Empleados();

            // Configurar para que se comporte como control dentro del panel
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Agregar al panel
            pnlPrincipal.Controls.Add(frm);

            // Mostrar el formulario embebido
            frm.Show();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {

            // Limpiar el panel antes de cargar un nuevo formulario
            pnlPrincipal.Controls.Clear();

            // Crear instancia del formulario Inventario
            Inventario frm = new Inventario();

            // Configurar para que se comporte como control dentro del panel
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Agregar al panel
            pnlPrincipal.Controls.Add(frm);

            // Mostrar el formulario embebido
            frm.Show();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {

            // Limpiar el panel antes de cargar un nuevo formulario
            pnlPrincipal.Controls.Clear();

            // Crear instancia del formulario Reportes
            Reportes frm = new Reportes();

            // Configurar para que se comporte como control dentro del panel
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Agregar al panel
            pnlPrincipal.Controls.Add(frm);

            // Mostrar el formulario embebido
            frm.Show();
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
                Application.Exit();
            }
        }
    }
}
