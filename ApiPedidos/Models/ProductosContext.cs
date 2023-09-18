using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Models;

public partial class ProductosContext : DbContext
{
    public ProductosContext()
    {
    }

    public ProductosContext(DbContextOptions<ProductosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedId).HasName("PK__pedido__E879757EE7EDEC95");

            entity.ToTable("pedido");

            entity.Property(e => e.PedId).HasColumnName("pedID");
            entity.Property(e => e.PedCant).HasColumnName("pedCant");
            entity.Property(e => e.PedIva).HasColumnName("pedIVA");
            entity.Property(e => e.PedProd).HasColumnName("pedProd");
            entity.Property(e => e.PedSubTot)
                .HasColumnType("money")
                .HasColumnName("pedSubTot");
            entity.Property(e => e.PedTotal)
                .HasColumnType("money")
                .HasColumnName("pedTotal");
            entity.Property(e => e.PedUsu).HasColumnName("pedUsu");
            entity.Property(e => e.PedVrUnit)
                .HasColumnType("money")
                .HasColumnName("pedVrUnit");

            entity.HasOne(d => d.PedProdNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.PedProd)
                .HasConstraintName("FK__pedido__pedProd__440B1D61");

            entity.HasOne(d => d.PedUsuNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.PedUsu)
                .HasConstraintName("FK__pedido__pedUsu__4316F928");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProId).HasName("PK__producto__5BBBEED5B5D2F4B6");

            entity.ToTable("producto");

            entity.Property(e => e.ProId).HasColumnName("proID");
            entity.Property(e => e.ProDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("proDesc");
            entity.Property(e => e.ProValor)
                .HasColumnType("money")
                .HasColumnName("proValor");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuId).HasName("PK__usuario__2F813BE395391BC0");

            entity.ToTable("usuario");

            entity.Property(e => e.UsuId).HasColumnName("usuID");
            entity.Property(e => e.UsuNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuNombre");
            entity.Property(e => e.UsuPass)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usuPass");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
