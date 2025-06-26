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

        public IQueryable<Product> GetFilteredProducts(string? category, string? name)
        {
            IQueryable<Product> products = _context.Products;
            if (!string.IsNullOrWhiteSpace(category))
                products = products.Where(p => EF.Functions.Like(p.Category, $"%{category}%"));
            if (!string.IsNullOrWhiteSpace(name))
                products = products.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));

            return products;
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}