using Kvpbldsck.NastolochkiAPI.Users.Api.Models;

namespace Kvpbldsck.NastolochkiAPI.Users.Api.Contracts;

public interface IUsersProvider
{
    Task<ICollection<User>> GetAsync(params Guid[] guids);
}
