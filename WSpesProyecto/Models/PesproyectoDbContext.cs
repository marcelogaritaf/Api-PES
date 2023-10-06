using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WSpesProyecto.Models;

public partial class PesproyectoDbContext : DbContext
{
    public PesproyectoDbContext()
    {
    }

    public PesproyectoDbContext(DbContextOptions<PesproyectoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignados> Asignados { get; set; }

    public virtual DbSet<Compilado> Compilados { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignados>(entity =>
        {
            entity.HasKey(e => e.IdAsignados).HasName("PK_ID_Asignados");

            entity.Property(e => e.IdAsignados)
                .ValueGeneratedNever()
                .HasColumnName("ID_Asignados");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin)
                .HasColumnType("smalldatetime")
                .HasColumnName("Fecha_Fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("smalldatetime")
                .HasColumnName("Fecha_Inicio");
            entity.Property(e => e.IdCompilado).HasColumnName("ID_Compilado");
            entity.Property(e => e.IdProductos).HasColumnName("ID_Productos");
            entity.Property(e => e.NombrePersona)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Persona");

            entity.HasOne(d => d.oCompilado).WithMany(p => p.Asignados)
                .HasForeignKey(d => d.IdCompilado)
                .HasConstraintName("FK_Asignados_Compilado");

            entity.HasOne(d => d.oProductos).WithMany(p => p.Asignados)
                .HasForeignKey(d => d.IdProductos)
                .HasConstraintName("FK_Asignados_Productos");
        });

        modelBuilder.Entity<Compilado>(entity =>
        {
            entity.HasKey(e => e.IdCompilado).HasName("PK_ID_Compilado");

            entity.ToTable("Compilado");

            entity.Property(e => e.IdCompilado)
                .ValueGeneratedNever()
                .HasColumnName("ID_Compilado");
            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CodigoPrograma).HasColumnName("Codigo_Programa");
            entity.Property(e => e.CodigoSubpartida).HasColumnName("Codigo_Subpartida");
            entity.Property(e => e.IdProductos).HasColumnName("ID_Productos");
            entity.Property(e => e.NumeroContratoVigente)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("Numero_Contrato_Vigente");
            entity.Property(e => e.NumeroScUcUa).HasColumnName("Numero_SC_UC_UA");
            entity.Property(e => e.Oficina)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PeriodoEjecucion)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("Periodo_Ejecucion");
            entity.Property(e => e.RequisionCertificacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Requision_Certificacion");
            entity.Property(e => e.TipoTramite)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("Tipo_Tramite");

            entity.HasOne(d => d.oProductos).WithMany(p => p.Compilados)
                .HasForeignKey(d => d.IdProductos)
                .HasConstraintName("FK_Compilado_Productos");
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.HasKey(e => e.IdProductos);

            entity.Property(e => e.IdProductos)
                .ValueGeneratedNever()
                .HasColumnName("ID_Productos");
            entity.Property(e => e.Articulo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Cantidad).HasColumnType("numeric(9, 0)");
            entity.Property(e => e.CodigoArticulo)
                .HasColumnType("numeric(9, 2)")
                .HasColumnName("Codigo_Articulo");
            entity.Property(e => e.CostoUnitario)
                .HasColumnType("numeric(12, 2)")
                .HasColumnName("Costo_Unitario");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(600)
                .IsUnicode(false);
            entity.Property(e => e.MontoTotal)
                .HasColumnType("numeric(12, 2)")
                .HasColumnName("Monto_Total");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
