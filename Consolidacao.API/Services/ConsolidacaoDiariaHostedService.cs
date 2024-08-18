using Consolidacao.API.Services.Interfaces;

public class ConsolidacaoDiariaHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public ConsolidacaoDiariaHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var consolidacaoService = scope.ServiceProvider.GetRequiredService<IConsolidacaoService>();

                var dataAtual = DateTime.UtcNow.Date;

                await consolidacaoService.ConsolidarDia(dataAtual);
            }

            // Aguarda até o próximo dia
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}