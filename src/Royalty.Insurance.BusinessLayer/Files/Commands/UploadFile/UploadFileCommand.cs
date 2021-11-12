using MediatR;
using Microsoft.AspNetCore.Http;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public class UploadFileCommand : IRequest<UserFileResponse>
    {
        public IFormFile File { get; set; }
        public int? AssignedTo { get; set; }

        public bool OverWriteExisting { get; set; }

        public byte FileFormatId { get; set; }
    }
}
