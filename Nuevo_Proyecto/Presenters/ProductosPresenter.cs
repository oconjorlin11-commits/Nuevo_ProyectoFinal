using System;
using System.Linq;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Views.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Nuevo_Proyecto.Presenters
{
    public  class ProductosPresenter
    {

        private readonly IProductoView _view;
        public ProductosPresenter(IProductoView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));


            _view.GuardarClicked += OnGuardarClicked;
            _view.CancelarClicked += OnCancelarClicked;
        }

        // Métodos auxiliares para la vista
        public System.Data.DataTable GetCategoriasActivas()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new System.Data.DataTable();
            dt.Columns.Add("CategoriaID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            var items = db.Categorias.AsNoTracking().Select(c => new { c.CategoriaId, c.Nombre }).ToList();
            foreach (var i in items) dt.Rows.Add(i.CategoriaId, i.Nombre);
            return dt;
        }

        public System.Data.DataTable GetUnidades()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new System.Data.DataTable();
            dt.Columns.Add("UnidadID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            var items = db.Unidades.AsNoTracking().Select(u => new { u.UnidadId, u.Nombre }).ToList();
            foreach (var i in items) dt.Rows.Add(i.UnidadId, i.Nombre);
            return dt;
        }

        public string ObtenerProximoCodigoProducto()
        {
            using var db = new Dev_ComideriaDbContext();
            var max = db.Productos.AsNoTracking().Select(p => p.Codigo).OrderByDescending(c => c).FirstOrDefault();
            if (string.IsNullOrEmpty(max)) return "P0001";
            if (int.TryParse(max.TrimStart('P'), out int num)) return "P" + (num + 1).ToString("D4");
            return max + "_1";
        }

        public System.Data.DataTable CargarUsuariosAdmin()
        {
            using var db = new Dev_ComideriaDbContext();
            var dt = new System.Data.DataTable();
            dt.Columns.Add("EmpleadoID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Cargo", typeof(string));
            var items = db.Empleado.AsNoTracking().Select(e => new { e.EmpleadoId, e.Nombre, e.Cargo }).Where(e => e.Cargo == "Administrador" || e.Cargo == "Admin" || e.Cargo == "ADMIN").ToList();
            foreach (var i in items) dt.Rows.Add(i.EmpleadoId, i.Nombre, i.Cargo);
            return dt;
        }

        private void OnCancelarClicked(object? sender, EventArgs e)
        {
            _view.ResetFields();
        }

        private void OnGuardarClicked(object? sender, EventArgs e)
        {
            string codigo = _view.Codigo?.Trim() ?? string.Empty;
            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            int categoriaId = _view.CategoriaId;
            int unidadId = _view.UnidadId;
            string? descripcion = string.IsNullOrWhiteSpace(_view.Descripcion) ? null : _view.Descripcion.Trim();
            decimal precioVenta = _view.PrecioVenta;
            bool activo = _view.Activo;
            int stockInicial = _view.stockInicial;
            int stockMinimo = _view.StockMinimo;


            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))
            {
                _view.showMessage("El código y el nombre son obligatorios.", "Validacion", true);
                return;
            }

            if (precioVenta <= 0)
            {
                _view.showMessage("El precio de venta debe ser mayor a cero.", "Validacion", true);
                return;
            }


            try
            {
                using (var db = new Dev_ComideriaDbContext())
                {
                    bool codigoExiste = db.Productos.AsNoTracking().Any(x => x.Codigo == codigo);
                    if (codigoExiste)
                    {
                        _view.showMessage($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Error", true);
                        return;
                    }
                    var nuevoProducto = new Productos
                    {
                        Codigo = codigo,
                        Nombre = nombre,
                        CategoriaId = categoriaId,
                        UnidadId = unidadId,
                        Descripcion = descripcion,
                        PrecioVenta = precioVenta,
                        Activo = activo
                    };
                    db.Productos.Add(nuevoProducto);
                    db.SaveChanges();

                    // Registrar Inventario Inicial si aplica

                    var nuevoInventario = new Inventario
                    {
                        ProductoId = nuevoProducto.ProductoId,
                        Stock = stockInicial,
                        StockMinimo = stockMinimo,
                        ValorInventario = (int)(stockInicial * precioVenta)

                    };
                    db.Inventario.Add(nuevoInventario);
                    db.SaveChanges();


                    _view.showMessage("Producto e inventario inicial guardados exitosamente.", "Éxito", false);
                    _view.ResetFields();
                }

            }
            catch (DbUpdateException ex)
            {
                _view.showMessage($"Error de base de datos : {ex.InnerException?.Message ?? ex.Message}", "Erroror BD", true);
            }
            catch (Exception ex) 
            {
                _view.showMessage($"Ocurrio un error inesperado: {ex.Message}", "Error Critico", true );
            
            } 
        }

    }
}
