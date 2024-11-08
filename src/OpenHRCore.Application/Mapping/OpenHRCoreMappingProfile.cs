﻿using AutoMapper;
using OpenHRCore.Application.DTOs.JobGrade;
using OpenHRCore.Domain.EmployeeModule.Entities;

namespace OpenHRCore.Application.Mapping
{
    /// <summary>
    /// Defines AutoMapper profile for mapping between DTOs and domain entities in the WorkForce module.
    /// </summary>
    public class OpenHRCoreMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenHRCoreMappingProfile"/> class and configures all mappings.
        /// </summary>
        public OpenHRCoreMappingProfile()
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
            CreateMap<UpdateJobGradeRequest,JobGrade>();

            // Map JobGrade to DTOs
            CreateMap<JobGrade, CreateJobGradeResponse>();
            CreateMap<JobGrade, DeleteJobGradeResponse>();
            CreateMap<JobGrade, UpdateJobGradeResponse>();
            CreateMap<JobGrade, GetAllJobGradesResponse>();
            CreateMap<JobGrade, GetJobGradeByIdResponse>();
        }
    }
}
