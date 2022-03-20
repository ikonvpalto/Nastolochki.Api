using Kvpbldsck.NastolochkiAPI.Users.Api.Contracts;
using Kvpbldsck.NastolochkiAPI.Users.Api.Models;
using Kvpbldsck.NastolochkiAPI.Users.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kvpbldsck.NastolochkiAPI.Users.Api.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUsersProvider _usersProvider;

    public UsersController(IUsersProvider usersProvider)
    {
        _usersProvider = usersProvider;
    }

    [HttpGet("{guid:guid}")]
    public async Task<User?> GetAsync([FromRoute] Guid guid)
    {
        var users = await _usersProvider.GetAsync(guid).ConfigureAwait(false);
        return users.SingleOrDefault();
    }

    [HttpGet("")]
    public async Task<ICollection<User>> GetAsync([FromQuery] Guid[] guids)
    {
        var users = await _usersProvider.GetAsync(guids).ConfigureAwait(false);
        return users;
    }
}
