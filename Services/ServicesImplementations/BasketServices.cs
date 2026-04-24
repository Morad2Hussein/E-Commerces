

namespace Services.ServicesImplementations
{
    public class BasketServices(IBasketRepository _basketRepository , IMapper _mapper) : IBasketServices
    {

        #region GetBasket By Id
        public async Task<BasketDTO> GetBasketAsync(string id)
        {
          var basket = await _basketRepository.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) : _mapper.Map<BasketDTO>(basket);
        } 
        #endregion
        #region Created Or Updated 
        public async Task<BasketDTO> UpdateOrCreateBasketAsync(BasketDTO basketDTO)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDTO);
            var createdOrupdated = await _basketRepository.UpdateOrCreateBasketAsync(basket);
            return createdOrupdated is null ? throw new Exception("can not Create or Update basket") :
                _mapper.Map<BasketDTO>(createdOrupdated);
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteAsync(string id)
        {
            var deleted = await _basketRepository.DeleteBasketAsync(id);

            if (!deleted)   throw new Exception($"Could not delete basket with ID: {id}");
            

            return deleted;
        }
        #endregion
    }
}
