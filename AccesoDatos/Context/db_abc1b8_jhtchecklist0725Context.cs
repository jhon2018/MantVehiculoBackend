using System;
using System.Collections.Generic;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Context;

public partial class db_abc1b8_jhtchecklist0725Context : DbContext
{
    public db_abc1b8_jhtchecklist0725Context()
    {
    }

    public db_abc1b8_jhtchecklist0725Context(DbContextOptions<db_abc1b8_jhtchecklist0725Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Conductor> Conductor { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimiento { get; set; }

    public virtual DbSet<Personal> Personal { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Vehiculo> Vehiculo { get; set; }

    public virtual DbSet<detalleReparacion> detalleReparacion { get; set; }

    public virtual DbSet<tipoReparacion> tipoReparacion { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql1004.site4now.net;Database=db_abc1b8_jhtchecklist0725;Trust Server Certificate=true;User Id=db_abc1b8_jhtchecklist0725_admin;Password=123456Fugitivos;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conductor>(entity =>
        {
            entity.HasKey(e => e.id_Conductor).HasName("PK__Conducto__89F4F5287D95F5A0");

            entity.Property(e => e.licencia)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.id_PersonalNavigation).WithMany(p => p.Conductor)
                .HasForeignKey(d => d.id_Personal)
                .HasConstraintName("FK__Conductor__id_Pe__4BAC3F29");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.id_Mantenimiento).HasName("PK__Mantenim__858EFA2C9F6F5A35");

            entity.Property(e => e.fecha_Mantenimiento).HasColumnType("datetime");
            entity.Property(e => e.observacion).HasColumnType("text");
            entity.Property(e => e.url_foto)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.id_ConductorNavigation).WithMany(p => p.Mantenimiento)
                .HasForeignKey(d => d.id_Conductor)
                .HasConstraintName("FK__Mantenimi__id_Co__4E88ABD4");

            entity.HasOne(d => d.id_ProveedorNavigation).WithMany(p => p.Mantenimiento)
                .HasForeignKey(d => d.id_Proveedor)
                .HasConstraintName("FK__Mantenimi__id_Pr__4F7CD00D");

            entity.HasOne(d => d.id_VehiculoNavigation).WithMany(p => p.Mantenimiento)
                .HasForeignKey(d => d.id_Vehiculo)
                .HasConstraintName("FK__Mantenimi__id_Ve__4D94879B");

            entity.HasOne(d => d.id_detalleReparacionNavigation).WithMany(p => p.Mantenimiento)
                .HasForeignKey(d => d.id_detalleReparacion)
                .HasConstraintName("FK__Mantenimi__id_de__5070F446");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.id_Personal).HasName("PK__Personal__B0312752E116FDBA");

            entity.Property(e => e.cargo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.dni)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.nombre_completo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            //entity.HasOne(d => d.id_UsuarioNavigation).WithMany(p => p.Personal)
            //    .HasForeignKey(d => d.id_Usuario)
            //    .HasConstraintName("FK__Personal__id_Usu__4AB81AF0");

            entity.HasOne(d => d.id_UsuarioNavigation)
        .WithMany(p => p.Personal)
        .HasForeignKey(d => d.id_Usuario)
        .OnDelete(DeleteBehavior.Cascade)
        .HasConstraintName("FK__Personal__id_Usu__4AB81AF0"); // ✅ Acción en cascada para eliminar Personal cuando se elimina Usuario


        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.id_Proveedor).HasName("PK__Proveedo__53B6E1A5C2DCAF80");

            entity.HasIndex(e => e.ruc, "UQ__Proveedo__C2B74E61DC4571E3").IsUnique();

            entity.Property(e => e.razon_social)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ruc)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.id_Usuario).HasName("PK__Usuario__8E901EAA79807E52");

            entity.HasIndex(e => e.correo, "UQ__Usuario__2A586E0B5E210A23").IsUnique();

            entity.Property(e => e.activo).HasDefaultValue(true);
            entity.Property(e => e.clave_hash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.fecha_creacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.rol).HasMaxLength(255);
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.id_Vehiculo).HasName("PK__Vehiculo__16F94EA1C1AF219A");

            entity.HasIndex(e => e.placa, "UQ__Vehiculo__0C0574250AC3FAC2").IsUnique();

            entity.Property(e => e.marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.placa)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<detalleReparacion>(entity =>
        {
            entity.HasKey(e => e.id_detalleReparacion).HasName("PK__detalleR__FAC6C20856207C82");

            entity.Property(e => e.descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.id_tipoReparacionNavigation).WithMany(p => p.detalleReparacion)
                .HasForeignKey(d => d.id_tipoReparacion)
                .HasConstraintName("FK__detalleRe__id_ti__4CA06362");
        });

        modelBuilder.Entity<tipoReparacion>(entity =>
        {
            entity.HasKey(e => e.id_tipoReparacion).HasName("PK__tipoRepa__CCAC3414DE5C6F9C");

            entity.Property(e => e.descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
