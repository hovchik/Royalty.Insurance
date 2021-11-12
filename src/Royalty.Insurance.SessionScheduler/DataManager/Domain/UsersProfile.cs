using System;
using System.Collections.Generic;
using System.Text;

namespace Royalty.Insurance.SessionScheduler.DataManager.Domain
{
    public class UsersProfile
    {
        public int Id { get; set; }
        public int UserStatusId { get; set; }
        public int UserLastStatusId { get; set; }
        public string Status { get; set; }

        public virtual User UserId { get; set; }
    }
}
