using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PocServiceBus.MessageBus;

namespace PocServiceBus.Api
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration?.GetSection("MessageQueueConfiguration")?["Connectionstring"], configuration?.GetSection("MessageQueueConfiguration")?["appqueue"]);
        }
    }
}
