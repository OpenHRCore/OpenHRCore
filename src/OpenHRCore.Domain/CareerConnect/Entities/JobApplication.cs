namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class JobApplication : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required Guid JobPostId { get; set; }
        public virtual JobPost? JobPost { get; set; }
        public required Guid ApplicantId { get; set; }
        public virtual Applicant? Applicant { get; set; }
        public required Guid CurrentStageId { get; set; }
        public virtual ApplicationStage? CurrentStage { get; set; }
        public required Guid CurrentStageStatusId { get; set; }
        public virtual StageStatus? CurrentStageStatus { get; set; }
        public Guid? JobOfferId { get; set; }
        public virtual JobOffer? JobOffer { get; set; }
        public required Guid ResumeId { get; set; }
        public virtual Resume? Resume { get; set; }
        public Guid? CoverLetterId { get; set; }
        public virtual CoverLetter? CoverLetter { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }

}
