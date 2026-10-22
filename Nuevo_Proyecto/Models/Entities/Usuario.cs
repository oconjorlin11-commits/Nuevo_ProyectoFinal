namespace Nuevo_Proyecto.Models.Entities
{
    /// <summary>
    /// Entidad Usuario - Mapea la tabla Usuarios de la base de datos
    /// Utilizada para autenticación en SQL Server y offline en SQLite
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Nombre de usuario (único) - usado para login
        /// </summary>
        public string NombreUsuario { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña en texto plano (en producción debería ser hasheada)
        /// </summary>
        public string? Contrasena { get; set; }

        /// <summary>
        /// Rol del sistema (Admin, Usuario, etc)
        /// </summary>
        public string? RolSistema { get; set; }

        /// <summary>
        /// ID del empleado asociado
        /// </summary>
        public int EmpleadoId { get; set; }

        /// <summary>
        /// Indica si el usuario está activo
        /// </summary>
        public bool Activo { get; set; } = true;

        /// <summary>
        /// Última fecha/hora de acceso
        /// </summary>
        public DateTime? UltimoAcceso { get; set; }

        /// <summary>
        /// Hash seguro de la contraseña (para modo offline)
        /// </summary>
        public string? ContrasenaHash { get; set; }

        // Navegación
        public virtual Empleado? Empleado { get; set; }
    }
}
