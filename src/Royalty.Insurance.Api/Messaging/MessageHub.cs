using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.BusinessLayer.Account;
using Royalty.Insurance.BusinessLayer.GroupMembers;
using Royalty.Insurance.BusinessLayer.Groups;
using Royalty.Insurance.BusinessLayer.ILogic;
using Royalty.Insurance.BusinessLayer.Messages;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Api.Messaging
{
    [Authorize]
    public class MessageHub : Hub<IMessageClient>
    {
        private int UserId => Context.User.UserId();
        private readonly UnreadTicker _unreadTicker;
        private readonly IOnlineLogic _onlineLogic;
        private readonly ISender _mediator;

        //protected ISender Mediator => _mediator ??= Context.GetService<ISender>(); 
        //remove for now till figUre out
        //private readonly IEventBroker<MessageResponse> _messageEventBroker;
        private readonly ILogger<MessageHub> _logger;


        public MessageHub(IOnlineLogic onlineLogic,  /*IEventBroker<MessageResponse> messageEventBroker,*/ ILogger<MessageHub> logger, UnreadTicker unreadTicker, ISender mediator)
        {
            _onlineLogic = onlineLogic;
            //_messageEventBroker = messageEventBroker;
            _logger = logger;
            _unreadTicker = unreadTicker;
            _mediator = mediator;
        }

        #region SignalR Events

        public override async Task OnConnectedAsync()
        {
            _logger.LogDebug($"Connection established connection id is {Context.ConnectionId}");
            var tasks = new List<Task>();
            // Add to Chat Groups
            var groups = await _mediator.Send(new GetOrCreateIfNotExistsIndividualGroupsQuery());
            groups.AddRange(await _mediator.Send(new GetUserGroupsByTypeQuery {GroupTypeCode = GroupTypeCode.Group}));
            tasks.AddRange(groups.Select(async group =>
                await Groups.AddToGroupAsync(Context.ConnectionId, group.Id.ToString())));
            await Task.WhenAll(tasks);
            if (!_onlineLogic.IsOnline(UserId))
            {
                var response = await _mediator.Send(new RecoverUserStatusCommand());
                await Clients.All.OnUserStatusChange(UserId, response.UserStatusId, response.CustomStatus);
            }
            _onlineLogic.AddOnlineDevice(UserId, Context.ConnectionId);
            GetUsersIndividualUnreadMessagesQuery request = new GetUsersIndividualUnreadMessagesQuery
            {
                UnreadMessageSetting = new List<UserUnreadMessageSettingQuery>()
            };
            request.UnreadMessageSetting.Add(new UserUnreadMessageSettingQuery{UserId = UserId, UnReadPreferenceInMinutes = 15});
            await _unreadTicker.LoadUnreadMessages(request, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogDebug($"Disconnect connection id is {Context.ConnectionId}");
            _onlineLogic.RemoveOnlineDevice(UserId, Context.ConnectionId);
            var tasks = new List<Task>();
            var groups = await _mediator.Send(new GetOrCreateIfNotExistsIndividualGroupsQuery());
            groups.AddRange(await _mediator.Send(new GetUserGroupsByTypeQuery { GroupTypeCode = GroupTypeCode.Group }));
            tasks.AddRange(groups.Select(async group =>
                await Groups.AddToGroupAsync(Context.ConnectionId, group.Id.ToString())));

            await Task.WhenAll(tasks);
            if (!_onlineLogic.IsOnline(UserId))
            {
                await _mediator.Send(new SetUserStatusCommand {UserStatus = UserStatusCode.Offline, UserId = UserId});
                await Clients.All.OnUserStatusChange(UserId, (int)UserStatusCode.Offline, null);
            }
            await Logout(Context.User);
            await base.OnDisconnectedAsync(exception);
        }

        #endregion

        #region Public Hub Actions

        public async Task CreateGroup(CreateGroupCommand request)
        {
            _logger.LogDebug($"Items in connection count {Context.Items.Count}");
            foreach (var contextItem in Context.Items)
            {
                _logger.LogDebug($"Items in connection key  {contextItem.Key} value {contextItem.Value} ");
            }

            GroupResponse response = await _mediator.Send(request);
            await Groups.AddToGroupAsync(Context.ConnectionId, response.Id.ToString());
            await Clients.User(UserId.ToString()).OnGroupCreated(response.Id);
        }

        public async Task CreateIndividualGroup(int userId)
        {
            GroupResponse response = await _mediator.Send(new CreateIndividualGroupCommand {UserId = userId });
             await Groups.AddToGroupAsync(Context.ConnectionId, response.Id.ToString());
            await Clients.Group(response.Id.ToString()).OnGroupCreated(response.Id);
        }

        public async Task AddMembers(AddMembersCommand request, int groupId)
        {
            request.GroupId = groupId;
            request.UserRequestedId = UserId;
            var response = await _mediator.Send(request);
            var connectionsId = new List<string>();
            response.SelectMany(item => item.Members)
                .ToList()
                .ForEach(item => connectionsId.AddRange(_onlineLogic.GetConnectionIdByUserId(item.UserId)));
            var tasks = new List<Task>();
            tasks.AddRange(connectionsId.Select(async connectionId =>
                await Groups.AddToGroupAsync(connectionId, groupId.ToString())));
            await Clients.Group(groupId.ToString())
                .OnMembersAdded(groupId,
                    response.SelectMany(item => item.Members.Select(member => member.UserId)).ToList());
            await Task.WhenAll(tasks);
        }

        public async Task RemoveMembers(RemoveMemberCommand request, int groupId)
        {
            request.GroupId = groupId;
            request.UserRequestedId = UserId;
            var response =  await _mediator.Send(request);
            var connectionsId = new List<string>();
            foreach (var member in response.Members)
            {
                connectionsId.AddRange(_onlineLogic.GetConnectionIdByUserId(member.UserId));
            }

            var tasks = new List<Task>();
            tasks.AddRange(connectionsId.Select(async connectionId =>
                await Groups.AddToGroupAsync(connectionId, groupId.ToString())));
            await Clients.Group(groupId.ToString())
                .OnMembersRemoved(groupId,
                    response.Members.Select(item => item.UserId).ToList());
            await Task.WhenAll(tasks);
        }

        public async Task Send(string content, int groupId)
        {
            var message = new CreateMessageCommand
            {
                Content = content,
                GroupId = groupId,
                UserId = UserId,
            };

            await AddMessage(message);
        }

        public async Task ForwardMessage(long messageId, int groupId)
        {
            var response = await _mediator.Send(new GetMessageByIdQuery {Id = messageId });
            var request = new ForwardMessageCommand
            {
                ParentId = response.MessageId,
                UserId = UserId,
                Content = response.Content,
                GroupId = groupId,
            };
            var result = await _mediator.Send(request);

            ReceiveMessageResponse sender = new ReceiveMessageResponse(result.MessageId, result.Content, result.GroupTypeId,
                result.GroupId, 
                UserId, result.SentDate, result.AttachmentsPath, result.MessageAuthorId);

            await Clients.Group(result.GroupId.ToString()).ReceiveMessage(sender);
        }

        public async Task Buzz(int groupId, int userId)
        {
            await Clients.Group(groupId.ToString()).OnBuzzFire(groupId, userId, UserId);
        }

        public async Task GetUserLatestMessages(int pageIndex, int count)
        {
            var messages = await _mediator.Send<PaginationResponse<FileMessageResponse>>(new GetUserMessagesQuery{UserId = UserId , PageIndex = pageIndex , PageSize = count});

            await LoadAsync(messages);
        }

        public async Task GetLatest(int groupId, int pageIndex, int count)
        {
            await GetMessages(groupId, count, pageIndex);
        }

        public async Task UnMute(int groupId)
        {
            await SetGroupMemberState(groupId, false);
            await Clients.Group(groupId.ToString()).OnUnMute(groupId);
        }

        public async Task Mute(int groupId)
        {
            await SetGroupMemberState(groupId, true);
            await Clients.Group(groupId.ToString()).OnMute(groupId);
        }

        public async Task SetStatus(int status)
        {
            await _mediator.Send(new SetUserStatusCommand { UserStatus = (UserStatusCode)status, UserId = UserId });
            await Clients.All.OnUserStatusChange(UserId, status, null);
        }

        public async Task SetCustomStatus(string customStatus)
        {
            List<Task> tasks = new List<Task>(2);
            int statusId = (int) UserStatusCode.Custom;
            
            tasks.Add(_mediator.Send(new SetCustomStatusCommand { CustomStatus = customStatus}));
            tasks.Add(Clients.All.OnUserStatusChange(UserId, statusId, customStatus));

            await Task.WhenAll(tasks);
        }

        public async Task Get(int chatId, int count, int pageIndex)
        {
            await GetMessages(chatId, count);
        }

        public async Task ReadMessage(int groupId, int messageId)
        {
            await _mediator.Send(new ReadMessageCommand {UserId = UserId, MessageId = messageId });
            await Clients.Group(groupId.ToString()).OnReadMessage(messageId, UserId);
        }

        public async Task ReadGroupMessage(int groupId)
        {
            await _mediator.Send(new ReadGroupMessageCommand {UserId = UserId, GroupId = groupId });
            await Clients.Group(groupId.ToString()).OnReadGroupMessage(groupId, UserId);
        }
        
        #endregion

        #region Helpers

        private async Task Logout(ClaimsPrincipal user)
        {
            TimeSpan updateInterval = TimeSpan.FromMinutes(15);
            await Task.Delay((int)updateInterval.TotalMilliseconds);
            if (!_onlineLogic.IsOnline(UserId))
            {
                await _mediator.Send(new LogoutCommand());
            }
        }


        private async Task GetMessages(int groupId, int count, int pageIndex = 1)
        {
            var messages = await _mediator.Send<PaginationResponse<FileMessageResponse>>(new GetUserGroupMessagesQuery
            {
                UserId = UserId,
                GroupId = groupId,
                PageIndex= pageIndex,
                PageSize = count
            });

            await LoadAsync(messages);
        }

        private async Task AddMessage(CreateMessageCommand request)
        {
            request.UserId = UserId;
            var response = await _mediator.Send(request);
            ReceiveMessageResponse sender = new ReceiveMessageResponse(response.MessageId, response.Content, response.GroupTypeId,
                response.GroupId, 
                UserId, response.SentDate, response.AttachmentsPath, response.MessageAuthorId);
            var regex =  new Regex(MessageConstants.RegexToModifyUser);
            var userIds = regex.Matches(request.Content).Select(item =>  int.Parse(item.Value)).ToList();
            var tasks = new List<Task>();
            foreach (var userId in userIds)
            {
                tasks.Add(Clients.Users(userId.ToString()).OnMessageMention(sender));
            }

            await Task.WhenAll(tasks);
            await Clients.Group(response.GroupId.ToString()).ReceiveMessage(sender);
        }

        private async Task LoadAsync(PaginationResponse<FileMessageResponse> messages)
        {
            await Clients.Client(Context.ConnectionId).LoadMessages(messages);
        }

        private async Task SetGroupMemberState(int groupId, bool mute)
        {
            MuteCommand request = new MuteCommand
            {
                GroupId = groupId,
                UserId = UserId,
                Mute = mute
            };
            
            await _mediator.Send(request);
        }

        #endregion
    }
}
