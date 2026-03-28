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
    IReciepeService reciepeService
)
{
    public async Task<string> Handle(RentRoomRequest request)
    {
        var validation = await roomRentalInformationValidator.Validate(request.User, request.Room, request.Hours);

        if(validation.Result is false)
        {
            Console.WriteLine(validation.Error);
            return validation.Error!;
        }

        var roomIsRented = await roomRentalRepository.RoomIsRented(request.Room);

        if (roomIsRented)
        {
            var error = "Room is already reserved.";
            Console.WriteLine(error);
            return error;
        }

        var rentalValue = await roomRentalValueCalculator.CalculateValue(
            request.Hours,
            request.RoomType,
            [RoomFeatureEnum.Projector]
        );

        var paymentValidation = await paymentValidatorService.ValidatePaymentMethod(request.paymentMethod);
        Console.WriteLine(paymentValidation.Message);

        if(!paymentValidation.Result)
            return paymentValidation.Message;

        var rentalData = $"{request.User} - ${request.Room} - ${rentalValue}";
        await roomRentalRepository.SaveRental(rentalData);
        await emailService.SendEmail();
        await reciepeService.PrintReciepe(rentalData);
        return "Rented successfully";
    }

}