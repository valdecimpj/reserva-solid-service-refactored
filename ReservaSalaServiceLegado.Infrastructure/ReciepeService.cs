using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Infrastructure;

public class ReciepeService(IEventLoggingService eventLoggingService) : IReciepeService
{
    public Task PrintReciepe(string data)
    {
        eventLoggingService.LogEvent("Reciepe printed");
        return Task.CompletedTask;
    }
}
