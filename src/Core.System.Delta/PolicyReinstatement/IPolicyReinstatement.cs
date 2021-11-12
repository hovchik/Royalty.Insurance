using System.Threading;
using System.Threading.Tasks;

namespace Core.System.Delta
{
    public interface IPolicyReinstatement
    {
        Task<PolicyReinstatementViewModel> SetUpAsync(PolicyReinstatementRequest request, CancellationToken cancellationToken);
    }
}
