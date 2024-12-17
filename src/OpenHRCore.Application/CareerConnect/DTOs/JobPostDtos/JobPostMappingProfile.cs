using AutoMapper;
using OpenHRCore.Domain.CareerConnect.Entities;

namespace OpenHRCore.Application.CareerConnect.DTOs.JobPostDtos
{
    public class JobPostMappingProfile : Profile
    {
        public JobPostMappingProfile()
        {
            //Request to Entity
            CreateMap<CreateJobPostRequest, JobPost>();

            //Entity to Response
            CreateMap<JobPost, GetJobPostResponse>();
        }
    }
}
