using System;
using System.Threading.Tasks;

namespace Royalty.Insurance.Common
{
    public interface IEventBroker<TMessage>
    {
        void Subscribe(Func<TMessage, Task> action);
        void Unsubscribe(Func<TMessage, Task> action);
        Task Send(TMessage message);
    }
}
