using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SupermarketApplication.Models.EntityLayer;

namespace SupermarketApplication.Models.DataAccessLayer;

public partial class SupermarketDbContext : DbContext
{
    public SupermarketDbContext()
    {
    }

    public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Username> Usernames { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BA2E75A7E");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PK__Manufact__357E5CA1794DBD45");

            entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ManufacturerName).HasMaxLength(100);
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.OfferId).HasName("PK__Offers__8EBCF0B1F3026762");

            entity.Property(e => e.OfferId).HasColumnName("OfferID");
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Product).WithMany(p => p.Offers)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Offers__ProductI__534D60F1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductID).HasName("PK__Products__B40CC6ED7051AECB");

            entity.HasIndex(e => e.Barcode, "UQ__Products__177800D392A38431").IsUnique();

            entity.Property(e => e.ProductID).HasColumnName("ProductID");
            entity.Property(e => e.Barcode).HasMaxLength(50);
            entity.Property(e => e.CategoryID).HasColumnName("CategoryID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ManufacturerID).HasColumnName("ManufacturerID");
            entity.Property(e => e.ProductName).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__3F466844");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Manufa__403A8C7D");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptID).HasName("PK__Receipts__CC08C400003FAC2F");

            entity.Property(e => e.ReceiptID).HasColumnName("ReceiptID");
            entity.Property(e => e.CashierID).HasColumnName("CashierID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cashier).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.CashierID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Receipts__Cashie__4AB81AF0");
        });

        modelBuilder.Entity<ReceiptDetail>(entity =>
        {
            entity.HasKey(e => e.ReceiptDetailID).HasName("PK__ReceiptD__82FADEDB3FCF8B67");

            entity.Property(e => e.ReceiptDetailID).HasColumnName("ReceiptDetailID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProductID).HasColumnName("ProductID");
            entity.Property(e => e.ReceiptID).HasColumnName("ReceiptID");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReceiptDe__Produ__4F7CD00D");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ReceiptID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReceiptDe__Recei__4E88ABD4");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockID).HasName("PK__Stocks__2C83A9E25F418DBD");

            entity.Property(e => e.StockID).HasColumnName("StockID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProductID).HasColumnName("ProductID");
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Unit).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stocks__ProductI__440B1D61");
        });

        modelBuilder.Entity<Username>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__Username__1788CCAC4513D04C");

            entity.ToTable("Username");

            entity.HasIndex(e => e.UserName, "UQ__Username__536C85E4DB214515").IsUnique();

            entity.Property(e => e.UserID).HasColumnName("UserID");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.UserType).HasMaxLength(50);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("Username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
