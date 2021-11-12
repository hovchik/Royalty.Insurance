using System.Threading;
using System.Threading.Tasks;

namespace Core.System.Delta
{
    public interface IAdditionalPremiumEndorsement
    {
        Task<PremiumEndorsementViewModel> SetUpAsync(PremiumEndorsementRequest request, CancellationToken cancellationToken);
    }
}
