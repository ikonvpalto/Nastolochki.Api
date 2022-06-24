using Kvpbldsck.NastolochkiAPI.Events.Api.Models;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Services.Contracts;

public interface IEventService
{
    Task<ICollection<Event>> GetAsync(CancellationToken cancellationToken);
    Task<Event> GetAsync(Guid guid, CancellationToken cancellationToken);
    Task<Guid> AddAsync(Event @event, CancellationToken cancellationToken);
}
