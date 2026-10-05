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

        public EmpleadoRepository(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
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
                .OrderBy(e => e.Nombre)
                .Select(ADto)
                .ToList();
        }

        public IReadOnlyList<EmpleadoDto> GetPorCargos(params string[] cargos)
        {
            using var db = _factory.CreateDbContext();
            return db.Empleados.AsNoTracking()
                .Where(e => e.Activo == true && e.Cargo != null && cargos.Contains(e.Cargo))
                .OrderBy(e => e.Nombre)
                .Select(ADto)
                .ToList();
        }

        public IReadOnlyList<EmpleadoDto> BuscarPorCodigo(string prefijoCodigo)
        {
            using var db = _factory.CreateDbContext();
            return db.Empleados.AsNoTracking()
                .Where(e => e.Codigo.StartsWith(prefijoCodigo))
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

        public void Crear(EmpleadoDto dto)
        {
            using var db = _factory.CreateDbContext();

            db.Empleados.Add(new Empleado
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Cedula = dto.Cedula,
                Telefono = dto.Telefono,
                Cargo = dto.Cargo,
                Salario = dto.Salario,
                Fechaingreso = dto.FechaIngreso,
                Activo = dto.Activo
            });

            db.SaveChanges();
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
            return true;
        }

        public bool Desactivar(string codigo) => CambiarEstado(codigo, false);
        public bool Reactivar(string codigo) => CambiarEstado(codigo, true);

        private bool CambiarEstado(string codigo, bool activo)
        {
            using var db = _factory.CreateDbContext();
            var emp = db.Empleados.FirstOrDefault(e => e.Codigo == codigo);
            if (emp == null) return false;

            emp.Activo = activo;
            db.SaveChanges();
            return true;
        }
    }
}
