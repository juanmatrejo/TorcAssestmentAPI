using TorcAssestmentAPI.Models;

namespace TorcAssestmentAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbTorcContext _context;

        public IRepository<Employee> Employees { get; }

        public UnitOfWork(DbTorcContext context)
        {
            _context = context;
            Employees = new Repository<Employee>(_context);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
