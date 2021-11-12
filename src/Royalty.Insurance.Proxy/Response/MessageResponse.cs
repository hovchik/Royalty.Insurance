
using System;

namespace Royalty.Insurance.Proxy.Response
{
    public class MessageResponse
    {
        public long MessageId { get; set; }
        public string Content { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public DateTime SentDate { get; set; }

        public bool IsRead { get; set; }

        public int? MessageAuthorId { get; set; }
    }
}
