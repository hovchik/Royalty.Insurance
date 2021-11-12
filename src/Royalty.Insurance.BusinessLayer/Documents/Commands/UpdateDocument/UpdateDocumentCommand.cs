using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class UpdateDocumentCommand : IRequest<DocumentResponse>
    {
        public int Id { get; set; }
        public int InsuredId { get; set; }
    }

}
