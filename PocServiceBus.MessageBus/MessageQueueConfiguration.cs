using System;
using System.Collections.Generic;
using System.Text;

namespace PocServiceBus.BusMessaging
{
    public class MessageQueueConfiguration
    {
        public string Connectionstring { get; set; }
        public string QueueName { get; set; }
    }
}
