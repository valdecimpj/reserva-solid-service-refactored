using ReservaSalaServiceLegado.Core.Enum;
using ReservaSalaServiceLegado.Core.Repository;
using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Core.UseCases.RentRoom;

public class RentRoomHandler(
    IRoomRentalInformationValidator roomRentalInformationValidator,
    IRoomRentalValueCalculator roomRentalValueCalculator,
    IPaymentValidatorService paymentValidatorService,
    IRoomRentalRepository roomRentalRepository,
    IEmailService emailService,
    IReciepeService reciepeService,
    IEventLoggingService eventLoggingService
)
{
    public async Task<RentRoomResponse> Handle(RentRoomRequest request)
    {
        var validation = await roomRentalInformationValidator.Validate(
            request.User,
            request.Room,
            request.Hours
        );

        if (validation.Result is false)
        {
            await eventLoggingService.LogEvent(validation.Error!);
            return new RentRoomResponse(false, validation.Error!, null);
        }

        var roomIsRented = await roomRentalRepository.RoomIsRented(request.Room);

        if (roomIsRented)
        {
            var error = "Room is already reserved.";
            await eventLoggingService.LogEvent(error);
            return new RentRoomResponse(false, error, null);
        }

        var rentalValue = await roomRentalValueCalculator.CalculateValue(
            request.Hours,
            request.RoomType,
            [RoomFeatureEnum.Projector]
        );

        var paymentValidation = await paymentValidatorService.ValidatePaymentMethod(
            request.paymentMethod
        );

        await eventLoggingService.LogEvent(paymentValidation.Message);

        if (!paymentValidation.Result)
            return new RentRoomResponse(false, paymentValidation.Message, null);

        var rentalData = $"{request.User} - {request.Room} - R${rentalValue}";
        await roomRentalRepository.SaveRental(rentalData);
        await emailService.SendEmail();
        await reciepeService.PrintReciepe(rentalData);
        await eventLoggingService.LogEvent("Rented successfully");
        return new RentRoomResponse(true, "Rented successfully", rentalData);
    }
}
