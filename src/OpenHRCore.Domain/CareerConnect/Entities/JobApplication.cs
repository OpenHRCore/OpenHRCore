namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class JobApplication : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string JobPostId { get; set; }
        public virtual JobPost? JobPost { get; set; }
        public required string ApplicantId { get; set; }
        public virtual Applicant? Applicant { get; set; }
        public required  ApplicationStatus Status { get; set; }
        public string? JobOfferId { get; set; }
        public virtual JobOffer? JobOffer { get; set; }
        public required string ResumeId { get; set; }
        public virtual Resume? Resume { get; set; }
        public string? CoverLetterId { get; set; }
        public virtual CoverLetter? CoverLetter { get; set; }
        public virtual ICollection<ApplicantStage> ApplicantStages { get; set; } = new List<ApplicantStage>();
        public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }

}
