using MediatR;
using Microsoft.AspNetCore.Http;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class UploadUserAvatarCommand : IRequest<UserResponse>
    {
        public IFormFile File { get; set; }
        public string FileContainer { get; set; }
    }
}
