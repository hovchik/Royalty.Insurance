using Domain;
using MediatR;
using System;

namespace Royalty.Insurance.BusinessLayer.GetUsersActivityLogsBySession
{
    public class GetUsersActivityLogsBySessionQuery : IRequest<UserActivityLog>
    {
        public Guid SessionId { get; set; }
    }
}
