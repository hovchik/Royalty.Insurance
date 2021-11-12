using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class CreateSavedRequestCommand : IRequest<SavedRequestResponse>
    {
        public string Request { get; set; }
        public string ShortDescription { get; set; }
    }
}