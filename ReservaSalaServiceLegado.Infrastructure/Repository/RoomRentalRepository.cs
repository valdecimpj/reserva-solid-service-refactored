using ReservaSalaServiceLegado.Core.Model;
using ReservaSalaServiceLegado.Core.Repository;

namespace ReservaSalaServiceLegado.Infrastructure.Repository;

public class RoomRentalRepository : IRoomRentalRepository
{
    private readonly IList<string> _reservedRooms = [];

    public Task<bool> CheckIfRoomIsRented(string room) =>
        Task.FromResult(_reservedRooms.Any(_ => _.Contains(room)));

    public Task SaveRental(RentalModel rental)
    {
        _reservedRooms.Add(rental.Data);
        return Task.CompletedTask;
    }
}
