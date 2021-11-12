using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public class UpdateFileCommand : IRequest<UserFileResponse>
    {
        public int Id { get; set; }

        public int? AssignToId { get; set; }
    }
}
