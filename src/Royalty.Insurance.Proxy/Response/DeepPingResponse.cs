
namespace Royalty.Insurance.Proxy.Response
{
    public class DeepPingResponse : PingResponse
    {
        public DeepPingResponse(bool status) : base(status)
        {
        }

        public DeepPingResponse(bool status, string databaseVersion) : base(status)
        {
            DatabaseVersion = databaseVersion;
        }

        public string DatabaseVersion { get; }
    }
}
