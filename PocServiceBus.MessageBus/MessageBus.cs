using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Options;

namespace PocServiceBus.BusMessaging
{
    public class MessageBus : IMessageBus
    {
        private readonly IHandlerData _handlerData;
        private readonly QueueClient _queueClient;
        private readonly MessageQueueConfiguration _messageQueueConfiguration;
        
         public MessageBus(IHandlerData handlerData, IOptions<MessageQueueConfiguration> messageQueueConfiguration)
        {
            _messageQueueConfiguration = messageQueueConfiguration.Value;
            _handlerData = handlerData;
            _queueClient = new QueueClient(_messageQueueConfiguration.Connectionstring, _messageQueueConfiguration.QueueName);
        }

        public async Task SendMessage(Core.Messages.Message message)
        {
            string data = JsonSerializer.Serialize(message);
            Message messageAz = new Message(Encoding.UTF8.GetBytes(data));

            await _queueClient.SendAsync(messageAz);
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

            await _handlerData.Handle(messageBody);

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
