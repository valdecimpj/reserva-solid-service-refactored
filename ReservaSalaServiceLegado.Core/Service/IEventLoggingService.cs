namespace ReservaSalaServiceLegado.Core.Service;

public interface IEventLoggingService
{
    Task LogEvent(string message);
}
