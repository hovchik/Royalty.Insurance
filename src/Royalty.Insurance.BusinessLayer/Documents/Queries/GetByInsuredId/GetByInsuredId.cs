using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class GetByInsuredId : IRequest<PaginationResponse<DocumentResponse>>
    {
        public int InsuredId { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
