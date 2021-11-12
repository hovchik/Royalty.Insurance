using System.Collections.Generic;
using Royalty.Insurance.Proxy.APIModels.Marketing;

namespace Royalty.Insurance.Proxy.Response
{
    public class ExcelModelResponse
    {
        public List<DetailedSearch> Data { get; set; }
        public List<string> Columns { get; set; }
    }
}