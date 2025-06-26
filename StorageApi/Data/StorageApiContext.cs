using Microsoft.EntityFrameworkCore;
using StorageApi.Models.Entities;

namespace StorageApi.Data
{
    public class StorageApiContext(DbContextOptions<StorageApiContext> options)
        : DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Wireless Mouse", Price = 299, Category = "Electronics", Shelf = "E1", Count = 15, Description = "Ergonomic mouse with 2.4 GHz receiver." },
                new Product { Id = 2, Name = "USB-C Charger", Price = 199, Category = "Electronics", Shelf = "E2", Count = 30, Description = "20W fast charger, compatible with most phones." },
                new Product { Id = 3, Name = "Yoga Mat", Price = 349, Category = "Sports", Shelf = "S3", Count = 12, Description = "Non-slip exercise mat made of durable material." },
                new Product { Id = 4, Name = "Notebook A5", Price = 79, Category = "Office", Shelf = "O1", Count = 50, Description = "80 pages, lined with soft cover." },
                new Product { Id = 5, Name = "Coffee Mug", Price = 99, Category = "Kitchen", Shelf = "K2", Count = 40, Description = "Ceramic cup 300 ml, dishwasher safe." },
                new Product { Id = 6, Name = "HDMI Cable 2m", Price = 149, Category = "Electronics", Shelf = "E3", Count = 20, Description = "HDMI 2.1 supporting 4K and 8K." },
                new Product { Id = 7, Name = "Reflective Vest", Price = 129, Category = "Sports", Shelf = "S4", Count = 18, Description = "Safety vest for running or cycling." },
                new Product { Id = 8, Name = "Scissors", Price = 59, Category = "Office", Shelf = "O2", Count = 25, Description = "Multi-purpose scissors with ergonomic grip." }
            );
            base.OnModelCreating(builder);
        }
    }
}
