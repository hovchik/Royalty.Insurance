using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Coverages
{
    public class UpdateCoverageCommand : IRequest<CoverageResponse>
    {
        public int Id { get; set; }
        public CoverageRequest Request { get; set; }
    }
}
