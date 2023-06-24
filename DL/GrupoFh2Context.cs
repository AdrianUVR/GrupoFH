using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class GrupoFh2Context : DbContext
{
    public GrupoFh2Context()
    {
    }

    public GrupoFh2Context(DbContextOptions<GrupoFh2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Error> Errors { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= GrupoFH2; User ID=sa; TrustServerCertificate=True; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.IdArea).HasName("PK__Area__2FC141AA97F824D9");

            entity.ToTable("Area");

            entity.Property(e => e.NombreArea)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433DEE8022DA");

            entity.ToTable("Departamento");

            entity.Property(e => e.DescripcionD)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.IdArea)
                .HasConstraintName("FK__Departame__IdAre__1B0907CE");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E272CAA55");

            entity.ToTable("Empleado");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Error>(entity =>
        {
            entity.HasKey(e => e.IdError).HasName("PK__Error__C8A4CFD9E82F5FF1");

            entity.ToTable("Error");

            entity.Property(e => e.DescripcionE)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Paso1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Paso2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("paso2");
            entity.Property(e => e.Paso3)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("paso3");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Errors)
                .HasForeignKey(d => d.IdArea)
                .HasConstraintName("FK__Error__IdArea__267ABA7A");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.IdTicket).HasName("PK__Ticket__4B93C7E72A1C2442");

            entity.ToTable("Ticket");

            entity.Property(e => e.Comentarios)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

            entity.HasOne(d => d.AsignadoANavigation).WithMany(p => p.TicketAsignadoANavigations)
                .HasForeignKey(d => d.AsignadoA)
                .HasConstraintName("FK__Ticket__Asignado__182C9B23");

            entity.HasOne(d => d.CerradoPorNavigation).WithMany(p => p.TicketCerradoPorNavigations)
                .HasForeignKey(d => d.CerradoPor)
                .HasConstraintName("FK__Ticket__CerradoP__1920BF5C");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdArea)
                .HasConstraintName("FK__Ticket__IdArea__1A14E395");

            entity.HasOne(d => d.IdErrorNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdError)
                .HasConstraintName("FK__Ticket__IdError__1BFD2C07");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
