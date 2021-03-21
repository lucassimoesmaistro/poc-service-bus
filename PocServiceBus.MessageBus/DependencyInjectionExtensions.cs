using System;
using Microsoft.Extensions.DependencyInjection;

namespace PocServiceBus.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection, string queueName)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus(connection, queueName));

            return services;
        }
    }
}
