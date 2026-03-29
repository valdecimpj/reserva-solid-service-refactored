using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Infrastructure.Service;

public class EmailService(IEventLoggingService eventLoggingService) : IEmailService
{
    public Task SendEmail()
    {
        eventLoggingService.LogEvent("Email sent");
        return Task.CompletedTask;
    }
}
