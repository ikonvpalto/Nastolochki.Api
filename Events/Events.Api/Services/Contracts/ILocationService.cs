using Kvpbldsck.NastolochkiAPI.Events.Api.Models;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Services.Contracts;

public interface ILocationService
{
    Task<ICollection<Location>> GetAsync(CancellationToken cancellationToken);
    Task<Location> GetAsync(Guid guid, CancellationToken cancellationToken);
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken);
}
