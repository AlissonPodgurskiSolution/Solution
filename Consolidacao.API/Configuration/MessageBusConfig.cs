using Consolidacao.API.Services;
using Core.Utils;
using MessageBus;

namespace Consolidacao.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<ConsolidacaoIntegrationHandler>();
    }
}