using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Royalty.Insurance.BusinessLayer.ILogic;

namespace Royalty.Insurance.BusinessLayer.Logic
{
    public class OnlineLogic : IOnlineLogic
    {
        private readonly ConcurrentDictionary<int, List<string>> _data = new ConcurrentDictionary<int, List<string>>();

        public bool IsOnline(int userId)
        {
            return _data.ContainsKey(userId) && _data[userId].Count > 0;
        }

        public List<string> GetConnectionIdByUserId(int userId)
        {
            return !_data.ContainsKey(userId) ? new List<string>() : _data[userId];
        }


        public IEnumerable<int> GetOnlineUsers()
        {
            return _data.Keys.Where(item => _data[item].Any());
        }

        public void AddOnlineDevice(int userId, string deviceId)
        {
            if (!_data.ContainsKey(userId))
                _data.TryAdd(userId, new List<string>());

            _data[userId].Add(deviceId);
        }

        public void RemoveOnlineDevice(int userId, string deviceId)
        {
            if (!_data.ContainsKey(userId) || !_data[userId].Contains(deviceId))
                return;

            _data[userId].RemoveAll(id => id == deviceId);
        }
    }
}
