using AutoMapper;

namespace StorageApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.Entities.Product, Models.Dtos.ProductDto>();
            CreateMap<Models.Entities.Product, Models.Dtos.ReadProductDto>();
            CreateMap<Models.Dtos.CreateProductDto, Models.Entities.Product>();
            CreateMap<Models.Dtos.UpdateProductDto, Models.Entities.Product>();
        }

    }
}
