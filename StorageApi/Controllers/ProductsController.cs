using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageApi.Data;
using StorageApi.Models.Entities;
using StorageApi.Models.Dtos;
using StorageApi.Mappings;

namespace StorageApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StorageApiContext _context;

        public ProductsController(StorageApiContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProduct(string? category, string? name)
        {
            IQueryable<Product> products = _context.Product;
            if (!string.IsNullOrWhiteSpace(category))
                products = products.Where(p => p.Category == category);
            if (!string.IsNullOrWhiteSpace(name))
                products = products.Where(p => p.Name == name);

            return products.Select(ProductMappings.FromEntity).ToList();
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return ProductMappings.FromEntity(product);
        }

        // GET: api/Products/stats
        [HttpGet("stats")]
        public async Task<ActionResult<ProductsStatDto>> GetProductStats()
        {
            var products = await _context.Product.ToListAsync();
            if (products.Count == 0)
            {
                return new ProductsStatDto { TotalPrice = 0, AveragePrice = 0, TotalCount = 0 };
            }

            var productsCount = products.Sum(product => product.Count);
            var priceTotal = products.Aggregate(0, (sum, product) => sum + product.Price * product.Count);
            var priceAverage = priceTotal / productsCount;

            return new ProductsStatDto
            {
                TotalPrice = priceTotal,
                AveragePrice = priceAverage,
                TotalCount = productsCount,
            };
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            var product = ProductMappings.ToEntity(productDto);

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateProductDto productDto)
        {
            var product = ProductMappings.ToEntity(productDto);

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
