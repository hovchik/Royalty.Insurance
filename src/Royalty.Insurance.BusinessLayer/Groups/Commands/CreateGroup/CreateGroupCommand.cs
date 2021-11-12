using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class CreateGroupCommand : IRequest<GroupResponse>
    {
        public string Name { get; set; }
    }
}
