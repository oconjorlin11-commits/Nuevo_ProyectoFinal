using System;
using System.Data;
using System.Linq;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Views.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Nuevo_Proyecto.Presenters
{
    public class InventarioPresenter
    {
        private readonly IInventarioView? _view;
        public InventarioPresenter(IInventarioView? view = null)
        {
            _view = view; // view es opcional
        }

        public DataTable GetMovimientosInventario(DateTime? desde, DateTime? hasta)
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new DataTable();
            dt.Columns.Add("MovimientoID");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("StockAnterior");
            dt.Columns.Add("StockNuevo");
            dt.Columns.Add("Observacion");
            dt.Columns.Add("NombreProducto");
            dt.Columns.Add("NombreEmpleado");
            dt.Columns.Add("PrecioVenta");
            dt.Columns.Add("ValorInventario");
            dt.Columns.Add("CantidadFacturasEmitidas");
            dt.Columns.Add("CantidadProductosDescontados");

            var query = db.MovimientoInventarios.AsNoTracking().AsQueryable();
            // AsNoTracking asegurado: mantenemos la consulta en modo no rastreado para rendimiento
            if (desde.HasValue) query = query.Where(m => m.Fecha >= desde.Value);
            if (hasta.HasValue) query = query.Where(m => m.Fecha <= hasta.Value);

            var items = (from m in query
                         join p in db.Productos on m.ProductoId equals p.ProductoId
                         join e in db.Empleados on m.EmpleadoId equals e.EmpleadoId
                         select new {
                             MovimientoID = m.MovimientoId,
                             Fecha = m.Fecha,
                             Tipo = m.Tipo,
                             Cantidad = m.Cantidad,
                             StockAnterior = m.StockAnterior,
                             StockNuevo = m.StockNuevo,
                             Observacion = m.Observacion,
                             Producto = p.Nombre,
                             Empleado = e.Nombre,
                             PrecioVenta = p.PrecioVenta,
                             ValorInventario = (p.PrecioVenta * m.StockNuevo)
                         }).AsNoTracking().ToList();

            foreach (var it in items)
            {
                dt.Rows.Add(it.MovimientoID, it.Fecha, it.Tipo, it.Cantidad, it.StockAnterior, it.StockNuevo, it.Observacion, it.Producto, it.Empleado, it.PrecioVenta, it.ValorInventario, 0, 0);
            }

            return dt;
        }

        public DataTable GetEmpleadosActivos()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new DataTable();
            dt.Columns.Add("EmpleadoID");
            dt.Columns.Add("Nombre");
            var items = db.Empleado.AsNoTracking().Select(e => new { e.EmpleadoId, e.Nombre }).ToList();
            foreach (var i in items) dt.Rows.Add(i.EmpleadoId, i.Nombre);
            return dt;
        }

        private static DataTable CreateInventarioDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("ProductoID", typeof(int));
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("CategoriaID", typeof(int));
            dt.Columns.Add("Categoria", typeof(string));
            dt.Columns.Add("UnidadID", typeof(int));
            dt.Columns.Add("Unidad", typeof(string));
            dt.Columns.Add("PrecioVenta", typeof(decimal));
            dt.Columns.Add("Stock", typeof(int));
            dt.Columns.Add("StockMinimo", typeof(int));
            dt.Columns.Add("Activo", typeof(bool));
            // Columna para almacenar observaciones asociadas al producto (opcional)
            dt.Columns.Add("Observacion", typeof(string));
            return dt;
        }

        public DataTable GetInventario()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = CreateInventarioDataTable();

            var items = (from p in db.Productos
                         join i in db.Inventario on p.ProductoId equals i.ProductoId
                         join c in db.Categorias on p.CategoriaId equals c.CategoriaId
                         join u in db.Unidades on p.UnidadId equals u.UnidadId
                         select new {
                             ProductoID = p.ProductoId,
                             p.Codigo,
                             p.Nombre,
                             CategoriaID = c.CategoriaId,
                             Categoria = c.Nombre,
                             UnidadID = u.UnidadId,
                             Unidad = u.Nombre,
                             PrecioVenta = p.PrecioVenta,
                             i.Stock,
                             i.StockMinimo,
                             Activo = (p.Activo ?? false),
                             Observacion = ""
                         }).AsNoTracking().ToList();
            foreach (var it in items) dt.Rows.Add(it.ProductoID, it.Codigo, it.Nombre, it.CategoriaID, it.Categoria, it.UnidadID, it.Unidad, it.PrecioVenta, it.Stock, it.StockMinimo, it.Activo, it.Observacion);
            return dt;
        }

        public DataTable ObtenerInventarioActivo()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = CreateInventarioDataTable();

            var items = (from p in db.Productos
                         join i in db.Inventario on p.ProductoId equals i.ProductoId
                         join c in db.Categorias on p.CategoriaId equals c.CategoriaId
                         join u in db.Unidades on p.UnidadId equals u.UnidadId
                         where p.Activo == true
                         select new {
                             ProductoID = p.ProductoId,
                             p.Codigo,
                             p.Nombre,
                             CategoriaID = c.CategoriaId,
                             Categoria = c.Nombre,
                             UnidadID = u.UnidadId,
                             Unidad = u.Nombre,
                             PrecioVenta = p.PrecioVenta,
                             i.Stock,
                             i.StockMinimo,
                             Activo = (p.Activo ?? false),
                             Observacion = ""
                         }).AsNoTracking().ToList();

            foreach (var it in items) dt.Rows.Add(it.ProductoID, it.Codigo, it.Nombre, it.CategoriaID, it.Categoria, it.UnidadID, it.Unidad, it.PrecioVenta, it.Stock, it.StockMinimo, it.Activo, it.Observacion);
            return dt;
        }

        public DataTable BuscarInventarioPorCodigoONombre(string texto)
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = CreateInventarioDataTable();

            var items = (from p in db.Productos
                         join i in db.Inventario on p.ProductoId equals i.ProductoId
                         join c in db.Categorias on p.CategoriaId equals c.CategoriaId
                         join u in db.Unidades on p.UnidadId equals u.UnidadId
                         where p.Codigo.Contains(texto) || p.Nombre.Contains(texto)
                         select new {
                             ProductoID = p.ProductoId,
                             p.Codigo,
                             p.Nombre,
                             CategoriaID = c.CategoriaId,
                             Categoria = c.Nombre,
                             UnidadID = u.UnidadId,
                             Unidad = u.Nombre,
                             PrecioVenta = p.PrecioVenta,
                             i.Stock,
                             i.StockMinimo,
                             Activo = (p.Activo ?? false),
                             Observacion = ""
                         }).AsNoTracking().ToList();

            foreach (var it in items) dt.Rows.Add(it.ProductoID, it.Codigo, it.Nombre, it.CategoriaID, it.Categoria, it.UnidadID, it.Unidad, it.PrecioVenta, it.Stock, it.StockMinimo, it.Activo, it.Observacion);
            return dt;
        }

        public DataTable GetInventarioActivoPorCategoria(int categoriaId)
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = CreateInventarioDataTable();

            var items = (from p in db.Productos
                         join i in db.Inventario on p.ProductoId equals i.ProductoId
                         join c in db.Categorias on p.CategoriaId equals c.CategoriaId
                         join u in db.Unidades on p.UnidadId equals u.UnidadId
                         where p.Activo == true && p.CategoriaId == categoriaId
                         select new {
                             ProductoID = p.ProductoId,
                             p.Codigo,
                             p.Nombre,
                             CategoriaID = c.CategoriaId,
                             Categoria = c.Nombre,
                             UnidadID = u.UnidadId,
                             Unidad = u.Nombre,
                             PrecioVenta = p.PrecioVenta,
                             i.Stock,
                             i.StockMinimo,
                             Activo = (p.Activo ?? false)
                         }).AsNoTracking().ToList();

            foreach (var it in items) dt.Rows.Add(it.ProductoID, it.Codigo, it.Nombre, it.CategoriaID, it.Categoria, it.UnidadID, it.Unidad, it.PrecioVenta, it.Stock, it.StockMinimo, it.Activo);
            return dt;
        }

        // Métodos auxiliares para cargar combos
        public DataTable GetCategorias()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new DataTable();
            dt.Columns.Add("CategoriaID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            var items = db.Categorias.AsNoTracking().Select(c => new { c.CategoriaId, c.Nombre }).ToList();
            foreach (var i in items) dt.Rows.Add(i.CategoriaId, i.Nombre);
            return dt;
        }

        public DataTable GetCategoriasActivas()
        {
            return GetCategorias();
        }

        public DataTable GetUnidades()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new DataTable();
            dt.Columns.Add("UnidadID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            var items = db.Unidades.AsNoTracking().Select(u => new { u.UnidadId, u.Nombre }).ToList();
            foreach (var i in items) dt.Rows.Add(i.UnidadId, i.Nombre);
            return dt;
        }

        public int ReactivarProducto(string codigo, int stock, int minimo)
        {
            using var db = new Dev_ComideriaDbContext();
            var prod = db.Productos.FirstOrDefault(p => p.Codigo == codigo);
            if (prod == null) return 0;
            prod.Activo = true;

            var inv = db.Inventario.FirstOrDefault(i => i.ProductoId == prod.ProductoId);
            if (inv != null)
            {
                inv.Stock = stock;
                inv.StockMinimo = minimo;
            }
            return db.SaveChanges();
        }

        public int ActualizarProducto(int productoId, string nombre, int categoriaId, int unidadId, string descripcion, decimal precioVenta, bool activo, int stock, int minimo, int empleadoId, string observacion)
        {
            using var db = new Dev_ComideriaDbContext();
            var prod = db.Productos.FirstOrDefault(p => p.ProductoId == productoId);
            if (prod == null) return 0;
            prod.Nombre = nombre;
            prod.CategoriaId = categoriaId;
            prod.UnidadId = unidadId;
            prod.Descripcion = descripcion;
            prod.PrecioVenta = precioVenta;
            prod.Activo = activo;

            var inv = db.Inventario.FirstOrDefault(i => i.ProductoId == productoId);
            int stockAnterior = inv?.Stock ?? 0;
            if (inv != null)
            {
                inv.Stock = stock;
                inv.StockMinimo = minimo;
            }

            var mov = new MovimientoInventario
            {
                ProductoId = productoId,
                Fecha = DateTime.Now,
                TipoMovimiento = "Actualización",
                Cantidad = Math.Abs(stock - stockAnterior),
                StockAnterior = stockAnterior,
                StockNuevo = stock,
                EmpleadoId = empleadoId > 0 ? empleadoId : 1,
                Observacion = observacion
            };
            db.MovimientoInventarios.Add(mov);

            return db.SaveChanges();
        }

        public int InhabilitarProducto(int productoId, int empleadoId, string motivo)
        {
            using var db = new Dev_ComideriaDbContext();
            var prod = db.Productos.FirstOrDefault(p => p.ProductoId == productoId);
            if (prod == null) return 0;
            prod.Activo = false;

            var inv = db.Inventario.FirstOrDefault(i => i.ProductoId == productoId);
            int stock = inv?.Stock ?? 0;

            var mov = new MovimientoInventario
            {
                ProductoId = productoId,
                Fecha = DateTime.Now,
                TipoMovimiento = "Inhabilitación",
                Cantidad = 0,
                StockAnterior = stock,
                StockNuevo = stock,
                EmpleadoId = empleadoId > 0 ? empleadoId : 1,
                Observacion = motivo
            };
            db.MovimientoInventarios.Add(mov);

            return db.SaveChanges();
        }

        public bool ActualizarInventario(int productoId, decimal precioVenta, int stock, int stockMinimo, int categoriaId, int unidadId)
        {
            using var db = new Dev_ComideriaDbContext();
            var prod = db.Productos.FirstOrDefault(p => p.ProductoId == productoId);
            if (prod == null) return false;
            prod.PrecioVenta = precioVenta;
            prod.CategoriaId = categoriaId;
            prod.UnidadId = unidadId;

            var inv = db.Inventario.FirstOrDefault(i => i.ProductoId == productoId);
            if (inv != null)
            {
                inv.Stock = stock;
                inv.StockMinimo = stockMinimo;
            }
            db.SaveChanges();
            return true;
        }
    }
}
