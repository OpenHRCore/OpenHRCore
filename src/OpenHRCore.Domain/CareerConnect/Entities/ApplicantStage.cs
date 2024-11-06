using OpenHRCore.Domain.CareerConnect.Enums;

namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class ApplicantStage : OpenHRCoreBaseEntity
    {
        public required string JobApplicationId { get; set; }
        public required virtual JobApplication JobApplication { get; set; }
        public ApplicationStatus Stage { get; set; }
        public DateTime DateMovedToStage { get; set; }
        public required string Status { get; set; }
        public string? Comments { get; set; }
    }
}
