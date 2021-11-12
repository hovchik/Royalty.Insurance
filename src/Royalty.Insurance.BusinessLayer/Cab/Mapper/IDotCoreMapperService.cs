using Royalty.Insurance.Proxy.APIModels.Core;
using Royalty.Insurance.Proxy.APIResponseModels;
using System;
using System.Linq.Expressions;
using Royalty.Insurance.Proxy.APIModels;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.Cab
{
    public interface IDotCoreMapperService
    {
        void UpdateEntity(QuoteSheetModel entity, DotCoreResponse coreRequest, DOTResponse request, List<CabVinResponse> vinCodeAndVehicleInfo);
        Expression<Func<DotCoreResponse, QuoteSheetModel>> MapResponse { get; }
    }
}
