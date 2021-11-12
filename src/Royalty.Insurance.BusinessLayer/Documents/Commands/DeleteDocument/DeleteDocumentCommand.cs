using MediatR;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class DeleteDocumentCommand : IRequest<Unit>
    {
        public int InsuredId { get; set; }

        public string FileName{ get; set; }
    }
}
