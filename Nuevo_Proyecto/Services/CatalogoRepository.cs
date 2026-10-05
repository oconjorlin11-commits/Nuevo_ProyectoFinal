using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Services
{
    public class CatalogoRepository : ICatalogoRepository
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;

        public CatalogoRepository(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
        }

        public IReadOnlyList<LookupDto> GetCategorias()
        {
            using var db = _factory.CreateDbContext();
            return db.Categorias.AsNoTracking().OrderBy(c => c.Nombre)
                .Select(c => new LookupDto { Id = c.CategoriaId, Nombre = c.Nombre }).ToList();
        }

        public IReadOnlyList<LookupDto> GetUnidades()
        {
            using var db = _factory.CreateDbContext();
            return db.Unidades.AsNoTracking().OrderBy(u => u.Nombre)
                .Select(u => new LookupDto { Id = u.UnidadId, Nombre = u.Nombre }).ToList();
        }

        public IReadOnlyList<LookupDto> GetFormasPago()
        {
            using var db = _factory.CreateDbContext();
            return db.FormaPagos.AsNoTracking().OrderBy(f => f.Nombre)
                .Select(f => new LookupDto { Id = f.FormaPagoId, Nombre = f.Nombre }).ToList();
        }

        public IReadOnlyList<LookupDto> GetClientesActivos()
        {
            using var db = _factory.CreateDbContext();
            return db.Clientes.AsNoTracking().Where(c => c.Activo == true).OrderBy(c => c.Nombre)
                .Select(c => new LookupDto { Id = c.ClienteId, Nombre = c.Nombre }).ToList();
        }

        public IReadOnlyList<LookupDto> GetEmpleadosActivos()
        {
            using var db = _factory.CreateDbContext();
            return db.Empleados.AsNoTracking().Where(e => e.Activo == true).OrderBy(e => e.Nombre)
                .Select(e => new LookupDto { Id = e.EmpleadoId, Nombre = e.Nombre }).ToList();
        }
    }
}
