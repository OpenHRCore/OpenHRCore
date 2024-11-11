using AutoMapper;
using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Application.Workforce.DTOs.EmployeeDtos
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            //Request to Entity
            CreateMap<CreateEmployeeRequest, Employee>();
            CreateMap<UpdateEmployeeRequest, Employee>();

            //Entity to Response
            CreateMap<Employee, GetEmployeeResponse>()
                .ForMember(dest => dest.JobLevelName,
                           opt => opt.MapFrom(src => src.JobLevel != null ? src.JobLevel.LevelName : null))
                .ForMember(dest => dest.JobGradeName,
                           opt => opt.MapFrom(src => src.JobGrade != null ? src.JobGrade.GradeName : null))
                .ForMember(dest => dest.JobTitleName,
                           opt => opt.MapFrom(src => src.JobPosition != null ? src.JobPosition.JobTitle : null))
                .ForMember(dest => dest.OrganizationUnitName,
                           opt => opt.MapFrom(src => src.OrganizationUnit != null ? src.OrganizationUnit.Name : null));
        }
    }
}
