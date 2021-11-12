using System.Threading;
using System.Threading.Tasks;

namespace Core.System.Delta
{
    public interface IBaseDeltaRequestHandler
    {
        Task<TResponse> PostAsync<TResponse, TRequest>(TRequest request, string url,
            CancellationToken cancellationToken);
    }
}
