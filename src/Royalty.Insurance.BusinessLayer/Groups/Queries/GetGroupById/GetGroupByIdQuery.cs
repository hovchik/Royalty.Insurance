using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class GetGroupByIdQuery : IRequest<GroupResponse>
    {
        public int Id { get; set; }
    }
}
