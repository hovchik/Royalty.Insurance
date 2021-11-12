
namespace Royalty.Insurance.Proxy.Response
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
