using System;
using System.Collections.Generic;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Files.Queries
{
    public class GetFilesWithPaginationQuery : IRequest<PaginationResponse<UserFileResponse>>
    {
        public string FileName { get; set; }
        public List<int> FormatIds { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime? StartDate { get; set; }
        public  DateTime? EndDate { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
