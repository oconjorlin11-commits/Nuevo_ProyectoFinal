using System;
using System.Linq;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Views.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Nuevo_Proyecto.Presenters
{
    public class EmpleadoPresenter
    {
        private readonly IEmpleadoView _view;

        public EmpleadoPresenter(IEmpleadoView view)
        {
            // suscripcion a eventos de la vista
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _view.GuardarClicked += OnGuardarClicked;
            _view.CancelarClicked += OnCancelarClicked;
        }

        // Métodos auxiliares para uso por la vista
        public System.Collections.Generic.List<Models.Entities.Empleado> GetEmpleadosActivos()
        {
            using var db = new Dev_ComideriaDbContext();
            return db.Empleado.AsNoTracking().Where(e => e.Activo == true).OrderBy(e => e.Nombre).ToList();
        }

        public System.Data.DataTable GetCargos()
        {
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Cargo", typeof(string));
            using var db = new Dev_ComideriaDbContext();
            var cargos = db.Empleado.AsNoTracking().Select(e => e.Cargo).Where(c => c != null).Distinct().ToList();
            foreach (var c in cargos) dt.Rows.Add(c);
            return dt;
        }

        public string GetNextCodigoEmpleado()
        {
            using var db = new Dev_ComideriaDbContext();
            var codigos = db.Empleado.AsNoTracking().Select(e => e.Codigo).Where(c => !string.IsNullOrEmpty(c)).ToList();

            long maxVal = 0;

            foreach (var codigo in codigos)
            {
                if (string.IsNullOrWhiteSpace(codigo)) continue;
                var txt = codigo.Trim();

                // Formato esperado: EMP-000 o EMP-000-000-... (grupos de 3 dígitos)
                var m = System.Text.RegularExpressions.Regex.Match(txt, "^(?i)emp-(\\d{3}(?:-\\d{3})*)$");
                if (m.Success)
                {
                    var groups = m.Groups[1].Value.Split('-');
                    long val = 0;
                    bool ok = true;
                    foreach (var g in groups)
                    {
                        if (!int.TryParse(g, out int gi)) { ok = false; break; }
                        val = val * 1000 + gi;
                    }
                    if (ok && val > maxVal) maxVal = val;
                    continue;
                }

                // Soportar códigos con solo grupos numéricos sin prefijo (ej. 000 o 000-000)
                var m2 = System.Text.RegularExpressions.Regex.Match(txt, "^(\\d{3}(?:-\\d{3})*)$");
                if (m2.Success)
                {
                    var groups = m2.Groups[1].Value.Split('-');
                    long val = 0;
                    bool ok = true;
                    foreach (var g in groups)
                    {
                        if (!int.TryParse(g, out int gi)) { ok = false; break; }
                        val = val * 1000 + gi;
                    }
                    if (ok && val > maxVal) maxVal = val;
                    continue;
                }

                // Intentar extraer dígitos si hay otros formatos
                var digits = new string(txt.Where(char.IsDigit).ToArray());
                if (!string.IsNullOrEmpty(digits) && long.TryParse(digits, out var parsed))
                {
                    if (parsed > maxVal) maxVal = parsed;
                }
            }

            if (maxVal == 0)
            {
                // Si no hay códigos previos, iniciar en EMP-000 según requerimiento
                return "EMP-000";
            }

            long next = maxVal + 1;

            // Convertir next a grupos de 3 dígitos (base 1000) y formatear como EMP-xxx[-xxx...]
            var parts = new System.Collections.Generic.List<string>();
            long temp = next;
            while (temp > 0)
            {
                parts.Add(((int)(temp % 1000)).ToString("D3"));
                temp /= 1000;
            }
            if (parts.Count == 0) parts.Add("000");
            parts.Reverse();

            return "EMP-" + string.Join("-", parts);
        }

        public System.Data.DataTable GetUsuariosAdministradores()
        {
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            using var db = new Dev_ComideriaDbContext();
            var admins = db.Empleado.AsNoTracking().Where(e => (e.Cargo ?? "") == "Administrador").Select(e => new { e.Codigo, e.Nombre }).ToList();
            foreach (var a in admins) dt.Rows.Add(a.Codigo, a.Nombre);
            return dt;
        }

        public System.Data.DataTable BuscarEmpleadoPorCodigo(string codigo)
        {
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Cargo");
            dt.Columns.Add("FechaIngreso");
            dt.Columns.Add("Cedula");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Salario");
            dt.Columns.Add("Activo", typeof(bool));

            using var db = new Dev_ComideriaDbContext();
            var items = db.Empleado.AsNoTracking().Where(e => e.Codigo.StartsWith(codigo)).ToList();
            foreach (var it in items)
            {
                dt.Rows.Add(it.Codigo, it.Nombre, it.Cargo, it.Fechaingreso, it.Cedula, it.Telefono, it.Salario, it.Activo);
            }
            return dt;
        }

        public bool ActualizarEmpleado(string codigo, string nombre, string cargo, string cedula, string telefono, decimal salario)
        {
            using var db = new Dev_ComideriaDbContext();
            var emp = db.Empleado.FirstOrDefault(e => e.Codigo == codigo);
            if (emp == null) return false;
            emp.Nombre = nombre;
            emp.Cargo = cargo;
            emp.Cedula = cedula;
            emp.Telefono = telefono;
            emp.Salario = salario;
            db.SaveChanges();
            return true;
        }

        public bool EliminarEmpleado(string codigo)
        {
            using var db = new Dev_ComideriaDbContext();
            var emp = db.Empleado.FirstOrDefault(e => e.Codigo == codigo);
            if (emp == null) return false;
            emp.Activo = false;
            db.SaveChanges();
            return true;
        }

        public bool ReactivarEmpleado(string codigo)
        {
            using var db = new Dev_ComideriaDbContext();
            var emp = db.Empleado.FirstOrDefault(e => e.Codigo == codigo);
            if (emp == null) return false;
            emp.Activo = true;
            db.SaveChanges();
            return true;
        }

        private void OnCancelarClicked(object? sender, EventArgs e)
        {
            _view.ResetFields();
        }

        private void OnGuardarClicked(object? sender, EventArgs e)
        {
            // 1. Extraer y limpiar los datos de la vista
            string codigo = _view.Codigo?.Trim() ?? string.Empty;
            string nombre = _view.Nombre?.Trim() ?? string.Empty;
            string cedula = _view.Cedula?.Trim() ?? string.Empty;
            string telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono.Trim();
            string cargo = string.IsNullOrWhiteSpace(_view.Cargo) ? null : _view.Cargo.Trim();
            decimal salario = _view.Salario;
            DateTime? fechaIngreso = _view.FechaIngreso;
            bool activo = _view.Activo;

            // 2. validaciones de negocio / presentacion
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(cedula))
            {
                _view.showMessage("El código, el nombre y la cédula son obligatorios.", "Validacion", true);
                return;
            }
            if (salario < 0)
            {
                _view.showMessage("El salario no puede ser negativo.", "Validacion", true);
                return;
            }


            // 3. persistencia con EF core

            try
            {
                using (var db = new Dev_ComideriaDbContext())
                {
                    bool codigoExiste = db.Empleado.AsNoTracking().Any(x => x.Codigo == codigo);
                    if (codigoExiste)
                    {
                        _view.showMessage($"El código '{codigo}' ya existe. Por favor, ingrese un código único.", "Error", true);
                        return;
                    }
                    var nuevoEmpleado = new Empleado
                    {
                        Codigo = codigo,
                        Nombre = nombre,
                        Cedula = cedula,
                        Telefono = telefono,
                        Cargo = cargo,
                        Salario = salario,
                        Fechaingreso = fechaIngreso,
                        Activo = activo,
                    };
                    db.Empleado.Add(nuevoEmpleado);
                    db.SaveChanges();

                    _view.showMessage("Empleado guardado exitosamente.", "Éxito", false);
                    _view.ResetFields();
                    // Solicitar a la vista que se cierre al agregar desde formulario modal
                    try
                    {
                        _view.CloseView();
                    }
                    catch
                    {
                        // Ignorar si la vista no implementa cierre
                    }



                }




             
            }
            catch (DbUpdateException ex)
            {
                // Manejo de errores de base de datos
                _view.showMessage($"Error al guardar el empleado en la base de datos: {ex.InnerException?.Message ?? ex.Message}", "Error BD", true);
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                _view.showMessage($"Ocurrió un error inesperado: {ex.Message}", "Error", true);
            }


        }

    }
}
