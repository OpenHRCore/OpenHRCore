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
                .ForMember(dest => dest.OrganizationUnit,
                           opt => opt.MapFrom(src => src.OrganizationUnit != null ? src.OrganizationUnit.Name : null));
        }
    }
}
