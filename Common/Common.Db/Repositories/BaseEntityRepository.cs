using Kvpbldsck.NastolochkiAPI.Common.Db.Contracts;
using Kvpbldsck.NastolochkiAPI.Common.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Kvpbldsck.NastolochkiAPI.Common.Db.Repositories;

public abstract class BaseEntityRepository<TModel> : BaseRepository<TModel>, IBaseEntityRepository<TModel>
    where TModel : class, IEntity
{
    protected BaseEntityRepository(DbContext dbContext, DbSet<TModel> dbSet, IQueryable<TModel>? baseQuery = null)
        : base(dbContext, dbSet, baseQuery)
    {
    }

    public async Task<TModel?> GetAsync(Guid guid, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return default;
        }

        return await BaseQuery
            .SingleOrDefaultAsync(s => s.Guid == guid, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<bool> IsExistsAsync(Guid guid, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return default;
        }

        return await DbSet
            .AnyAsync(s => s.Guid == guid, cancellationToken)
            .ConfigureAwait(false);
    }

    public virtual async Task<bool> DeleteAsync(Guid guid, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }

        var model = await DbSet.SingleOrDefaultAsync(s => s.Guid == guid, cancellationToken);

        if (model is not null)
        {
            DbContext.Remove(model);
            return await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }

        return false;
    }
}
