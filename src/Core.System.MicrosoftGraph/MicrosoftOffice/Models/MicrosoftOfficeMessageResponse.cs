using System;
using System.Collections.Generic;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public class MicrosoftOfficeMessageResponse
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public string ConversationId { get; set; }

        public bool? HasAttachments { get; set; }

        public DateTime? SentDateTime { get; set; }

        public string FromEmailAddress { get; set; }

        public IEnumerable<string> CcRecipients { get; set; }

        public string FolderId { get; set; }

        public bool? IsRead { get; set; }
    }
}
