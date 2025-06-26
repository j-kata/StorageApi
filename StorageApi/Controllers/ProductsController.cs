using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageApi.Data;
using StorageApi.Models.Entities;
using StorageApi.Models.Dtos;
using AutoMapper;
using StorageApi.Services;
using NuGet.Protocol.Core.Types;

namespace StorageApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct(string? category, string? name)
        {
            var products = await _repository.GetFilteredProductsAsync(category, name).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _repository.GetProductAsync(id);

            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        // GET: api/Products/stats
        [HttpGet("stats")]
        public async Task<ActionResult<ProductStatsDto>> GetProductStats(string? category, string? name)
        {
            var stats = await _repository.GetFilteredProductsAsync(category, name)
                .GroupBy(p => 1)
                .Select(g => new ProductStatsDto
                {
                    TotalCount = g.Sum(p => p.Count),
                    TotalPrice = g.Sum(p => p.Price * p.Count),
                    AveragePrice = g.Average(p => p.Price)
                }).FirstOrDefaultAsync();

            return stats ?? new ProductStatsDto();
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto productDto)
        {
            if (id != productDto.Id)
                return BadRequest();

            var product = await _repository.GetProductAsync(id);

            if (product == null)
                return NotFound();

            _mapper.Map(productDto, product);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            _repository.AddProduct(product);
            await _repository.SaveChangesAsync();

            var readProductDto = _mapper.Map<ReadProductDto>(product);

            return CreatedAtAction("GetProduct", new { id = readProductDto.Id }, readProductDto);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repository.GetProductAsync(id);

            if (product == null)
                return NotFound();

            _repository.RemoveProduct(product);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
