
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Validator
{
    public class PaginationCommand : IRequest
    {
        public int PageIndex { get; set; }
        public int  PageSize { get; set; }
}
}
