using Kvpbldsck.NastolochkiAPI.Gateway.Shared.Models;
using Refit;

namespace Kvpbldsck.NastolochkiAPI.Gateway.Shared.Contracts;

public interface IEventService
{
    [Post("/events")]
    Task<Guid> CreateAsync(Event @event);

    [Get("/events")]
    Task<ICollection<EventPreview>> GetAsync();

    [Get("/events/{guid}")]
    Task<Event> GetAsync(Guid guid);

    [Put("/events/{@event.Guid}")]
    Task UpdateAsync(Event @event);

    [Delete("/events/{guid}")]
    Task DeleteAsync(Guid guid);
}
