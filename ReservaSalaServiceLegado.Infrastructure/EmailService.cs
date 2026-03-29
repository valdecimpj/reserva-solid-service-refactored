using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Infrastructure;

public class EmailService(IEventLoggingService eventLoggingService) : IEmailService
{
    public Task SendEmail()
    {
        eventLoggingService.LogEvent("Email sent");
        return Task.CompletedTask;
    }
}
