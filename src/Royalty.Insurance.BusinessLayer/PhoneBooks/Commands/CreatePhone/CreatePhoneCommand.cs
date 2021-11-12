using MediatR;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.PhoneBooks
{
    public class CreatePhoneCommand : IRequest<PhoneBookResponse>
    {
        public string Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
