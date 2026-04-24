

using Microsoft.AspNetCore.Authorization;
using Shared.DTOS.OrderDTOS;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServicesManager _servicesManager) : ApiController
    {

        #region Create Order 
        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrderAsync(OrderRequest orderRequest)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _servicesManager.OrderServices.CreateOrderAsync(orderRequest, userEmail );
            return Ok(order);
        }
        #endregion

        #region Get Order Id
        [HttpGet("{id:guid}") ]
        public async Task<ActionResult<OrderResult>> GetOrderByIdAsync(Guid id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _servicesManager.OrderServices.GetOrderByIdAsync(id);
           
            return Ok(order);
        }
        #endregion
        #region Get Order By Email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrdersForUserAsync()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _servicesManager.OrderServices.GetOrdersByEmailAsync(userEmail);
            return Ok(orders);
        }
        #endregion
        #region Get All Delivery Method 
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _servicesManager.OrderServices.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
        #endregion

    }
}
