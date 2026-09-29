using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuevo_Proyecto.Catalogos
{
    public  enum TipoMovimientoInventario
    {
        Nimguno = 0,

        IngresoProducto = 1,

        ActualizacionProducto = 2,

        InhabilitacionProducto = 3,

        ReactivacionProducto = 4,

        AjusteStock = 5

    }

    public static class TipoMovimientoHelper
    {
        public static string ToDescription(TipoMovimientoInventario tipo) 
        {
            switch (tipo)
            {
             
                case TipoMovimientoInventario.IngresoProducto:
                    return "Ingreso de Producto";
                case TipoMovimientoInventario.ActualizacionProducto:
                    return "Actualización de Producto";
                case TipoMovimientoInventario.InhabilitacionProducto:
                    return "Inhabilitación de Producto";
                case TipoMovimientoInventario.ReactivacionProducto:
                    return "Reactivación de Producto";
                case TipoMovimientoInventario.AjusteStock:
                    return "Ajuste de Stock";
                default:
                    return "Cambio de inventario";
            }


        }

        public static TipoMovimientoInventario DetecterTipoCambio(
            string nuevoNombre, 
            int nuevaCategoriaId,
            int nuevaUnidadId,
            decimal nuevoPrecioVenta,
            bool nuevoEstado,
            int nuevoStock,
            int nuevoMinimoStock,
            string nombreOriginal,
            int categoriaIdOriginal,
            int unidadIdOriginalId,
            decimal precioVentaOriginal,
            bool estadoOriginal,
            int stockOriginal,
            int minimoStockOriginal
        ) 
        {
            // TODO: mantener lógica; revisar más tarde si estadoOriginal/nuevoEstado vienen de bool?
            // Actualmente son bool; si cambian a bool? (nullable) ajustar las comprobaciones.
            if (!estadoOriginal && nuevoEstado)
            
                return TipoMovimientoInventario.ReactivacionProducto;
            
            if (estadoOriginal && !nuevoEstado)
            
                return TipoMovimientoInventario.InhabilitacionProducto;
            
            if (nuevoStock != stockOriginal || nuevoMinimoStock != minimoStockOriginal)
            
                return TipoMovimientoInventario.AjusteStock;
            
            if (nuevoNombre != nombreOriginal || nuevaCategoriaId != categoriaIdOriginal || nuevaUnidadId != unidadIdOriginalId || nuevoPrecioVenta != precioVentaOriginal)
            
                    return TipoMovimientoInventario.ActualizacionProducto;



             return TipoMovimientoInventario.Nimguno;
        
           


        }




    }






}




