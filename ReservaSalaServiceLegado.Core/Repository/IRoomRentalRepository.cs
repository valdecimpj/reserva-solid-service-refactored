using ReservaSalaServiceLegado.Core.Model;

namespace ReservaSalaServiceLegado.Core.Repository;

public interface IRoomRentalRepository
{
    Task SaveRental(RentalDataModel rentalDataModel);
    Task<bool> CheckIfRoomIsRented(string room);
}
