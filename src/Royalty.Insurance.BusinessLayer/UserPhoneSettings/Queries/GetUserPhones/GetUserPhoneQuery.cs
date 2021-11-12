using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.UserPhoneSettings
{
    public class GetUserPhoneQuery : IRequest<List<UserPhoneResponse>>
    {
    }
}