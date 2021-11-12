using MediatR;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class DeleteSavedRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}