using ReservaSalaServiceLegado.Core.Repository;

namespace ReservaSalaServiceLegado.Infrastructure;

public class RoomRentalRepository : IRoomRentalRepository
{
    private readonly IList<string> reservedRooms = [];

    public Task<bool> RoomIsRented(string room) => Task.FromResult(reservedRooms.Any(_ => _.Contains(room)));

    public Task SaveRental(string data)
    {
        reservedRooms.Add(data);
        return Task.CompletedTask;
    }
}