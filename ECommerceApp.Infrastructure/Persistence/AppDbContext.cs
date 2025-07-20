namespace ECommerceApp.Infrastructure.Persistence
{
    using ECommerceApp.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Database context
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Balance> Balances => Set<Balance>();
        public DbSet<Order> Orders => Set<Order>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(o => o.Id);
                entity.Property(o => o.Id).HasColumnName("id");
                entity.Property(o => o.CreatedAt).HasColumnName("created_at");
            });
            #endregion

            #region Balances
            modelBuilder.Entity<Balance>(entity =>
            {
                entity.ToTable("balances");

                entity.HasKey(o => o.Id);
                entity.Property(o => o.Id).HasColumnName("id");
                entity.Property(o => o.UserId).HasColumnName("user_id");
                entity.Property(o => o.TotalBalance).HasColumnName("total_balance");
                entity.Property(o => o.AvailableBalance).HasColumnName("available_balance");
                entity.Property(o => o.BlockedBalance).HasColumnName("blocked_balance");
                entity.Property(o => o.CreatedAt).HasColumnName("created_at");
                entity.Property(o => o.UpdatedAt).HasColumnName("updated_at");
            });
            #endregion

            #region Orders
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasKey(o => o.Id);
                entity.Property(o => o.Id).HasColumnName("id");
                entity.Property(o => o.UserId).HasColumnName("user_id");
                entity.Property(o => o.OrderId).HasColumnName("order_id");
                entity.Property(o => o.Amount).HasColumnName("amount");
                entity.Property(o => o.Status).HasColumnName("status").HasConversion<byte>();
                entity.Property(o => o.CreatedAt).HasColumnName("created_at");
            });
            #endregion
        }
    }
}
