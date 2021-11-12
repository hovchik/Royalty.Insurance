
namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class UserUnreadMessageSettingQuery
    {
        public int UserId { get; set; }

        public int UnReadPreferenceInMinutes { get; set; }
    }
}
