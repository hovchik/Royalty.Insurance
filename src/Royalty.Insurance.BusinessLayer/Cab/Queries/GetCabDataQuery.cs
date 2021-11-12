using MediatR;
using Royalty.Insurance.Proxy.APIResponseModels;

namespace Royalty.Insurance.BusinessLayer.Cab
{
    public class GetCabDataQuery : IRequest<QuoteSheetModel>
    {
        public int DotNumber { get; set; }
    }
}
