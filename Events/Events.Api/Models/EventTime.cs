namespace Kvpbldsck.NastolochkiAPI.Events.Api.Models;

public sealed class EventTime
{
    public bool IsVoting { get; init; }

    public bool IsVoted { get; init; }

    public ICollection<EventTimeVariant> TimeVariants { get; init; }
}
