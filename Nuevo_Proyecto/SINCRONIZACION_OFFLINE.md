# Sistema de Sincronización Offline - Guía de Implementación

## 🎯 Resumen

Tu aplicación ahora tiene un **sistema completo de sincronización offline** que:
1. ✅ Detecta automáticamente cuando no hay conexión a SQL Server
2. ✅ Cambia automáticamente a SQLite como respaldo
3. ✅ Guarda las operaciones en una cola (ColaOperaciones)
4. ✅ Sincroniza automáticamente cuando se restablece la conexión

## 📁 Nuevos Servicios Creados

### 1. **ConnectionStateManager** (Services/ConnectionStateManager.cs)
- Verifica la conexión a SQL Server al iniciar
- Detecta cuando falla la conexión y cambia a SQLite
- Intenta restablecerse periódicamente
- Sincroniza automáticamente la cola cuando se restablece

**Métodos principales:**
```csharp
// Verificar conexión al iniciar
bool conectado = ConnectionStateManager.VerificarConexionAlServidor();

// Conocer estado actual
if (ConnectionStateManager.UsandoSqlite) 
{
	// Usando SQLite como respaldo
}

// Intentar restablecer conexión (llamar periódicamente)
await ConnectionStateManager.IntentarRestablecerConexion();
```

### 2. **ColaOperacionesService** (Services/ColaOperacionesService.cs)
- Gestiona la cola de operaciones en SQLite
- Registra operaciones fallidas
- Sincroniza cuando se restablece

**Métodos principales:**
```csharp
var colaService = new ColaOperacionesService();

// Registrar una operación
int secuencia = colaService.RegistrarOperacion(
	nombreEntidad: "Empleado",
	accion: "CREATE",
	identificadorEntidad: "EMP-001",
	payload: empleadoDto  // cualquier objeto
);

// Obtener operaciones pendientes
var pendientes = colaService.ObtenerOperacionesPendientes();

// Ver estadísticas
var stats = colaService.ObtenerEstadisticas();
// { "Pendiente": 3, "Confirmada": 10, "Error": 1 }

// Reintentar operación fallida
colaService.ReintenlarOperacion(secuencia);

// Limpiar operaciones antiguas (>30 días)
colaService.LimpiarOperacionesAntiguas(30);
```

### 3. **HybridContextFactory** (Services/HybridContextFactory.cs)
- Choose automáticamente entre SQL Server y SQLite
- Proporciona fallback automático

**Uso:**
```csharp
var hybridFactory = new HybridContextFactory();

// Ejecutar con fallback automático
var resultado = hybridFactory.EjecutarConFallbackSync(
	operacionSql: (db) => db.Empleados.ToList(),
	operacionSqlite: (db) => db.Empleados.ToList()
);
```

### 4. **ComideriaSqliteContextFactory** (Data/ComideriaSqliteContextFactory.cs)
- Factory para crear instancias del contexto SQLite
- Crea automáticamente la BD en AppData/Local/Comideria
- Crea tablas si no existen

## 🚀 Cómo Usar

### Paso 1: Verificación al Iniciar (ya implementado en Program.cs)
```csharp
// En Program.cs se ejecuta automáticamente:
bool sqlServerDisponible = ConnectionStateManager.VerificarConexionAlServidor();

if (!sqlServerDisponible)
{
	Console.WriteLine("⚠️ Usando SQLite en modo offline");
}
```

### Paso 2: Capturar Errores y Guardar en Cola

Actualiza tu **EmpleadoRepository** para capturar errores:

```csharp
public async Task<EmpleadoDto?> CrearEmpleado(EmpleadoCreateDto dto)
{
	try
	{
		using var db = _factory.CreateDbContext();
		var empleado = new Empleado { ... };
		db.Empleados.Add(empleado);
		await db.SaveChangesAsync();
		return MapToDto(empleado);
	}
	catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 26)
	{
		// Error de conexión - guardar en cola
		var colaService = new ColaOperacionesService();
		await colaService.RegistrarOperacion(
			nombreEntidad: "Empleado",
			accion: "CREATE",
			identificadorEntidad: dto.Codigo,
			payload: dto
		);

		// Usar SQLite como respaldo
		var sqliteFactory = new ComideriaSqliteContextFactory();
		using var dbLocal = sqliteFactory.CreateDbContext();
		// ... guardar en SQLite

		return null;
	}
}
```

### Paso 3: Sincronización Periódica

Agregar un timer que intente sincronizar cada X segundos:

```csharp
// En el MenuPrincipal o un FormBase
private System.Windows.Forms.Timer _syncTimer;

public MenuPrincipal()
{
	InitializeComponent();

	// Crear timer para intentar sincronizar cada 30 segundos
	_syncTimer = new System.Windows.Forms.Timer();
	_syncTimer.Interval = 30000; // 30 segundos
	_syncTimer.Tick += async (s, e) =>
	{
		await ConnectionStateManager.IntentarRestablecerConexion();
	};
}

private void MenuPrincipal_Load(object sender, EventArgs e)
{
	_syncTimer.Start();
}

private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
{
	_syncTimer.Stop();
	_syncTimer.Dispose();
}
```

## 📊 Flujo de Operación

```
┌─ Iniciar Aplicación
│
├─ ConnectionStateManager.VerificarConexionAlServidor()
│  ├─ ¿SQL Server disponible?
│  │  ├─ SÍ: ConnectionStateManager.UsandoSqlite = false
│  │  └─ NO: ConnectionStateManager.UsandoSqlite = true
│
├─ Usuario realiza una acción (CREATE, UPDATE, DELETE)
│  │
│  ├─ Si SQL Server está disponible:
│  │  └─ Operación directa a SQL Server ✓
│  │
│  └─ Si SQL Server NO está disponible:
│     ├─ Operación guardada en SQLite ✓
│     ├─ Operación guardada en ColaOperaciones ✓
│     └─ Usuario recibe aviso: "Modo offline"
│
├─ Cada 30 segundos: IntentarRestablecerConexion()
│  │
│  ├─ ¿Se puede conectar a SQL Server?
│  │  ├─ SÍ: 
│  │  │  ├─ ConnectionStateManager.UsandoSqlite = false
│  │  │  ├─ SincronizarColaOperaciones()
│  │  │  │  ├─ Leer todas las operaciones pendientes
│  │  │  │  ├─ Procesar cada una en orden
│  │  │  │  └─ Marcar como "Confirmada" o "Error"
│  │  │  └─ Notificar al usuario: "Sincronización completada"
│  │  │
│  │  └─ NO: Seguir esperando...
│
└─ La aplicación continúa funcionando sin interrupciones
```

## ⚙️ Configuración

### Ubicación de Base de Datos SQLite
Por defecto se guarda en:
```
C:\Users\{Usuario}\AppData\Roaming\Comideria\comideria_offline.db
```

Para cambiar la ubicación:
```csharp
var factory = new ComideriaSqliteContextFactory(@"D:\MiAplicacion\offline.db");
```

## 📋 Tabla de Estados de Operaciones

| Estado | Significado |
|--------|-----------|
| **Pendiente** | Esperando sincronización a SQL Server |
| **Confirmada** | Sincronizada exitosamente a SQL Server |
| **Error** | Falló al sincronizar, puede reintentarse |

## 🔍 Monitoreo

Ver estado de la sincronización:
```csharp
var colaService = new ColaOperacionesService();
var stats = colaService.ObtenerEstadisticas();

Console.WriteLine($"Pendientes: {stats["Pendiente"]}");
Console.WriteLine($"Confirmadas: {stats["Confirmada"]}");
Console.WriteLine($"Errores: {stats["Error"]}");
```

## ⚠️ Casos de Error Esperados

### Error: "SQL Server no encontrado"
- **Causa**: El servidor SQL no está accesible
- **Acción**: Automáticamente se usa SQLite
- **Solución**: Verificar conexión de red

### Cola con operaciones "Error"
- **Causa**: Falló la sincronización de alguna operación
- **Acción**: Verificar los detalles en UltimoError
- **Solución**: Corregir el problema y llamar a `ReintenlarOperacion()`

## 🎓 Ejemplo Completo de Uso

```csharp
// 1. Verificar al iniciar
if (!ConnectionStateManager.VerificarConexionAlServidor())
{
	MensajeHelper.Advertencia("Trabajando en modo offline. Los cambios se sincronizarán cuando se restablezca la conexión.");
}

// 2. Cuando se produce un error de conexión
var colaService = new ColaOperacionesService();
try
{
	// Intentar operación
}
catch (SqlException ex) when (ex.Number == 26)
{
	// Guardar en cola
	colaService.RegistrarOperacion(
		nombreEntidad: "Producto",
		accion: "UPDATE",
		identificadorEntidad: producto.ID.ToString(),
		payload: producto
	);

	MensajeHelper.Informacion("Cambio guardado. Se sincronizará cuando haya conexión.");
}

// 3. Iniciar sincronización periódica (en MenuPrincipal)
private System.Windows.Forms.Timer _syncTimer = new();

// En FormLoad:
_syncTimer.Interval = 30000; // Cada 30 segundos
_syncTimer.Tick += async (s,e) => 
{
	await ConnectionStateManager.IntentarRestablecerConexion();
};
_syncTimer.Start();
```

## ✅ Próximas Mejoras

1. Implementar lógica específica en `ProcesarOperacion` para cada entidad
2. Agregar UI que muestre estado de sincronización
3. Agregar manejo de conflictos de datos
4. Implementar compresión de PayloadJson para operaciones grandes

---

**Estado**: ✅ Sistema implementado y compilando correctamente
**Próximo paso**: Integrar la captura de errores en tus repositories
