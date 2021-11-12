using System.Collections.Generic;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class GetAchTypeQuery : IRequest<List<AchTypeResponse>>
    {

    }
}