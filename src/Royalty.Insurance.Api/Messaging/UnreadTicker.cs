using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Royalty.Insurance.BusinessLayer.ILogic;
using Royalty.Insurance.BusinessLayer.Messages;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.Api.Messaging
{
    public class UnreadTicker
    {
        private readonly IOnlineLogic _onlineLogic;
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(60);

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<MessageHub> _hub;

        public UnreadTicker(IHubContext<MessageHub> hub, IOnlineLogic onlineLogic, IServiceScopeFactory scopeFactory)
        {
            _hub = hub;
            _onlineLogic = onlineLogic;
            _scopeFactory = scopeFactory;
            // ReSharper disable once ObjectCreationAsStatement
            new Timer(LoadUnreadMessages, null, _updateInterval, _updateInterval);
        }

        public async void LoadUnreadMessages(object state)
        {
            // This function must be re-entrant as it's running as a timer interval handler

            GetUsersIndividualUnreadMessagesQuery request = new GetUsersIndividualUnreadMessagesQuery
            {
                UnreadMessageSetting = new List<UserUnreadMessageSettingQuery>()
            };

            var userIds = _onlineLogic.GetOnlineUsers();
            foreach (var userid in userIds)
            {
                request.UnreadMessageSetting.Add(new UserUnreadMessageSettingQuery
                {
                    UserId = userid,
                    UnReadPreferenceInMinutes = 15,//TODO hardcoded read from settings
                });
            }

            await LoadUnreadMessages(request);

        }

        public async Task LoadUnreadMessages(GetUsersIndividualUnreadMessagesQuery request, string connectionId = null)
        {

            List<Task> boardTasks = new List<Task>();
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
                var unreadMessages = await mediator.Send(request);
                boardTasks.AddRange(unreadMessages.Select(item => BroadcastUserUnreadMessageChange(item, connectionId)));
            }

            await Task.WhenAll(boardTasks);
        }

        private async Task BroadcastUserUnreadMessageChange(GroupUnreadMessageResponse response, string connectionId = null)
        {
            if (!string.IsNullOrWhiteSpace(connectionId))
            {
                await _hub.Clients.Client(connectionId).SendAsync(nameof(IMessageClient.BroadcastUserUnreadMessageChange), response);
            }
            else
            {
                await _hub.Clients.User(response.SendUserId.ToString()).SendAsync(nameof(IMessageClient.BroadcastUserUnreadMessageChange), response);
            }
        }
    }
}
