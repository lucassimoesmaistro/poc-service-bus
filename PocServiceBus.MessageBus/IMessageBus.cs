using PocServiceBus.Core.Integration;
using System;
using System.Threading.Tasks;

namespace PocServiceBus.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        Task SendMessage(IntegrationEvent integrationEvent);
        void RegisterOnMessageHandlerAndReceiveMessages();
    }
}
