using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Agencies
{
    public class UpdateAgencyCommand : IRequest<AgencyResponse>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }
    }
}
