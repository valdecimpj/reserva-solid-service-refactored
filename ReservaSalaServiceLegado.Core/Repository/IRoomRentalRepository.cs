using ReservaSalaServiceLegado.Core.Model;

namespace ReservaSalaServiceLegado.Core.Repository;

public interface IRoomRentalRepository
{
    Task SaveRental(RentalModel rental);
    Task<bool> CheckIfRoomIsRented(string room);
}
