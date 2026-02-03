using TorcAssestmentAPI.Models;

namespace TorcAssestmentAPI.Services
{
    public interface IEmployeeService
    {
        Task Create(EmployeeDto employee);
        Task<List<EmployeeDto>> GetAll();
        Task<EmployeeDto> GetById(int id);
        Task Update(EmployeeDto employee);
        Task<List<int>> Delete(List<int> id);
    }
}
