

using Microsoft.Extensions.Configuration;
using Services_Abstractions.Contracts.PaymentServices;
using Stripe;
using Product = Domain.Entities.ProductModule.Product;
using Order = Domain.Entities.OrderModule.Order;

namespace Services.ServicesImplementations.PaymentServicesImplementations
{
    public class PaymentServices(
        IConfiguration _configuration, IBasketRepository _basketRepository,
        IUnitOfWork _unitOfWork, IMapper _mapper) : IPaymentServices
    {
        public async Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId)
        {
            // set up secret key and client
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];
            // get basket  by basket id
            var basket = await GetBasketAsync(basketId);
            // validate item price and shiipingPrice 
            await ValidateBasketAsync(basket);
            // calculate total price = Subtotal + shipping price
            var total = CalculateTotal(basket);
            await CreationOrUpdatePaymentIntentAsync(basket, total);
            await _basketRepository.UpdateOrCreateBasketAsync(basket);
            return _mapper.Map<BasketDTO>(basket);
        }

        private async Task CreationOrUpdatePaymentIntentAsync(CustomerBasket basket, long total)
        {
            var stripeService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId) || basket.PaymentIntentId == "string")
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = total,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };
                var paymentIntent = await stripeService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = total
                };
                await stripeService.UpdateAsync(basket.PaymentIntentId, options);
            }
        }


        #region Update Payment Status  Stripe 
        public async Task UpdatePaymentStatusAsync(string json, string signatureHeader)
        {
            var endpointSecret = _configuration.GetSection("StripeSettings")["EndPointSecret"];


            var stripeEvent = EventUtility.ParseEvent(json, throwOnApiVersionMismatch:false);

            stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, endpointSecret, throwOnApiVersionMismatch: false);
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {

                 await UpdatePaymentStatusRecievedAysnc(paymentIntent!.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPartiallyFunded)
            {
                await UpdatePaymentStatusFailedAysnc(paymentIntent!.Id);
            }
            // ... handle other event types
            else
            {
                // Unexpected event type
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
        }


        #endregion

        #region HelperMethods
        private async Task<CustomerBasket> GetBasketAsync(string basketId) =>
         await _basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);
        private async Task ValidateBasketAsync(CustomerBasket basket)
        {
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id) ??
                    throw new ProductNotFoundException(item.Id);
                item.Price = productItem.Price;
            }
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("Delivery method is not selected");
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().
                GetByIdAsync(basket.DeliveryMethodId.Value) ??
                     throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Price;
        }

        private long CalculateTotal(CustomerBasket basket) =>
            (long)(basket.Items.Sum(item => item.Price) + basket.ShippingPrice!) * 100;

        private async Task UpdatePaymentStatusRecievedAysnc(string paymentIntentId)
        {
            var repo = _unitOfWork.GetRepository<Order, Guid>();
            var spec = new OrderWithPaymentIntentSpecifications(paymentIntentId);
            var order = await repo.GetByIdAsync(spec);
            if (order is not null)
            {
                order.PaymentStatus = OrderPaymentStatus.PaymentReceived;
                 repo.Update(order);
                await _unitOfWork.SaveChangesAsync();
            }


        }
        private async Task UpdatePaymentStatusFailedAysnc(string paymentIntentId)
        {
            var repo = _unitOfWork.GetRepository<Order, Guid>();
            var spec = new OrderWithPaymentIntentSpecifications(paymentIntentId);
            var order = await repo.GetByIdAsync(spec);
            if (order is not null)
            {
                order.PaymentStatus = OrderPaymentStatus.PaymentFailed;
                repo.Update(order);
                await _unitOfWork.SaveChangesAsync();
            }


        }



        #endregion

    }
}
