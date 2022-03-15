namespace Kvpbldsck.NastolochkiAPI.Gateway.Shared.Models;

public sealed class Event : EventBase
{
    public string Description { get; init; }

    public ICollection<User> Users { get; init; }

    public User Organizer { get; init; }
}
