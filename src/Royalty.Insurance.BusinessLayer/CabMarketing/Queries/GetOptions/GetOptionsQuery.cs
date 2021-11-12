using MediatR;
using Royalty.Insurance.Proxy.APIModels.Marketing;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetOptionsQuery : IRequest<CabMarketingOptions>
    {
    }
}
