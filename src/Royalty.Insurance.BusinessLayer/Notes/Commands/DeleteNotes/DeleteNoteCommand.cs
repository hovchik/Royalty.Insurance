using MediatR;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class DeleteNoteCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
