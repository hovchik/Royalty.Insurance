using MediatR;
using Microsoft.AspNetCore.Http;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class UploadDocumentIntoSharePointCommand : IRequest<DocumentResponse>
    {
        public IFormFile DocumentFile { get; set; }

        public int? InsuredId { get; set; }
    }
}