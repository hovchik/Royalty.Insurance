using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Proxy.VINModel;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.VinCheck
{
    public interface IVinMapperService
    {
        IEnumerable<VinCheckResponse> Map(VinModel model);
    }
}