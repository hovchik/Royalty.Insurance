using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class DeactivateUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
