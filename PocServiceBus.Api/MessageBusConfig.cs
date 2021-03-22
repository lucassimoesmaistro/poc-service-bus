using Microsoft.Extensions.DependencyInjection;
using PocServiceBus.Api.Consumer;
using PocServiceBus.BusMessaging;

namespace PocServiceBus.Api
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBus, MessageBus>()
                .AddHostedService<ConsumerService>();
        }
    }
}
