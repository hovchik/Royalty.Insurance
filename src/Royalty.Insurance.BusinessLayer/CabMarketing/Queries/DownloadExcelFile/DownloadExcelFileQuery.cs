using MediatR;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using System.Collections.Generic;
using System.Common.Storage.Response;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class DownloadExcelFileQuery : IRequest<FileResponse>
    {
        public List<DetailedSearch> Data { get; set; }
        public List<string> Columns { get; set; }
    }
}
