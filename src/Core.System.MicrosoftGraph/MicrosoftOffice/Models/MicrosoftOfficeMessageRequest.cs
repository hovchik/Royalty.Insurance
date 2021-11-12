using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public class MicrosoftOfficeMessageRequest
    {
        public string FromEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
        
        public IEnumerable<string> CcRecipients { get; set; }

        public IEnumerable<string> ToRecipients { get; set; }

        public List<IFormFile> Attachments { get; set; }
    }
}
