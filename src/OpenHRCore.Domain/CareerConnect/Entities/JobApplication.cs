using OpenHRCore.Domain.CareerConnect.Enums;

namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class JobApplication : OpenHRCoreBaseEntity
    {
        public required string JobPostId { get; set; }
        public required virtual JobPost JobPost { get; set; }
        public required string ApplicantId { get; set; }
        public required virtual Applicant Applicant { get; set; }
        public ApplicationStatus Status { get; set; }
        public virtual ICollection<ApplicantStage> ApplicantStages { get; set; } = new List<ApplicantStage>();
        public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
        public string? JobOfferId { get; set; }
        public virtual JobOffer? JobOffer { get; set; }
        public required string ResumeId { get; set; }
        public required virtual Resume Resume { get; set; }
        public string? CoverLetterId { get; set; }
        public virtual CoverLetter? CoverLetter { get; set; }
    }
}
