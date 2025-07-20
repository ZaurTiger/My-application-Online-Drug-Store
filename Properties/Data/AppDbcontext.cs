using Microsoft.EntityFrameworkCore;
using Api_v2.Properties.Models;

namespace Api_v2.Properties.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; } = null!;
        
        public DbSet<Cart> Cart { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;
        
        public DbSet<OrderItems> OrderItems { get; set; } = null!;
        
        public DbSet<Medicine> Medicines { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.Item)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(oi => oi.ItemId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Item)
                .WithMany(m => m.Carts)
                .HasForeignKey(c => c.ItemId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);
        }
    }
}