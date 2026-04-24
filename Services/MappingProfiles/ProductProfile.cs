

using AutoMapper;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() { 
          

            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();

            CreateMap<Product,ProductResultDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl , opt => opt.MapFrom<PictureUrlResolver>());
        }
    }
}
