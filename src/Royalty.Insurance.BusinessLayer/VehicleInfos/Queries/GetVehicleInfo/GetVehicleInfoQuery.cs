using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.VehicleInfos
{
    public class GetVehicleInfoQuery : IRequest<List<VehicleInfoResponse>>
    {
    }
}
