using System.Collections.Generic;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetUsersIndividualUnreadMessagesQuery : IRequest<List<GroupUnreadMessageResponse>>
    {
        public List<UserUnreadMessageSettingQuery> UnreadMessageSetting { get; set; }
    }
}
