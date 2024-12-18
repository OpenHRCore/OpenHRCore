﻿namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class ApplicantStage : OpenHRCoreBaseEntity
    {
        public required Guid JobApplicationId { get; set; }
        public virtual JobApplication? JobApplication { get; set; }
        public ApplicationStage Stage { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime? DateMovedToStage { get; set; }
        public string? Comments { get; set; }
    }
}
