using Microsoft.Extensions.Hosting;
using PocServiceBus.Core.Integration;
using PocServiceBus.Core.Mediator;
using PocServiceBus.MessageBus;
using System;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PocServiceBus.Api.Consumer
{
    public class ConsumerService : BackgroundService
    {
        private readonly IMessageBus _bus;
        //private readonly IMediatorHandler _mediatorHandler;
        public ConsumerService(IMessageBus bus)//, IMediatorHandler mediatorHandler)
        {
            _bus = bus;
            _bus.ProcessMessageReceived += ProcessMessage;
            //_mediatorHandler = mediatorHandler;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.RegisterOnMessageHandlerAndReceiveMessages();
            return Task.CompletedTask;
        }

        private void ProcessMessage(string message)
        {
            Type type = GetMessageType(message);
            var integrationEvent = GetJsonGenericType<type>(message);

            if (integrationEvent.MessageType.Equals("Cliente"))
            {

            }
            else
            {

            }
        }
        private T GetJsonGenericType<T>(string message)
        {
            var generatedType = JsonSerializer.Deserialize<T>(message);
            return (T)Convert.ChangeType(generatedType, typeof(T));
        }
        private Type GetMessageType(string message)
        {
            var data = JsonDocument.Parse(message);
            string messageType = data.RootElement.GetProperty($"MessageType").GetString();
            string assemblyName = Assembly.GetExecutingAssembly().FullName;
            var type = Type.GetType($"{assemblyName}.{messageType}");
            //var myObject = (IntegrationEvent)Activator.CreateInstance(type);
            return type;
        }
    }
}
