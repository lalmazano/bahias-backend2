using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Infrastructure.Database.Context;


public partial class ModelContext : DbContext
{

    public ModelContext(DbContextOptions options)
               : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // No configurar nada aquí, la configuración de la cadena de conexión se manejará al registrar los contextos
        }
    }
    public virtual DbSet<Rol> Rols { get; set; }
    public virtual DbSet<UserRol> UserRols { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<EstadoBahium> EstadoBahia { get; set; }
    public virtual DbSet<Bahium> Bahia { get; set; }

    public virtual DbSet<EstadoReserva> EstadoReservas { get; set; }

    public virtual DbSet<Parametro> Parametros { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<TipoVehiculo> TipoVehiculos { get; set; }

    public virtual DbSet<Ubicacion> Ubicacions { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("BAHIAS");


        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("ROL", "BAHIAS");

            entity.Property(e => e.IdRol)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ROL");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("CREATED_AT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("STATUS");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<UserRol>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdRol });

            entity.ToTable("USER_ROL", "BAHIAS");

            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_USER");

            entity.Property(e => e.IdRol)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ROL");

            entity.Property(e => e.AssignedAt)
                .HasPrecision(6)
                .HasColumnName("ASSIGNED_AT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP   -- Fecha de asignación\n");

            entity.HasOne(d => d.IdRolNavigation)
                .WithMany(p => p.UserRols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_UR_ROL");

            entity.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.UserRols)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_UR_USUARIO");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("USUARIO", "BAHIAS");

            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_USER");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("CREATED_AT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");

            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("STATUS");

            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LASTNAME");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");

            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");

            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("UPDATED_AT");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
        });

        #region Tablas Bahias

        modelBuilder.Entity<EstadoBahium>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("ESTADO_BAHIA", "BAHIAS");

            entity.Property(e => e.IdEstado)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ESTADO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Bahium>(entity =>
        {
            entity.HasKey(e => e.IdBahia);

            entity.ToTable("BAHIA", "BAHIAS");

            entity.Property(e => e.IdBahia)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_BAHIA");
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP\n")
                .HasColumnName("FECHA_CREACION");
            entity.Property(e => e.IdEstado)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ESTADO");
            entity.Property(e => e.IdReserva)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_RESERVA");
            entity.Property(e => e.IdUbicacion)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_UBICACION");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Bahia)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK_BAHIA_RESERVA");

            entity.HasOne(d => d.IdUbicacionNavigation).WithMany(p => p.Bahia)
                .HasForeignKey(d => d.IdUbicacion)
                .HasConstraintName("FK_BAHIA_UBICACION");
        });

        modelBuilder.Entity<EstadoReserva>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("ESTADO_RESERVA", "BAHIAS");

            entity.Property(e => e.IdEstado)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ESTADO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Parametro>(entity =>
        {
            entity.HasKey(e => e.IdParametro);

            entity.ToTable("PARAMETRO", "BAHIAS");

            entity.HasIndex(e => e.Clave, "UQ_PARAMETRO_CLAVE").IsUnique();

            entity.Property(e => e.IdParametro)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_PARAMETRO");
            entity.Property(e => e.Clave)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("CLAVE");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Valor)
                .IsUnicode(false)
                .HasColumnName("VALOR");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva);

            entity.ToTable("RESERVA", "BAHIAS");

            entity.Property(e => e.IdReserva)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_RESERVA");
            entity.Property(e => e.CreadoEn)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP\n")
                .HasColumnName("CREADO_EN");
            entity.Property(e => e.Estado)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("ESTADO");
            entity.Property(e => e.FinTs)
                .HasPrecision(6)
                .HasColumnName("FIN_TS");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.IdVehiculo)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_VEHICULO");
            entity.Property(e => e.InicioTs)
                .HasPrecision(6)
                .HasColumnName("INICIO_TS");
            entity.Property(e => e.Observacion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("OBSERVACION");
        });

        modelBuilder.Entity<TipoVehiculo>(entity =>
        {
            entity.HasKey(e => e.IdTipo);

            entity.ToTable("TIPO_VEHICULO", "BAHIAS");

            entity.HasIndex(e => e.Nombre, "UQ_TIPO_NOMBRE").IsUnique();

            entity.Property(e => e.IdTipo)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_TIPO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ICON");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Ubicacion>(entity =>
        {
            entity.HasKey(e => e.IdUbicacion);

            entity.ToTable("UBICACION", "BAHIAS");

            entity.Property(e => e.IdUbicacion)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_UBICACION");
            entity.Property(e => e.Detalle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DETALLE");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.IdVehiculo);

            entity.ToTable("VEHICULO", "BAHIAS");

            entity.HasIndex(e => new { e.Placa, e.IdUsuario }, "UQ_VEHICULO_PLACA").IsUnique();

            entity.Property(e => e.IdVehiculo)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_VEHICULO");
            entity.Property(e => e.Activo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'S' ")
                .IsFixedLength()
                .HasColumnName("ACTIVO");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("COLOR");
            entity.Property(e => e.CreadoEn)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP\n")
                .HasColumnName("CREADO_EN");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MARCA");
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODELO");
            entity.Property(e => e.Placa)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PLACA");
            entity.Property(e => e.Tipo)
                .HasColumnType("NUMBER")
                .HasColumnName("TIPO");

            entity.HasOne(d => d.TipoNavigation).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.Tipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICULO_TIPO");
        });




        #endregion

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
