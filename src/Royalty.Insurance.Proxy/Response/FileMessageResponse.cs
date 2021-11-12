using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.Response
{
    public class FileMessageResponse : MessageResponse
    {
        public IEnumerable<string> AttachmentsPath { get; set; }

        public int GroupCreatedById { get; set; }

        public int  GroupTypeId { get; set; }
    }
}
