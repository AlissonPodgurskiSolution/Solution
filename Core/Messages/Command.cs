using FluentValidation.Results;
using MediatR;

namespace Core.Messages;

public abstract class Command : Message, IRequest<ValidationResult>
{
    protected Command()
    {
        Timestamp = DateTime.UtcNow;
    }

    public DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; set; }

    public virtual bool EhValido()
    {
        throw new NotImplementedException();
    }
}