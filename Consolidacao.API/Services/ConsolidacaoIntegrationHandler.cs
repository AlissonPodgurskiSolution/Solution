using Consolidacao.API.Application.Comands;
using Core.Messages.Integration;
using FluentValidation.Results;
using MessageBus;
using NetDevPack.Mediator;

namespace Consolidacao.API.Services;

public class ConsolidacaoIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public ConsolidacaoIntegrationHandler(
        IServiceProvider serviceProvider,
        IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    private void SetResponder()
    {
    }

    private void OnConnect(object s, EventArgs e)
    {
        SetResponder();
    }

    private void SetSubscribers()
    {
        _bus.SubscribeAsync<AdicionarParaConsolidacaoEvent>("AdicionarParaConsolidacao", async request =>
        {
            Console.WriteLine("Mensagem recebida na fila AdicionarParaConsolidacao...");
            await CapturarLancamento(request);
        }, false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        SetSubscribers();
        return Task.CompletedTask;
    }

    private async Task<ResponseMessage> CapturarLancamento(AdicionarParaConsolidacaoEvent message)
    {
        var command =
            new LancamentoConsolidacaoCommand(message.Data, message.Tipo, message.Valor, message.Descricao, message.Id);
        ValidationResult sucesso;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            sucesso = await mediator.SendCommand(command);
        }

        return new ResponseMessage(sucesso);
    }
}