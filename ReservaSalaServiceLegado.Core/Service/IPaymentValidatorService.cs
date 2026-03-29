namespace ReservaSalaServiceLegado.Core.Service;

public interface IPaymentValidatorService
{
    Task<(bool Result, string Message)> ValidatePaymentMethod(string paymentMethod);
}
