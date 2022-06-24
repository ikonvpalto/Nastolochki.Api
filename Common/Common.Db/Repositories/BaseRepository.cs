using System.Linq.Expressions;
using Kvpbldsck.NastolochkiAPI.Common.Db.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Kvpbldsck.NastolochkiAPI.Common.Db.Repositories;

public abstract class BaseRepository<TModel> : IBaseRepository<TModel>
    where TModel : class
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<TModel> DbSet;
    protected readonly IQueryable<TModel> BaseQuery;

    protected BaseRepository(DbContext dbContext, DbSet<TModel> dbSet, IQueryable<TModel>? baseQuery = null)
    {
        DbContext = dbContext;
        DbSet = dbSet;
        BaseQuery = baseQuery ?? dbSet;
    }

    public virtual async Task<bool> CreateAsync(TModel model, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }

        DbSet.Add(model);
        return await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
    }

    public virtual async Task<bool> CreateAsync(ICollection<TModel> models, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }

        DbSet.AddRange(models);
        return await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
    }

    public async Task<ICollection<TModel>> GetAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new List<TModel>();
        }

        return await BaseQuery
            .AsNoTracking()
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ICollection<TModel>> GetAsync(Expression<Func<TModel, bool>> query, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new List<TModel>();
        }

        return await BaseQuery
            .AsNoTracking()
            .Where(query)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<bool> IsExistsAsync(Expression<Func<TModel, bool>> query, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return default;
        }

        return await DbSet.AnyAsync(query, cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task<bool> UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }

        DbSet.Update(model);
        return await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TModel, bool>> query, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }

        var models = await DbSet.Where(query).ToListAsync(cancellationToken);
        DbContext.RemoveRange(models);
        return await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
    }
}
