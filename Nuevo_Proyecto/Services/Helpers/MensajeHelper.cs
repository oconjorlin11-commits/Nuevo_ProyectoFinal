using System;
using System.Windows.Forms;

namespace Nuevo_Proyecto.Services.Helpers
{
    /// <summary>
    /// Clase para estandarizar mensajes en toda la aplicación.
    /// Proporciona métodos para mostrar errores, advertencias, éxito e información de forma consistente.
    /// </summary>
    public static class MensajeHelper
    {
        /// <summary>
        /// Muestra un mensaje de error de validación.
        /// Usado cuando falta un campo obligatorio o formato inválido.
        /// </summary>
        public static void Error(string mensaje, string titulo = "Error de validación")
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        /// <summary>
        /// Muestra un mensaje de error del sistema.
        /// Usado cuando ocurre una excepción o error no controlado.
        /// </summary>
        public static void ErrorSistema(string mensaje, string titulo = "Error del sistema")
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        /// <summary>
        /// Muestra un mensaje de advertencia.
        /// Usado para alertar sobre situaciones que no son errores pero requieren atención.
        /// </summary>
        public static void Advertencia(string mensaje, string titulo = "Advertencia")
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Muestra un mensaje de éxito.
        /// Usado cuando una operación se completó correctamente.
        /// </summary>
        public static void Exito(string mensaje, string titulo = "Operación exitosa")
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Muestra un mensaje de información.
        /// Usado para información general que el usuario debe conocer.
        /// </summary>
        public static void Informacion(string mensaje, string titulo = "Información")
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Muestra una confirmación con opciones Sí/No.
        /// Retorna True si el usuario hace clic en Sí, False si hace clic en No.
        /// </summary>
        public static bool Confirmacion(string mensaje, string titulo = "Confirmación")
        {
            DialogResult resultado = MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            return resultado == DialogResult.Yes;
        }

        /// <summary>
        /// Muestra una confirmación con opciones Sí/No/Cancelar.
        /// Retorna: DialogResult.Yes (Sí), DialogResult.No (No), DialogResult.Cancel (Cancelar).
        /// </summary>
        public static DialogResult ConfirmacionAvanzada(string mensaje, string titulo = "Confirmación")
        {
            return MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );
        }

        // ======================================================================
        // MENSAJES PREDEFINIDOS ESPECÍFICOS POR ENTIDAD
        // ======================================================================

        /// <summary>
        /// Mensajes para validación de campos obligatorios.
        /// </summary>
        public static class Validacion
        {
            public static void CampoObligatorio(string nombreCampo)
            {
                Error($"El campo {nombreCampo} es obligatorio.", "Campo incompleto");
            }

            public static void FormatoInvalido(string nombreCampo, string formato)
            {
                Error($"El campo {nombreCampo} tiene formato inválido.\nFormato esperado: {formato}", "Formato incorrecto");
            }

            public static void RangoInvalido(string nombreCampo, string rango)
            {
                Error($"El campo {nombreCampo} está fuera del rango válido.\n{rango}", "Valor inválido");
            }

            public static void LongitudInvalida(string nombreCampo, int minimo, int maximo)
            {
                Error($"El campo {nombreCampo} debe tener entre {minimo} y {maximo} caracteres.", "Longitud inválida");
            }

            public static void DuplicadoExistente(string entidad, string valor)
            {
                Error($"El {entidad} '{valor}' ya existe en el sistema.", "Registro duplicado");
            }
        }

        /// <summary>
        /// Mensajes para operaciones CRUD (Crear, Leer, Actualizar, Eliminar).
        /// </summary>
        public static class Operacion
        {
            public static void Guardado(string entidad = "registro")
            {
                Exito($"El {entidad} se guardó correctamente.", "Guardado exitoso");
            }

            public static void Actualizado(string entidad = "registro")
            {
                Exito($"El {entidad} se actualizó correctamente.", "Actualización exitosa");
            }

            public static void Eliminado(string entidad = "registro")
            {
                Exito($"El {entidad} se eliminó correctamente.", "Eliminación exitosa");
            }

            public static void Reactivado(string entidad = "registro")
            {
                Exito($"El {entidad} se reactivó correctamente.", "Reactivación exitosa");
            }

            public static bool ConfirmarEliminacion(string entidad, string nombre = "")
            {
                string mensaje = string.IsNullOrEmpty(nombre)
                    ? $"¿Está seguro de que desea eliminar este {entidad}?"
                    : $"¿Está seguro de que desea eliminar el {entidad} '{nombre}'?";

                return Confirmacion(mensaje, "Confirmar eliminación");
            }

            public static bool ConfirmarActualizacion(string entidad = "registro")
            {
                return Confirmacion($"¿Desea guardar los cambios en el {entidad}?", "Confirmar cambios");
            }
        }

        /// <summary>
        /// Mensajes para operaciones de búsqueda y acceso.
        /// </summary>
        public static class Busqueda
        {
            public static void NoEncontrado(string entidad, string termino)
            {
                Advertencia($"No se encontraron {entidad}s que coincidan con '{termino}'.", "Búsqueda sin resultados");
            }

            public static void SinDatos(string seccion)
            {
                Advertencia($"No hay datos disponibles en {seccion}.", "Sin datos");
            }
        }

        /// <summary>
        /// Mensajes para permisos y autorización.
        /// </summary>
        public static class Permiso
        {
            public static void AccesoDenegado(string accion = "realizar esta acción")
            {
                Error($"No tiene permisos para {accion}. Se requiere autorización de un administrador.", "Acceso denegado");
            }

            public static void RolInsuficiente(string rolRequerido)
            {
                Error($"Esta operación requiere el rol de {rolRequerido}.", "Permiso insuficiente");
            }
        }

        /// <summary>
        /// Mensajes para operaciones de motivos (actualización, eliminación, baja).
        /// </summary>
        public static class Motivo
        {
            public static void MotivoRequerido(string tipoOperacion = "esta operación")
            {
                Error($"Debe proporcionar un motivo válido para {tipoOperacion}.", "Motivo requerido");
            }

            public static void MotivoInvalido(string razon = "")
            {
                string mensaje = string.IsNullOrEmpty(razon)
                    ? "El motivo ingresado no es válido."
                    : $"El motivo ingresado no es válido.\n{razon}";

                Error(mensaje, "Motivo inválido");
            }
        }

        /// <summary>
        /// Mensajes para operaciones de importación/exportación.
        /// </summary>
        public static class Transferencia
        {
            public static void ExportadoExitosamente(string destino)
            {
                Exito($"Los datos se exportaron correctamente a:\n{destino}", "Exportación exitosa");
            }

            public static void ImportadoExitosamente(int cantidad)
            {
                Exito($"Se importaron {cantidad} registro(s) correctamente.", "Importación exitosa");
            }

            public static void ArchivoEnUso(string nombreArchivo)
            {
                Advertencia($"El archivo '{nombreArchivo}' está en uso por otra aplicación.\nCiérrelo e intente nuevamente.", "Archivo en uso");
            }
        }

        /// <summary>
        /// Mensajes específicos para la entidad Empleado.
        /// </summary>
        public static class Empleado
        {
            public static void CamposObligatorios()
            {
                Error("El código, el nombre y la cédula son obligatorios.", "Campos incompletos");
            }

            public static void SalarioInvalido()
            {
                Error("El salario debe ser mayor a 1.", "Valor inválido");
            }

            public static void LongitudCamposExcedida()
            {
                Error("Algún campo excede el largo permitido (código 10, nombre 100, cédula 20, teléfono 20, cargo 50).", "Longitud excedida");
            }

            public static void NombreInvalido()
            {
                Error("El nombre debe contener al menos dos palabras (ej: Jose Alejandro Ordoñez Medina).", "Formato incorrecto");
            }

            public static void CedulaInvalida()
            {
                Error("El formato de cédula es incorrecto. Use el formato: xxx-xxxxxx-xxxxA (ej: 561-021007-1000A).", "Formato incorrecto");
            }

            public static void TelefonoInvalido()
            {
                Error("El formato de teléfono es incorrecto. Use el formato: xxxx-xxxx (ej: 7635-7836).", "Formato incorrecto");
            }

            public static void CodigoDuplicado(string codigo)
            {
                Error($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Código duplicado");
            }

            public static void CedulaDuplicada(string cedula)
            {
                Error($"La cédula '{cedula}' ya está registrada en el sistema. No se puede ingresar cédulas duplicadas.", "Cédula duplicada");
            }

            public static void TelefonoDuplicado(string telefono)
            {
                Error($"El teléfono '{telefono}' ya está en uso por otro empleado. No se puede ingresar teléfonos duplicados.", "Teléfono duplicado");
            }

            public static void GuardadoExitoso()
            {
                Exito("Empleado y usuario guardados exitosamente.", "Guardado exitoso");
            }

            public static void UsuarioNoCreado()
            {
                Advertencia("Empleado guardado, pero hubo un error al crear el usuario del sistema.", "Aviso");
            }

            public static void ActualizadoExitoso()
            {
                Exito("Empleado actualizado correctamente.", "Actualización exitosa");
            }

            public static void ReactivadoExitoso()
            {
                Exito("Empleado reactivado correctamente.", "Reactivación exitosa");
            }

            public static void EliminandoExitoso()
            {
                Exito("Empleado eliminado (inactivado) correctamente.", "Eliminación exitosa");
            }

            public static void SeleccionarPrimero()
            {
                Advertencia("Debe seleccionar un empleado primero.", "Selección requerida");
            }

            public static void SinCambios()
            {
                Advertencia("No se ha hecho ningún cambio. Debe modificar al menos un campo antes de editar.", "Sin cambios");
            }

            public static void NoSeActualizo()
            {
                Error("No se pudo actualizar el empleado.", "Error en operación");
            }

            public static void NoSeReactivo()
            {
                Error("No se pudo reactivar el empleado.", "Error en operación");
            }

            public static void NoSeElimino()
            {
                Error("No se pudo eliminar el empleado.", "Error en operación");
            }
        }

        /// <summary>
        /// Mensajes específicos para la entidad Cliente.
        /// </summary>
        public static class Cliente
        {
            public static void CodigoRequerido()
            {
                Error("El código debe completarse.", "Campo requerido");
            }

            public static void NombreDireccionObligatorios()
            {
                Error("El nombre y la dirección son obligatorios.", "Campos incompletos");
            }

            public static void LongitudCamposExcedida()
            {
                Error("Algún campo excede el largo permitido.", "Longitud excedida");
            }

            public static void CodigoDuplicado(string codigo)
            {
                Error($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Código duplicado");
            }

            public static void GuardadoExitoso()
            {
                Exito("Cliente guardado exitosamente.", "Guardado exitoso");
            }

            public static void ActualizadoExitoso()
            {
                Exito("Cliente actualizado correctamente.", "Actualización exitosa");
            }

            public static void ReactivadoExitoso()
            {
                Exito("Cliente reactivado correctamente.", "Reactivación exitosa");
            }

            public static void EliminandoExitoso()
            {
                Exito("Cliente eliminado (inactivado) correctamente.", "Eliminación exitosa");
            }

            public static void SeleccionarPrimero()
            {
                Advertencia("Debe seleccionar un cliente primero.", "Selección requerida");
            }

            public static void SinCambios()
            {
                Advertencia("No se ha hecho ningún cambio. Debe modificar al menos un campo antes de editar.", "Sin cambios");
            }

            public static void NoSeActualizo()
            {
                Error("No se pudo actualizar el cliente.", "Error en operación");
            }

            public static void NoSeReactivo()
            {
                Error("No se pudo reactivar el cliente.", "Error en operación");
            }

            public static void NoSeElimino()
            {
                Error("No se pudo eliminar el cliente.", "Error en operación");
            }
        }

        /// <summary>
        /// Mensajes específicos para la entidad Productos.
        /// </summary>
        public static class Producto
        {
            public static void CodigoNombreObligatorios()
            {
                Error("El código y el nombre son obligatorios.", "Campos incompletos");
            }

            public static void NombreExcedidoLongitud()
            {
                Error("El nombre del producto no puede exceder 20 caracteres.", "Longitud excedida");
            }

            public static void DescripcionExcedidoLongitud()
            {
                Error("La descripción del producto no puede exceder 20 caracteres.", "Longitud excedida");
            }

            public static void CodigoExcedidoLongitud()
            {
                Error("El código no puede exceder 10 caracteres.", "Longitud excedida");
            }

            public static void NombreDuplicado(string nombre)
            {
                Error($"Ya existe un producto con el nombre '{nombre}'. Por favor, ingrese un nombre único.", "Nombre duplicado");
            }

            public static void NombreSimilarExistente(string nombre)
            {
                Error($"Existe un producto con un nombre muy similar a '{nombre}'. Por favor, ingrese un nombre completamente diferente.", "Nombre similar");
            }

            public static void CodigoDuplicado(string codigo)
            {
                Error($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Código duplicado");
            }

            public static void CategoriaUnidadRequeridas()
            {
                Error("Seleccione la categoría y la unidad del producto.", "Selección requerida");
            }

            public static void PrecioInvalido()
            {
                Error("El precio de venta debe ser mayor a cero.", "Valor inválido");
            }

            public static void StockInvalido()
            {
                Error("El stock inicial y el stock mínimo no pueden ser negativos.", "Valor inválido");
            }

            public static void GuardadoExitoso()
            {
                Exito("Producto e inventario inicial guardados exitosamente.", "Guardado exitoso");
            }
        }

        /// <summary>
        /// Mensajes específicos para operaciones de Base de Datos.
        /// </summary>
        public static class BaseDatos
        {
            public static void ErrorAlGuardar(string detalles = "")
            {
                string mensaje = string.IsNullOrEmpty(detalles)
                    ? "Error al guardar en la base de datos."
                    : $"Error al guardar en la base de datos: {detalles}";
                ErrorSistema(mensaje, "Error BD");
            }

            public static void ErrorAlActualizar(string detalles = "")
            {
                string mensaje = string.IsNullOrEmpty(detalles)
                    ? "Error al actualizar en la base de datos."
                    : $"Error al actualizar: {detalles}";
                ErrorSistema(mensaje, "Error BD");
            }

            public static void ErrorAlEliminar(string detalles = "")
            {
                string mensaje = string.IsNullOrEmpty(detalles)
                    ? "Error al eliminar en la base de datos."
                    : $"Error al eliminar: {detalles}";
                ErrorSistema(mensaje, "Error BD");
            }

            public static void ErrorInesperado(string detalles = "")
            {
                string mensaje = string.IsNullOrEmpty(detalles)
                    ? "Ocurrió un error inesperado."
                    : $"Ocurrió un error inesperado: {detalles}";
                ErrorSistema(mensaje, "Error crítico");
            }
        }

        /// <summary>
        /// Mensajes específicos para operaciones de Autenticación y Seguridad.
        /// </summary>
        public static class Seguridad
        {
            public static void IntentosAgotados()
            {
                Error("Demasiados intentos fallidos. Cierre la aplicación e intente de nuevo.", "Acceso bloqueado");
            }

            public static void PermisoModificarDenegado()
            {
                Error("No tiene permisos para modificar. Se requiere autorizacion de un administrador.", "Permiso denegado");
            }

            public static void PermisoEliminarDenegado()
            {
                Error("No tiene permisos para eliminar.", "Permiso denegado");
            }
        }
    }
}
