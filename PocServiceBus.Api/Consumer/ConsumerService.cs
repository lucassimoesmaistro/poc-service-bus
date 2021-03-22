using Microsoft.Extensions.Hosting;
using PocServiceBus.BusMessaging;
using System.Threading;
using System.Threading.Tasks;

namespace PocServiceBus.Api.Consumer
{
    public class ConsumerService : BackgroundService
    {
        private readonly IMessageBus _bus;
        public ConsumerService(IMessageBus bus)
        {
            _bus = bus;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.RegisterOnMessageHandlerAndReceiveMessages();
            return Task.CompletedTask;
        }

    }
}
