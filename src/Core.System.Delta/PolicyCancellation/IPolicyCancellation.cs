
using System.Threading;
using System.Threading.Tasks;

namespace Core.System.Delta
{
    public interface IPolicyCancellation
    {
        Task<PolicyCancellationViewModel> SetUpAsync(PolicyCancellationRequest request, CancellationToken cancellationToken);
    }
}
