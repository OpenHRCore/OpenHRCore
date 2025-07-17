namespace OpenHRCore.Recruitment.Domain.Entities
{
    public class JobApplication : OpenHRCoreBaseEntity
    {
        public required Guid JobPostingId { get; set; }
        public virtual JobPosting? JobPosting { get; set; }
        public required Guid CandidateId { get; set; }
        public virtual Candidate? Candidate { get; set; }
        public required ApplicationChannel ApplicationChannel { get; set; }
        public required string ResumeFilePath { get; set; }
        public string? CoverLetterFilePath { get; set; }
        public DateTimeOffset ApplicationDate { get; set; } = DateTimeOffset.UtcNow;
        public required Guid CurrentStageId { get; set; }
        public virtual ApplicationStage? CurrentStage { get; set; }
        public required StageStatus CurrentStageStatus { get; set; } = StageStatus.Applied;

    }

}
