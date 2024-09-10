using AutoMapper;
using OpenHRCore.WorkForce.Application.DTOs.JobGrade;

namespace OpenHRCore.WorkForce.Application.Mapping
{
    /// <summary>
    /// Defines AutoMapper profile for mapping between DTOs and domain entities in the WorkForce module.
    /// </summary>
    public class WorkForceMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkForceMappingProfile"/> class and configures all mappings.
        /// </summary>
        public WorkForceMappingProfile()
        {
            ConfigureJobGradeMappings();
        }

        /// <summary>
        /// Configures mappings for JobGrade-related DTOs and entities.
        /// </summary>
        private void ConfigureJobGradeMappings()
        {
            // Map CreateJobGradeRequest to JobGrade
            CreateMap<CreateJobGradeRequest, JobGrade>();

            // Map JobGrade to DTOs
            CreateMap<JobGrade, CreateJobGradeResponse>();
            CreateMap<JobGrade, DeleteJobGradeResponse>();
            CreateMap<JobGrade, UpdateJobGradeResponse>();
            CreateMap<JobGrade, GetAllJobGradesResponse>();
            CreateMap<JobGrade, GetJobGradeByIdResponse>();
        }
    }
}
