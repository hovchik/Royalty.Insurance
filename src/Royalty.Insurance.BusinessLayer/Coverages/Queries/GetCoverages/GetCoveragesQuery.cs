using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.Coverages
{
    public class GetCoveragesQuery : IRequest<List<CoverageResponse>>
    {
    }
}
