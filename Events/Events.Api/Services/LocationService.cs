using Kvpbldsck.NastolochkiAPI.Common.Contract.Exceptions;
using Kvpbldsck.NastolochkiAPI.Events.Api.Models;
using Kvpbldsck.NastolochkiAPI.Events.Api.Repositories.Contracts;
using Kvpbldsck.NastolochkiAPI.Events.Api.Resources;
using Kvpbldsck.NastolochkiAPI.Events.Api.Services.Contracts;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Services;

public sealed class LocationService : ILocationService
{
    private readonly ILocationsRepository _locationsRepository;

    public LocationService(ILocationsRepository locationsRepository)
    {
        _locationsRepository = locationsRepository;
    }

    public async Task<ICollection<Location>> GetAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new List<Location>();
        }

        return await _locationsRepository.GetAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<Location> GetAsync(Guid guid, CancellationToken cancellationToken)
    {
        var location = await _locationsRepository.GetAsync(guid, cancellationToken).ConfigureAwait(false);

        if (cancellationToken.IsCancellationRequested)
        {
            return new ();
        }

        if (location is null)
        {
            throw new NotFoundException(string.Format(Localization.LocationNotFound, guid));
        }

        return location;
    }

    public async Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
    {
        if (await _locationsRepository.IsExistsAsync(location.Guid, cancellationToken).ConfigureAwait(false) && !cancellationToken.IsCancellationRequested)
        {
            throw new AlreadyExistsException(string.Format(Localization.LocationGuidAlreadyExists, location.Guid));
        }

        return await _locationsRepository.CreateAsync(location, cancellationToken).ConfigureAwait(false)
            ? location.Guid
            : Guid.Empty;
    }
}
