using System.Common.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace Royalty.Insurance.Api.Messaging
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.UserId().ToString();
        }
    }
}
