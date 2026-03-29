using ReservaSalaServiceLegado.Core.Model;

namespace ReservaSalaServiceLegado.Core.UseCases.RentRoom;

public class RentRoomResponse(bool success, string message, RentalDataModel? rentalDataModel)
{
    public bool Success { get; init; } = success;
    public string Message { get; init; } = message;
    public string? Data { get; init; } = rentalDataModel?.Data;
}
