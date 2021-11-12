using System.Collections.Generic;

namespace Domain
{
    public class UserStatus
    {
        public UserStatus()
        {
            UsersProfileUserLastStatuses = new HashSet<UsersProfile>();
            UsersProfileUserStatuses = new HashSet<UsersProfile>();
        }

        
        public int Id { get; set; }
        
        
        public string Name { get; set; }

        public ICollection<UsersProfile> UsersProfileUserLastStatuses { get; set; }
        public ICollection<UsersProfile> UsersProfileUserStatuses { get; set; }
    }
}
