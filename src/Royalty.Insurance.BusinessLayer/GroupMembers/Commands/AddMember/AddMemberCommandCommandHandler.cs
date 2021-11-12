using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class AddMemberCommandCommandHandler : IRequestHandler<AddMemberCommand, List<GroupMemberResponse>>
    {
        private readonly IRequestHandler<AddMembersCommand, List<GroupMemberResponse>> _handler;

        public AddMemberCommandCommandHandler(IRequestHandler<AddMembersCommand, List<GroupMemberResponse>> handler)
        {
            _handler = handler;
        }

        public async Task<List<GroupMemberResponse>> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {

            return await _handler.Handle(new AddMembersCommand {GroupId = request.GroupId, MemberIds = new List<int> {request.MemberId}}, cancellationToken);
        }
    }
}
