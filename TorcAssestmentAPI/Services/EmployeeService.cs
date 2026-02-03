using AutoMapper;
using FluentValidation;
using TorcAssestmentAPI.Models;
using TorcAssestmentAPI.Repository;

namespace TorcAssestmentAPI.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(EmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _unitOfWork.Employees.Create(employee);
            _unitOfWork.Commit();
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            var employees = await _unitOfWork.Employees.GetAll();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetById(int id)
        {
            var employee = await _unitOfWork.Employees.GetById(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task Update(EmployeeDto dto)
        {
            var result = _unitOfWork.Employees.GetById(dto.Id);
            if (result == null) { return; }

            var employee = _mapper.Map<Employee>(dto);
            result = _unitOfWork.Employees.Update(employee.Id, employee);
            _unitOfWork.Commit();
        }

        public async Task<List<int>> Delete(List<int> ids)
        {
            var employees = _unitOfWork.Employees.GetAll().Result.Where(e => ids.Contains(e.Id)).ToList();
            var employeeIds = employees.Select(e => e.Id).ToHashSet();
            var notFound = ids.Where(id => !employeeIds.Contains(id)).ToList();

            _unitOfWork.Employees.Delete(employees);
            _unitOfWork.Commit();

            return notFound;
        }
    }
}
