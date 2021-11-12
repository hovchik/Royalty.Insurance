using System.Collections.Generic;
using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.GroupMembers;
using Royalty.Insurance.BusinessLayer.Groups;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.Api.Messaging
{
    public interface IMessageClient
    {
        #region SignalR Events

        Task OnUserStatusChange(int userId, int status, string customStatus);

        Task ReceiveMessage(ReceiveMessageResponse sender);

        Task OnMessageMention(ReceiveMessageResponse sender);

        Task OnGroupCreated(int groupId);

        Task OnMembersAdded(int groupId, List<int> memberIds);

        Task OnMembersRemoved(int groupId, List<int> memberIds);

        Task OnReadMessage(long messageId, int userId);
        Task OnReadGroupMessage(long groupId, int userId);

        Task OnBuzzFire(int groupId, int userId, int buzzerUserId);

        Task OnFileMessageReceive(ReceiveMessageResponse response);

        Task BroadcastUserUnreadMessageChange(GroupUnreadMessageResponse response);

        Task OnMute(int groupId);
        Task OnUnMute(int groupId);

        #endregion

        #region SignalR Methods

        Task LoadMessages(PaginationResponse<FileMessageResponse> messages);


        Task Get(int groupId, int count, int pageIndex);

        Task GetLatest(int groupId, int pageIndex, int count);

        Task GetUserLatestMessages(int pageIndex, int count);

        Task ReadMessage(int messageId);

        Task ReadGroupMessage(int groupId);

        Task CreateGroup(CreateGroupCommand request);

        Task RemoveMembers(RemoveMemberCommand request, int groupId);

        Task CreateIndividualGroup(int userId);

        Task AddMembers(AddMembersCommand request, int groupId);

        Task SetStatus(int status);
        Task SetCustomStatus(string customStatus);

        Task UnMute(int groupId);

        Task Mute(int groupId);

        Task ForwardMessage(long messageId, int groupId);

        Task Buzz(int groupId, int userId);

        #endregion
    }
}
