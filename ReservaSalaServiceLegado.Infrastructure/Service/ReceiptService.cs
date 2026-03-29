using ReservaSalaServiceLegado.Core.Model;
using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Infrastructure.Service;

public class ReceiptService(IEventLoggingService eventLoggingService) : IReceiptService
{
    public Task PrintReceipt(RentalModel rental)
    {
        eventLoggingService.LogEvent("Receipt printed");
        return Task.CompletedTask;
    }
}
