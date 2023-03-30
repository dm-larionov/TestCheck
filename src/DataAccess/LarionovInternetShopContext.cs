using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public partial class LarionovInternetShopContext : DbContext
    {
        public LarionovInternetShopContext()
        {
        }

        public LarionovInternetShopContext(DbContextOptions<LarionovInternetShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Good> Goods { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserOrder> UserOrders { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Good>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("Category_id");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Goods)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Goods_Categories");

                entity.HasMany(d => d.Orders)
                    .WithMany(p => p.Goods)
                    .UsingEntity<Dictionary<string, object>>(
                        "OrderGood",
                        l => l.HasOne<Order>().WithMany().HasForeignKey("OrderId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Order_Goods_Orders"),
                        r => r.HasOne<Good>().WithMany().HasForeignKey("GoodId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Order_Goods_Goods"),
                        j =>
                        {
                            j.HasKey("GoodId", "OrderId");

                            j.ToTable("Order_Goods");

                            j.IndexerProperty<int>("GoodId").HasColumnName("Good_id");

                            j.IndexerProperty<int>("OrderId").HasColumnName("Order_id");
                        });
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(30);

                entity.Property(e => e.UserId).HasColumnName("User_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Firstname).HasMaxLength(50);

                entity.Property(e => e.Lastname).HasMaxLength(50);

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Middlename).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<UserOrder>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.OrderId });

                entity.ToTable("User_orders");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.OrderId).HasColumnName("Order_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_orders_Orders");

                entity.HasOne(d => d.OrderNavigation)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_orders_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}