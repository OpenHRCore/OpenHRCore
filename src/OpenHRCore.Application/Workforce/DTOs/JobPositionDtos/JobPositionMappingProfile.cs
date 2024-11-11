using AutoMapper;
using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Application.Workforce.DTOs.JobPositionDtos
{
    public class JobPositionMappingProfile : Profile
    {
        public JobPositionMappingProfile()
        {
            //Request to Entity
            CreateMap<CreateJobPositionRequest, JobPosition>();
            CreateMap<UpdateJobPositionRequest, JobPosition>();

            //Entity to Response
            CreateMap<JobPosition, GetJobPositionResponse>()
                .ForMember(dest => dest.JobLevel,
                           opt => opt.MapFrom(src => src.JobLevel != null ? src.JobLevel.LevelName : null))
                .ForMember(dest => dest.JobGrade,
                           opt => opt.MapFrom(src => src.JobGrade != null ? src.JobGrade.GradeName : null))
                .ForMember(dest => dest.OrganizationUnit,
                           opt => opt.MapFrom(src => src.OrganizationUnit != null ? src.OrganizationUnit.Name : null));
        }
    }
}
