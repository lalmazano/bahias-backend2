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





        #endregion

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
