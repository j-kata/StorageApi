using StorageApi.Models.Entities;

namespace StorageApi.Services
{
    public interface IProductsRepository
    {
        public IQueryable<Product> GetFilteredProducts(string? category, string? name);
        public Task<Product?> GetProductAsync(int id);
        public void AddProduct(Product product);
        public void RemoveProduct(Product product);
        public Task<bool> SaveChangesAsync();
    }
}