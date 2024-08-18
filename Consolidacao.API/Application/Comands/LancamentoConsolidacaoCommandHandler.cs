using Consolidacao.API.Models;
using Consolidacao.API.Models.Interfaces;
using FluentValidation.Results;
using MediatR;
using MessageBus;
using NetDevPack.Messaging;

namespace Consolidacao.API.Application.Comands;

public class LancamentoConsolidacaoCommandHandler : CommandHandler,
    IRequestHandler<LancamentoConsolidacaoCommand, ValidationResult>
{
    private readonly IMessageBus _bus;
    private readonly ILancamentoConsolidacaoRepository _lancamentoRepository;

    public LancamentoConsolidacaoCommandHandler(ILancamentoConsolidacaoRepository lancamentoRepository, IMessageBus bus)
    {
        _lancamentoRepository = lancamentoRepository;
        _bus = bus;
    }

    public async Task<ValidationResult> Handle(LancamentoConsolidacaoCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new LancamentoConsolidacaoCommandValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid) return validationResult;

        var lancamento = new LancamentoConsolidacao(request.Data, request.Tipo, request.Valor, request.Descricao,
            request.LancamentoId);

        _lancamentoRepository.Adicionar(lancamento);
        _lancamentoRepository.UnitOfWork.Commit();

        return ValidationResult;
    }
}