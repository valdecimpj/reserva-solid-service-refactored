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
    public async Task Handle(RentRoomRequest request)
    {
        var validation = await roomRentalInformationValidator.Validate(request.User, request.Room, request.Hours);

        if(validation.Result is false)
        {
            Console.WriteLine(validation.Error);
            return;
        }

        var roomIsRented = await roomRentalRepository.RoomIsRented(request.Room);

        if (roomIsRented)
        {
            Console.WriteLine("Room is already reserved.");
            return;
        }

        var rentalValue = await roomRentalValueCalculator.CalculateValue(
            request.Hours,
            request.RoomType,
            [RoomFeatureEnum.Projector]
        );

        paymentValidatorService.Validate()




    }

}