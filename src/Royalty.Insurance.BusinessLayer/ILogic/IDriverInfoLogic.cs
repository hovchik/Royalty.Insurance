using System.Collections.Generic;
using System.Threading.Tasks;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.ILogic
{
    public interface IDriverInfoLogic
    {
        Task<List<DriverInfoResponse>> Get();
        Task<DriverInfoResponse> Get(int id);
        Task<DriverInfoResponse> Update(int id, DriverInfoRequest request, int createdBy);
        Task<DriverInfoResponse> Create(DriverInfoRequest request, int createdBy);
    }
}
