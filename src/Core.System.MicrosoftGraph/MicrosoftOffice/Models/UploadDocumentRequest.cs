using System.IO;

namespace Core.System.MicrosoftGraph
{
    public class UploadDocumentRequest
    {
        public string GroupId { get; set; }

        public string FileName { get; set; }

        public Stream DocumentStream { get; set; }
    }
}
