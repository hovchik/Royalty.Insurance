using FluentValidation;
using Royalty.Insurance.Proxy.APIModels.Marketing;

namespace Royalty.Insurance.BusinessLayer.CabMarketing.Queries.GetQuery
{
    public class GetQueryValidation : AbstractValidator<MarketingRequest>
    {
        public GetQueryValidation()
        {
            //should clarified with Ruben
        }
    }
}
