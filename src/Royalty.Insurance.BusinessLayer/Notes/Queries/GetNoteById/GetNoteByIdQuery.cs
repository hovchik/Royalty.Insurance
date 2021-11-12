using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class GetNoteByIdQuery : IRequest<NoteResponse>
    {
        public int Id { get; set; }
    }
}
