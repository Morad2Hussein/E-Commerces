using ShippingAddress = Domain.Entities.OrderModule.Address;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Address Mapping to AddressDTO
            CreateMap<ShippingAddress, AddressDtos>().ReverseMap();
            // DeliveryMethod Mapping to DeliveryMethodResult
            CreateMap<DeliveryMethod, DeliveryMethodResult>().
                ForMember(dest => dest.Cost, options => options.MapFrom(src => src.Price));
            // OrderItem Mapping to OrderItemDTO
            #region OrderItem Mapping to OrderItemDTO
            CreateMap<OrderItem, OrderItemDTO>()
                 .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                 .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                 .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Product.PictureUrl));
            #endregion
            // order to orderResult
            #region MyRegion
            CreateMap<Order, OrderResult>()
                   .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
                   .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                   .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Subtotal + src.DeliveryMethod.Price));
            #endregion
        }
    }
}
