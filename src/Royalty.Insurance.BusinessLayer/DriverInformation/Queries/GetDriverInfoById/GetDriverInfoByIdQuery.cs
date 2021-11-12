using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.DriverInfo
{
    public class GetDriverInfoByIdQuery : IRequest<DriverInfoResponse>
    {
        public int Id { get; set; }
    }
}