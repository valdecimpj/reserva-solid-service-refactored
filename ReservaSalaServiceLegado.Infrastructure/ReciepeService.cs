using ReservaSalaServiceLegado.Core.Service;

namespace ReservaSalaServiceLegado.Infrastructure;

public class ReciepeService : IReciepeService
{
    public Task PrintReciepe(string data)
    {
        Console.WriteLine($"Comprovante: {data}");
        return Task.CompletedTask;
    }
}