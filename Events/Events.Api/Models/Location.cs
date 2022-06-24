using Kvpbldsck.NastolochkiAPI.Common.Db.Models;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Models;

public sealed class Location : IEntity
{
    public Guid Guid { get; init; }

    public string Address { get; init; }

    public string Name { get; init; }

    public Location Copy(
        Guid? guid = null,
        string? address = null,
        string? name = null)
    {
        return new()
        {
            Guid = guid ?? Guid,
            Address = address ?? Address,
            Name = name ?? Name,
        };
    }
}
