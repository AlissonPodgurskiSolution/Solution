using FluentValidation;

namespace Consolidacao.API.Application.Comands;

public class LancamentoConsolidacaoCommandValidator : AbstractValidator<LancamentoConsolidacaoCommand>
{
    public LancamentoConsolidacaoCommandValidator()
    {
        RuleFor(c => c.Data)
            .NotEmpty().WithMessage("A data é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data não pode ser no futuro.");

        RuleFor(c => c.Tipo)
            .IsInEnum().WithMessage("O tipo de lançamento é inválido.");

        RuleFor(c => c.Valor)
            .GreaterThan(0).WithMessage("O valor do lançamento deve ser maior que zero.");

        RuleFor(c => c.Descricao)
            .MaximumLength(500).WithMessage("A descrição não pode exceder 500 caracteres.");
    }
}