namespace ReservaSalaServiceLegado.Core.Service.RoomRentalInformationValidator;

public interface IRoomRentalInformationValidatorService
{
    Task<(bool Result, string? Error)> Validate(string user, string room, int hours);
}
