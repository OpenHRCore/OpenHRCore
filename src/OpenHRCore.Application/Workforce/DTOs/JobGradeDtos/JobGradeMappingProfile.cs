using AutoMapper;
using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Application.Workforce.DTOs.JobGradeDtos
{
    public class JobGradeMappingProfile : Profile
    {
        public JobGradeMappingProfile()
        {
            //Request to Entity
            CreateMap<CreateJobGradeRequest, JobGrade>();
            CreateMap<UpdateJobGradeRequest, JobGrade>();

            //Entity to Response
            CreateMap<JobGrade, GetJobGradeResponse>();
        }
    }
}
