using ReservaSalaServiceLegado.Core.Enum;

public record RentRoomRequest(
    string User,
    string Room,
    RoomTypeEnum RoomType,
    int Hours,
    IList<RoomFeatureEnum> roomFeatures,
    string paymentMethod);