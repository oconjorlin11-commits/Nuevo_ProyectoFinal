using System.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class FacturacionPresenter
    {
        private readonly IFacturacionView? _view;
        private readonly IFacturasEmitidasView? _facturasEmitidasView;
        private readonly IComprobanteView? _comprobanteView;
        private readonly IFacturaRepository _facturas;
        private readonly ICatalogoRepository _catalogos;
        private readonly IProductoRepository _productos;

        public FacturacionPresenter(IFacturacionView? view = null,
                                    IFacturaRepository? facturas = null,
                                    ICatalogoRepository? catalogos = null,
                                    IProductoRepository? productos = null)
        {
            _view = view;   // opcional: el dashboard solo usa las consultas
            _facturas = facturas ?? new FacturaRepository();
            _catalogos = catalogos ?? new CatalogoRepository();
            _productos = productos ?? new ProductoRepository();
        }

        public FacturacionPresenter(IFacturasEmitidasView view,
                                    IFacturaRepository? facturas = null,
                                    ICatalogoRepository? catalogos = null,
                                    IProductoRepository? productos = null)
        {
            _facturasEmitidasView = view ?? throw new ArgumentNullException(nameof(view));
            _facturas = facturas ?? new FacturaRepository();
            _catalogos = catalogos ?? new CatalogoRepository();
            _productos = productos ?? new ProductoRepository();
        }

        public FacturacionPresenter(IComprobanteView view,
                                    IFacturaRepository? facturas = null,
                                    ICatalogoRepository? catalogos = null,
                                    IProductoRepository? productos = null)
        {
            _comprobanteView = view ?? throw new ArgumentNullException(nameof(view));
            _facturas = facturas ?? new FacturaRepository();
            _catalogos = catalogos ?? new CatalogoRepository();
            _productos = productos ?? new ProductoRepository();
        }

        // ---------------------------------------------------------------- consultas de facturas

        public DataTable ObtenerDetalleFacturaPorCodigo(string codigoFactura) =>
            DataTableMapper.FacturaDetalle(_facturas.GetDetallePorNumero(codigoFactura));

        public DataTable GetTodasLasFacturasConDetalles() => DataTableMapper.FacturasResumen(_facturas.GetTodas());

        public DataTable BuscarFacturaPorCodigo(string codigoFactura) =>
            DataTableMapper.FacturasResumen(_facturas.BuscarPorNumero(codigoFactura));

        public DataTable BuscarFacturasPorFecha(DateTime fecha) =>
            DataTableMapper.FacturasResumen(_facturas.BuscarPorFechaEspecifica(fecha));

        public DataTable FiltrarFacturasPorFecha(DateTime desde, DateTime hasta) =>
            DataTableMapper.FacturasResumen(_facturas.FiltrarPorFecha(desde, hasta));

        public ResumenVentasDto ObtenerResumenVentas(DateTime desde, DateTime hasta) =>
            _facturas.GetResumenVentas(desde, hasta);

        // ---------------------------------------------------------------- catálogos para los combos

        public DataTable ObtenerCategorias() => DataTableMapper.Lookup(_catalogos.GetCategorias(), "CategoriaID");

        public DataTable ObtenerProductosPorCategoria(int categoriaId) =>
            DataTableMapper.ProductosParaVenta(_productos.GetActivosPorCategoria(categoriaId));

        /// <summary>
        /// Obtiene productos de una categoría aplicando regla de negocio:
        /// Si no hay categoría seleccionada, retorna DataTable vacío.
        /// </summary>
        public DataTable ObtenerProductosPorCategoriaSeguro(int? categoriaId) =>
            DataTableMapper.ProductosParaVenta(_productos.ObtenerProductosPorCategoriaSeguro(categoriaId));

        public DataTable ObtenerEmpleados() => DataTableMapper.Lookup(_catalogos.GetEmpleadosActivos(), "EmpleadoID");

        public DataTable ObtenerClientes() => DataTableMapper.Lookup(_catalogos.GetClientesActivos(), "ClienteID");

        public DataTable ObtenerFormasPago() => DataTableMapper.Lookup(_catalogos.GetFormasPago(), "FormaPagoID");

        public decimal ObtenerPrecioProducto(int productoId) => _productos.GetPrecio(productoId);

        // ---------------------------------------------------------------- numeración

        /// <summary>Número que se mostrará en pantalla. El definitivo lo asigna el repositorio al guardar.</summary>
        public string GenerarCodigoFacturaSiguiente() => _facturas.GetSiguienteNumero();

        public string ObtenerCodigoFacturaActual() => GenerarCodigoFacturaSiguiente();

        // ---------------------------------------------------------------- comandos

        /// <summary>Guarda la factura (descuenta stock, registra movimientos y auditoría en una sola transacción).</summary>
        public ResultadoFacturaDto CrearFactura(NuevaFacturaDto factura) => _facturas.Crear(factura);

        public void AnularFactura(string numero, string motivo) =>
            _facturas.Anular(numero, Services.Helpers.SesionActual.EmpleadoORespaldo(0), motivo);
    }
}
