using System;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class GetDocumentsQuery :  IRequest<DocumentPaginationViewModel>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string FileName { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
