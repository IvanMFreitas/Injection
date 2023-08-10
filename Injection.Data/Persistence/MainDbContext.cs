using Microsoft.EntityFrameworkCore;
using Injection.Entities;

namespace Injection.Data.Persistence
{
    public class MainDbContext : DbContext, IMainDbContext
    {
        public MainDbContext (){}
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var item in ChangeTracker.Entries<IEntity>().AsEnumerable())
            {
                //Auto Timestamp
                item.Entity.CreatedAt = DateTime.Now;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=Injection;User Id=sa;Password=ab123CD*;Encrypt=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Order>().ToTable("Order");

            var personId1 = Guid.NewGuid();
            var personId2 = Guid.NewGuid();
            var product1Id = Guid.NewGuid();
            var product2Id = Guid.NewGuid();

            //Seed some dummy data
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = personId1, Name = "Person 1", Email = "person1@api.com", IsAdmin = true, CreatedAt = DateTime.Now },
                new Person { Id = personId2, Name = "Person 2", Email = "person2@api.com", IsAdmin = false, CreatedAt = DateTime.Now });

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = product1Id, Name = "Product 1", Price = 2m, CreatedAt = DateTime.Now },
                new Product { Id = product2Id, Name = "Product 2", Price = 5m, CreatedAt = DateTime.Now });

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = Guid.NewGuid(), PersonId = personId1, ProductId = product1Id, Qty = 1, Total = 2m, CreatedAt = DateTime.Now  });

        }
    }
}