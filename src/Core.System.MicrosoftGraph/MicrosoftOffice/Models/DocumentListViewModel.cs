using System.Collections.Generic;

namespace Core.System.MicrosoftGraph
{
    public class DocumentListViewModel
    {
        public List<UserDriveItem> Documents { get; set; }

        public string SkipToken { get; set; }
    }
}
