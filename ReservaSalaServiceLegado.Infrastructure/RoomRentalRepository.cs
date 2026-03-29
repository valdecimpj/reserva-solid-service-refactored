using ReservaSalaServiceLegado.Core.Repository;

namespace ReservaSalaServiceLegado.Infrastructure;

public class RoomRentalRepository : IRoomRentalRepository
{
    private readonly IList<string> _reservedRooms = [];

    public Task<bool> RoomIsRented(string room) =>
        Task.FromResult(_reservedRooms.Any(_ => _.Contains(room)));

    public Task SaveRental(string data)
    {
        _reservedRooms.Add(data);
        return Task.CompletedTask;
    }
}
