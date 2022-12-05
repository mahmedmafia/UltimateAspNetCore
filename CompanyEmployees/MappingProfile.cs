using AutoMapper;
using Entities.Models;
using Shared.DTO;

namespace CompanyEmployees
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c=> c.FullAddress,
                opt => opt.MapFrom(entity => string.Join(" ", entity.Address, entity.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CompanyCreationDto, Company>();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<EmployeeCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();


        }
    }
}
