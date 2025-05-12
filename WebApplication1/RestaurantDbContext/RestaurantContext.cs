using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class RestaurantContext : IdentityDbContext<User, Role, string>
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des relations
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.NoAction); // Pas de suppression en cascade

            // Exemple pour une autre relation avec suppression SetNull
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction); // Définit la clé étrangère sur NULL au lieu de supprimer

            // Configuration des relations
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.NoAction); // Pas de suppression en cascade


            // D'autres relations peuvent être configurées de la même manière
        }
    }
}
