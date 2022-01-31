using System.Linq.Expressions;
namespace EmpComp.Repositories.Base
{
    public interface IRepositoryBase<T>
    {
        void Create(T entity);
        Task CreateAsync(T entity);

        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        void Update(T entity);
        Task UpdateAsync(T entity);

        void Delete(T entity);
        Task DeleteAsync(T entity);
    }
}
