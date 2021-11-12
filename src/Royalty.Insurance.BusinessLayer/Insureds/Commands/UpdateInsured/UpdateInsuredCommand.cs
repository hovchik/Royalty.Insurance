using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class UpdateInsuredCommand : IRequest<InsuredResponse>
    {
        public int Id { get; set; }
        public InsuredRequest Request { get; set; }
    }
}
