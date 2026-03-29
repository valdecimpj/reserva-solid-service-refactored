namespace ReservaSalaServiceLegado.Core.Model;

public class RentalDataModel(string user, string room, decimal value)
{
    private string _user { get; set; } = user;
    private string _room { get; set; } = room;
    private decimal _value { get; set; } = value;
    public string Data => $"{_user} - {_room} - R${_value}";
}
