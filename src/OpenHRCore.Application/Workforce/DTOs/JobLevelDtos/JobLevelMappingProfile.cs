using AutoMapper;
using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Application.Workforce.DTOs.JobLevelDtos
{
    public class JobLevelMappingProfile : Profile
    {
        public JobLevelMappingProfile()
        {
            //Request to Entity
            CreateMap<CreateJobLevelRequest, JobLevel>();
            CreateMap<UpdateJobLevelRequest, JobLevel>();

            //Entity to Response
            CreateMap<JobLevel, GetJobLevelResponse>();

        }
    }
}
