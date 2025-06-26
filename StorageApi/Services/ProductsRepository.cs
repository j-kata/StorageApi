using Microsoft.EntityFrameworkCore;
using StorageApi.Data;
using StorageApi.Models.Entities;

namespace StorageApi.Services
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly StorageApiContext _context;

        public ProductsRepository(StorageApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(string? category, string? name)
        {
            IQueryable<Product> products = _context.Product;
            if (!string.IsNullOrWhiteSpace(category))
                products = products.Where(p => p.Category == category);
            if (!string.IsNullOrWhiteSpace(name))
                products = products.Where(p => p.Name == name);

            return await products.ToListAsync();
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await _context.Product.FindAsync(id);
        }

        public void AddProduct(Product product)
        {
            _context.Product.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            _context.Product.Remove(product);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}