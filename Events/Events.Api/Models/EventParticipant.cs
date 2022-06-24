namespace Kvpbldsck.NastolochkiAPI.Events.Api.Models;

public sealed class EventParticipant
{
    public Guid EventGuid { get; init; }

    public Guid ParticipantGuid { get; init; }
}
