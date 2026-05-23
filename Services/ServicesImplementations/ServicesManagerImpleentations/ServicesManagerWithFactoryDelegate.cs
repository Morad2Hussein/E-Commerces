

using Services_Abstractions.Contracts.CacheSerices;

namespace Services.ServicesImplementations.ServicesManagerImpleentations
{
    public class ServicesManagerWithFactoryDelegate(
        Func<IProductServices> _productFactory , Func<IBasketServices> _basketFactory
      , Func<IAuthenticationService> _authenticationFactory , Func<IOrderServices> _orderFactory
      , Func<IPaymentServices> _paymentFactory , Func<ICacheSerices> _cacheFactory) : IServicesManager
    {
        public IProductServices ProductServices => _productFactory.Invoke();

        public IBasketServices BasketServices => _basketFactory.Invoke();

        public IAuthenticationService AuthenticationService => _authenticationFactory.Invoke();

        public IOrderServices OrderServices => _orderFactory.Invoke();

        public IPaymentServices PaymentServices => _paymentFactory.Invoke();

        public ICacheSerices CacheSerices => _cacheFactory.Invoke();
    }
}
