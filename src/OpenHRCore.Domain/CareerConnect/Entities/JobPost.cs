﻿using OpenHRCore.Domain.CareerConnect.Enums;

namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class JobPost : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public JobPostStatus JobPostStatus { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}