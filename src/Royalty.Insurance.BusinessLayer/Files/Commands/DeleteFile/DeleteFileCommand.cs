using MediatR;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public class DeleteFileCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
