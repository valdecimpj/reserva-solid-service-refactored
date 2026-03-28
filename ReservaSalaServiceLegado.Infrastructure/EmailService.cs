using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Infrastructure;

public class EmailService : IEmailService
{
    public Task SendEmail()
    {
        Console.WriteLine("Email enviado");
        return Task.CompletedTask;
    }
}