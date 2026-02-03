using Microsoft.EntityFrameworkCore;
using TorcAssestmentAPI.Models;

namespace TorcAssestmentAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        // DbContext and DbSet for the entity
        protected readonly DbTorcContext _context;
        protected readonly DbSet<T> _dbSet;

        // Constructor
        public Repository(DbTorcContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Get all entities
        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        // Get entity by ID
        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Create a new entity
        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Update an existing entity
        public async Task<T?> Update(int id, T entity)
        {
            var existing = await _dbSet.FindAsync(id);
            if (existing == null)
            { return null; }

            _context.Entry(existing).CurrentValues.SetValues(entity);
            return existing;
        }

        // Delete an entity
        public async Task Delete(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
