using FinalProject.Entity;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAccess;
using System.Linq.Expressions;
using System.Security.Principal;

namespace FinalProject.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly DbSet<T> _dbSet;
        protected readonly DAccess.DbAccess _context;

        public Repository(DAccess.DbAccess context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
        }
    }
}