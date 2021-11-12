
namespace Royalty.Insurance.Proxy.Response
{
    public class PingResponse
    {
        public PingResponse(bool status)
        {
            Status = status;
        }

        public bool Status { get; }
    }
}
