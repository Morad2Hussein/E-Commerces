
namespace Services_Abstractions.Contracts.PaymentServices
{
    public interface IPaymentServices
    {
        #region Create Or Update Payment Intent 
        Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId);
        #endregion
        #region Stripe
        Task UpdatePaymentStatusAsync(string json, string signatureHeader);
        #endregion
    }
}
