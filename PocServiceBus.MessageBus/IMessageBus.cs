using PocServiceBus.Core.Integration;
using System;
using System.Threading.Tasks;

namespace PocServiceBus.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        public event ProcessData ProcessMessageReceived;
        Task SendMessage(IntegrationEvent integrationEvent);
        void RegisterOnMessageHandlerAndReceiveMessages();
    }
}
