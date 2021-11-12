using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Agencies.Queries
{
    public class GetAgencyByIdQuery : IRequest<AgencyResponse>
    {
        public int Id { get; set; }
    }
}
