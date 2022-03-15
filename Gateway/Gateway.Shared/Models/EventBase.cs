namespace Kvpbldsck.NastolochkiAPI.Gateway.Shared.Models;

public abstract class EventBase : IEntity
{
    public Guid Guid { get; set; }

    public string Name { get; init; }

    public Time Time { get; init; }

    public Place Place { get; init; }

    public Avatar Avatar { get; init; }
}
