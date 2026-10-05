namespace Nuevo_Proyecto.Models.DTOs
{
    /// <summary>Par Id / Nombre para combos (categorías, unidades, formas de pago, etc.).</summary>
    public class LookupDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
