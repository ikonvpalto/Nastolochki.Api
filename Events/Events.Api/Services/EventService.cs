using Kvpbldsck.NastolochkiAPI.Common.Contract.Exceptions;
using Kvpbldsck.NastolochkiAPI.Events.Api.Models;
using Kvpbldsck.NastolochkiAPI.Events.Api.Repositories.Contracts;
using Kvpbldsck.NastolochkiAPI.Events.Api.Resources;
using Kvpbldsck.NastolochkiAPI.Events.Api.Services.Contracts;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Services;

public sealed class EventService : IEventService
{
    private readonly IEventsRepository _eventsRepository;
    private readonly ILocationsRepository _locationsRepository;

    public EventService(IEventsRepository eventsRepository, ILocationsRepository locationsRepository)
    {
        _eventsRepository = eventsRepository;
        _locationsRepository = locationsRepository;
    }

    public async Task<ICollection<Event>> GetAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new List<Event>();
        }

        return await _eventsRepository.GetAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<Event> GetAsync(Guid guid, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ();
        }

        var @event = await _eventsRepository.GetAsync(guid, cancellationToken).ConfigureAwait(false);

        if (@event is null)
        {
            throw new NotFoundException(string.Format(Localization.EventNotFound, guid));
        }

        return @event;
    }

    public async Task<Guid> AddAsync(Event @event, CancellationToken cancellationToken)
    {
        @event = @event.Copy(
            participants: @event.Participants.DistinctBy(p => p.ParticipantGuid).ToList()
        );

        if (await _eventsRepository.IsExistsAsync(@event.Guid, cancellationToken).ConfigureAwait(false) && !cancellationToken.IsCancellationRequested)
        {
            throw new AlreadyExistsException(string.Format(Localization.EventGuidAlreadyExists, @event.Guid));
        }

        if (!await _locationsRepository.IsExistsAsync(@event.LocationGuid, cancellationToken).ConfigureAwait(false) && !cancellationToken.IsCancellationRequested)
        {
            throw new NotFoundException(string.Format(Localization.LocationNotFound, @event.LocationGuid));
        }

        return await _eventsRepository.CreateAsync(@event, cancellationToken).ConfigureAwait(false)
            ? @event.Guid
            : Guid.Empty;
    }
}
