using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Catalogos;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Services
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;
        private readonly AuditoriaService _auditoria;

        public InventarioRepository(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
            _auditoria = new AuditoriaService(_factory);
        }

        public IReadOnlyList<ProductoInventarioDto> GetTodos()
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking().OrderBy(p => p.Nombre)
                .Select(Proyecciones.ProductoInventario).ToList();
        }

        public IReadOnlyList<ProductoInventarioDto> GetActivos()
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking().Where(p => p.Activo == true).OrderBy(p => p.Codigo)
                .Select(Proyecciones.ProductoInventario).ToList();
        }

        public IReadOnlyList<ProductoInventarioDto> GetActivosPorCategoria(int categoriaId)
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking()
                .Where(p => p.Activo == true && p.CategoriaId == categoriaId).OrderBy(p => p.Nombre)
                .Select(Proyecciones.ProductoInventario).ToList();
        }

        public IReadOnlyList<ProductoInventarioDto> Buscar(string texto)
        {
            using var db = _factory.CreateDbContext();
            return db.Productos.AsNoTracking()
                .Where(p => p.Codigo.Contains(texto) || p.Nombre.Contains(texto)).OrderBy(p => p.Codigo)
                .Select(Proyecciones.ProductoInventario).ToList();
        }

        public IReadOnlyList<ProductoInventarioDto> BuscarPorCodigoExacto(string codigo)
        {
            using var db = _factory.CreateDbContext();
            string codigoNormalizado = codigo.ToUpper();
            return db.Productos.AsNoTracking()
                .Where(p => p.Codigo.ToUpper() == codigoNormalizado).OrderBy(p => p.Codigo)
                .Select(Proyecciones.ProductoInventario).ToList();
        }

        /// <summary>
        /// Busca productos por código O nombre (búsqueda parcial, case-insensitive)
        /// Incluye productos ACTIVOS E INACTIVOS para permitir reactivación
        /// </summary>
        public IReadOnlyList<ProductoInventarioDto> BuscarPorCodigoONombreProducto(string termino)
        {
            using var db = _factory.CreateDbContext();
            string terminoNormalizado = termino.ToUpper();

            return db.Productos.AsNoTracking()
                .Where(p => 
                    p.Codigo.ToUpper().Contains(terminoNormalizado) ||
                    p.Nombre.ToUpper().Contains(terminoNormalizado)
                )
                .OrderBy(p => p.Codigo)
                .Select(Proyecciones.ProductoInventario).ToList();
        }

        public IReadOnlyList<MovimientoInventarioDto> GetMovimientos(DateTime? desde, DateTime? hasta)
        {
            using var db = _factory.CreateDbContext();
            var query = db.MovimientoInventarios.AsNoTracking().AsQueryable();

            if (desde.HasValue) query = query.Where(m => m.Fecha >= desde.Value);
            if (hasta.HasValue)
            {
                var fin = Proyecciones.FinDeDia(hasta.Value);
                query = query.Where(m => m.Fecha <= fin);
            }

            return query
                .OrderByDescending(m => m.Fecha).ThenByDescending(m => m.MovimientoId)
                .Select(m => new MovimientoInventarioDto
                {
                    MovimientoId = m.MovimientoId,
                    Fecha = m.Fecha,
                    Tipo = m.TipoMovimiento,
                    Cantidad = m.Cantidad,
                    StockAnterior = m.StockAnterior,
                    StockNuevo = m.StockNuevo,
                    Observacion = m.Observacion,
                    NombreProducto = m.Producto.Nombre,
                    NombreEmpleado = m.Empleado.Nombre,
                    PrecioVenta = m.Producto.PrecioVenta,
                    ValorInventario = m.Producto.PrecioVenta * m.StockNuevo
                })
                .ToList();
        }

        public int Reactivar(string codigo, int stock, int minimo, int empleadoId, string motivo = "Producto reactivado")
        {
            // ✅ VALIDACIÓN: Rechazar motivos vacíos o inválidos
            if (string.IsNullOrWhiteSpace(motivo))
            {
                throw new InvalidOperationException("El motivo de reactivación no puede estar vacío.");
            }

            // Usar validación FLEXIBLE para reactivación
            if (!EsMotivoValidoParaActualizacion(motivo))
            {
                throw new InvalidOperationException("El motivo ingresado no es válido. Debe indicar una razón real.");
            }

            using var db = _factory.CreateDbContext();
            var prod = db.Productos.FirstOrDefault(p => p.Codigo == codigo);
            if (prod == null) return 0;

            var inv = db.Inventario.FirstOrDefault(i => i.ProductoId == prod.ProductoId);
            int stockAnterior = inv?.Stock ?? 0;

            // Actualizar producto usando SQL directo para evitar conflicto con triggers
            db.Database.ExecuteSqlInterpolated($@"
                UPDATE Productos
                SET Activo = 1
                WHERE ProductoId = {prod.ProductoId}
            ");

            // Actualizar inventario usando SQL directo
            if (inv == null)
            {
                db.Database.ExecuteSqlInterpolated($@"
                    INSERT INTO Inventario (ProductoId, Stock, StockMinimo)
                    VALUES ({prod.ProductoId}, {stock}, {minimo})
                ");
            }
            else
            {
                db.Database.ExecuteSqlInterpolated($@"
                    UPDATE Inventario
                    SET Stock = {stock}, StockMinimo = {minimo}
                    WHERE ProductoId = {prod.ProductoId}
                ");
            }

            db.MovimientoInventarios.Add(MovimientosHelper.Crear(prod.ProductoId,
                TipoMovimientoInventario.ReactivacionProducto, Math.Abs(stock - stockAnterior),
                stockAnterior, stock, empleadoId, motivo));

            int result = db.SaveChanges();

            if (result > 0)
            {
                // Registrar auditoría
                _auditoria.RegistrarAccion(
                    seccion: "Inventario",
                    accion: "REACTIVAR",
                    registroId: prod.ProductoId,
                    usuario: SesionActual.NombreEmpleado,
                    rolUsuario: SesionActual.Rol,
                    usuarioAutorizador: SesionActual.UsuarioAutorizador,
                    detalle: $"Producto: {codigo}, Stock: {stock}, Mínimo: {minimo}",
                    descripcion: motivo
                );
            }

            return result;
        }

        public int Actualizar(int productoId, string nombre, int categoriaId, int unidadId, string? descripcion,
                              decimal precioVenta, bool activo, int stock, int minimo, int empleadoId, string observacion)
        {
            using var db = _factory.CreateDbContext();
            var prod = db.Productos.FirstOrDefault(p => p.ProductoId == productoId);
            if (prod == null) return 0;

            var inv = db.Inventario.FirstOrDefault(i => i.ProductoId == productoId);
            int stockAnterior = inv?.Stock ?? 0;
            int minimoAnterior = inv?.StockMinimo ?? 0;

            // El tipo de movimiento se deduce de lo que realmente cambió
            var tipo = TipoMovimientoHelper.DetectarTipoCambio(
                nombre, categoriaId, unidadId, precioVenta, activo, stock, minimo,
                prod.Nombre, prod.CategoriaId, prod.UnidadId, prod.PrecioVenta, prod.Activo ?? false,
                stockAnterior, minimoAnterior);

            // Actualizar producto
            prod.Nombre = nombre;
            prod.CategoriaId = categoriaId;
            prod.UnidadId = unidadId;
            if (descripcion != null) prod.Descripcion = descripcion;   // null = conservar la actual
            prod.PrecioVenta = precioVenta;
            prod.Activo = activo;

            // Actualizar inventario usando SQL directo para evitar conflicto con triggers
            if (inv == null)
            {
                db.Database.ExecuteSqlInterpolated($@"
                    INSERT INTO Inventario (ProductoId, Stock, StockMinimo)
                    VALUES ({productoId}, {stock}, {minimo})
                ");
            }
            else
            {
                db.Database.ExecuteSqlInterpolated($@"
                    UPDATE Inventario
                    SET Stock = {stock}, StockMinimo = {minimo}
                    WHERE ProductoId = {productoId}
                ");
            }

            // Crear movimiento
            db.MovimientoInventarios.Add(MovimientosHelper.Crear(productoId, tipo,
                Math.Abs(stock - stockAnterior), stockAnterior, stock, empleadoId, observacion));

            int result = db.SaveChanges();

            if (result > 0)
            {
                // Registrar auditoría
                _auditoria.RegistrarAccion(
                    seccion: "Inventario",
                    accion: "MODIFICAR",
                    registroId: productoId,
                    usuario: SesionActual.NombreEmpleado,
                    rolUsuario: SesionActual.Rol,
                    usuarioAutorizador: SesionActual.UsuarioAutorizador,
                    detalle: $"Producto: {nombre}, Categoría: {categoriaId}, Stock: {stock}, Precio: {precioVenta}",
                    descripcion: observacion
                );
            }

            return result;
        }

        public int Inhabilitar(int productoId, int empleadoId, string motivo)
        {
            // ✅ VALIDACIÓN: Rechazar motivos vacíos o inválidos
            if (string.IsNullOrWhiteSpace(motivo))
            {
                throw new InvalidOperationException("El motivo de baja no puede estar vacío.");
            }

            // Usar validación ESTRICTA para baja/eliminación
            if (!EsMotivoValidoParaBaja(motivo))
            {
                throw new InvalidOperationException("El motivo ingresado no es válido. Debe indicar una razón real y justificada.");
            }

            using var db = _factory.CreateDbContext();
            var prod = db.Productos.FirstOrDefault(p => p.ProductoId == productoId);
            if (prod == null) return 0;

            var inv = db.Inventario.AsNoTracking().FirstOrDefault(i => i.ProductoId == productoId);
            int stock = inv?.Stock ?? 0;

            // Actualizar producto usando SQL directo para evitar conflicto con triggers
            db.Database.ExecuteSqlInterpolated($@"
                UPDATE Productos
                SET Activo = 0
                WHERE ProductoId = {productoId}
            ");

            // Actualizar Stock a 0 cuando se inhabilita el producto
            db.Database.ExecuteSqlInterpolated($@"
                UPDATE Inventario
                SET Stock = 0
                WHERE ProductoId = {productoId}
            ");

            // Crear movimiento
            db.MovimientoInventarios.Add(MovimientosHelper.Crear(productoId,
                TipoMovimientoInventario.InhabilitacionProducto, 0, stock, 0, empleadoId, motivo));

            int result = db.SaveChanges();

            if (result > 0)
            {
                // Registrar auditoría
                _auditoria.RegistrarAccion(
                    seccion: "Inventario",
                    accion: "ELIMINAR",
                    registroId: productoId,
                    usuario: SesionActual.NombreEmpleado,
                    rolUsuario: SesionActual.Rol,
                    usuarioAutorizador: SesionActual.UsuarioAutorizador,
                    detalle: $"Producto: {prod.Codigo} - {prod.Nombre}",
                    descripcion: motivo
                );
            }

            return result;
        }

        /// <summary>
        /// Valida motivos de ACTUALIZACIÓN/REACTIVACIÓN - MÁS FLEXIBLE
        /// Permite motivos operacionales sin palabras clave específicas
        /// </summary>
        private bool EsMotivoValidoParaActualizacion(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                return false;

            motivo = motivo.Trim();

            // Mínimo 10 caracteres
            if (motivo.Length < 10)
                return false;

            var palabras = motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            if (palabras.Length < 2)
                return false;

            var motivoLower = motivo.ToLower();

            // SOLO RECHAZAR FRASES ABSURDAS (SIN RAZÓN)
            var frasesAbsurdas = new[]
            {
                "porque sí", "porque si", "me dio la gana", "sin razón", "sin motivo",
                "al azar", "capricho"
            };

            foreach (var frase in frasesAbsurdas)
            {
                if (motivoLower.Contains(frase))
                    return false;
            }

            // Rechazar si culpa a persona SIN razón objetiva en actualización
            if (motivoLower.Contains("porque el") || motivoLower.Contains("porque la"))
            {
                var razonesObjetivas = new[] { "ingreso", "error", "corregir", "corregí", "ajust", "cambio", 
                    "actualiz", "revisar", "corrección", "control", "verificación" };
                bool tieneRazon = false;
                foreach (var r in razonesObjetivas)
                {
                    if (motivoLower.Contains(r)) { tieneRazon = true; break; }
                }
                if (!tieneRazon) return false;
            }

            // Detectar palabras incompletas
            foreach (var palabra in palabras)
            {
                if (palabra.Length < 3)
                {
                    var palabrasCortas = new[] { "de", "en", "el", "la", "un", "a", "y", "o", "si", "no", "es", "lo", "se", "le", "al", "su" };
                    if (!palabrasCortas.Contains(palabra.ToLower()))
                        return false;
                }
            }

            // Detectar spam
            int cambiosMayuscula = 0;
            for (int i = 1; i < motivo.Length; i++)
            {
                bool esMayusc = char.IsUpper(motivo[i]);
                bool anteriorMayusc = char.IsUpper(motivo[i - 1]);
                if (esMayusc != anteriorMayusc && char.IsLetter(motivo[i]) && char.IsLetter(motivo[i - 1]))
                    cambiosMayuscula++;
            }

            if (cambiosMayuscula > 3)
                return false;

            return true;
        }

        /// <summary>
        /// Valida que el motivo sea coherente y profesional para BAJA/ELIMINACIÓN.
        /// Detecta intentos de evasión, motivos sin justificación real, y frases irresponsables.
        /// </summary>
        private bool EsMotivoValidoParaBaja(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                return false;

            motivo = motivo.Trim();

            // Mínimo 10 caracteres
            if (motivo.Length < 10)
                return false;

            var palabras = motivo.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            if (palabras.Length < 2)
                return false;

            var motivoLower = motivo.ToLower();

            // ============================================================================
            // 1️⃣ DETECTAR FRASES CLARAMENTE SIN RAZÓN VÁLIDA
            // ============================================================================
            var frasesAbsurdas = new[]
            {
                "porque sí", "porque si", "me dio la gana", "me dio gana", 
                "sin razón", "sin motivo", "sin justificación", "sin justificacion",
                "al azar", "capricho", "aleatoriamente", "simplemente", "solo"
            };

            foreach (var frase in frasesAbsurdas)
            {
                if (motivoLower.Contains(frase))
                    return false;
            }

            // ============================================================================
            // 2️⃣ DETECTAR EVASIONES INTELIGENTES - Culpar a personas en lugar de razones
            // ============================================================================
            // Patrones: "porque el/la [persona] [acción sin motivo]"
            var personasYAcciones = new[]
            {
                "porque el administrador", "porque la administrador", "porque el jefe", "porque la jefa",
                "porque el gerente", "porque el director", "porque la directora",
                "porque alguien", "porque él", "porque ella", "porque yo",
                "porque el dueño", "porque la empresa", "porque la compañía"
            };

            foreach (var patron in personasYAcciones)
            {
                if (motivoLower.Contains(patron))
                {
                    // Si culpa a una persona, debe tener una razón objetiva después
                    if (!ContieneMotivoObjetivo(motivoLower))
                        return false;
                }
            }

            // Detectar acciones vacías: "porque [persona] quiso", "porque [persona] decidió", etc.
            var accionesVacias = new[] { "quiso", "decidió", "decidio", "se le dio la gana", "le vino en gana", "se le antojó", "se le antojo" };
            if (ObtienePrimeraPersona(motivoLower) && ContienePalabrasVacias(motivoLower, accionesVacias))
                return false;

            // ============================================================================
            // 3️⃣ VERIFICAR QUE TENGA MOTIVOS VÁLIDOS O COHERENTES
            // ============================================================================
            // Palabras que indican motivos reales
            var motivosValidos = new[]
            {
                // Daño
                "daño", "dañado", "roto", "quebrado", "fracturado", "rotura",
                "mojado", "oxidado", "oxidacion", "corrosión", "corrosion", "deterioro",

                // Caducidad/Expiración
                "caducado", "caducidad", "vencido", "vencimiento", "expirado", "expiry", "fecha",
                "vence", "caducar", "podrido", "fermentado", "descompuesto",

                // Pérdida/Robo
                "robo", "robado", "robaron", "perdida", "perdido", "perdieron", "extraviado",
                "desaparecido", "hurto", "falta", "desapareció",

                // Obsoleto/No usar
                "obsoleto", "antiguo", "viejo", "descontinuado", "discontinuado", "fuera de moda",
                "no se usa", "no se vende", "no hay demanda", "sin movimiento",

                // Stock/Inventario
                "stock", "inventario", "agotado", "exceso", "sobrante", "quemado",

                // Retiro/Cambio
                "retiro", "retirado", "cambio", "cambiado", "devolución", "devuelto", "reemplazo",
                "reemplazado", "retornado", "retorno",

                // Error
                "error", "equivocación", "equivocacion", "malentendido", "confusión", "confusion",
                "fallo", "defecto", "defectuoso", "no funciona", "averia", "avería",

                // Inspección/Revisión
                "inspección", "inspeccion", "revisión", "revision", "auditoría", "auditoria",
                "revisado", "inspeccionado", "encontrado", "descubierto", "detectado",

                // Mantenimiento/Limpieza
                "mantenimiento", "limpieza", "limpio", "higiene", "sanitario", "desinfección",
                "desinfeccion", "control", "daño en", "problema en", "falta de", "exceso de"
            };

            bool tieneMotivoValido = false;
            foreach (var palabraClave in motivosValidos)
            {
                if (motivoLower.Contains(palabraClave))
                {
                    tieneMotivoValido = true;
                    break;
                }
            }

            // Si no tiene ninguna palabra clave de motivo válido = rechazar
            if (!tieneMotivoValido)
                return false;

            // ============================================================================
            // 4️⃣ DETECTAR PALABRAS INCOMPLETAS
            // ============================================================================
            foreach (var palabra in palabras)
            {
                if (palabra.Length < 3)
                {
                    var palabrasCortas = new[] { "de", "en", "el", "la", "un", "a", "y", "o", "si", "no", "es", "lo", "se", "le", "al", "su", "da", "x", "2", "3", "4", "5" };
                    if (!palabrasCortas.Contains(palabra.ToLower()))
                        return false;
                }

                // Detectar palabras cortadas
                if (palabra.Length > 3 && (palabra.EndsWith("cad") || palabra.EndsWith("dañ") || 
                    palabra.EndsWith("rust") || palabra.EndsWith("prod")) && palabra.Length < 8)
                {
                    if (!palabra.Equals("stock"))
                        return false;
                }
            }

            // ============================================================================
            // 5️⃣ DETECTAR SPAM - Cambios erráticos de mayúscula
            // ============================================================================
            int cambiosMayuscula = 0;
            for (int i = 1; i < motivo.Length; i++)
            {
                bool esMayusc = char.IsUpper(motivo[i]);
                bool anteriorMayusc = char.IsUpper(motivo[i - 1]);
                if (esMayusc != anteriorMayusc && char.IsLetter(motivo[i]) && char.IsLetter(motivo[i - 1]))
                    cambiosMayuscula++;
            }

            if (cambiosMayuscula > 3)
                return false;

            return true;
        }

        /// <summary>
        /// Verifica si el motivo contiene una razón objetiva (no carácter de persona)
        /// </summary>
        private bool ContieneMotivoObjetivo(string motivoLower)
        {
            var razonesObjetivas = new[]
            {
                "daño", "roto", "caducado", "vencido", "robo", "perdida", "obsoleto",
                "error", "defecto", "fallo", "mal", "politica", "politica",
                "norma", "regulación", "regulacion", "auditoría", "auditoria",
                "stock", "inventario", "calidad", "seguridad", "higiene", "envío",
                "retiro", "cambio", "devolución", "devuelto", "análisis", "analisis",
                "inspección", "inspeccion", "control", "capacitación", "capacitacion"
            };

            foreach (var razon in razonesObjetivas)
            {
                if (motivoLower.Contains(razon))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Detecta si la frase culpa a una persona/entidad
        /// </summary>
        private bool ObtienePrimeraPersona(string motivoLower)
        {
            var personasReferencias = new[]
            {
                "administrador", "jefe", "gerente", "director", "dueño", "empresa",
                "compañía", "él", "ella", "yo", "nosotros", "alguien"
            };

            foreach (var persona in personasReferencias)
            {
                if (motivoLower.Contains(persona))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Detecta si contiene palabras que indican acciones vacías/sin motivo
        /// </summary>
        private bool ContienePalabrasVacias(string motivoLower, string[] palabras)
        {
            foreach (var palabra in palabras)
            {
                if (motivoLower.Contains(palabra))
                    return true;
            }

            return false;
        }
    }
}
