namespace ReservaSalaServiceLegado.Core.Service;

public interface IPaymentValidatorService
{
    Task<bool> ValidatePaymentMethod(string paymentMethod);
}