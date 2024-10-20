using Microsoft.EntityFrameworkCore;
using PMC.Domain.Repositories;
using PMC.Infrastructure.Persistence;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
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
    }
}
