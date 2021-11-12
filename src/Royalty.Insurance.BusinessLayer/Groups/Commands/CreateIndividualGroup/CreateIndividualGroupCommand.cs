using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class CreateIndividualGroupCommand : IRequest<GroupResponse>
    {
        public int UserId { get; set; }
    }
}
