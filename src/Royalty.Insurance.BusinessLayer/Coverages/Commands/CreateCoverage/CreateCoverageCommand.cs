using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Coverages
{
    public class CreateCoverageCommand : IRequest<CoverageResponse>
    {
        public CoverageRequest Request { get; set; }
    }
}
