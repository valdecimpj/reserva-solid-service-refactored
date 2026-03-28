using ReservaSalaServiceLegado.Core.Enum;

namespace ReservaSalaServiceLegado.Core.Service;

public interface IRoomRentalValueCalculator
{
    Task<decimal> CalculateValue(int hours, RoomTypeEnum roomType, IList<RoomFeatureEnum> features);
}