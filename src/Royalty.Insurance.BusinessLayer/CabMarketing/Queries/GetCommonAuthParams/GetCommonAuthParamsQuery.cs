using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetCommonAuthParamsQuery : IRequest<List<CommonAuthTypeResponse>>
    {
    }
}
