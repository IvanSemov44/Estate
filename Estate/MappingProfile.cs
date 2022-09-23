using AutoMapper;
using Entities.Models;
using Shared.DataTransferObject;
using Shared.DataTransferObjects;

namespace Estate
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c=>c.FullAddress,
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<Employee, EmployeeDto>();

            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<CompanyForUpdateDto, Company>();

            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
        }
    }
}
