using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class DocumentPaginationViewModel
    {
        public PaginationResponse<DocumentResponse> Documents { get; set; }
    }
}
