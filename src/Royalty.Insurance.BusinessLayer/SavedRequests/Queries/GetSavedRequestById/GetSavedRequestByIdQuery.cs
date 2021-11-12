using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class GetSavedRequestByIdQuery : IRequest<SavedRequestResponse>
    {
        public int Id { get; set; }
    }
}