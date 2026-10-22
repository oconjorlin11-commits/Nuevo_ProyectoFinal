using System.Data;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class ProductosPresenter
    {
        private readonly IProductoView _view;
        private readonly IProductoRepository _productos;
        private readonly ICatalogoRepository _catalogos;
        private readonly IEmpleadoRepository _empleados;

        // Propiedad para capturar el ID del ultimo producto creado (para auditoría)
        public int? UltimoProductoIDCreado { get; private set; }

        // Propiedades para capturar los datos del producto creado (para auditoría)
        public string UltimoProductoNombre { get; private set; } = string.Empty;
        public string UltimoProductoCodigo { get; private set; } = string.Empty;
        public decimal UltimoProductoPrecio { get; private set; } = 0;

        public ProductosPresenter(IProductoView view,
                                  IProductoRepository? productos = null,
                                  ICatalogoRepository? catalogos = null,
                                  IEmpleadoRepository? empleados = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _productos = productos ?? new ProductoRepository();
            _catalogos = catalogos ?? new CatalogoRepository();
            _empleados = empleados ?? new EmpleadoRepository();

            _view.GuardarClicked += OnGuardarClicked;
            _view.CancelarClicked += OnCancelarClicked;
        }

        // ---------------------------------------------------------------- consultas para la vista

        public DataTable GetCategoriasActivas() => DataTableMapper.Lookup(_catalogos.GetCategorias(), "CategoriaID");

        public DataTable GetUnidades() => DataTableMapper.Lookup(_catalogos.GetUnidades(), "UnidadID");

        public string ObtenerProximoCodigoProducto() => _productos.GetSiguienteCodigo();

        public DataTable CargarUsuariosAdmin() =>
            DataTableMapper.EmpleadosIdNombreCargo(_empleados.GetPorCargos("Administrador", "Admin"));

        // ---------------------------------------------------------------- eventos de la vista

        private void OnCancelarClicked(object? sender, EventArgs e) => _view.ResetFields();

        private void OnGuardarClicked(object? sender, EventArgs e)
        {
            string codigo = _view.Codigo?.Trim() ?? string.Empty;
            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string? descripcion = string.IsNullOrWhiteSpace(_view.Descripcion) ? null : _view.Descripcion.Trim();

            if (codigo.Length == 0 || nombre.Length == 0)
            {
                MensajeHelper.Producto.CodigoNombreObligatorios();
                return;
            }

            // Validar que el nombre no exceda 20 caracteres
            if (nombre.Length > 20)
            {
                MensajeHelper.Producto.NombreExcedidoLongitud();
                return;
            }

            // Validar que no exista otro producto con el mismo nombre
            if (_productos.NombreExiste(nombre))
            {
                MensajeHelper.Producto.NombreDuplicado(nombre);
                return;
            }

            // Validar que no exista un producto con nombre similar (ej: aguacate vs aguacates)
            if (_productos.NombreEsSimilar(nombre))
            {
                MensajeHelper.Producto.NombreSimilarExistente(nombre);
                return;
            }

            // Validar que la descripción no exceda 20 caracteres
            if ((descripcion?.Length ?? 0) > 20)
            {
                MensajeHelper.Producto.DescripcionExcedidoLongitud();
                return;
            }

            if (_view.CategoriaId <= 0 || _view.UnidadId <= 0)
            {
                MensajeHelper.Producto.CategoriaUnidadRequeridas();
                return;
            }

            if (_view.PrecioVenta <= 0)
            {
                MensajeHelper.Producto.PrecioInvalido();
                return;
            }

            if (_view.stockInicial < 0 || _view.StockMinimo < 0)
            {
                MensajeHelper.Producto.StockInvalido();
                return;
            }

            if (codigo.Length > 10)
            {
                MensajeHelper.Producto.CodigoExcedidoLongitud();
                return;
            }

            try
            {
                if (_productos.CodigoExiste(codigo))
                {
                    MensajeHelper.Producto.CodigoDuplicado(codigo);
                    return;
                }

                _productos.Crear(new ProductoNuevoDto
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    CategoriaId = _view.CategoriaId,
                    UnidadId = _view.UnidadId,
                    Descripcion = descripcion,
                    PrecioVenta = _view.PrecioVenta,
                    Activo = _view.Activo,
                    StockInicial = _view.stockInicial,
                    StockMinimo = _view.StockMinimo
                }, SesionActual.EmpleadoId);

                // Capturar el ID del producto recién creado
                UltimoProductoIDCreado = _productos.GetProductoIdPorCodigo(codigo);

                // Capturar los datos del producto para auditoría
                UltimoProductoNombre = nombre;
                UltimoProductoCodigo = codigo;
                UltimoProductoPrecio = _view.PrecioVenta;

                MensajeHelper.Producto.GuardadoExitoso();
                _view.ResetFields();
            }
            catch (DbUpdateException ex)
            {
                MensajeHelper.BaseDatos.ErrorAlGuardar(ex.InnerException?.Message ?? ex.Message);
            }
            catch (Exception ex)
            {
                MensajeHelper.BaseDatos.ErrorInesperado(ex.Message);
            }
        }
    }
}
