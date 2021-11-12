using MediatR;
using Royalty.Insurance.Proxy.APIModels.Marketing;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetSearchingRootQuery : IRequest<SearchingRoot>
    {
        public string Request { get; set; }
    }
}
