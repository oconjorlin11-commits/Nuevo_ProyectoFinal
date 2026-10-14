using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services.Interfaz_service
{
    public interface IClienteRepository
    {
        IReadOnlyList<ClienteDto> GetActivos();
        IReadOnlyList<ClienteDto> BuscarPorCodigo(string prefijoCodigo);
        IReadOnlyList<string> GetNotas();
        string GetSiguienteCodigo();
        bool CodigoExiste(string codigo);
        bool TelefonoExiste(string telefono);
        int Crear(ClienteDto cliente);  // Retorna el ClienteID del cliente creado
        bool Actualizar(string codigo, string nombre, string? telefono, string? direccion, string? nota, string? motivo = null);
        bool Desactivar(string codigo, string? motivo = null);
        bool Reactivar(string codigo, string? motivo = null);

        /// <summary>Cliente genérico para ventas sin cliente (se crea la primera vez).</summary>
        ClienteDto ObtenerOCrearConsumidorFinal();

        /// <summary>
        /// Valida las reglas de negocio para crear un nuevo cliente.
        /// Devuelve null si es válido, o un mensaje de error si no.
        /// </summary>
        string? ValidarNuevoCliente(string codigo, string nombre, string? telefono, string? direccion, string? nota);
    }
}
