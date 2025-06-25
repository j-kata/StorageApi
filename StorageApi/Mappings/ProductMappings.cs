using StorageApi.Models.Dtos;
using StorageApi.Models.Entities;

namespace StorageApi.Mappings
{
    public static class ProductMappings
    {
        public static ProductDto FromEntity(Product product)
        {             
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Count = product.Count
            };
        }

        public static Product ToEntity(CreateProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Category = productDto.Category,
                Description = productDto.Description,
                Count = productDto.Count,
                Shelf = productDto.Shelf
            };
        }

        public static Product ToEntity(UpdateProductDto productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                Category = productDto.Category,
                Shelf = productDto.Shelf,
                Count = productDto.Count,
                Description = productDto.Description
            };
        }
    }
}
