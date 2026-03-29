using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Infrastructure.Service;

public class EventLoggingService : IEventLoggingService
{
    private readonly IList<string> _eventLogs = [];

    public IList<string> GetLoggedEvents() => _eventLogs;

    public Task LogEvent(string message)
    {
        Console.WriteLine(message);
        _eventLogs.Add(message);
        return Task.CompletedTask;
    }
}
