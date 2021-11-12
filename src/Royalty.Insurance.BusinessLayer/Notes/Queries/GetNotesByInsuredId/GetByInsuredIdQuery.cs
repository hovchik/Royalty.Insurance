using MediatR;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class GetByInsuredIdQuery : IRequest<NoteResponseListView>
    {
        public int InsuredId { get; set; }
    }
}
