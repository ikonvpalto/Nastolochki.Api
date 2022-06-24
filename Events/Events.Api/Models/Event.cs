using Kvpbldsck.NastolochkiAPI.Common.Db.Models;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Models;

public sealed class Event : IEntity
{
    public Guid Guid { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public ICollection<EventParticipant> Participants { get; init; }

    public EventTime Time { get; init; }

    public Location? Location { get; init; }

    public Guid LocationGuid { get; init; }

    public Event Copy(
        Guid? guid = null,
        string? name = null,
        string? description = null,
        ICollection<EventParticipant>? participants = null,
        EventTime? eventTime = null,
        Location? location = null,
        Guid? locationGuid = null)
    {
        return new()
        {
            Guid = guid ?? Guid,
            Name = name ?? Name,
            Description = description ?? Description,
            Participants = participants ?? Participants,
            Time = eventTime ?? Time,
            Location = location ?? Location?.Copy(),
            LocationGuid = locationGuid ?? LocationGuid
        };
    }
}
