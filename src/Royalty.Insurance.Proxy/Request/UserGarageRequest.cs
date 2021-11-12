using Microsoft.AspNetCore.Http;

namespace Royalty.Insurance.Proxy.Request
{
    public class UserGarageRequest
    {
        public IFormFile File { get; set; }
    }
}
