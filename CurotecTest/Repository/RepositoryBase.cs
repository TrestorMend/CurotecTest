using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly CurotecDbContext _context;

        public RepositoryBase(CurotecDbContext context) => _context = context;

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {

            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsNoTracking();

            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty.AsPath()));

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T?> Get(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                return null;

            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T?> Get(string key)
        {
            var entity = await _context.Set<T>().FindAsync(key);

            if (entity == null)
                return null;

            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Update(T entity, params Expression<Func<T, object>>[] properties)
        {
            foreach (var property in properties)
            {
                var propertyName = Property(property);
                _context.Entry(entity).Property(propertyName).IsModified = true;
            }
        }

        private string Property(Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
            {
                if (expression.Body.GetType() == typeof(UnaryExpression))
                {
                    var unaryBody = expression.Body as UnaryExpression;

                    var property = unaryBody.Operand as MemberExpression;

                    return property.Member.Name;
                }
            }
            else
            {
                var propertyInfo = (PropertyInfo)body.Member;
                return propertyInfo.Name;
            }

            return string.Empty;
        }
    }
}
