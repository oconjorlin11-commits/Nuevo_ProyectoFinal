using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Models.Entities;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Services
{
    public class ClienteRepository : IClienteRepository
    {
        public const string CodigoConsumidorFinal = "CLI-CF";
        public const string NombreConsumidorFinal = "Consumidor Final";

        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;

        public ClienteRepository(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
        }

        private static readonly Expression<Func<Cliente, ClienteDto>> ADto = c => new ClienteDto
        {
            ClienteId = c.ClienteId,
            Codigo = c.Codigo,
            Nombre = c.Nombre,
            Telefono = c.Telefono,
            Direccion = c.Direccion,
            Nota = c.Nota,
            Activo = c.Activo ?? false
        };

        public IReadOnlyList<ClienteDto> GetActivos()
        {
            using var db = _factory.CreateDbContext();
            return db.Clientes.AsNoTracking()
                .Where(c => c.Activo == true && c.Codigo != CodigoConsumidorFinal)
                .OrderBy(c => c.Codigo)
                .Select(ADto)
                .ToList();
        }

        public IReadOnlyList<ClienteDto> BuscarPorCodigo(string codigoExacto)
        {
            using var db = _factory.CreateDbContext();
            return db.Clientes.AsNoTracking()
                .Where(c => c.Codigo == codigoExacto)
                .OrderBy(c => c.Codigo)
                .Select(ADto)
                .ToList();
        }

        public IReadOnlyList<string> GetNotas()
        {
            using var db = _factory.CreateDbContext();
            return db.Clientes.AsNoTracking()
                .Select(c => c.Nota)
                .Where(n => n != null && n != "")
                .Distinct()
                .OrderBy(n => n)
                .ToList()!;
        }

        public string GetSiguienteCodigo()
        {
            using var db = _factory.CreateDbContext();
            var codigos = db.Clientes.AsNoTracking().Select(c => c.Codigo).ToList();
            return CodigoGenerator.SiguienteAgrupado("CLI", codigos, "CLI-001");
        }

        public bool CodigoExiste(string codigo)
        {
            using var db = _factory.CreateDbContext();
            return db.Clientes.AsNoTracking().Any(c => c.Codigo == codigo);
        }

        public bool TelefonoExiste(string telefono)
        {
            using var db = _factory.CreateDbContext();
            return db.Clientes.AsNoTracking().Any(c => c.Telefono == telefono);
        }

        /// <summary>
        /// Valida las reglas de negocio para crear un nuevo cliente.
        /// Devuelve null si es válido, o un mensaje de error si no.
        /// </summary>
        public string? ValidarNuevoCliente(string codigo, string nombre, string? telefono, string? direccion, string? nota)
        {
            // Validar nombre: debe ser "Nombre Apellido" (exactamente un espacio)
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio.";

            nombre = nombre.Trim();
            var partes = nombre.Split(' ');

            // Debe tener exactamente 2 partes (nombre y apellido)
            if (partes.Length != 2)
                return "El nombre debe contener exactamente un nombre y un apellido separados por espacio (ej: Jose Ordoz).";

            // Cada parte debe tener al menos 1 carácter
            if (partes[0].Length == 0 || partes[1].Length == 0)
                return "El nombre y el apellido no pueden estar vacíos.";

            // Validar teléfono: formato XXXX-XXXX (4 dígitos, guion, 4 dígitos)
            if (string.IsNullOrWhiteSpace(telefono))
                return "El teléfono es obligatorio.";

            telefono = telefono.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\d{4}-\d{4}$"))
                return "El teléfono debe tener formato XXXX-XXXX (ej: 7777-1111).";

            // Validar que el teléfono no sea duplicado
            if (TelefonoExiste(telefono))
                return $"El teléfono '{telefono}' ya está registrado en el sistema.";

            // Validar dirección: no más de 50 caracteres
            if (string.IsNullOrWhiteSpace(direccion))
                return "La dirección es obligatoria.";

            direccion = direccion.Trim();
            if (direccion.Length > 50)
                return "La dirección no puede exceder 50 caracteres.";

            // Validar nota: obligatoria
            if (string.IsNullOrWhiteSpace(nota))
                return "Debe seleccionar una nota.";

            return null;  // Todas las validaciones pasaron
        }

        public void Crear(ClienteDto dto)
        {
            using var db = _factory.CreateDbContext();

            db.Clientes.Add(new Cliente
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Nota = dto.Nota ?? string.Empty,   // la entidad no admite null
                Activo = dto.Activo
            });

            db.SaveChanges();
        }

        public bool Actualizar(string codigo, string nombre, string? telefono, string? direccion, string? nota)
        {
            using var db = _factory.CreateDbContext();
            var cliente = db.Clientes.FirstOrDefault(c => c.Codigo == codigo);
            if (cliente == null) return false;

            var antes = new { cliente.Nombre, cliente.Telefono, cliente.Direccion, cliente.Nota };

            cliente.Nombre = nombre.Trim();
            cliente.Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
            cliente.Direccion = direccion?.Trim() ?? string.Empty;
            cliente.Nota = nota?.Trim() ?? string.Empty;

            db.SaveChanges();
            return true;
        }

        public bool Desactivar(string codigo) => CambiarEstado(codigo, false);
        public bool Reactivar(string codigo) => CambiarEstado(codigo, true);

        private bool CambiarEstado(string codigo, bool activo)
        {
            using var db = _factory.CreateDbContext();
            var cliente = db.Clientes.FirstOrDefault(c => c.Codigo == codigo);
            if (cliente == null) return false;

            cliente.Activo = activo;
            db.SaveChanges();
            return true;
        }

        public ClienteDto ObtenerOCrearConsumidorFinal()
        {
            using var db = _factory.CreateDbContext();
            var cf = BuscarOCrearConsumidorFinal(db);
            db.SaveChanges();
            return new ClienteDto
            {
                ClienteId = cf.ClienteId,
                Codigo = cf.Codigo,
                Nombre = cf.Nombre,
                Direccion = cf.Direccion,
                Nota = cf.Nota,
                Activo = true
            };
        }

        /// <summary>
        /// Busca (o agrega al contexto, sin guardar) el cliente genérico. Lo usa también la facturación
        /// para que la venta y el cliente se guarden en la misma transacción.
        /// </summary>
        internal static Cliente BuscarOCrearConsumidorFinal(Dev_ComideriaDbContext db)
        {
            var cf = db.Clientes.FirstOrDefault(c => c.Codigo == CodigoConsumidorFinal || c.Nombre == NombreConsumidorFinal);
            if (cf != null)
            {
                if (cf.Activo != true) cf.Activo = true;
                return cf;
            }

            cf = new Cliente
            {
                Codigo = CodigoConsumidorFinal,
                Nombre = NombreConsumidorFinal,
                Telefono = null,
                Direccion = "N/A",
                Nota = "Cliente genérico para ventas sin cliente",
                Activo = true
            };
            db.Clientes.Add(cf);
            return cf;
        }
    }
}
