namespace Nuevo_Proyecto.Data.DbSqlite.Entities;

public partial class ColaOperacione
{
    public int Secuencia { get; set; }

    public string OperacionId { get; set; } = null!;

    public string ClaveIdempotencia { get; set; } = null!;

    public string NombreEntidad { get; set; } = null!;

    public string Accion { get; set; } = null!;

    public string IdentificadorEntidad { get; set; } = null!;

    public string PayloadJson { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public int Intentos { get; set; }

    public string CreadaUtc { get; set; } = null!;

    public string? UltimoIntentoUtc { get; set; }

    public string? ConfirmadaUtc { get; set; }

    public string? ProximoIntentoUtc { get; set; }

    public string? UltimoError { get; set; }
}

