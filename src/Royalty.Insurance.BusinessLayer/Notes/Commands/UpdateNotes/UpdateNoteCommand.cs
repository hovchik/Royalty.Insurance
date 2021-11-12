using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class UpdateNoteCommand : IRequest<NoteResponse>
    {
        public NoteRequest Request { get; set; }
        public int Id { get; set; }
    }
}
