using Kvpbldsck.NastolochkiAPI.Common.Contract.Models;

namespace Kvpbldsck.NastolochkiAPI.Users.Api.Models;

public sealed record User : IEntity
{
    public Guid Guid { get; init; }

    public Avatar Avatar { get; init; }

    public string Name { get; set; }
}
