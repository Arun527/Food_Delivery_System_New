using Microsoft.EntityFrameworkCore;

namespace Food_Delivery.Models
{
    public class FoodDeliveryDbContext : DbContext
    {
        public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> OPtion) : base(OPtion) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Customer>().HasIndex(s => s.ContactNumber).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(s => s.Email).IsUnique();
            modelBuilder.Entity<Hotel>().HasIndex(s => s.ContactNumber).IsUnique();
            modelBuilder.Entity<Hotel>().HasIndex(s => s.Email).IsUnique();
            modelBuilder.Entity<DeliveryPerson>().HasIndex(s => s.ContactNumber).IsUnique();
       
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<DeliveryPerson> DeliveryPerson { get; set; }
        public DbSet<OrderShipmentDetail> OrderShipmentDetail { get; set; }
    }
}
