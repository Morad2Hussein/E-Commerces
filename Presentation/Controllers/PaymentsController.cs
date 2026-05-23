
namespace Presentation.Controllers
{
    public class PaymentsController(IServicesManager _servicesManager) : ApiController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
         => Ok(await _servicesManager.PaymentServices.CreateOrUpdatePaymentIntent(basketId));

        [HttpPost("webhook")]   
        public async Task<ActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                var signatureHeader = Request.Headers["Stripe-Signature"];
            await _servicesManager.PaymentServices.UpdatePaymentStatusAsync(json, signatureHeader);
            return new EmptyResult();

        }

    }
}
 