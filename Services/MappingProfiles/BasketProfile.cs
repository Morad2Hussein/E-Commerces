namespace Services.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            // Map the item types first (AutoMapper needs this for the collection)
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();

            // Single map with explicit member bindings — no duplicate CreateMap
            CreateMap<CustomerBasket, BasketDTO>()
                .ForMember(dest => dest.BasketItemDTO, opt => opt.MapFrom(src => src.Items))
                .ReverseMap()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.BasketItemDTO));
        }
    }
}