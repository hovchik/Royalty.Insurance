using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.VehicleInfos
{
    public class UpdateVehicleInfoCommand :VehicleInfoRequest, IRequest<VehicleInfoResponse>
    {
        public int Id { get; set; }
    }
}
