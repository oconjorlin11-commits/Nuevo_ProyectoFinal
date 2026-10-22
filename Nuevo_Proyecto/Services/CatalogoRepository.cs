using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Data.DbSqlite;

namespace Nuevo_Proyecto.Services
{
    public class CatalogoRepository : ICatalogoRepository
    {
        private readonly HybridContextFactory _hybridFactory;

        public CatalogoRepository(HybridContextFactory? hybridFactory = null)
        {
            _hybridFactory = hybridFactory ?? new HybridContextFactory();
        }

        public IReadOnlyList<LookupDto> GetCategorias()
        {
            return _hybridFactory.EjecutarConFallbackSync(
                sqlDb => sqlDb.Categorias.AsNoTracking().OrderBy(c => c.Nombre)
                    .Select(c => new LookupDto { Id = c.CategoriaId, Nombre = c.Nombre }).ToList(),
                sqliteDb => sqliteDb.Categorias.AsNoTracking().OrderBy(c => c.Nombre)
                    .Select(c => new LookupDto { Id = c.CategoriaId, Nombre = c.Nombre }).ToList()
            );
        }

        public IReadOnlyList<LookupDto> GetUnidades()
        {
            return _hybridFactory.EjecutarConFallbackSync(
                sqlDb => sqlDb.Unidades.AsNoTracking().OrderBy(u => u.Nombre)
                    .Select(u => new LookupDto { Id = u.UnidadId, Nombre = u.Nombre }).ToList(),
                sqliteDb => sqliteDb.Unidades.AsNoTracking().OrderBy(u => u.Nombre)
                    .Select(u => new LookupDto { Id = u.UnidadId, Nombre = u.Nombre }).ToList()
            );
        }

        public IReadOnlyList<LookupDto> GetFormasPago()
        {
            return _hybridFactory.EjecutarConFallbackSync(
                sqlDb => sqlDb.FormaPagos.AsNoTracking().OrderBy(f => f.Nombre)
                    .Select(f => new LookupDto { Id = f.FormaPagoId, Nombre = f.Nombre }).ToList(),
                sqliteDb => sqliteDb.FormaPagos.AsNoTracking().OrderBy(f => f.Nombre)
                    .Select(f => new LookupDto { Id = f.FormaPagoId, Nombre = f.Nombre }).ToList()
            );
        }

        public IReadOnlyList<LookupDto> GetClientesActivos()
        {
            return _hybridFactory.EjecutarConFallbackSync(
                sqlDb => sqlDb.Clientes.AsNoTracking().Where(c => c.Activo == true).OrderBy(c => c.Nombre)
                    .Select(c => new LookupDto { Id = c.ClienteId, Nombre = c.Nombre }).ToList(),
                sqliteDb => sqliteDb.Clientes.AsNoTracking().Where(c => c.Activo == true).OrderBy(c => c.Nombre)
                    .Select(c => new LookupDto { Id = c.ClienteId, Nombre = c.Nombre }).ToList()
            );
        }

        public IReadOnlyList<LookupDto> GetEmpleadosActivos()
        {
            return _hybridFactory.EjecutarConFallbackSync(
                sqlDb => sqlDb.Empleados.AsNoTracking().Where(e => e.Activo == true).OrderBy(e => e.Codigo)
                    .Select(e => new LookupDto { Id = e.EmpleadoId, Nombre = e.Nombre }).ToList(),
                sqliteDb => sqliteDb.Empleados.AsNoTracking().Where(e => e.Activo == true).OrderBy(e => e.Codigo)
                    .Select(e => new LookupDto { Id = e.EmpleadoId, Nombre = e.Nombre }).ToList()
            );
        }
    }
}
