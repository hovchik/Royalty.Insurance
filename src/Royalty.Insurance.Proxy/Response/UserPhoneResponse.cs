namespace Royalty.Insurance.Proxy.Response
{
    public class UserPhoneResponse
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string IpAddress { get; set; }
        public int Extension { get; set; }
        public int UserOwnerId { get; set; }
    }
}