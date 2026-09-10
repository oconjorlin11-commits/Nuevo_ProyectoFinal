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
                        PrecioVentas = precioVenta,
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
