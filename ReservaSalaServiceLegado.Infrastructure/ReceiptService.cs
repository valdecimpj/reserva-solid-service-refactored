using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Infrastructure;

public class ReceiptService(IEventLoggingService eventLoggingService) : IReceiptService
{
    public Task PrintReceipt(string data)
    {
        eventLoggingService.LogEvent("Receipt printed");
        return Task.CompletedTask;
    }
}
