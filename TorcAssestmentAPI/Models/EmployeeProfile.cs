using AutoMapper;

namespace TorcAssestmentAPI.Models
{
    public class EmployeeProfile : Profile
    {

        // Constructor to define mapping configurations
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
