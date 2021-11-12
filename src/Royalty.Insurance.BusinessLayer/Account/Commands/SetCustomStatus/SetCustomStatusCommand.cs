using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class SetCustomStatusCommand : IRequest<Unit>
    {
        public string CustomStatus { get; set; }
    }
}
