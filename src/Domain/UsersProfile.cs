namespace Domain
{
    public class UsersProfile
    {
        public int Id { get; set; }
        public int UserStatusId { get; set; }
        public int UserLastStatusId { get; set; }
        
        public string Status { get; set; }

        public User User { get; set; }
        public UserStatus UserLastStatus { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
