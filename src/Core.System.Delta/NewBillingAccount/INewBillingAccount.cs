using System.Threading;
using System.Threading.Tasks;

namespace Core.System.Delta
{
    public interface INewBillingAccount
    {
        Task<DeltaBillingAccountViewModel> SetUpAsync(NewBillingAccountRequest request, CancellationToken cancellationToken);
    }
}
