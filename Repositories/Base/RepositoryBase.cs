using System.Linq.Expressions;
using EmpComp.Models;
namespace EmpComp.Repositories.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationContext _context;
        protected RepositoryBase(ApplicationContext context)
        {
            _context = context;
        }
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public Task CreateAsync(T entity)
        {
            return _context.Set<T>().AddAsync(entity).AsTask();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public Task DeleteAsync(T entity)
        {
            return Task.Run(() => _context.Set<T>().Remove(entity));
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public Task UpdateAsync(T entity)
        {
            return Task.Run(() => _context.Set<T>().Update(entity));
        }
    }
}
