using MediatR;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.PhoneBooks
{
    public class GetPhonesQuery : IRequest<PhoneBookResponse>
    {
        public int Id { get; set; }
    }
}
