using MediatR;
using Royalty.Insurance.Proxy.APIResponseModels;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class CreateInsuredCommand : IRequest<InsuredResponse>
    {
        public QuoteSheetRequest Request { get; set; }
    }
}
