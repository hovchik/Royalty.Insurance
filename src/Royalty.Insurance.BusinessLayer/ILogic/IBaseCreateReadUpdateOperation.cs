using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.Validator;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.ILogic
{
    public interface IBaseCreateReadUpdateOperation<in TRequest, TResponse> where TResponse : class where TRequest: new()

    {
        Task<TResponse> GetAsync(int id);
        Task<PaginationResponse<TResponse>> GetAsync(PaginationCommand command);
        Task<TResponse> CreateAsync(TRequest request);

        Task<TResponse> UpdateAsync(int id, TRequest request);
    }
}
