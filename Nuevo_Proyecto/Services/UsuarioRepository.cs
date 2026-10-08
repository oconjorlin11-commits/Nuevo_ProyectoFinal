using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Services.Interfaz_service;

namespace Nuevo_Proyecto.Services
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbContextFactory<Dev_ComideriaDbContext> _factory;

        public UsuarioRepository(IDbContextFactory<Dev_ComideriaDbContext>? factory = null)
        {
            _factory = factory ?? new ComideriaContextFactory();
        }

        public bool CrearUsuarioEmpleado(EmpleadoDto dto)
        {
            try
            {
                using var db = _factory.CreateDbContext();

                // Llamar al procedimiento almacenado con todos los parámetros del empleado
                db.Database.ExecuteSqlInterpolated(
                    $@"EXEC dbo.usp_InsertarEmpleadoYUsuario
                        @Codigo = {dto.Codigo},
                        @Nombre = {dto.Nombre},
                        @Cedula = {dto.Cedula},
                        @Telefono = {dto.Telefono},
                        @Cargo = {dto.Cargo},
                        @Salario = {dto.Salario},
                        @FechaIngreso = {dto.FechaIngreso},
                        @Activo = {(dto.Activo ? 1 : 0)}"
                );

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                System.Diagnostics.Debug.WriteLine($"Error creating employee and user: {ex.GetBaseException().Message}");
                return false;
            }
        }
    }
}
