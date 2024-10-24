using Microsoft.EntityFrameworkCore;
using PMC.Domain.Repositories;
using PMC.Infrastructure.Persistence;
using System.Linq.Expressions;
using System.Reflection;

namespace PMC.Infrastructure.Repositories
{
    internal class Repository<T>(PrescriptionManagementDbContext context) : IRepository<T> where T : class
    {
        private readonly PrescriptionManagementDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<T>, int)> FindAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            var baseQuery = _dbSet.Where(predicate);

            var totalCount = await baseQuery.CountAsync();

            var query = baseQuery; 

            // Apply sorting
            if (!string.IsNullOrEmpty(sortColumn))
            {
                query = ApplySorting(query, sortColumn, sortDirection);
            }

            var users = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            
            return (users, totalCount);
        }

        public async Task<(IEnumerable<T>, int)> GetAllAsync(int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            var totalCount = await _dbSet.CountAsync();

            var query = _dbSet.AsQueryable();

            // Apply sorting
            if (!string.IsNullOrEmpty(sortColumn))
            {
                query = ApplySorting(query, sortColumn, sortDirection);
            }

            var users = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        //public IQueryable<T> Query()
        //{
        //    return _dbSet.AsQueryable();
        //}

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
             _dbSet.Update(entity);
             await _context.SaveChangesAsync();
        }


        private IQueryable<T> ApplySorting(IQueryable<T> query, string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                return query; // If no sort column, return as is

            // Use reflection to get the property and apply sorting dynamically
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = typeof(T).GetProperty(sortColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null) return query;

            // Create the sort expression (e.g., p => p.SortColumn)
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var sortExpression = Expression.Lambda(propertyAccess, parameter);

            // Determine the sorting direction
            var methodName = sortDirection.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";

            // Create the OrderBy or OrderByDescending method dynamically
            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), property.PropertyType },
                query.Expression,
                Expression.Quote(sortExpression)
            );

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        
    }
}
