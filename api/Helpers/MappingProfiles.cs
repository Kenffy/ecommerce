using api.Dtos;
using AutoMapper;
using Core.Entities;

namespace api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
            .ForMember(x => x.ProductBrand, y => y.MapFrom(z => z.ProductBrand.Name))
            .ForMember(x => x.ProductType, y => y.MapFrom(z => z.ProductType.Name))
            .ForMember(x => x.PictureUrl, y => y.MapFrom<ProductUrlResolver>());
        }
    }
}