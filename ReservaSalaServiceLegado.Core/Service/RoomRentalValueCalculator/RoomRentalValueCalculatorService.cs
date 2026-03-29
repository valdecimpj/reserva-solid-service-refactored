using ReservaSalaServiceLegado.Core.Enum;
using ReservaSalaServiceLegado.Core.Service.RoomRentalValueCalculator;

namespace ReservaSalaServiceLegado.Infrastructure;

public class RoomRentalValueCalculatorService : IRoomRentalValueCalculatorService
{
    public Task<decimal> CalculateValue(
        int hours,
        RoomTypeEnum roomType,
        IList<RoomFeatureEnum> features
    )
    {
        decimal value = 50 * hours;
        value += GetRoomTypeValue(roomType);
        value += features.Select(GetFeatureValue).Sum();
        return Task.FromResult(value);
    }

    private decimal GetFeatureValue(RoomFeatureEnum roomFeature, int _) =>
        roomFeature switch
        {
            RoomFeatureEnum.Projector => 20,
            _ => 0,
        };

    private decimal GetRoomTypeValue(RoomTypeEnum roomType) =>
        roomType switch
        {
            RoomTypeEnum.Laboratory => 30,
            _ => 0,
        };
}
