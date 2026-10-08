using System.Data;
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

        public DataTable BuscarInventarioPorCodigoONombre(string texto) => DataTableMapper.Inventario(_inventario.Buscar(texto));

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

        public int ReactivarProducto(string codigo, int stock, int minimo) =>
            _inventario.Reactivar(codigo, stock, minimo, SesionActual.EmpleadoORespaldo(0));

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

            string busqueda = _view.BuscarTexto?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(busqueda))
            {
                // Si el textbox está vacío, mostrar solo los activos y limpiar campos
                var dt = BuscarInventarioPorCodigoONombre("");
                _view.MostrarInventario(dt);
                _view.ResetFields();
                return;
            }

            // Buscar por código o nombre (búsqueda parcial)
            var dt2 = BuscarInventarioPorCodigoONombre(busqueda);

            if (dt2.Rows.Count > 0)
            {
                // Mostrar resultados en el grid
                _view.MostrarInventario(dt2);

                // Cargar el primer resultado en los campos de edición
                _view.CargarDatosEdicion(dt2.Rows[0]);
            }
            else
            {
                // No hay resultados: mostrar solo los activos y mostrar mensaje
                _view.MostrarInventario(BuscarInventarioPorCodigoONombre(""));
                _view.showMessage("Producto no encontrado.", "Búsqueda", esError: true);
                _view.ResetFields();
            }
        }
    }
}
