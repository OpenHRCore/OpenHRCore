using OpenHRCore.Domain.CareerConnect.Enums;

namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class ApplicantStage : OpenHRCoreBaseEntity
    {
        public required string JobApplicationId { get; set; }
        public required virtual JobApplication JobApplication { get; set; }

        public ApplicationStage Stage { get; set; } // Enum for the stage of the application
        public ApplicationStatus Status { get; set; } // Enum for the status within the stage

        public DateTime DateMovedToStage { get; set; }
        public string? Comments { get; set; }
    }
}
