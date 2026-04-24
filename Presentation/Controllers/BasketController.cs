using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    public class BasketController(IServicesManager _servicesManager) : ApiController
    {
        [Authorize]
        #region EndPoint To Get Basket By ID
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasketAsync(string id)
        => Ok(await _servicesManager.BasketServices.GetBasketAsync(id));
        #endregion
        #region EndPoint To Create Or Update Basket 
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasketAsync(BasketDTO basketDTO)
            => Ok(await _servicesManager.BasketServices.UpdateOrCreateBasketAsync(basketDTO));
        #endregion
        #region EndPoint To Delete Basket By ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasketAsync(string id)
        {
            var deleted = await _servicesManager.BasketServices.DeleteAsync(id);
            return  NoContent() ;
        } 
        #endregion

    }
}
