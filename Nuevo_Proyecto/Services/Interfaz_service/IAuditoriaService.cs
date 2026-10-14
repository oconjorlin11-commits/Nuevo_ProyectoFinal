namespace Nuevo_Proyecto.Services.Interfaz_service
{
    public interface IAuditoriaService
    {
        /// <summary>
        /// Registra un evento en la tabla de auditoría
        /// </summary>
        bool RegistrarAuditoria(
            string seccion,
            string accion,
            string usuario,
            string rolUsuario,
            string? usuarioAutorizador = null,
            int? registroID = null,
            string? detalle = null,
            string? ipEquipo = null,
            string? descripcion = null
        );
    }
}
