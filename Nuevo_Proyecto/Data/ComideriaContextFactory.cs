using Microsoft.EntityFrameworkCore;

namespace Nuevo_Proyecto.Data
{
    /// <summary>
    /// Crea contextos de vida corta. Los repositorios lo reciben por constructor
    /// (mismo patrón que VetCare, pero sin un contexto único y eterno que se desactualiza).
    /// </summary>
    public class ComideriaContextFactory : IDbContextFactory<Dev_ComideriaDbContext>
    {
        public Dev_ComideriaDbContext CreateDbContext() => new Dev_ComideriaDbContext();
    }
}
