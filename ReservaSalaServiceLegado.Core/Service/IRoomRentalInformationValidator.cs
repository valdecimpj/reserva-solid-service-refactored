namespace ReservaSalaServiceLegado.Core.Service;

public interface IRoomRentalInformationValidator
{
    Task<(bool Result, string? Error)> Validate(string user, string room, int hours);
}
