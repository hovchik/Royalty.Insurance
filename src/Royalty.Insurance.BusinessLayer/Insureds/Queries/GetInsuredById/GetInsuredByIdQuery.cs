using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class GetInsuredByIdQuery : IRequest<InsuredResponse>
    {
        public int Id { get; set; }
    }
}
