using System.Threading;
using System.Threading.Tasks;

namespace Core.System.MicrosoftGraph
{
    public interface IGetDefaultPrivateGroupId
    {
        Task<string> Handle(CancellationToken cancellationToken);
    }
}
