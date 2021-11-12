using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class GenerateDocumentFromTemplateCommand : IRequest<DocumentResponse>
    {
        public int InsuredId { get; set; }
        public int TemplateId { get; set; }
    }
}
