using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class InicioSesion : Form
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT u.RolSistema, e.Nombre, e.Cargo
   FROM Usuarios u
   INNER JOIN Empleados e ON u.CodigoEmpleado = e.Codigo
   WHERE u.NombreUsuario = @usuario 
     AND u.Contraseña = @password 
     AND u.Activo = 1";

            var parametros = new[]
            {
                new SqlParameter("@usuario", txtUsuario.Text.Trim()),
                new SqlParameter("@password", txtContraseña.Text.Trim())
            };

            DataTable dt = Nuevo_Proyecto.Presenters.DbExecutor.ExecuteQuery(sql, parametros);

            if (dt.Rows.Count > 0)
            {
                string rolSistema = dt.Rows[0]["RolSistema"].ToString();
                string nombreEmpleado = dt.Rows[0]["Nombre"].ToString();
                string cargoLaboral = dt.Rows[0]["Cargo"].ToString();

                MessageBox.Show($"Bienvenido {rolSistema} {nombreEmpleado} ({cargoLaboral})");

                InicioMenu frmMenu = new InicioMenu();
                frmMenu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }

        }
    }
}
