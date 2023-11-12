using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Models;

public partial class HrmssisContext : DbContext
{
    public HrmssisContext()
    {
    }

    public HrmssisContext(DbContextOptions<HrmssisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencium> Asistencia { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EmpleadoVacacione> EmpleadoVacaciones { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<VacacionesAusencia> VacacionesAusencias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.IdAsis).HasName("PK__asistenc__B015D886A06273B9");

            entity.ToTable("asistencia");

            entity.Property(e => e.IdAsis).HasColumnName("id_asis");

            entity.Property(e => e.Entrada)
               .IsUnicode(false)
               .HasColumnName("entrada");
            entity.Property(e => e.IdCedula).HasColumnName("id_cedula");
           

            entity.HasOne(d => d.IdCedulaNavigation).WithMany()
                 .HasForeignKey(d => d.IdCedula)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_asistencia_empleado");
            /*
            entity.ToTable("asistencia");
            entity.HasKey(e => e.Id_asis);

            entity.Property(e => e.Id_asis)
               .ValueGeneratedNever()
               .HasColumnName("id_asis");

            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
           
            entity.Property(e => e.Entrada)
                .HasColumnType("datetime")
                .HasColumnName("entrada");
            entity.Property(e => e.IdCedula).HasColumnName("id_cedula");
            entity.Property(e => e.Salida)
                .HasColumnType("datetime")
                .HasColumnName("salida");

            entity.HasOne(d => d.IdCedulaNavigation).WithMany()
                .HasForeignKey(d => d.IdCedula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_asistencia_empleado");*/
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento);

            entity.ToTable("departamento");

            entity.Property(e => e.IdDepartamento)
                .ValueGeneratedNever()
                .HasColumnName("id_departamento");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("nombre_departamento");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.IdCedula);


            entity.ToTable("documentos");
            /*
            entity
                .HasNoKey()
                .ToTable("documentos");*/

            entity.Property(e => e.Cedula).HasColumnName("cedula");
            entity.Property(e => e.Contrato).HasColumnName("contrato");
            entity.Property(e => e.IdCedula).HasColumnName("id_cedula");
            entity.Property(e => e.Otros).HasColumnName("otros");

            entity.HasOne(d => d.IdCedulaNavigation).WithMany()
                .HasForeignKey(d => d.IdCedula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_empleado_documentos");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Cedula);

            entity.ToTable("empleado");

            entity.Property(e => e.Cedula)
                .ValueGeneratedNever()
                .HasColumnName("cedula");
            entity.Property(e => e.Correo)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("date")
                .HasColumnName("fecha_contratacion");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono).HasColumnName("telefono");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK_empleado_departamento");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_Empleado_Roles");
        });

        modelBuilder.Entity<EmpleadoVacacione>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("empleado_vacaciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCedula).HasColumnName("id_cedula");

            entity.HasOne(d => d.IdNavigation).WithMany()
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK_empleado_vacaciones_id");

            entity.HasOne(d => d.IdCedulaNavigation).WithMany()
                .HasForeignKey(d => d.IdCedula)
                .HasConstraintName("FK_empleado_vacaciones_cedula");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdMaster).HasName("PK__roles_ne__A51485034645EFE7");

            entity.ToTable("roles");

            entity.Property(e => e.IdMaster).HasColumnName("id_master");
            entity.Property(e => e.Clave)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Nombre)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("rol");
            entity.Property(e => e.Usuario)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<VacacionesAusencia>(entity =>
        {
            entity.ToTable("vacaciones_ausencias");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Finalizacion)
                .HasColumnType("datetime")
                .HasColumnName("finalizacion");
            entity.Property(e => e.Inicio)
                .HasColumnType("datetime")
                .HasColumnName("inicio");
            entity.Property(e => e.Motivo)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("motivo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
