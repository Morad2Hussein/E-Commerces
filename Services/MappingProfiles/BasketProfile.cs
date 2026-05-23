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
                .ReverseMap();
        }
    }
}