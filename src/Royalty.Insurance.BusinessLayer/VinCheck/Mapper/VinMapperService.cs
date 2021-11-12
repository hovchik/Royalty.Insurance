using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Proxy.VINModel;
using System.Collections.Generic;
using System.Linq;

namespace Royalty.Insurance.BusinessLayer.VinCheck
{
    public class VinMapperService : IVinMapperService
    {
        public IEnumerable<VinCheckResponse> Map(VinModel model)
        {
            return from result in model.Results
                   where !string.IsNullOrEmpty(result.Value) && !result.Value.Equals("Not Applicable") && !result.Value.Equals("0")
                   select new VinCheckResponse
                   {
                       Field = result.Variable,
                       Value = result.Value
                   };
        }
    }
}