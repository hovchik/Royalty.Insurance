using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.Response
{
    public class PaginationResponse<T> where T : class
    {
        public List<T> Response { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }

        public int PageCount { get; set; }
    }
}
