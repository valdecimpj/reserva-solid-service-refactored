using ReservaSalaServiceLegado.Core.Model;
using ReservaSalaServiceLegado.Core.Repository;
using ReservaSalaServiceLegado.Core.Service;
using ReservaSalaServiceLegado.Core.Service.RoomRentalInformationValidator;
using ReservaSalaServiceLegado.Core.Service.RoomRentalValueCalculator;

namespace ReservaSalaServiceLegado.Core.UseCases.RentRoom;

public class RentRoomHandler(
    IRoomRentalInformationValidatorService roomRentalInformationValidatorService,
    IRoomRentalValueCalculatorService roomRentalValueCalculatorService,
    IPaymentValidatorService paymentValidatorService,
    IRoomRentalRepository roomRentalRepository,
    IEmailService emailService,
    IReceiptService receiptService,
    IEventLoggingService eventLoggingService
)
{
    public async Task<RentRoomResponse> Handle(RentRoomRequest request)
    {
        var inputDataValidation = await roomRentalInformationValidatorService.Validate(
            request.User,
            request.Room,
            request.Hours
        );

        if (inputDataValidation.Result is false)
        {
            await eventLoggingService.LogEvent(inputDataValidation.Error!);
            return new RentRoomResponse(false, inputDataValidation.Error!, null);
        }

        var roomIsRented = await roomRentalRepository.CheckIfRoomIsRented(request.Room);

        if (roomIsRented)
        {
            var error = "Room is already reserved.";
            await eventLoggingService.LogEvent(error);
            return new RentRoomResponse(false, error, null);
        }

        var rentalValue = await roomRentalValueCalculatorService.CalculateValue(
            request.Hours,
            request.RoomType,
            request.roomFeatures
        );

        var paymentValidation = await paymentValidatorService.ValidatePaymentMethod(
            request.paymentMethod
        );

        await eventLoggingService.LogEvent(paymentValidation.Message);

        if (paymentValidation.Result is false)
            return new RentRoomResponse(false, paymentValidation.Message, null);

        var rental = new RentalModel(request.User, request.Room, rentalValue);
        await roomRentalRepository.SaveRental(rental);
        await emailService.SendEmail();
        await receiptService.PrintReceipt(rental);
        var successMessage = "Rented successfully";
        await eventLoggingService.LogEvent(successMessage);
        return new RentRoomResponse(true, successMessage, rental);
    }
}
