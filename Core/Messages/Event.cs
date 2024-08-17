using MediatR;

namespace Core.Messages;

public class Event : Message, INotification
{
    protected Event()
    {
        Timestamp = DateTime.UtcNow;
    }

    public DateTime Timestamp { get; private set; }
}