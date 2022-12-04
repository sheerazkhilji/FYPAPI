using ClassLibrary1;
using FYPAPI.IServices;
using System.Threading;
using System.Threading.Tasks;

namespace FYPAPI.PaymentHelper
{
    public class PaymentMethod
    {
        private readonly IStripeAppService _stripeService;
        public PaymentMethod(IStripeAppService stripeAppService)
        {
            _stripeService=stripeAppService;
        }
        public async Task<object> AddStripePayment(AddStripePaymentclass payment, CancellationToken ct)
        {
            StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(
                payment,
                ct);



            return createdPayment;
        }
    }
}
