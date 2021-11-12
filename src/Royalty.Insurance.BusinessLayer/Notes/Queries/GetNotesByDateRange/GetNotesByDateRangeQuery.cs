using MediatR;
using Royalty.Insurance.Proxy.Response;
using System;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class GetNotesByDateRangeQuery : IRequest<PaginationResponse<NoteResponse>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
