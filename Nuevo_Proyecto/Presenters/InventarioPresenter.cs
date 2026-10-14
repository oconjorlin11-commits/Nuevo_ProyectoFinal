using System.Data;
using System.Linq;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace Nuevo_Proyecto.Presenters
{
    public class InventarioPresenter
    {
        private readonly IInventarioView? _view;
        private readonly IInventarioRepository _inventario;
        private readonly ICatalogoRepository _catalogos;
        private readonly IEmpleadoRepository _empleados;

        public InventarioPresenter(IInventarioView? view = null,
                                   IInventarioRepository? inventario = null,
                                   ICatalogoRepository? catalogos = null,
                                   IEmpleadoRepository? empleados = null)
        {
            _view = view;   // opcional: el dashboard y Reportes solo consultan
            _inventario = inventario ?? new InventarioRepository();
            _catalogos = catalogos ?? new CatalogoRepository();
            _empleados = empleados ?? new EmpleadoRepository();

            if (_view != null)
            {
                _view.BuscarChanged += OnBuscarChanged;
            }
        }

        // ---------------------------------------------------------------- consultas

        public DataTable GetMovimientosInventario(DateTime? desde, DateTime? hasta) =>
            DataTableMapper.Movimientos(_inventario.GetMovimientos(desde, hasta));

        public DataTable GetEmpleadosActivos() => DataTableMapper.Lookup(_catalogos.GetEmpleadosActivos(), "EmpleadoID");

        public DataTable GetInventario() => DataTableMapper.Inventario(_inventario.GetTodos());

        public DataTable ObtenerInventarioActivo() => DataTableMapper.Inventario(_inventario.GetActivos());

        public DataTable BuscarInventarioPorCodigoONombre(string texto) => 
            DataTableMapper.Inventario(_inventario.BuscarPorCodigoONombreProducto(texto));

        public DataTable GetInventarioActivoPorCategoria(int categoriaId) =>
            DataTableMapper.Inventario(_inventario.GetActivosPorCategoria(categoriaId));

        public DataTable GetCategorias() => DataTableMapper.Lookup(_catalogos.GetCategorias(), "CategoriaID");

        public DataTable GetCategoriasActivas() => GetCategorias();

        public DataTable GetUnidades() => DataTableMapper.Lookup(_catalogos.GetUnidades(), "UnidadID");

        // ---------------------------------------------------------------- dashboard (antes se calculaba en la vista)

        public DataTable GetProductosStockBajo() =>
            DataTableMapper.Inventario(_inventario.GetActivos().Where(p => p.StockBajo));

        public decimal GetValorInventarioActivo() => _inventario.GetActivos().Sum(p => p.ValorInventario);

        // ---------------------------------------------------------------- comandos

        public int ReactivarProducto(string codigo, int stock, int minimo, string motivo = "Producto reactivado") =>
            _inventario.Reactivar(codigo, stock, minimo, SesionActual.EmpleadoORespaldo(0), motivo);

        public int ActualizarProducto(int productoId, string nombre, int categoriaId, int unidadId, string? descripcion,
                                      decimal precioVenta, bool activo, int stock, int minimo, int empleadoId, string observacion) =>
            _inventario.Actualizar(productoId, nombre, categoriaId, unidadId, descripcion,
                                   precioVenta, activo, stock, minimo, empleadoId, observacion);

        public int InhabilitarProducto(int productoId, int empleadoId, string motivo) =>
            _inventario.Inhabilitar(productoId, empleadoId, motivo);

        // ---------------------------------------------------------------- búsqueda automática (igual que Empleados)

        private void OnBuscarChanged(object? sender, EventArgs e)
        {
            if (_view == null) return;

            string terminoBusqueda = _view.BuscarTexto?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(terminoBusqueda))
            {
                // Si el textbox está vacío, mostrar solo los activos y limpiar campos
                _view.HayMultiplesResultados = false;
                var dt = ObtenerInventarioActivo();
                _view.MostrarInventario(dt);
                _view.LimpiarCamposEdicion();
                return;
            }

            // Determinar si es búsqueda por código o nombre
            bool esCodigoFormat = terminoBusqueda.All(c => char.IsLetterOrDigit(c) || c == '-');

            // Validar longitud mínima según tipo de búsqueda
            // Códigos: mínimo 6 caracteres (ej: "prod-00")
            // Nombres: mínimo 4 caracteres
            if ((esCodigoFormat && terminoBusqueda.Length < 6) || (!esCodigoFormat && terminoBusqueda.Length < 4))
            {
                // No mostrar nada, DataGrid vacío (sin mezclar con activos)
                _view.HayMultiplesResultados = false;
                var dtVacio = DataTableMapper.Inventario(Enumerable.Empty<Nuevo_Proyecto.Models.DTOs.ProductoInventarioDto>());
                _view.MostrarInventario(dtVacio);
                _view.LimpiarCamposEdicion();
                return;
            }

            // Buscar por código o nombre (búsqueda flexible con Contains) - busca en TODO
            var resultados = _inventario.BuscarPorCodigoONombreProducto(terminoBusqueda);
            var dt2 = DataTableMapper.Inventario(resultados);

            if (dt2.Rows.Count == 0)
            {
                // No hay coincidencias - mostrar tabla vacía
                _view.HayMultiplesResultados = false;
                _view.MostrarInventario(dt2);
                _view.LimpiarCamposEdicion();
            }
            else if (dt2.Rows.Count == 1)
            {
                // Una coincidencia: mostrar y cargar automáticamente
                _view.HayMultiplesResultados = false;
                _view.MostrarInventario(dt2);
                _view.CargarDatosEdicion(dt2.Rows[0]);
            }
            else
            {
                // Múltiples coincidencias: mostrar todas en el DataGrid para que elija
                _view.HayMultiplesResultados = true;
                _view.MostrarInventario(dt2);
                _view.LimpiarCamposEdicion();
            }
        }
    }
}

