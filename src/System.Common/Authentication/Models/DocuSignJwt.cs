namespace System.Common.Authentication.Models
{
    public class DocUSignJwt
    {
        public string ClientId { get; set; }
        public string ImpersonatedUserId { get; set; }
        public string BasePath { get; set; }
        public string AuthServer { get; set; }
        public string PrivateKey { get; set; }

        public string ReturnUrl { get; set; }
    }
}
