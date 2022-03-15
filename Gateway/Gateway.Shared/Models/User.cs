namespace Kvpbldsck.NastolochkiAPI.Gateway.Shared.Models;

public sealed class User : IEntity
{
    public Guid Guid { get; set; }

    public Avatar Avatar { get; init; }

    public string Name { get; set; }
}
