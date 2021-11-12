namespace Royalty.Insurance.Proxy.Request
{
    public class UserPhoneRequest
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string IpAddress { get; set; }
        public int UserOwnerId { get; set; }
        public int Extension { get; set; }
    }
}