using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public class CheckUserFileExistsQuery : IRequest<BaseResponse<bool>>
    {
        public string FileName { get; set; }
    }
}
