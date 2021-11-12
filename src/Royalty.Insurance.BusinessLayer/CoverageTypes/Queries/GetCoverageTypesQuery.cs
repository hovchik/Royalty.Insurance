using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.CoverageTypes
{
    public class GetCoverageTypesQuery : IRequest<List<CoverageTypeResponse>>
    {
    }
}