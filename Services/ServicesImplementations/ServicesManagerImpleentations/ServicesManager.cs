

namespace Services.ServicesImplementations.ServicesManager
{
    public class ServicesManager(
        IUnitOfWork _unitOfWork, IMapper _mapper,
        IBasketRepository _basketRepository,
        UserManager<User> _userManager,
        IOptions<JwtOptions> _options
        ) : IServicesManager
    {

        // IUnitOfWork _unitOfWork, IBasketRepository _basketRepository, IMapper _mapper
        #region Lazy 

        private readonly Lazy<IProductServices> _productServices =
            new Lazy<IProductServices>(() => new ProductServices(_unitOfWork, _mapper));
        private readonly Lazy<IBasketServices> _basketServices =
                 new Lazy<IBasketServices>(() => new BasketServices(_basketRepository, _mapper));
        private readonly Lazy<IAuthenticationService> _authenticationService =
            new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _options)); 
        private readonly Lazy<IOrderServices> _orderServices =
            new Lazy<IOrderServices>(() => new OrderServices(_unitOfWork,  _basketRepository , _mapper));
        #endregion
        public IProductServices ProductServices => _productServices.Value;

        public IBasketServices BasketServices => _basketServices.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IOrderServices OrderServices => _orderServices.Value;
    }
}
