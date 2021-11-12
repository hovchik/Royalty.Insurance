using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.VehicleInfos
{
    public class GetVehicleInfoByIdQuery : IRequest<VehicleInfoResponse>
    {
        public int Id { get; set; }
    }
}
