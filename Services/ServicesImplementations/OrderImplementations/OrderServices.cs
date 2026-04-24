

using ShippingAddress = Domain.Entities.OrderModule.Address;

namespace Services.ServicesImplementations.OrderImplementations
{
    internal class OrderServices(
        IUnitOfWork _unitOfWork, IBasketRepository _basketRepository, IMapper _mapper) : IOrderServices

    {
        #region GetDeliveryMethodsAsync
        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var delivery = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResult>>(delivery);
        }
        #endregion

        #region GetOrderByIdAsync
        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            
            var spec = new OrderWithIncludeSpecifications(id);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            var order = orders.FirstOrDefault() ?? throw new OrderNotFoundExceptions(id);
             return _mapper.Map<OrderResult>(order);

        }
        #endregion
       #region GetOrdersByEmailAsync
        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string useremail)
        {
                var spec = new OrderWithIncludeSpecifications(useremail);
                var orders =await  _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
                return  _mapper.Map<IEnumerable<OrderResult>>(orders);
        }
        #endregion
        #region CreateOrderAsync
        public async Task<OrderResult> CreateOrderAsync(OrderRequest order, string useremail)
        {

            // Mapping from AddressDto to shipping address in OrderResult
            var shippingAddress = _mapper.Map<ShippingAddress>(order.ShippingAddress);
            // [OrderItems] => Basket [BasketId] => BasketItems => OrderItems
            #region create Order Items
            var basket =
                   await _basketRepository.GetBasketAsync(order.BasketId) ??
                   throw new BasketNotFoundException(order.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product =
                    await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            } 
            #endregion
            // Get Delivery Method Price => DeliveryMethodId => DeliveryMethod => Price
            var deliveryMethod =
                await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(order.DeliveryMethodId) ??
                throw new DeliveryMethodNotFoundException(order.DeliveryMethodId);
            //  Calculate Subtotal => OrderItems => Price * Quantity
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            // Create Order => Add to DB => Return OrderResult DTO
            var newOrder = new Order(useremail, shippingAddress, orderItems,  deliveryMethod, subtotal );
            // save to DB
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(newOrder);
            await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<OrderResult>(newOrder);
        }
        #endregion
        #region Create OrderItem Method 
        private OrderItem CreateOrderItem(BasketItem item , Product product)
        {
            return new OrderItem
            (
              new ProductInOrderItem(product.Id, product.Name, product.PictureUrl),
                item.Price,
               item.Quantity
                )
            ;
        }
        #endregion

    }
}
