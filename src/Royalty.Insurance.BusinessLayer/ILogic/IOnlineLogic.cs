using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.ILogic
{
    public interface IOnlineLogic
    {
        bool IsOnline(int userId);

        void AddOnlineDevice(int userId, string deviceId);
        void RemoveOnlineDevice(int userId, string deviceId);

        List<string> GetConnectionIdByUserId(int userId);

        IEnumerable<int> GetOnlineUsers();
    }
}
