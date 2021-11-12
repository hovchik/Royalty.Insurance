using System;
using Domain;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class TokenProviderCommand : IRequest<LoginResponse>
    {
        public User User { get; set; }
        public Guid SessionId { get; set; }
        public string UserIpAddress { get; set; }
    }
}
