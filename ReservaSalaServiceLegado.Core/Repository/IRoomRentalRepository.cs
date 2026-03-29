namespace ReservaSalaServiceLegado.Core.Repository;

public interface IRoomRentalRepository
{
    Task SaveRental(string data);
    Task<bool> CheckIfRoomIsRented(string room);
}
