using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.ProRateCalculator
{
    public class GetProRateCalcQuery : IRequest<ProRateResponse>
    {
        public ProRateRequest ProRateRequest { get; set; }
    }
}