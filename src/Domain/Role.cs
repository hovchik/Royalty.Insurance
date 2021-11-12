using System.Collections.Generic;


namespace Domain
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        
        public int Id { get; set; }
        
        
        public string Name { get; set; }
        public int Type { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
