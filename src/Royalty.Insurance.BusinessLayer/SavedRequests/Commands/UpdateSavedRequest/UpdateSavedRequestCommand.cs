using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class UpdateSavedRequestCommand : IRequest<SavedRequestResponse>
    {
        public int Id { get; set; }
        public string Request { get; set; }
        public string ShortDescription { get; set; }
    }
}