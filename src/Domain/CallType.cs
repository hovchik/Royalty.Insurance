using System.Collections.Generic;

namespace Domain
{
    public class CallType
    {
        public CallType()
        {
            UserPhoneCallHistoryCurrentCallTypes = new HashSet<UserPhoneCallHistory>();
            UserPhoneCallHistoryInitialCallTypes = new HashSet<UserPhoneCallHistory>();
        }

        
        public int Id { get; set; }
        
        public string Name { get; set; }

        public ICollection<UserPhoneCallHistory> UserPhoneCallHistoryCurrentCallTypes { get; set; }
        public ICollection<UserPhoneCallHistory> UserPhoneCallHistoryInitialCallTypes { get; set; }
    }
}
