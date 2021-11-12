using System.Threading.Tasks;


namespace Royalty.Insurance.BusinessLayer.ILogic
{
    public interface IBaseCreateReadUpdateDeleteOperation<in TRequest,TResponse> : IBaseCreateReadUpdateOperation<TRequest, TResponse> where TRequest : new() where  TResponse : class
    {
        Task DeleteAsync(int id);
    }
}
