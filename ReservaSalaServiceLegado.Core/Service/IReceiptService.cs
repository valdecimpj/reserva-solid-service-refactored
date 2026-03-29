using ReservaSalaServiceLegado.Core.Model;

namespace ReservaSalaServiceLegado.Core.Service;

public interface IReceiptService
{
    Task PrintReceipt(RentalModel rental);
}
