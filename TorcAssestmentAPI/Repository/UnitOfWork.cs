using TorcAssestmentAPI.Models;

namespace TorcAssestmentAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        // DbContext instance
        private readonly DbTorcContext _context;

        // Repository for Employee entity
        public IRepository<Employee> Employees { get; }

        // Constructor
        public UnitOfWork(DbTorcContext context)
        {
            _context = context;
            Employees = new Repository<Employee>(_context);
        }

        // Commit changes to the database
        public int Commit()
        {
            return _context.SaveChanges();
        }

        // Dispose DbContext
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
