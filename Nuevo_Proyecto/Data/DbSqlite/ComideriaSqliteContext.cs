using System;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Data.DbSqlite.Entities;
using Nuevo_Proyecto.Models.Entities;

namespace Nuevo_Proyecto.Data.DbSqlite
{
    public partial class ComideriaSqliteContext : DbContext
    {
        public ComideriaSqliteContext(DbContextOptions<ComideriaSqliteContext> options)
            : base(options)
        {
        }

        // Tablas del Dominio Comideria
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Productos> Productos { get; set; } = null!;
        public virtual DbSet<Inventario> Inventario { get; set; } = null!;
        public virtual DbSet<Facturas> Facturas { get; set; } = null!;
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; } = null!;
        public virtual DbSet<Unidade> Unidades { get; set; } = null!;
        public virtual DbSet<FormaPago> FormaPagos { get; set; } = null!;

        // Tablas de Sincronización y Resiliencia Offline
        public virtual DbSet<ColaOperacione> ColaOperaciones { get; set; } = null!;
        public virtual DbSet<MapeoClavesSincronizacion> MapeoClavesSincronizacions { get; set; } = null!;
        public virtual DbSet<ConfiguracionSincronizacion> ConfiguracionSincronizacions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ignorar entidades que no están en el esquema local
            modelBuilder.Ignore<MovimientoInventario>();

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClienteId);
                entity.Property(e => e.Codigo).HasMaxLength(50);
                entity.Property(e => e.Nombre).HasMaxLength(150);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.EmpleadoId);
                entity.Property(e => e.Codigo).HasMaxLength(50);
                entity.Property(e => e.Nombre).HasMaxLength(150);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);
                entity.Property(e => e.NombreUsuario).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Contrasena).HasMaxLength(255);
                entity.Property(e => e.RolSistema).HasMaxLength(50);
                entity.Property(e => e.ContrasenaHash).HasMaxLength(255);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.CategoriaId);
                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.HasKey(e => e.ProductoId);
                entity.Property(e => e.Codigo).HasMaxLength(50);
                entity.Property(e => e.Nombre).HasMaxLength(150);
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.InventarioId);
            });

            modelBuilder.Entity<Facturas>(entity =>
            {
                entity.HasKey(e => e.FacturaId);
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.DetalleId);
            });

            modelBuilder.Entity<Unidade>(entity =>
            {
                entity.HasKey(e => e.UnidadId);
                entity.Property(e => e.Nombre).HasMaxLength(20);
            });

            modelBuilder.Entity<FormaPago>(entity =>
            {
                entity.HasKey(e => e.FormaPagoId);
                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<ColaOperacione>(entity =>
            {
                entity.HasKey(e => e.Secuencia);
                entity.Property(e => e.Secuencia).ValueGeneratedOnAdd();
                entity.Property(e => e.OperacionId).HasMaxLength(50);
                entity.Property(e => e.Estado).HasMaxLength(30);
            });

            modelBuilder.Entity<MapeoClavesSincronizacion>(entity =>
            {
                entity.HasKey(e => new { e.NombreEntidad, e.ClaveLocal });
                entity.Property(e => e.NombreEntidad).HasMaxLength(50);
                entity.Property(e => e.ClaveLocal).HasMaxLength(50);
            });

            modelBuilder.Entity<ConfiguracionSincronizacion>(entity =>
            {
                entity.HasKey(e => e.Clave);
                entity.Property(e => e.Clave).HasMaxLength(100);
            });
        }
    }
}

