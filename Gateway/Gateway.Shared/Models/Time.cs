namespace Kvpbldsck.NastolochkiAPI.Gateway.Shared.Models;

public sealed class Time
{
    public ICollection<DateTime> Variants { get; init; }

    public bool IsFixated => ChosenTime is not null;

    public DateTime? ChosenTime { get; init; }
}
