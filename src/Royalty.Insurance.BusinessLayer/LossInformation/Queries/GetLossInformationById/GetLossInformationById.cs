using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.LossInfo
{
    public class GetLossInformationById : IRequest<LossInfoResponse>
    {
        public int Id { get; set; }
    }
}