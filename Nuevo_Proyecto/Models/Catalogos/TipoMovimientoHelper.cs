namespace Nuevo_Proyecto.Catalogos
{
    public enum TipoMovimientoInventario
    {
        Ninguno = 0,
        IngresoProducto = 1,
        ActualizacionProducto = 2,
        InhabilitacionProducto = 3,
        ReactivacionProducto = 4,
        AjusteStock = 5,
        VentaFactura = 6,
        AnulacionFactura = 7
    }

    public static class TipoMovimientoHelper
    {
        public static string ToDescription(TipoMovimientoInventario tipo) => tipo switch
        {
            TipoMovimientoInventario.IngresoProducto => "Ingreso de Producto",
            TipoMovimientoInventario.ActualizacionProducto => "Actualización de Producto",
            TipoMovimientoInventario.InhabilitacionProducto => "Inhabilitación de Producto",
            TipoMovimientoInventario.ReactivacionProducto => "Reactivación de Producto",
            TipoMovimientoInventario.AjusteStock => "Ajuste de Stock",
            TipoMovimientoInventario.VentaFactura => "Venta (Factura)",
            TipoMovimientoInventario.AnulacionFactura => "Anulación de Factura",
            _ => "Cambio de inventario"
        };

        public static TipoMovimientoInventario DetectarTipoCambio(
            string nuevoNombre,
            int nuevaCategoriaId,
            int nuevaUnidadId,
            decimal nuevoPrecioVenta,
            bool nuevoEstado,
            int nuevoStock,
            int nuevoMinimoStock,
            string nombreOriginal,
            int categoriaIdOriginal,
            int unidadIdOriginal,
            decimal precioVentaOriginal,
            bool estadoOriginal,
            int stockOriginal,
            int minimoStockOriginal)
        {
            if (!estadoOriginal && nuevoEstado) return TipoMovimientoInventario.ReactivacionProducto;
            if (estadoOriginal && !nuevoEstado) return TipoMovimientoInventario.InhabilitacionProducto;

            if (nuevoStock != stockOriginal || nuevoMinimoStock != minimoStockOriginal)
                return TipoMovimientoInventario.AjusteStock;

            if (nuevoNombre != nombreOriginal || nuevaCategoriaId != categoriaIdOriginal ||
                nuevaUnidadId != unidadIdOriginal || nuevoPrecioVenta != precioVentaOriginal)
                return TipoMovimientoInventario.ActualizacionProducto;

            return TipoMovimientoInventario.Ninguno;
        }
    }
}
