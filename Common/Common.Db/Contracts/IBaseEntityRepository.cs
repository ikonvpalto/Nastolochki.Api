using Kvpbldsck.NastolochkiAPI.Common.Db.Models;

namespace Kvpbldsck.NastolochkiAPI.Common.Db.Contracts;

public interface IBaseEntityRepository<TModel> : IBaseRepository<TModel>
    where TModel : IEntity
{
    Task<TModel?> GetAsync(Guid guid, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(Guid guid, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid guid, CancellationToken cancellationToken);
}
