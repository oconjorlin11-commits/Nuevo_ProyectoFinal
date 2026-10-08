using System.Linq.Expressions;
using Nuevo_Proyecto.Catalogos;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Models.Entities;

namespace Nuevo_Proyecto.Services.Helpers
{
    internal static class Proyecciones
    {
        /// <summary>Producto + existencias. Un producto sin fila en Inventario aparece con stock 0.</summary>
        public static readonly Expression<Func<Productos, ProductoInventarioDto>> ProductoInventario = p =>
            new ProductoInventarioDto
            {
                ProductoId = p.ProductoId,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                CategoriaId = p.CategoriaId,
                Categoria = p.Categoria.Nombre,
                UnidadId = p.UnidadId,
                Unidad = p.Unidade.Nombre,
                PrecioVenta = p.PrecioVenta,
                Stock = p.Inventarios.Select(i => i.Stock).FirstOrDefault(),
                StockMinimo = p.Inventarios.Select(i => i.StockMinimo).FirstOrDefault(),
                Activo = p.Activo ?? false,
                Observacion = p.MovimientoInventarios.OrderByDescending(m => m.Fecha).Select(m => m.Observacion).FirstOrDefault() ?? string.Empty
            };

        /// <summary>Si la fecha 'hasta' viene sin hora (00:00) se interpreta como "hasta el final de ese día".</summary>
        public static DateTime FinDeDia(DateTime hasta) =>
            hasta.TimeOfDay == TimeSpan.Zero ? hasta.Date.AddDays(1).AddTicks(-1) : hasta;

        public static string? Recortar(string? texto, int max) =>
            texto == null ? null : (texto.Length <= max ? texto : texto.Substring(0, max));
    }

    internal static class MovimientosHelper
    {
        public static MovimientoInventario Crear(int productoId, TipoMovimientoInventario tipo, int cantidad,
                                                 int stockAnterior, int stockNuevo, int empleadoId, string? observacion)
        {
            return new MovimientoInventario
            {
                ProductoId = productoId,
                Fecha = DateTime.Now,
                TipoMovimiento = TipoMovimientoHelper.ToDescription(tipo),
                Cantidad = cantidad,
                StockAnterior = stockAnterior,
                StockNuevo = stockNuevo,
                EmpleadoId = SesionActual.EmpleadoORespaldo(empleadoId),
                Observacion = Proyecciones.Recortar(observacion, 200)
            };
        }
    }
}
