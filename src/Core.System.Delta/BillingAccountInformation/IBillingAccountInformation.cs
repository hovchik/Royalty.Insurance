using System.Threading;
using System.Threading.Tasks;

namespace Core.System.Delta
{
    public interface IBillingAccountInformation
    {
        Task<BillingAccountInformationViewModel> SetUpAsync(BillingAccountInformationRequest request, CancellationToken cancellationToken);
    }
}
