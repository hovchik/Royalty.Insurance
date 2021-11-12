using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class CreateMessageWithAttachmentCommand : CreateMessageCommand
    {
        public List<IFormFile> Files { get; set; }
    }
}
