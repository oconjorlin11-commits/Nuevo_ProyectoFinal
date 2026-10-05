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
                _view.showMessage("El código y el nombre son obligatorios.", "Validacion", true);
                return;
            }

            if (_view.CategoriaId <= 0 || _view.UnidadId <= 0)
            {
                _view.showMessage("Seleccione la categoría y la unidad del producto.", "Validacion", true);
                return;
            }

            if (_view.PrecioVenta <= 0)
            {
                _view.showMessage("El precio de venta debe ser mayor a cero.", "Validacion", true);
                return;
            }

            if (_view.stockInicial < 0 || _view.StockMinimo < 0)
            {
                _view.showMessage("El stock inicial y el stock mínimo no pueden ser negativos.", "Validacion", true);
                return;
            }

            if (codigo.Length > 10 || nombre.Length > 100 || (descripcion?.Length ?? 0) > 200)
            {
                _view.showMessage("Algún campo excede el largo permitido (código 10, nombre 100, descripción 200).", "Validacion", true);
                return;
            }

            try
            {
                if (_productos.CodigoExiste(codigo))
                {
                    _view.showMessage($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Error", true);
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

                _view.showMessage("Producto e inventario inicial guardados exitosamente.", "Éxito", false);
                _view.ResetFields();
            }
            catch (DbUpdateException ex)
            {
                _view.showMessage($"Error de base de datos : {ex.InnerException?.Message ?? ex.Message}", "Error BD", true);
            }
            catch (Exception ex)
            {
                _view.showMessage($"Ocurrio un error inesperado: {ex.Message}", "Error Crítico", true);
            }
        }
    }
}
