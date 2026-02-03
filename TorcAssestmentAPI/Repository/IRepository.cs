namespace TorcAssestmentAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task Create(T entity);
        Task<T?> Update(int id, T entity);
        Task Delete(List<T> entity);
    }
}
