using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel;
using TorcAssestmentAPI.Models;

namespace TorcAssestmentAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbTorcContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbTorcContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<T?> Update(int id, T entity)
        {
            var existing = await _dbSet.FindAsync(id);
            if (existing == null)
            { return null; }

            _context.Entry(existing).CurrentValues.SetValues(entity);
            return existing;
        }

        public async Task Delete(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
