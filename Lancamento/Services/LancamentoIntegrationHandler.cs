using MessageBus;

namespace Lancamento.API.Services;

public class LancamentoIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public LancamentoIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //SetSubscribers();
        return Task.CompletedTask;
    }

    //private void SetSubscribers()
    //{
    //    _bus.SubscribeAsync<PedidoAutorizadoIntegrationEvent>("PedidoAutorizado", async request =>
    //        await BaixarEstoque(request));
    //}

    //private async Task ProcessarLancamento(LancamentoRealizadoIntegrationEvent message)
    //{
    //    using (var scope = _serviceProvider.CreateScope())
    //    {
    //        var lancamentoRepository = scope.ServiceProvider.GetRequiredService<ILancamentoRepository>();

    //        // Assume que os lançamentos podem ser criados em lote, então iteramos por eles
    //        var lancamentos = message.Itens.Select(item => new Models.Lancamento(item.Data, item.Tipo, item.Valor, item.Descricao)).ToList();

    //        foreach (var lancamento in lancamentos)
    //        {
    //            lancamentoRepository.Adicionar(lancamento);
    //        }

    //        if (!await lancamentoRepository.UnitOfWork.Commit())
    //        {
    //            throw new DomainException($"Problemas ao processar lançamentos do evento {message.EventId}");
    //        }

    //        // Publica um evento de integração se necessário, após o sucesso do commit
    //        var lancamentoProcessado = new LancamentoProcessadoIntegrationEvent(message.ClienteId, message.EventId);
    //        await _bus.PublishAsync(lancamentoProcessado);
    //    }
    //}

    //public async void CancelarLancamento(PedidoAutorizadoIntegrationEvent message)
    //{
    //    var lancamentoCancelado = new LancamentoCanceladoIntegrationEvent(message.ClienteId, message.EventId);
    //    await _bus.PublishAsync(lancamentoCancelado);
    //}
}