using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.States.Queries.GetStateByName
{
    public class GetStateByNameQuery : IRequest<StateResponse>
    {
        public string StateName { get; set; }
    }
}
