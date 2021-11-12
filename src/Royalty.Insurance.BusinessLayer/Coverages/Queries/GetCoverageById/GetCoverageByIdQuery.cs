using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Coverages
{
    public class GetCoverageByIdQuery : IRequest<CoverageResponse>
    {
        public int Id { get; set; }
    }
}
