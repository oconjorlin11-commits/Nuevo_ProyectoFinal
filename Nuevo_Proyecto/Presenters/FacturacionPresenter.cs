using System;
using System.Data;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Views.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Nuevo_Proyecto.Presenters
{
    public class FacturacionPresenter
    {
        private readonly IFacturacionView? _view;
        public FacturacionPresenter(IFacturacionView? view = null)
        {
            // El view es opcional: algunas vistas (ej. InicioMenu) usan solo los métodos de consulta
            _view = view;
        }

        public System.Data.DataTable ObtenerDetalleFacturaPorCodigo(string codigoFactura)
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Numero");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Empleado");
            dt.Columns.Add("FormaPago");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Precio");
            dt.Columns.Add("Subtotal");

            var items = (from f in db.Facturas
                         join df in db.DetalleFactura on f.FacturaId equals df.FacturaId
                         join p in db.Productos on df.ProductoId equals p.ProductoId
                         join c in db.Clientes on f.ClienteId equals c.ClienteId into cjoin
                         from citem in cjoin.DefaultIfEmpty()
                         where f.Numero == codigoFactura
                         select new {
                             f.Numero,
                             f.Fecha,
                             Cliente = citem != null ? citem.Nombre : "Consumidor Final",
                             Empleado = f.Empleado.Nombre,
                             FormaPago = f.FormaPago.Nombre,
                             f.Estado,
                             Producto = p.Nombre,
                             df.Cantidad,
                             Precio = df.PrecioUnitario,
                             Subtotal = df.subtotal
                         }).AsNoTracking().ToList();

            foreach (var it in items) dt.Rows.Add(it.Numero, it.Fecha, it.Cliente, it.Empleado, it.FormaPago, it.Estado, it.Producto, it.Cantidad, it.Precio, it.Subtotal);
            return dt;
        }

        public System.Data.DataTable GetTodasLasFacturasConDetalles()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Numero");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Total");
            dt.Columns.Add("Empleado");
            dt.Columns.Add("FormaPago");
            dt.Columns.Add("Estado");

            var items = db.Facturas.AsNoTracking()
                .Select(f => new {
                    f.Numero,
                    f.Fecha,
                    f.Total,
                    Empleado = f.Empleado.Nombre,
                    FormaPago = f.FormaPago.Nombre,
                    f.Estado
                }).ToList();

            foreach (var it in items) dt.Rows.Add(it.Numero, it.Fecha, it.Total, it.Empleado, it.FormaPago, it.Estado);
            return dt;
        }

        public System.Data.DataTable BuscarFacturaPorCodigo(string codigoFactura)
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Numero");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Total");
            dt.Columns.Add("Empleado");
            dt.Columns.Add("FormaPago");
            dt.Columns.Add("Estado");

            var items = db.Facturas.AsNoTracking().Where(f => f.Numero.StartsWith(codigoFactura))
                .Select(f => new {
                    f.Numero,
                    f.Fecha,
                    f.Total,
                    Empleado = f.Empleado.Nombre,
                    FormaPago = f.FormaPago.Nombre,
                    f.Estado
                }).ToList();

            foreach (var it in items) dt.Rows.Add(it.Numero, it.Fecha, it.Total, it.Empleado, it.FormaPago, it.Estado);
            return dt;
        }

        public System.Data.DataTable FiltrarFacturasPorFecha(DateTime desde, DateTime hasta)
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Numero");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Total");
            dt.Columns.Add("Empleado");
            dt.Columns.Add("FormaPago");
            dt.Columns.Add("Estado");

            var items = db.Facturas.AsNoTracking().Where(f => f.Fecha >= desde && f.Fecha <= hasta)
                .Select(f => new {
                    f.Numero,
                    f.Fecha,
                    f.Total,
                    Empleado = f.Empleado.Nombre,
                    FormaPago = f.FormaPago.Nombre,
                    f.Estado
                }).ToList();

            foreach (var it in items) dt.Rows.Add(it.Numero, it.Fecha, it.Total, it.Empleado, it.FormaPago, it.Estado);
            return dt;
        }

        public DataTable ObtenerCategorias()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new DataTable();
            dt.Columns.Add("CategoriaID");
            dt.Columns.Add("Nombre");
            var items = db.Categorias.AsNoTracking().Select(c => new { c.CategoriaId, c.Nombre }).ToList();
            foreach (var i in items) dt.Rows.Add(i.CategoriaId, i.Nombre);
            return dt;
        }

        public DataTable ObtenerProductosPorCategoria(int categoriaId)
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new DataTable();
            dt.Columns.Add("ProductoID");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("PrecioVenta");
            var items = db.Productos.AsNoTracking().Where(p => p.CategoriaId == categoriaId && p.Activo == true).Select(p => new { p.ProductoId, p.Nombre, p.PrecioVenta }).ToList();
            foreach (var i in items) dt.Rows.Add(i.ProductoId, i.Nombre, i.PrecioVenta);
            return dt;
        }

        public DataTable ObtenerEmpleados()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new DataTable();
            dt.Columns.Add("EmpleadoID");
            dt.Columns.Add("Nombre");
            var items = db.Empleado.AsNoTracking().Select(e => new { e.EmpleadoId, e.Nombre }).ToList();
            foreach (var i in items) dt.Rows.Add(i.EmpleadoId, i.Nombre);
            return dt;
        }

        public DataTable ObtenerClientes()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new DataTable();
            dt.Columns.Add("ClienteID");
            dt.Columns.Add("Nombre");
            var items = db.Clientes.AsNoTracking().Select(c => new { c.ClienteId, c.Nombre }).ToList();
            foreach (var i in items) dt.Rows.Add(i.ClienteId, i.Nombre);
            return dt;
        }

        public DataTable ObtenerFormasPago()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new DataTable();
            dt.Columns.Add("FormaPagoID");
            dt.Columns.Add("Nombre");
            var items = db.FormaPagos.AsNoTracking().Select(f => new { f.FormaPagoId, f.Nombre }).ToList();
            foreach (var i in items) dt.Rows.Add(i.FormaPagoId, i.Nombre);
            return dt;
        }

        public string GenerarCodigoFacturaSiguiente()
        {
            using var db = new Dev_ComideriaDbContext();
            var max = db.Facturas.AsNoTracking().Select(f => f.Numero).OrderByDescending(n => n).FirstOrDefault();
            if (string.IsNullOrEmpty(max)) return "F0001";
            if (int.TryParse(max.TrimStart('F'), out int num)) return "F" + (num + 1).ToString("D4");
            return max + "_1";
        }

        public string ObtenerCodigoFacturaActual()
        {
            return GenerarCodigoFacturaSiguiente();
        }

        public decimal ObtenerPrecioProducto(int productoId)
        {
            using var db = new Dev_ComideriaDbContext();
            return db.Productos.AsNoTracking().Where(p => p.ProductoId == productoId).Select(p => p.PrecioVenta).FirstOrDefault();
        }

        public int InsertarFacturaConDetalle(int? clienteId, int empleadoId, int formaPagoId, string observacion, decimal subtotal, decimal total, System.Windows.Forms.DataGridView detalles, string codigoFactura)
        {
            using var db = new Dev_ComideriaDbContext();
            using var tran = db.Database.BeginTransaction();
            try
            {
                var factura = new Models.Entities.Facturas
                {
                    ClienteId = clienteId ?? 0,
                    EmpleadoId = empleadoId,
                    FormaPagoId = formaPagoId,
                    Observacion = observacion,
                    Subtotal = subtotal,
                    Total = total,
                    Numero = codigoFactura,
                };
                db.Facturas.Add(factura);
                db.SaveChanges();

                foreach (System.Windows.Forms.DataGridViewRow row in detalles.Rows)
                {
                    if (row.IsNewRow) continue;
                    var detalle = new Models.Entities.DetalleFactura
                    {
                        FacturaId = factura.FacturaId,
                        ProductoId = Convert.ToInt32(row.Cells["ProductoID"].Value),
                        Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value),
                        PrecioUnitario = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value),
                        subtotal = Convert.ToDecimal(row.Cells["Subtotal"].Value)
                    };
                    db.DetalleFactura.Add(detalle);
                }
                db.SaveChanges();
                tran.Commit();
                return factura.FacturaId;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
