using ReservaSalaServiceLegado.Core.Model;
using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Infrastructure;

public class ReceiptService(IEventLoggingService eventLoggingService) : IReceiptService
{
    public Task PrintReceipt(RentalDataModel _rentalData)
    {
        eventLoggingService.LogEvent("Receipt printed");
        return Task.CompletedTask;
    }
}
