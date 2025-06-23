using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMantenimientoTrabajadores.Models.Dtos;
using PruebaTecnicaMantenimientoTrabajadores.Models.Entities;

namespace PruebaTecnicaMantenimientoTrabajadores.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Trabajador> Trabajadores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=TrabajadoresPrueba;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departam__3214EC07B962C14F");

            entity.ToTable("Departamento");

            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Distrito__3214EC07FB7923F1");

            entity.ToTable("Distrito");

            entity.Property(e => e.NombreDistrito)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Distritos)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK__Distrito__IdProv__3D5E1FD2");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Provinci__3214EC074A06F69A");

            entity.Property(e => e.NombreProvincia)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Provincia__IdDep__3E52440B");
        });

        modelBuilder.Entity<Trabajador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trabajad__3214EC0732ED2E28");

            entity.Property(e => e.Nombres)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Trabajado__IdDep__3F466844");

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.IdDistrito)
                .HasConstraintName("FK__Trabajado__IdDis__403A8C7D");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK__Trabajado__IdPro__412EB0B6");
        });
        modelBuilder.Entity<TrabajadorDto>().HasNoKey();
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
