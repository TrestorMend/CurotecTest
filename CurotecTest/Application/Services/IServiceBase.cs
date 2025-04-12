using System.Linq.Expressions;

namespace Application.Services
{
    public interface IServiceBase<T>
    {
        Task<T?> Get(int id);

        Task<T?> Get(string key);

        Task<IEnumerable<T>> GetAll();

        Task<T> Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        void Update(T entity, params Expression<Func<T, object>>[] properties);
    }
}
