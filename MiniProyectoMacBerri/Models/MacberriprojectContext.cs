using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MiniProyectoMacBerri.Models;

public partial class MacberriprojectContext : DbContext
{
    public MacberriprojectContext()
    {
    }

    public MacberriprojectContext(DbContextOptions<MacberriprojectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Detail> Details { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Reserve> Reserves { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Shopcart> Shopcarts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserService> UserServices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Furakam; Database=macberriproject; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Detail>(entity =>
        {
            entity.HasKey(e => e.IdDetail).HasName("PK__detail__EA8338083BB65D0D");

            entity.ToTable("detail");

            entity.Property(e => e.IdDetail)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id_detail");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdSale).HasColumnName("id_sale");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Details)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detail__id_produ__5535A963");

            entity.HasOne(d => d.IdSaleNavigation).WithMany(p => p.Details)
                .HasForeignKey(d => d.IdSale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detail__id_sale__5629CD9C");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__product__BA39E84FEEF24F8E");

            entity.ToTable("product");

            entity.Property(e => e.IdProduct)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id_product");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("price");
            entity.Property(e => e.UrlImage)
                .HasMaxLength(250)
                .HasColumnName("url_image");
        });

        modelBuilder.Entity<Reserve>(entity =>
        {
            entity.HasKey(e => e.IdReserve).HasName("PK__reserve__423CBE5927F55CA9");

            entity.ToTable("reserve");

            entity.Property(e => e.IdReserve)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id_reserve");
            entity.Property(e => e.IdService).HasColumnName("id_service");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.LimitDate)
                .HasColumnType("datetime")
                .HasColumnName("limit_date");
            entity.Property(e => e.RequestedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("requested_at");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.Reserves)
                .HasForeignKey(d => d.IdService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reserve__id_serv__4CA06362");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Reserves)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reserve__id_user__4BAC3F29");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__rol__6ABCB5E0717EC4FD");

            entity.ToTable("rol");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.IdSale).HasName("PK__sale__D18B01577403FC97");

            entity.ToTable("sale");

            entity.Property(e => e.IdSale)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id_sale");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.SaleDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("sale_date");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sale__id_user__5165187F");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PK__service__D06FB5A8A2D49A6D");

            entity.ToTable("service");

            entity.Property(e => e.IdService)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id_service");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UrlImage)
                .HasMaxLength(250)
                .HasColumnName("url_image");
        });

        modelBuilder.Entity<Shopcart>(entity =>
        {
            entity.HasKey(e => e.IdShopcart).HasName("PK__shopcart__AFFD978E2713C3B9");

            entity.ToTable("shopcart");

            entity.Property(e => e.IdShopcart)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id_shopcart");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Shopcarts)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shopcart__id_pro__5AEE82B9");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Shopcarts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shopcart__id_use__59FA5E80");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__user__D2D146377B37A0AB");

            entity.ToTable("user");

            entity.Property(e => e.IdUser)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id_user");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Lastnames)
                .HasMaxLength(80)
                .HasColumnName("lastnames");
            entity.Property(e => e.Names)
                .HasMaxLength(80)
                .HasColumnName("names");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__id_rol__3C69FB99");
        });

        modelBuilder.Entity<UserService>(entity =>
        {
            entity.HasKey(e => e.IdUserServices).HasName("PK__user_ser__ABE7A4A5437F5E87");

            entity.ToTable("user_services");

            entity.Property(e => e.IdUserServices)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id_user_services");
            entity.Property(e => e.IdService).HasColumnName("id_service");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.UserServices)
                .HasForeignKey(d => d.IdService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_serv__id_se__45F365D3");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserServices)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_serv__id_us__46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
