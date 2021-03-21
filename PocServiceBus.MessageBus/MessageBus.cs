using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using PocServiceBus.Core.Integration;
using System.Threading;

namespace PocServiceBus.MessageBus
{
    public delegate void ProcessData(IntegrationEvent integrationEvent);
    public class MessageBus : IMessageBus
    {
        private readonly QueueClient _queueClient;
        public event ProcessData ProcessMessageReceived;

        private readonly string _connectionString;
        private readonly string _queue;
        public MessageBus(string connectionString, string queue)
        {
            t_connectionString = connectionString;
            _queue = queue;
            _queueClient = new QueueClient(_connectionString, _queue);
        }

        public async Task SendMessage(IntegrationEvent integrationEvent)
        {
            string data = JsonSerializer.Serialize(integrationEvent);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await _queueClient.SendAsync(message);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }
        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var integrationEvent = JsonSerializer.Deserialize<IntegrationEvent>(Encoding.UTF8.GetString(message.Body));
            
            ProcessMessageReceived?.Invoke(integrationEvent);

            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _queueClient.CloseAsync();
        }
    }
}
