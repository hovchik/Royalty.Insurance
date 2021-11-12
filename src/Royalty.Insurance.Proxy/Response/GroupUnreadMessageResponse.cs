using System;

namespace Royalty.Insurance.Proxy.Response
{
    public class GroupUnreadMessageResponse
    {
        public int GroupId { get; set; }

        public int UnreadMessageCount { get; set; }

        public int GroupTypeId { get; set; }

        public string LastMessage { get; set; }

        public DateTime LastMessageDate { get; set; }

        public int SendUserId { get; set; }

        public long MessageId { get; set; }
    }
}