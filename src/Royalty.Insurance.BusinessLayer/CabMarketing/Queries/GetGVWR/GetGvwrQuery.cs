using MediatR;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetGvwrQuery : IRequest<List<GvwrResponse>>
    {
    }
}
