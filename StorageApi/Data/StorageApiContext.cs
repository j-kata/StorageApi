using Microsoft.EntityFrameworkCore;
using StorageApi.Models.Entities;

namespace StorageApi.Data
{
    public class StorageApiContext : DbContext
    {
        public StorageApiContext(DbContextOptions<StorageApiContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
    }
}
