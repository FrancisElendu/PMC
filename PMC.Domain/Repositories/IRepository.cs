using System.Linq.Expressions;

namespace PMC.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<(IEnumerable<T>, int)> GetAllAsync(int pageNumber, int pageSize);
        Task<(IEnumerable<T>, int)> FindAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        //IQueryable<T> Query();
    }
}
