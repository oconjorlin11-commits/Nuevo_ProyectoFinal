namespace Nuevo_Proyecto.Services.Helpers
{
    /// <summary>
    /// Proporciona mensajes de advertencia estandarizados para formularios de creación y edición
    /// </summary>
    public static class AdvertenciaFormHelper
    {
        /// <summary>
        /// Obtiene el mensaje de advertencia estandarizado según el tipo de formulario y acción
        /// </summary>
        /// <param name="tipoFormulario">Tipo: "Cliente", "Empleado", "Producto"</param>
        /// <param name="esEdicion">true para edición, false para creación</param>
        /// <returns>Mensaje de advertencia</returns>
        public static string ObtenerMensajeAdvertencia(string tipoFormulario, bool esEdicion = false)
        {
            if (esEdicion)
            {
                return tipoFormulario?.ToLower() switch
                {
                    "cliente" => "¿Está seguro de que no desea guardar los cambios?\n\nSi sale ahora, perderá todas las modificaciones realizadas al cliente.",
                    "empleado" => "¿Está seguro de que no desea guardar los cambios?\n\nSi sale ahora, perderá todas las modificaciones realizadas al empleado.",
                    "producto" => "¿Está seguro de que no desea guardar los cambios?\n\nSi sale ahora, perderá todas las modificaciones realizadas al producto.",
                    _ => "¿Está seguro de que no desea guardar los cambios?\n\nSe perderán todos los datos modificados."
                };
            }
            else
            {
                return tipoFormulario?.ToLower() switch
                {
                    "cliente" => "¿Está seguro de que no desea continuar?\n\nSi sale ahora, perderá toda la información que haya completado para agregar el nuevo cliente.",
                    "empleado" => "¿Está seguro de que no desea continuar?\n\nSi sale ahora, perderá toda la información que haya completado para agregar el nuevo empleado.",
                    "producto" => "¿Está seguro de que no desea continuar?\n\nSi sale ahora, perderá toda la información que haya completado para agregar el nuevo producto.",
                    _ => "¿Está seguro de que no desea continuar?\n\nSe perderán todos los datos ingresados."
                };
            }
        }

        /// <summary>
        /// Obtiene el título estandarizado del cuadro de diálogo de confirmación
        /// </summary>
        /// <param name="tipoFormulario">Tipo: "Cliente", "Empleado", "Producto"</param>
        /// <param name="esEdicion">true para edición, false para creación</param>
        /// <returns>Título del cuadro de diálogo</returns>
        public static string ObtenerTituloConfirmacion(string tipoFormulario, bool esEdicion = false)
        {
            if (esEdicion)
            {
                return tipoFormulario?.ToLower() switch
                {
                    "cliente" => "Confirmar salida - Editar Cliente",
                    "empleado" => "Confirmar salida - Editar Empleado",
                    "producto" => "Confirmar salida - Editar Producto",
                    _ => "Confirmar salida"
                };
            }
            else
            {
                return tipoFormulario?.ToLower() switch
                {
                    "cliente" => "Confirmar salida - Nuevo Cliente",
                    "empleado" => "Confirmar salida - Nuevo Empleado",
                    "producto" => "Confirmar salida - Nuevo Producto",
                    _ => "Confirmar salida"
                };
            }
        }
    }
}
