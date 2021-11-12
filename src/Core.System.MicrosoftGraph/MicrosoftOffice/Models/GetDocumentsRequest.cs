
using System;

namespace Core.System.MicrosoftGraph
{
    public class GetDocumentsRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string FileName { get; set; }
        public string SkipToken { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
