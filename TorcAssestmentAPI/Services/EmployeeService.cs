using AutoMapper;
using FluentValidation;
using TorcAssestmentAPI.Models;
using TorcAssestmentAPI.Repository;

namespace TorcAssestmentAPI.Services
{
    public class EmployeeService : IEmployeeService
    {

        // Dependency Injection for Unit of Work and Mapper
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Constructor
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Create Employee
        public async Task Create(EmployeeDto dto)
        {
            // Map DTO to Entity
            var employee = _mapper.Map<Employee>(dto);

            // Create Employee
            await _unitOfWork.Employees.Create(employee);
            _unitOfWork.Commit();
            _unitOfWork.Dispose();
        }

        // Get All Employees
        public async Task<List<EmployeeDto>> GetAll()
        {
            // Retrieve all employees
            var employees = await _unitOfWork.Employees.GetAll();
            _unitOfWork.Dispose();

            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        // Get Employee by ID
        public async Task<EmployeeDto> GetById(int id)
        {
            // Retrieve employee by ID
            var employee = await _unitOfWork.Employees.GetById(id);
            _unitOfWork.Dispose();

            return _mapper.Map<EmployeeDto>(employee);
        }

        // Update Employee
        public async Task Update(EmployeeDto dto)
        {
            //  
            var result = _unitOfWork.Employees.GetById(dto.Id);
            if (result == null) { return; }

            // Map DTO to Entity
            var employee = _mapper.Map<Employee>(dto);

            // Update Employee
            result = _unitOfWork.Employees.Update(employee.Id, employee);
            _unitOfWork.Commit();
            _unitOfWork.Dispose();
        }

        // Delete Employees by IDs
        public async Task<List<int>> Delete(List<int> ids)
        {
            // Retrieve employees to delete
            var employees = _unitOfWork.Employees.GetAll().Result.Where(e => ids.Contains(e.Id)).ToList();

            // Identify IDs not found
            var employeeIds = employees.Select(e => e.Id).ToHashSet();

            // List of IDs that were not found
            var notFound = ids.Where(id => !employeeIds.Contains(id)).ToList();

            // Delete Employees
            _unitOfWork.Employees.Delete(employees);
            _unitOfWork.Commit();
            _unitOfWork.Dispose();

            // Return list of not found IDs
            return notFound;
        }
    }
}
