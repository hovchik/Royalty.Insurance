
using System.Threading;
using System.Threading.Tasks;

namespace Core.System.Delta
{
    public interface IInsuredInformationChange
    {
        Task<InsuredInformationChangeViewModel> SetUpAsync(InsuredInformationChangeRequest request, CancellationToken cancellationToken);
    }
}
