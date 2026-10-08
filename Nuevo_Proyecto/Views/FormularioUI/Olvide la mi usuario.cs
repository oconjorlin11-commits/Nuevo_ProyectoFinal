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
    public partial class Olvide_la_mi_usuario : Form
    {
        private readonly IAuthService _auth;

        public Olvide_la_mi_usuario()
        {
            InitializeComponent();
            _auth = new AuthService();

            // Cargar opciones de busqueda
            CBFormadeBusqueda.Items.AddRange(new object[]
            {
                "Cedula del Empleado",
                "Nombre de Usuario",
                "Contrasena",
                "Fecha de Ingreso (YYYY-MM-DD)"
            });
            CBFormadeBusqueda.SelectedIndex = 0;

            // Ocultar todos los labels y lineas inicialmente
            OcultarResultados();

            // Configurar eventos
            BtnVeerificar.Click += BtnVeerificar_Click;
            BtnCancelar.Click += BtnCancelar_Click;
        }

        private void OcultarResultados()
        {
            // Ocultar labels de verificacion
            LblVerificacion.Visible = false;

            // Ocultar labels de guia
            LblverNombre.Visible = false;
            LblverCargo.Visible = false;
            LblverUsuario.Visible = false;
            LblVerContraseña.Visible = false;

            // Ocultar labels de respuesta
            LblRespNombre.Visible = false;
            LblRespCargo.Visible = false;
            LblRespUsuario.Visible = false;
            LblRespContraseña.Visible = false;

            // Ocultar lineas
            LblLineas1.Visible = false;
            LblLineas2.Visible = false;
            LblLineas3.Visible = false;
            LblLineas4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
        }

        private void MostrarResultadosExitosos()
        {
            // Mostrar labels de verificacion en verde
            LblVerificacion.Visible = true;
            LblVerificacion.Text = "Verificacion Exitosa";
            LblVerificacion.ForeColor = Color.Green;

            // Mostrar labels de guia en verde
            LblverNombre.Visible = true;
            LblverNombre.ForeColor = Color.Green;
            LblverCargo.Visible = true;
            LblverCargo.ForeColor = Color.Green;
            LblverUsuario.Visible = true;
            LblverUsuario.ForeColor = Color.Green;
            LblVerContraseña.Visible = true;
            LblVerContraseña.ForeColor = Color.Green;

            // Mostrar labels de respuesta
            LblRespNombre.Visible = true;
            LblRespNombre.ForeColor = Color.Green;
            LblRespCargo.Visible = true;
            LblRespCargo.ForeColor = Color.Green;
            LblRespUsuario.Visible = true;
            LblRespUsuario.ForeColor = Color.Green;
            LblRespContraseña.Visible = true;
            LblRespContraseña.ForeColor = Color.Green;

            // Mostrar lineas en verde
            LblLineas1.Visible = true;
            LblLineas1.ForeColor = Color.Green;
            LblLineas2.Visible = true;
            LblLineas2.ForeColor = Color.Green;
            LblLineas3.Visible = true;
            LblLineas3.ForeColor = Color.Green;
            LblLineas4.Visible = true;
            LblLineas4.ForeColor = Color.Green;
            label5.Visible = true;
            label5.ForeColor = Color.Green;
            label6.Visible = true;
            label6.ForeColor = Color.Green;
            label7.Visible = true;
            label7.ForeColor = Color.Green;
            label8.Visible = true;
            label8.ForeColor = Color.Green;
            label9.Visible = true;
            label9.ForeColor = Color.Green;
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

        private void BtnVeerificar_Click(object? sender, EventArgs e)
        {
            BuscarUsuario();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void BuscarUsuario()
        {
            try
            {
                // Validar campos
                if (string.IsNullOrWhiteSpace(TxtUsuarioAdmin.Text))
                {
                    MessageBox.Show("Por favor, ingrese el Usuario Admin.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtUsuarioAdmin.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtContraseñaAdmin.Text))
                {
                    MessageBox.Show("Por favor, ingrese la Contrasena Admin.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtContraseñaAdmin.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtInformacionAIngresar.Text))
                {
                    MessageBox.Show("Por favor, ingrese el Valor a buscar.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtInformacionAIngresar.Focus();
                    return;
                }

                // Validar credenciales admin
                var resultadoAuth = _auth.Autenticar(TxtUsuarioAdmin.Text, TxtContraseñaAdmin.Text);

                if (!resultadoAuth.Exitoso || resultadoAuth.Sesion == null)
                {
                    MessageBox.Show("Las credenciales de administrador son invalidas.", "Autenticacion Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtUsuarioAdmin.Focus();
                    return;
                }

                // Verificar que es administrador
                if (!resultadoAuth.Sesion.Rol.Contains("Admin", StringComparison.OrdinalIgnoreCase) &&
                    !resultadoAuth.Sesion.Cargo.Contains("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Solo los administradores pueden usar esta funcion.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Buscar usuario segun el tipo de busqueda
                string tipoBusqueda = CBFormadeBusqueda.SelectedItem?.ToString() ?? "Cedula del Empleado";
                string valorBusqueda = TxtInformacionAIngresar.Text;

                using var db = new Dev_ComideriaDbContext();
                UsuarioInfo? usuario = RealizarBusqueda(db, tipoBusqueda, valorBusqueda);

                if (usuario == null)
                {
                    MostrarErrorBusqueda();
                    return;
                }

                // Mostrar datos en los labels
                LblRespNombre.Text = usuario.NombreEmpleado ?? "N/A";
                LblRespCargo.Text = usuario.Cargo ?? "N/A";
                LblRespUsuario.Text = usuario.NombreUsuario ?? "N/A";
                LblRespContraseña.Text = usuario.Contraseña ?? "N/A";

                // Mostrar resultados en verde
                MostrarResultadosExitosos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OcultarResultados();
            }
        }

        private UsuarioInfo? RealizarBusqueda(Dev_ComideriaDbContext db, string tipoBusqueda, string valorBusqueda)
        {
            switch (tipoBusqueda)
            {
                case "Cedula del Empleado":
                    return db.Database.SqlQuery<UsuarioInfo>($@"
                        SELECT u.NombreUsuario, 
                               u.[Contraseña], 
                               u.RolSistema,
                               e.Nombre AS NombreEmpleado,
                               e.Cedula,
                               e.Cargo
                        FROM Usuarios u
                        INNER JOIN Empleados e ON u.EmpleadoID = e.EmpleadoID
                        WHERE e.Cedula = {valorBusqueda}
                          AND u.Activo = 1
                          AND e.Activo = 1")
                        .AsEnumerable()
                        .FirstOrDefault();

                case "Nombre de Usuario":
                    return db.Database.SqlQuery<UsuarioInfo>($@"
                        SELECT u.NombreUsuario, 
                               u.[Contraseña], 
                               u.RolSistema,
                               e.Nombre AS NombreEmpleado,
                               e.Cedula,
                               e.Cargo
                        FROM Usuarios u
                        INNER JOIN Empleados e ON u.EmpleadoID = e.EmpleadoID
                        WHERE u.NombreUsuario = {valorBusqueda}
                          AND u.Activo = 1
                          AND e.Activo = 1")
                        .AsEnumerable()
                        .FirstOrDefault();

                case "Contrasena":
                    return db.Database.SqlQuery<UsuarioInfo>($@"
                        SELECT u.NombreUsuario, 
                               u.[Contraseña], 
                               u.RolSistema,
                               e.Nombre AS NombreEmpleado,
                               e.Cedula,
                               e.Cargo
                        FROM Usuarios u
                        INNER JOIN Empleados e ON u.EmpleadoID = e.EmpleadoID
                        WHERE u.[Contraseña] = {valorBusqueda}
                          AND u.Activo = 1
                          AND e.Activo = 1")
                        .AsEnumerable()
                        .FirstOrDefault();

                case "Fecha de Ingreso (YYYY-MM-DD)":
                    return db.Database.SqlQuery<UsuarioInfo>($@"
                        SELECT u.NombreUsuario, 
                               u.[Contraseña], 
                               u.RolSistema,
                               e.Nombre AS NombreEmpleado,
                               e.Cedula,
                               e.Cargo
                        FROM Usuarios u
                        INNER JOIN Empleados e ON u.EmpleadoID = e.EmpleadoID
                        WHERE CONVERT(DATE, e.FechaIngreso) = CONVERT(DATE, {valorBusqueda})
                          AND u.Activo = 1
                          AND e.Activo = 1")
                        .AsEnumerable()
                        .FirstOrDefault();

                default:
                    return null;
            }
        }

        private class UsuarioInfo
        {
            public string NombreUsuario { get; set; } = string.Empty;
            public string? Contraseña { get; set; }
            public string? RolSistema { get; set; }
            public string? NombreEmpleado { get; set; }
            public string? Cedula { get; set; }
            public string? Cargo { get; set; }
        }

        private void LblVerificacion_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
