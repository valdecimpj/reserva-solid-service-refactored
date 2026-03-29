using ReservaSalaServiceLegado.Core.Enum;

namespace ReservaSalaServiceLegado.Core.Service.RoomRentalValueCalculator;

public interface IRoomRentalValueCalculatorService
{
    Task<decimal> CalculateValue(int hours, RoomTypeEnum roomType, IList<RoomFeatureEnum> features);
}
