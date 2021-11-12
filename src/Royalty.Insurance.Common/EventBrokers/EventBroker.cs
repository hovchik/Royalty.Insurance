using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Royalty.Insurance.Common.EventBrokers
{
    public class EventBroker<TMessage> : IEventBroker<TMessage>
    {
        private readonly ICollection<Func<TMessage, Task>> actions
            = new List<Func<TMessage, Task>>();

        private readonly ILogger<EventBroker<TMessage>> logger;

        public EventBroker(ILogger<EventBroker<TMessage>> logger)
        {
            this.logger = logger;
        }

        public void Subscribe(Func<TMessage, Task> action)
        {
            actions.Add(action);
        }

        public void Unsubscribe(Func<TMessage, Task> action)
        {
            actions.Remove(action);
        }

        public async Task Send(TMessage message)
        {
            await Task.WhenAll(actions.Select(async s =>
            {
                try
                {
                    await s(message);
                }
                catch (Exception e)
                {
                    logger.LogError(e.ToString());
                }
            }));
        }
    }
}
