using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class CreateNoteCommand : IRequest<NoteResponse>
    {
        public NoteRequest Request { get; set; }
    }
}
