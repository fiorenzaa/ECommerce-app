using Microsoft.EntityFrameworkCore;
using ECommerce.Models;

namespace ECommerce.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product => Set<Product>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Contoh: Mengatur nama tabel secara eksplisit
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<CartItem>().ToTable("CartItem");
        }
    }
}
