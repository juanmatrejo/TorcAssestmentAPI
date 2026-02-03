using AutoMapper;

namespace TorcAssestmentAPI.Models
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
