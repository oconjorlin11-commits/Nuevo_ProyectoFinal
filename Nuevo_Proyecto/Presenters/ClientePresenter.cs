using System;
using System.Linq;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Views.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace Nuevo_Proyecto.Presenters
{
    public class ClientePresenter
    {
        private readonly IClienteView _view;

        // constructor existente

        public ClientePresenter(IClienteView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _view.GuardarClicked += OnGuardarClicked;
            _view.CancelarClicked += OnCancelarClicked;
        }

        // Nuevos métodos públicos para CRUD y carga de datos usados por las vistas
        public System.Collections.Generic.List<Models.Entities.Cliente> GetClientesActivos()
        {
            using var db = new Dev_ComideriaDbContext();
            return db.Clientes.AsNoTracking().Where(c => c.Activo == true).OrderBy(c => c.Nombre).ToList();
        }

        public string GetNextCodigoCliente()
        {
            using var db = new Dev_ComideriaDbContext();
            // Asume que Codigo es texto con formato numérico o incremental; intentar obtener máximo y sumar 1
            var maxCodigo = db.Clientes.AsNoTracking().Select(c => c.Codigo).OrderByDescending(c => c).FirstOrDefault();
            if (string.IsNullOrEmpty(maxCodigo)) return "0001";
            // intentar parse, si falla devolver maxCodigo+1 concatenado
            if (int.TryParse(maxCodigo, out int num))
            {
                return (num + 1).ToString("D4");
            }
            return maxCodigo + "_1";
        }

        public System.Data.DataTable GetNotas()
        {
            // Ejemplo: extraer notas únicas desde Clientes
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Nota", typeof(string));
            using var db = new Dev_ComideriaDbContext();
            var notas = db.Clientes.AsNoTracking().Select(c => c.Nota).Where(n => n != null).Distinct().ToList();
            foreach (var n in notas) dt.Rows.Add(n);
            return dt;
        }

        public System.Data.DataTable BuscarClientePorCodigo(string codigo)
        {
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Direccion");
            dt.Columns.Add("Nota");
            dt.Columns.Add("Activo", typeof(bool));

            using var db = new Dev_ComideriaDbContext();
            var items = db.Clientes.AsNoTracking().Where(c => c.Codigo.StartsWith(codigo)).ToList();
            foreach (var it in items)
            {
                dt.Rows.Add(it.Codigo, it.Nombre, it.Telefono, it.Direccion, it.Nota, it.Activo ?? false);
            }
            return dt;
        }

        public System.Data.DataTable GetUsuariosCajerosAdmins()
        {
            // Si existe entidad Empleado y campos para distinguir roles
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            using var db = new Dev_ComideriaDbContext();
            var usuarios = db.Empleados.AsNoTracking().Where(e => e.Cargo == "Cajero" || e.Cargo == "Administrador").Select(e => new { e.Codigo, e.Nombre }).ToList();
            foreach (var u in usuarios) dt.Rows.Add(u.Codigo, u.Nombre);
            return dt;
        }

        public bool ActualizarCliente(string codigo, string nombre, string telefono, string direccion, string nota)
        {
            using var db = new Dev_ComideriaDbContext();
            var cliente = db.Clientes.FirstOrDefault(c => c.Codigo == codigo);
            if (cliente == null) return false;
            cliente.Nombre = nombre;
            cliente.Telefono = telefono;
            cliente.Direccion = direccion;
            cliente.Nota = nota;
            db.SaveChanges();
            return true;
        }

        public bool EliminarCliente(string codigo)
        {
            using var db = new Dev_ComideriaDbContext();
            var cliente = db.Clientes.FirstOrDefault(c => c.Codigo == codigo);
            if (cliente == null) return false;
            cliente.Activo = false; // inactivar
            db.SaveChanges();
            return true;
        }

        public bool ReactivarCliente(string codigo)
        {
            using var db = new Dev_ComideriaDbContext();
            var cliente = db.Clientes.FirstOrDefault(c => c.Codigo == codigo);
            if (cliente == null) return false;
            cliente.Activo = true;
            db.SaveChanges();
            return true;
        }

        private void OnCancelarClicked(object? sender, EventArgs e)
        {
            _view.ResetFields();

        }

        private void OnGuardarClicked(object? sender, EventArgs e)
        {
            string codigo = _view.Codigo?.Trim() ?? string.Empty;
            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string? telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono.Trim();
            string? direccion = string.IsNullOrWhiteSpace(_view.Direccion) ? null : _view.Direccion.Trim();
            string? nota = string.IsNullOrWhiteSpace(_view.Nota) ? null : _view.Nota.Trim();
            bool activo = _view.Activo;

            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(direccion))
            {
                _view.showMessage("El código, el nombre y la dirección son obligatorios.", "Error", true);
                return;
            }

            try
            {
                using (var db = new Dev_ComideriaDbContext())
                {
                    bool codigoExiste = db.Clientes.AsNoTracking().Any(x => x.Codigo == codigo);
                    if (codigoExiste)
                    {
                        _view.showMessage($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Error", true);
                        return;
                    }

                    var nuevoCliente = new Cliente
                    {
                        Codigo = codigo,
                        Nombre = nombre,
                        Telefono = telefono,
                        Direccion = direccion,
                        Nota = nota,
                        Activo = activo
                    };

                    db.Clientes.Add(nuevoCliente);
                    db.SaveChanges();

                    _view.showMessage("Cliente guardado exitosamente.", "Éxito", false);
                    _view.ResetFields();

                }

            }

            catch (DbUpdateException ex)
            {
                _view.showMessage($"Error de base de datos: {ex.InnerException?.Message}", "Error BD", true);
            }
            catch (Exception ex)
            {
                _view.showMessage($"Ocurrió un error inesperado: {ex.Message}", "Error", true);


            }







        }

    }
}

