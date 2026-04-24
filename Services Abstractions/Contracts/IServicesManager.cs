


namespace Services_Abstractions.Contracts
{
    public interface IServicesManager
    {
        public IProductServices ProductServices { get; }
        public IBasketServices BasketServices { get; }
        public IAuthenticationService AuthenticationService { get; }
        public IOrderServices OrderServices { get; }
    }
}
