using Kvpbldsck.NastolochkiAPI.Users.Api.Contracts;
using Kvpbldsck.NastolochkiAPI.Users.Api.Models;

namespace Kvpbldsck.NastolochkiAPI.Users.Api.Services;

public sealed class UsersProvider : IUsersProvider
{
    private static readonly User[] Users =
    {
        new()
        {
            Guid = Guid.Parse("0333c64b-cd1f-4b3c-8938-d228446fcc3c"),
            Name = "Валера",
            Avatar = new()
            {
                Uri = new("https://sdl-stickershop.line.naver.jp/products/0/0/1/1033677/LINEStorePC/main.png?__=20161019", UriKind.Absolute)
            }
        },
        new()
        {
            Guid = Guid.Parse("f53e2469-fae6-4e49-9cb3-ac0f992120e1"),
            Name = "Полина",
            Avatar = new()
            {
                Uri = new("https://pm1.narvii.com/7938/e453ab256ded58353f34df07c8708d22768a58f6r1-644-635v2_00.jpg", UriKind.Absolute)
            }
        }
    };

    public Task<ICollection<User>> GetAsync(params Guid[] guids)
    {
        ICollection<User> result = guids is not null && guids.Length > 0
            ? Users
                .Where(u => guids.Contains(u.Guid))
                .ToArray()
            : Users;

        return Task.FromResult(result);
    }
}
