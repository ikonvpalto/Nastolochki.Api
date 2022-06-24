using System.Linq.Expressions;

namespace Kvpbldsck.NastolochkiAPI.Common.Db.Contracts;

public interface IBaseRepository<TModel>
{
    Task<bool> CreateAsync(TModel model, CancellationToken cancellationToken);

    Task<bool> CreateAsync(ICollection<TModel> models, CancellationToken cancellationToken);

    Task<ICollection<TModel>> GetAsync(CancellationToken cancellationToken);

    Task<ICollection<TModel>> GetAsync(Expression<Func<TModel, bool>> query, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(Expression<Func<TModel, bool>> query, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(TModel model, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Expression<Func<TModel, bool>> query, CancellationToken cancellationToken);
}
