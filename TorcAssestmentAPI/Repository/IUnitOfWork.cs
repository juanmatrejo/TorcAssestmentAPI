using TorcAssestmentAPI.Models;

namespace TorcAssestmentAPI.Repository
{
    public interface IUnitOfWork
    {
        IRepository<Employee> Employees { get; }
        int Commit();
    }
}
