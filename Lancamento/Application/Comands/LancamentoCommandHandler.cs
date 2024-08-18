using FluentValidation.Results;
using Lancamento.API.Models;
using MediatR;
using NetDevPack.Messaging;

namespace Lancamento.API.Application.Comands;

public class LancamentoCommandHandler : CommandHandler,
    IRequestHandler<LancamentoCommand, ValidationResult>
{
    private readonly ILancamentoRepository _lancamentoRepository;

    public LancamentoCommandHandler(ILancamentoRepository lancamentoRepository)
    {
        _lancamentoRepository = lancamentoRepository;
    }

    public async Task<ValidationResult> Handle(LancamentoCommand request, CancellationToken cancellationToken)
    {
        var validator = new LancamentoCommandValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid) return validationResult;

        var lancamento = new Models.Lancamento(request.Data, request.Tipo, request.Valor, request.Descricao);

        _lancamentoRepository.Adicionar(lancamento);
        _lancamentoRepository.UnitOfWork.Commit();

        return ValidationResult;
    }
}