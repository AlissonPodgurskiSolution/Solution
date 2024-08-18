using Core.Messages.Integration;
using EasyNetQ;
using FluentValidation.Results;
using Lancamento.API.Models;
using MediatR;
using MessageBus;
using NetDevPack.Messaging;

namespace Lancamento.API.Application.Comands;

public class LancamentoCommandHandler : CommandHandler,
    IRequestHandler<LancamentoCommand, ValidationResult>
{
    private readonly ILancamentoRepository _lancamentoRepository;
    private readonly IMessageBus _bus;

    public LancamentoCommandHandler(ILancamentoRepository lancamentoRepository, IMessageBus bus)
    {
        _lancamentoRepository = lancamentoRepository;
        _bus = bus;
    }

    public async Task<ValidationResult> Handle(LancamentoCommand request, CancellationToken cancellationToken)
    {
        var validator = new LancamentoCommandValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid) return validationResult;

        var lancamento = new Models.Lancamento(request.Data, request.Tipo, request.Valor, request.Descricao);

        _lancamentoRepository.Adicionar(lancamento);
        _lancamentoRepository.UnitOfWork.Commit();

        var lancamentoCadastrado = new AdicionarParaConsolidacaoEvent(lancamento.Id, lancamento.Data, lancamento.Tipo, lancamento.Valor, lancamento.Descricao);
        await _bus.PublishAsync(lancamentoCadastrado);

        return ValidationResult;
    }
}