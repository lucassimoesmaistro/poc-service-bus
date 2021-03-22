using PocServiceBus.Core.Messages;
using System;
using System.Threading.Tasks;

namespace PocServiceBus.BusMessaging
{
    public interface IMessageBus : IDisposable
    {
        Task SendMessage(Message message);
        void RegisterOnMessageHandlerAndReceiveMessages();
    }
}
