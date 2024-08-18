using Core.Utils;
using Lancamento.API.Services;
using MessageBus;

namespace Lancamento.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<LancamentoIntegrationHandler>();
    }
}