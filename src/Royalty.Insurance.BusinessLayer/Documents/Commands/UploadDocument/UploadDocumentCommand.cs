using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class UploadDocumentCommand : IRequest<DocumentListViewModel>
    {
        public List<IFormFile> Files { get; set; }
        
        public int? InsuredId { get; set; }
    }
}
