using ClassLibrary1;
using System.Threading;
using System.Threading.Tasks;

namespace FYPAPI.IServices
{
    public interface IStripeAppService
    {
        Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer, CancellationToken ct);
        Task<StripeCustomer> AddStripeCustomerAsyncformodelservice(AddStripeCardForModelService obj,
            CancellationToken ct );
        Task<StripePayment> AddStripePaymentAsync(AddStripePaymentclass payment, CancellationToken ct);
    }
}
