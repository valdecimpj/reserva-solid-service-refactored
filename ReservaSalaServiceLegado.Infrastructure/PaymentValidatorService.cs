using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Infrastructure;

public class PaymentValidatorService : IPaymentValidatorService
{
    public Task<(bool Result, string Message)> ValidatePaymentMethod(string paymentMethod)
    {
        if (paymentMethod.ToLower() == "pix")
            return Task.FromResult((true, "PIX payment ok"));
        else
            return Task.FromResult((false, "Invalid payment"));
    }
}
