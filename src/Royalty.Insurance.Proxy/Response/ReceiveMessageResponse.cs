using System;
using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.Response
{
    public class ReceiveMessageResponse
    {
        public ReceiveMessageResponse(long messageId, string content, int groupTypeId, int groupId, int userId, DateTime sendDateTime,
            IEnumerable<string> attachmentsPath, int? messageAuthorId)
        {
            MessageId = messageId;
            Content = content;
            GroupTypeId = groupTypeId;
            GroupId = groupId;
            UserId = userId;
            SentDate = sendDateTime;
            AttachmentsPath = attachmentsPath;
            MessageAuthorId = messageAuthorId;
        }

        public long MessageId { get; set; }

        public string Content { get; set; }

        public int GroupId { get; set; }

        public int UserId { get; set; }

        public DateTime SentDate { get; set; }

        public int? MessageAuthorId { get; set; }

        public int GroupTypeId { get; set; }

        public IEnumerable<string> AttachmentsPath { get; set; }

    }
}
