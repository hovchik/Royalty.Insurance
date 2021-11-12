using Domain;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class FindByEmailCommand : IRequest<User>
    {
        public string Email { get; set; }
    }
}
