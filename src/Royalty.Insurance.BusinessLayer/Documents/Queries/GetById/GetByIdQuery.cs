using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class GetByIdQuery : IRequest<DocumentResponse>
    {
        public int Id { get; set; }
    }
}
