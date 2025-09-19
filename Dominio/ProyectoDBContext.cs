using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace proyectoPratica01.Dominio;

public partial class ProyectoDBContext : DbContext
{
    public ProyectoDBContext()
    {
    }

    public ProyectoDBContext(DbContextOptions<ProyectoDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<DetallesFactura> DetallesFacturas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FormasPago> FormasPagos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=facturacion_P1;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.IdArticulo).HasName("pk_articulos");

        });

        modelBuilder.Entity<DetallesFactura>(entity =>
        {
            entity.HasKey(e => new { e.NroFactura, e.IdDetFactura }).HasName("pk_det_facturas");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.NroFactura).HasName("pk_facturas");

        });

        modelBuilder.Entity<FormasPago>(entity =>
        {
            entity.HasKey(e => e.IdFormaPago).HasName("pk_formas_pago");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
