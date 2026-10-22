# Estrategia de Estandarización de Mensajes

## Resumen
Se ha creado una clase `MensajeHelper` que proporciona una forma consistente y predecible de mostrar mensajes al usuario en toda la aplicación, respetando la lógica de negocio.

## Estructura de Mensajes

### 1. **Métodos Básicos** (Nivel General)
```csharp
// Error de validación
MensajeHelper.Error("El campo Nombre es obligatorio.", "Campo incompleto");

// Error del sistema
MensajeHelper.ErrorSistema("Error inesperado: " + ex.Message, "Error del sistema");

// Advertencia
MensajeHelper.Advertencia("No hay cambios para guardar.", "Advertencia");

// Éxito
MensajeHelper.Exito("El cliente se guardó correctamente.", "Operación exitosa");

// Información
MensajeHelper.Informacion("Debe llenar todos los campos.", "Información");

// Confirmación
if (MensajeHelper.Confirmacion("¿Desea salir sin guardar?")) { /* ... */ }
```

### 2. **Métodos Específicos por Categoría** (Nivel Negocio)

#### Validación de Campos
```csharp
// Campo obligatorio
MensajeHelper.Validacion.CampoObligatorio("Nombre del cliente");

// Formato inválido
MensajeHelper.Validacion.FormatoInvalido("Teléfono", "XXXX-XXXX (ej: 7777-1111)");

// Rango inválido
MensajeHelper.Validacion.RangoInvalido("Salario", "Debe ser mayor a 0");

// Longitud inválida
MensajeHelper.Validacion.LongitudInvalida("Descripción", 10, 500);

// Duplicado existente
MensajeHelper.Validacion.DuplicadoExistente("cliente", "Alex Paredes");
```

#### Operaciones CRUD
```csharp
// Guardado exitoso
MensajeHelper.Operacion.Guardado("cliente");

// Actualización exitosa
MensajeHelper.Operacion.Actualizado("producto");

// Eliminación exitosa
MensajeHelper.Operacion.Eliminado("empleado");

// Reactivación
MensajeHelper.Operacion.Reactivado("cliente");

// Confirmar eliminación
if (MensajeHelper.Operacion.ConfirmarEliminacion("cliente", "Juan Pérez")) { /* ... */ }

// Confirmar actualización
if (MensajeHelper.Operacion.ConfirmarActualizacion("cliente")) { /* ... */ }
```

#### Búsqueda
```csharp
// No encontrado
MensajeHelper.Busqueda.NoEncontrado("empleado", "juanito");

// Sin datos
MensajeHelper.Busqueda.SinDatos("el inventario");
```

#### Permisos
```csharp
// Acceso denegado general
MensajeHelper.Permiso.AccesoDenegado("crear empleados");

// Rol insuficiente
MensajeHelper.Permiso.RolInsuficiente("Administrador");
```

#### Motivos (Actualización, Eliminación, Baja)
```csharp
// Motivo requerido
MensajeHelper.Motivo.MotivoRequerido("actualizar el cliente");

// Motivo inválido
MensajeHelper.Motivo.MotivoInvalido("Debe tener mínimo 10 caracteres");
```

#### Transferencia (Importación/Exportación)
```csharp
// Exportado exitosamente
MensajeHelper.Transferencia.ExportadoExitosamente("C:\\reportes\\cliente.xlsx");

// Importado exitosamente
MensajeHelper.Transferencia.ImportadoExitosamente(45);

// Archivo en uso
MensajeHelper.Transferencia.ArchivoEnUso("cliente.xlsx");
```

## Beneficios

✅ **Consistencia**: Todos los mensajes siguen la misma estructura
✅ **Mantenibilidad**: Cambiar un tipo de mensaje se hace en un solo lugar
✅ **Localización**: Fácil traducir mensajes (cambiar idioma)
✅ **Legibilidad**: El código es más limpio y legible
✅ **Respeta lógica de negocio**: Separa por categorías según operaciones reales
✅ **Reutilizable**: Se puede usar en cualquier formulario

## Ejemplo de Implementación

### Antes (Sin Estandarización)
```csharp
private void btnGuardar_Click(object sender, EventArgs e)
{
	if (string.IsNullOrWhiteSpace(txtNombre.Text))
	{
		MessageBox.Show("Por favor ingrese el nombre.", "Campo vacio");
		return;
	}

	MessageBox.Show("Registro guardado.", "Éxito");
}
```

### Después (Con Estandarización)
```csharp
private void btnGuardar_Click(object sender, EventArgs e)
{
	if (string.IsNullOrWhiteSpace(txtNombre.Text))
	{
		MensajeHelper.Validacion.CampoObligatorio("Nombre");
		return;
	}

	MensajeHelper.Operacion.Guardado("cliente");
}
```

## Próximos Pasos

1. **Actualizar formularios principales** (NuevoCliente, NuevoEmpleado, NuevoProducto)
2. **Actualizar Presenters** (ClientePresenter, EmpleadoPresenter, etc.)
3. **Actualizar formularios de motivos** (actualización, eliminación, baja)
4. **Revisar y estandarizar** todos los MessageBox.Show() existentes

## Notas

- No requiere refactorizing exhaustivo
- Se puede implementar progresivamente
- Compatible con la lógica actual
- Fácil de extender con nuevos métodos según necesidades
