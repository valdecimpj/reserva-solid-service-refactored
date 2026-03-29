namespace ReservaSalaServiceLegado.Core.Service.RoomRentalInformationValidator;

public class RoomRentalInformationValidatorService : IRoomRentalInformationValidatorService
{
    public Task<(bool Result, string? Error)> Validate(string user, string room, int hours)
    {
        if (string.IsNullOrWhiteSpace(user))
            return Task.FromResult<(bool Result, string? Error)>((false, "Invalid user"));

        if (string.IsNullOrWhiteSpace(room))
            return Task.FromResult<(bool Result, string? Error)>((false, "Invalid room"));

        if (hours <= 0)
            return Task.FromResult<(bool Result, string? Error)>((false, "Invalid hours"));

        return Task.FromResult<(bool Result, string? Error)>((true, null));
    }
}
