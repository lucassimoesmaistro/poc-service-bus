using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using PocServiceBus.Core.Integration;
using System.Threading;

namespace PocServiceBus.MessageBus
{
    public delegate void ProcessData(string message);
    public class MessageBus : IMessageBus
    {
        private readonly QueueClient _queueClient;

        private readonly string _connectionString;
        private readonly string _queue;

        public event ProcessData ProcessMessageReceived;

        public MessageBus(string connectionString, string queue)
        {
            _connectionString = connectionString;
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
            var messageBody = Encoding.UTF8.GetString(message.Body);

            ProcessMessageReceived?.Invoke(messageBody);

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
