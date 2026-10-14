using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Services
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;
        private readonly AuditoriaService _auditoria;

        public EmpleadoRepository(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
            _auditoria = new AuditoriaService(_factory);
        }

        private static readonly Expression<Func<Empleado, EmpleadoDto>> ADto = e => new EmpleadoDto
        {
            EmpleadoId = e.EmpleadoId,
            Codigo = e.Codigo,
            Nombre = e.Nombre,
            Cedula = e.Cedula,
            Telefono = e.Telefono,
            Cargo = e.Cargo,
            Salario = e.Salario,
            FechaIngreso = e.Fechaingreso,
            Activo = e.Activo ?? false
        };

        public IReadOnlyList<EmpleadoDto> GetActivos()
        {
            using var db = _factory.CreateDbContext();
            return db.Empleados.AsNoTracking()
                .Where(e => e.Activo == true)
                .OrderBy(e => e.Codigo)
                .Select(ADto)
                .ToList();
        }

        public IReadOnlyList<EmpleadoDto> GetPorCargos(params string[] cargos)
        {
            using var db = _factory.CreateDbContext();
            return db.Empleados.AsNoTracking()
                .Where(e => e.Activo == true && e.Cargo != null && cargos.Contains(e.Cargo))
                .OrderBy(e => e.Codigo)
                .Select(ADto)
                .ToList();
        }

        public IReadOnlyList<EmpleadoDto> BuscarPorCodigo(string codigoExacto)
        {
            using var db = _factory.CreateDbContext();
            string codigoNormalizado = codigoExacto.ToUpper();
            return db.Empleados.AsNoTracking()
                .Where(e => e.Codigo.ToUpper() == codigoNormalizado)
                .OrderBy(e => e.Codigo)
                .Select(ADto)
                .ToList();
        }

        /// <summary>
        /// Busca empleados por código O nombre (búsqueda parcial, case-insensitive)
        /// Incluye empleados ACTIVOS E INACTIVOS para permitir reactivación
        /// </summary>
        public IReadOnlyList<EmpleadoDto> BuscarPorCodigoONombre(string termino)
        {
            using var db = _factory.CreateDbContext();
            string terminoNormalizado = termino.ToUpper();

            return db.Empleados.AsNoTracking()
                .Where(e => 
                    e.Codigo.ToUpper().Contains(terminoNormalizado) ||
                    e.Nombre.ToUpper().Contains(terminoNormalizado)
                )
                .OrderBy(e => e.Codigo)
                .Select(ADto)
                .ToList();
        }

        public IReadOnlyList<string> GetCargos()
        {
            using var db = _factory.CreateDbContext();
            return db.Empleados.AsNoTracking()
                .Select(e => e.Cargo)
                .Where(c => c != null && c != "")
                .Distinct()
                .OrderBy(c => c)
                .ToList()!;
        }

        public string GetSiguienteCodigo()
        {
            using var db = _factory.CreateDbContext();
            var codigos = db.Empleados.AsNoTracking().Select(e => e.Codigo).ToList();
            return CodigoGenerator.SiguienteAgrupado("EMP", codigos, "EMP-000");
        }

        public bool CodigoExiste(string codigo)
        {
            using var db = _factory.CreateDbContext();
            return db.Empleados.AsNoTracking().Any(e => e.Codigo == codigo);
        }

        public bool CedulaExiste(string cedula, string? codigoExcluir = null)
        {
            using var db = _factory.CreateDbContext();
            var query = db.Empleados.AsNoTracking().Where(e => e.Cedula == cedula);

            // Si se proporciona un código a excluir (para edición), exclúyelo
            if (!string.IsNullOrWhiteSpace(codigoExcluir))
            {
                query = query.Where(e => e.Codigo != codigoExcluir);
            }

            return query.Any();
        }

        public bool TelefonoExiste(string telefono, string? codigoExcluir = null)
        {
            using var db = _factory.CreateDbContext();
            var query = db.Empleados.AsNoTracking().Where(e => e.Telefono == telefono);

            // Si se proporciona un código a excluir (para edición), exclúyelo
            if (!string.IsNullOrWhiteSpace(codigoExcluir))
            {
                query = query.Where(e => e.Codigo != codigoExcluir);
            }

            return query.Any();
        }

        public int Crear(EmpleadoDto dto)
        {
            using var db = _factory.CreateDbContext();

            var empleado = new Empleado
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Cedula = dto.Cedula,
                Telefono = dto.Telefono,
                Cargo = dto.Cargo,
                Salario = dto.Salario,
                Fechaingreso = dto.FechaIngreso,
                Activo = dto.Activo
            };

            db.Empleados.Add(empleado);
            db.SaveChanges();

            // Registrar auditoría
            _auditoria.RegistrarAccion(
                seccion: "Empleados",
                accion: "CREAR",
                registroId: empleado.EmpleadoId,
                usuario: SesionActual.NombreEmpleado,
                rolUsuario: SesionActual.Rol,
                detalle: $"Empleado: {dto.Codigo} - {dto.Nombre}, Cargo: {dto.Cargo}",
                descripcion: "Nuevo empleado creado"
            );

            return empleado.EmpleadoId;
        }

        public bool Actualizar(string codigo, string nombre, string? cargo, string cedula, string? telefono, decimal salario)
        {
            using var db = _factory.CreateDbContext();
            var emp = db.Empleados.FirstOrDefault(e => e.Codigo == codigo);
            if (emp == null) return false;

            var antes = new { emp.Nombre, emp.Cargo, emp.Cedula, emp.Telefono, emp.Salario };

            emp.Nombre = nombre.Trim();
            emp.Cargo = string.IsNullOrWhiteSpace(cargo) ? null : cargo.Trim();
            emp.Cedula = cedula.Trim();
            emp.Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
            emp.Salario = salario;

            db.SaveChanges();

            // Registrar auditoría
            _auditoria.RegistrarAccion(
                seccion: "Empleados",
                accion: "MODIFICAR",
                registroId: emp.EmpleadoId,
                usuario: SesionActual.NombreEmpleado,
                rolUsuario: SesionActual.Rol,
                detalle: $"Empleado: {codigo} - {nombre}, Cargo: {cargo}, Salario: {salario}",
                descripcion: "Empleado actualizado"
            );

            return true;
        }

        public bool Desactivar(string codigo) => CambiarEstado(codigo, false);
        public bool Reactivar(string codigo) => CambiarEstado(codigo, true);

        private bool CambiarEstado(string codigo, bool activo)
        {
            using var db = _factory.CreateDbContext();
            var emp = db.Empleados.FirstOrDefault(e => e.Codigo == codigo);
            if (emp == null) return false;

            string accion = activo ? "REACTIVAR" : "DESACTIVAR";
            emp.Activo = activo;
            db.SaveChanges();

            // Registrar auditoría
            _auditoria.RegistrarAccion(
                seccion: "Empleados",
                accion: accion,
                registroId: emp.EmpleadoId,
                usuario: SesionActual.NombreEmpleado,
                rolUsuario: SesionActual.Rol,
                detalle: $"Empleado: {codigo} - {emp.Nombre}",
                descripcion: $"Empleado {(activo ? "reactivado" : "desactivado")}"
            );

            return true;
        }

        /// <summary>
        /// Valida motivos de ACTUALIZACIÓN - MÁS FLEXIBLE
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
        /// Valida que el motivo sea coherente y profesional para BAJA/DESPIDO.
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

            // DETECTAR FRASES ABSURDAS
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

            // DETECTAR EVASIONES: Culpar a otros sin razón real
            var personasRestringidas = new[] { "el administrador", "la administradora", "el gerente", "la gerenta", "el jefe", "la jefa" };
            foreach (var persona in personasRestringidas)
            {
                if (motivoLower.Contains("porque " + persona))
                {
                    // Permitir si hay justificación real
                    var razonesValidas = new[] { "incumplimiento", "violación", "robo", "asistencia", "conducta", "disciplina", "rendimiento" };
                    bool tieneRazon = false;
                    foreach (var razon in razonesValidas)
                    {
                        if (motivoLower.Contains(razon)) { tieneRazon = true; break; }
                    }
                    if (!tieneRazon)
                        return false;
                }
            }

            // EXIGIR PALABRAS CLAVE DE CAUSAS DE BAJA VÁLIDAS
            var motivosDeBajaValidos = new[]
            {
                "incumplimiento", "violación", "robo", "asistencia", "ausencia", "conducta", 
                "disciplina", "rendimiento", "incompetencia", "insubordinación", "caducado", 
                "agotado", "retiro", "dimisión", "renuncia", "contrato", "termino"
            };

            bool tieneMotivoBaja = false;
            foreach (var mot in motivosDeBajaValidos)
            {
                if (motivoLower.Contains(mot))
                {
                    tieneMotivoBaja = true;
                    break;
                }
            }

            if (!tieneMotivoBaja)
                return false;

            // DETECTAR PALABRAS INCOMPLETAS
            foreach (var palabra in palabras)
            {
                if (palabra.Length < 3)
                {
                    var palabrasCortas = new[] { "de", "en", "el", "la", "un", "a", "y", "o", "si", "no", "es", "lo", "se", "le", "al", "su" };
                    if (!palabrasCortas.Contains(palabra.ToLower()))
                        return false;
                }
            }

            // DETECTAR SPAM
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
        /// Actualiza un empleado con validación de motivo (flexible para cambios operacionales)
        /// </summary>
        public bool ActualizarConMotivo(string codigo, string nombre, string? cargo, string cedula, string? telefono, decimal salario, string motivo, int empleadoId)
        {
            // Validar motivo
            if (string.IsNullOrWhiteSpace(motivo))
            {
                throw new InvalidOperationException("El motivo de actualización no puede estar vacío.");
            }

            if (!EsMotivoValidoParaActualizacion(motivo))
            {
                throw new InvalidOperationException("El motivo ingresado no es válido. Debe indicar una razón real.");
            }

            using var db = _factory.CreateDbContext();
            var emp = db.Empleados.FirstOrDefault(e => e.Codigo == codigo);
            if (emp == null) return false;

            var antes = new { emp.Nombre, emp.Cargo, emp.Cedula, emp.Telefono, emp.Salario };

            emp.Nombre = nombre.Trim();
            emp.Cargo = string.IsNullOrWhiteSpace(cargo) ? null : cargo.Trim();
            emp.Cedula = cedula.Trim();
            emp.Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
            emp.Salario = salario;

            db.SaveChanges();

            // Registrar auditoría
            _auditoria.RegistrarAccion(
                seccion: "Empleados",
                accion: "MODIFICAR",
                registroId: emp.EmpleadoId,
                usuario: SesionActual.NombreEmpleado,
                rolUsuario: SesionActual.Rol,
                usuarioAutorizador: SesionActual.UsuarioAutorizador,
                detalle: $"Empleado: {codigo} - {nombre}, Cargo: {cargo}, Salario: {salario}",
                descripcion: motivo
            );

            return true;
        }

        /// <summary>
        /// Inhabilita un empleado (despido) con validación de motivo (estricto)
        /// </summary>
        public bool InhabilitarConMotivo(string codigo, string motivo, int empleadoId)
        {
            // Validar motivo
            if (string.IsNullOrWhiteSpace(motivo))
            {
                throw new InvalidOperationException("El motivo de despido no puede estar vacío.");
            }

            if (!EsMotivoValidoParaBaja(motivo))
            {
                throw new InvalidOperationException("El motivo ingresado no es válido. Debe indicar una razón real y justificada.");
            }

            using var db = _factory.CreateDbContext();
            var emp = db.Empleados.FirstOrDefault(e => e.Codigo == codigo);
            if (emp == null) return false;

            emp.Activo = false;
            db.SaveChanges();

            // Registrar auditoría
            _auditoria.RegistrarAccion(
                seccion: "Empleados",
                accion: "DESPEDIR",
                registroId: emp.EmpleadoId,
                usuario: SesionActual.NombreEmpleado,
                rolUsuario: SesionActual.Rol,
                usuarioAutorizador: SesionActual.UsuarioAutorizador,
                detalle: $"Empleado: {codigo} - {emp.Nombre}",
                descripcion: motivo
            );

            return true;
        }
    }
}

