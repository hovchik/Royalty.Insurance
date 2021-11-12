using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class eCheckAgaveCommand : IRequest<AgaveRoyaltyResponse>
    {
        public AgaveRoyaltyCheckRequest AgaveCheckRequest { get; set; }
    }
}