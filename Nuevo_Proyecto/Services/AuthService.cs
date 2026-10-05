using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Services
{
    /// <summary>
    /// Valida usuarios contra la tabla Usuarios que ya existe en la base (NombreUsuario, [Contraseña],
    /// RolSistema, CodigoEmpleado, Activo) + las columnas nuevas ContrasenaHash y UltimoAcceso
    /// creadas por Database/01_seguridad_y_auditoria.sql.
    ///
    /// Migración automática: si el usuario todavía tiene la contraseña en texto plano y entra bien,
    /// se guarda su hash y se borra el texto plano.
    /// </summary>
    public class AuthService : IAuthService
    {
        private const string MensajeGenerico = "Usuario o contraseña incorrectos.";

        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;

        public AuthService(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
        }

        // Fila plana para la consulta SQL (no es una entidad del modelo)
        private sealed class FilaLogin
        {
            public string NombreUsuario { get; set; } = string.Empty;
            public string? ContrasenaLegacy { get; set; }
            public string? ContrasenaHash { get; set; }
            public string? RolSistema { get; set; }
            public int EmpleadoId { get; set; }
            public string Codigo { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
            public string? Cargo { get; set; }
        }

        public ResultadoLoginDto Autenticar(string usuario, string contrasena)
        {
            usuario = (usuario ?? string.Empty).Trim();
            contrasena = (contrasena ?? string.Empty).Trim();

            if (usuario.Length == 0 || contrasena.Length == 0)
                return Fallo("Ingrese su usuario y contraseña.");

            try
            {
                using var db = _factory.CreateDbContext();

                var fila = db.Database.SqlQuery<FilaLogin>($@"
                    SELECT u.NombreUsuario              AS NombreUsuario,
                           u.[Contraseña]               AS ContrasenaLegacy,
                           u.ContrasenaHash             AS ContrasenaHash,
                           u.RolSistema                 AS RolSistema,
                           e.EmpleadoID                 AS EmpleadoId,
                           e.Codigo                     AS Codigo,
                           e.Nombre                     AS Nombre,
                           e.Cargo                      AS Cargo
                    FROM Usuarios u
                    INNER JOIN Empleados e ON u.CodigoEmpleado = e.Codigo
                    WHERE u.NombreUsuario = {usuario}
                      AND u.Activo = 1
                      AND ISNULL(e.Activo, 1) = 1")
                    .AsEnumerable()
                    .FirstOrDefault();

                if (fila == null) return Fallo(MensajeGenerico);

                bool valido;
                bool migrar = false;

                if (!string.IsNullOrEmpty(fila.ContrasenaHash))
                {
                    valido = PasswordHasher.Verificar(contrasena, fila.ContrasenaHash);
                }
                else
                {
                    // Cuenta aún sin migrar: compara con el texto plano y la migra al entrar
                    valido = !string.IsNullOrEmpty(fila.ContrasenaLegacy)
                             && string.Equals(fila.ContrasenaLegacy.Trim(), contrasena, StringComparison.Ordinal);
                    migrar = valido;
                }

                if (!valido) return Fallo(MensajeGenerico);

                if (migrar)
                {
                    var hash = PasswordHasher.Hash(contrasena);
                    db.Database.ExecuteSqlInterpolated(
                        $"UPDATE Usuarios SET ContrasenaHash = {hash}, [Contraseña] = 'MIGRADO' WHERE NombreUsuario = {fila.NombreUsuario}");
                }

                db.Database.ExecuteSqlInterpolated(
                    $"UPDATE Usuarios SET UltimoAcceso = SYSDATETIME() WHERE NombreUsuario = {fila.NombreUsuario}");

                return new ResultadoLoginDto
                {
                    Exitoso = true,
                    Mensaje = "Bienvenido",
                    Sesion = new SesionUsuarioDto
                    {
                        EmpleadoId = fila.EmpleadoId,
                        CodigoEmpleado = fila.Codigo,
                        NombreEmpleado = fila.Nombre,
                        NombreUsuario = fila.NombreUsuario,
                        Rol = fila.RolSistema ?? string.Empty,
                        Cargo = fila.Cargo ?? string.Empty
                    }
                };
            }
            catch (Exception ex)
            {
                return Fallo("No se pudo validar el acceso. Verifique la conexión y que haya ejecutado " +
                             "Database/01_seguridad_y_auditoria.sql." + Environment.NewLine + ex.Message);
            }
        }

        private static ResultadoLoginDto Fallo(string mensaje) => new() { Exitoso = false, Mensaje = mensaje };
    }
}
