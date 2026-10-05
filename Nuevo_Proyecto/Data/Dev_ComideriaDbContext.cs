using System;
using System.Collections.Generic;
using Nuevo_Proyecto.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Nuevo_Proyecto.Services.Helpers;


namespace Nuevo_Proyecto.Data;



public partial class Dev_ComideriaDbContext : DbContext
{
    public Dev_ComideriaDbContext()
    {
    }

    public Dev_ComideriaDbContext(DbContextOptions<Dev_ComideriaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Facturas> Facturas { get; set; }


    public virtual DbSet<FormaPago> FormaPagos { get; set; }


    public virtual DbSet<Inventario> Inventario { get; set; }

    public virtual DbSet<MovimientoInventario> MovimientoInventarios { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Unidade> Unidades { get; set; }


    public virtual DbSet<InventarioConValor> InventarioConValor { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(AppConfig.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("Pk__Categoria__F353C1C5E92F8F9D");

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Nombre).HasMaxLength(50);


        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0A77758CCC8");

            entity.HasIndex(e => e.Codigo, "UQ__Clientes__06370DAC2256700E").IsUnique();

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Codigo).HasMaxLength(10);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Nota).HasMaxLength(200);
            entity.Property(e => e.Telefono).HasMaxLength(20);


        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PK__DetalleF__6E19D6FADB708048");

            entity.ToTable("DetalleFactura");

            entity.Property(e => e.DetalleId).HasColumnName("DetalleID");
            entity.Property(e => e.FacturaId).HasColumnName("FacturaID");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.subtotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Factura).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleFactura_Factura");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleFactura_Producto");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.ToTable("Empleados");

            entity.HasKey(e => e.EmpleadoId).HasName("PK__Empleado__958BE6F02F0D128D");

            entity.HasIndex(e => e.Codigo, "UQ__Empleado__06370DACAF531A5D").IsUnique();

            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Cargo).HasMaxLength(50);
            entity.Property(e => e.Cedula).HasMaxLength(20);
            entity.Property(e => e.Codigo).HasMaxLength(10);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Salario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.ToTable("Estados");

            entity.HasKey(e => e.EstadoId).HasName("PK__Estados__FEF86B60B9D51A96");

            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.NombreEstado).HasMaxLength(50);
        });

        modelBuilder.Entity<Facturas>(entity =>
        {
            entity.HasKey(e => e.FacturaId).HasName("PK__Facturas__5C024805EBF4FE4E");

            entity.HasIndex(e => e.Numero, "UQ__Facturas__7E532BC63C01B3C0").IsUnique();

            entity.Property(e => e.FacturaId).HasColumnName("FacturaID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("ACTIVA");
            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FormaPagoId).HasColumnName("FormaPagoID");
            entity.Property(e => e.Numero).HasMaxLength(20);
            entity.Property(e => e.Observacion).HasMaxLength(200);
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Facturas_Cliente");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facturas_Empleado");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_Facturas_Estados");

            entity.HasOne(d => d.FormaPago).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FormaPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facturas_FormaPago");
        });

        modelBuilder.Entity<FormaPago>(entity =>
        {
            entity.HasKey(e => e.FormaPagoId).HasName("PK__FormasPa__920B091E41A811C4");

            entity.ToTable("FormasPago");

            entity.Property(e => e.FormaPagoId).HasColumnName("FormaPagoID");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.InventarioId).HasName("PK__Inventar__FB8A24B7E3EB84C3");

            entity.ToTable("Inventario");

            entity.Property(e => e.InventarioId).HasColumnName("InventarioID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.ValorInventario).HasComputedColumnSql("([Stock]*(1))", true);

            entity.HasOne(d => d.Productos).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventario_Producto");
        });

        modelBuilder.Entity<MovimientoInventario>(entity =>
        {
            entity.HasKey(e => e.MovimientoId).HasName("PK__Movimien__BF923FCCE406C21E");

            entity.ToTable("MovimientosInventario");

            entity.Property(e => e.MovimientoId).HasColumnName("MovimientoID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Observacion).HasMaxLength(200);
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.TipoMovimiento).HasColumnName("Tipo").HasMaxLength(50);

            entity.HasOne(d => d.Empleado).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientosInventario_Empleado");

            entity.HasOne(d => d.Producto).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientosInventario_Producto");
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AE83C7691FB2");

            entity.HasIndex(e => e.Codigo, "UQ__Producto__06370DACBD530DA3").IsUnique();

            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Codigo).HasMaxLength(10);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnidadId).HasColumnName("UnidadID");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Categoria");

            entity.HasOne(d => d.Unidade).WithMany(p => p.Productos)
                .HasForeignKey(d => d.UnidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Unidad");
        });

        modelBuilder.Entity<Unidade>(entity =>
        {
            entity.ToTable("Unidades");

            entity.HasKey(e => e.UnidadId).HasName("PK__Unidades__C6F324360CA7D0D7");

            entity.Property(e => e.UnidadId).HasColumnName("UnidadID");
            entity.Property(e => e.Nombre).HasMaxLength(20);
        });

        modelBuilder.Entity<InventarioConValor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_InventarioConValor");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.MovimientoId).HasColumnName("MovimientoID");
            entity.Property(e => e.NombreProducto).HasMaxLength(100);
            entity.Property(e => e.Observacion).HasMaxLength(200);
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.TipoMovimiento).HasMaxLength(50);
            entity.Property(e => e.ValorInventario).HasColumnType("decimal(21, 2)");
        });

        OnModelCreatingPartial(modelBuilder);








    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}

