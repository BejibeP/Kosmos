using System.Linq.Expressions;

namespace Bejibe.Kosmos.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(bool ignoreArchived = true, params Expression<Func<T, object>>[] includes);

        Task<T?> GetById(long id, bool ignoreArchived = true, params Expression<Func<T, object>>[] includes);

        Task<T?> Create(T entity);

        Task<T?> Update(T entity);

        Task<bool> Archive(long id);

        Task<bool> Delete(long id);
    }
}
