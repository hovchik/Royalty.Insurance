using System.Threading;
using System.Threading.Tasks;

namespace Core.System.Delta
{
    public interface IReturnPremiumEndorsement
    {
        Task<ReturnPremiumEndorsementViewModel> SetUpAsync(ReturnPremiumEndorsementRequest request, CancellationToken cancellationToken);
    }
}
