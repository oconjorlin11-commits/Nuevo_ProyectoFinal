using Nuevo_Proyecto.Models.DTOs;

namespace Nuevo_Proyecto.Services.Interfaz_service
{
    public interface IFacturaRepository
    {
        string GetSiguienteNumero();
        ResultadoFacturaDto Crear(NuevaFacturaDto factura);
        void Anular(string numero, int empleadoId, string motivo);

        IReadOnlyList<FacturaResumenDto> GetTodas();
        IReadOnlyList<FacturaResumenDto> BuscarPorNumero(string prefijoNumero);
        IReadOnlyList<FacturaResumenDto> BuscarPorFechaEspecifica(DateTime fecha);
        IReadOnlyList<FacturaResumenDto> FiltrarPorFecha(DateTime desde, DateTime hasta);
        IReadOnlyList<FacturaDetalleDto> GetDetallePorNumero(string numero);

        /// <summary>Total y cantidad de facturas NO anuladas entre dos fechas (dashboard).</summary>
        ResumenVentasDto GetResumenVentas(DateTime desde, DateTime hasta);
    }
}
