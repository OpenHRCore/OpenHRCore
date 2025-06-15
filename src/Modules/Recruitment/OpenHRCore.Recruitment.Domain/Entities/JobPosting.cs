namespace OpenHRCore.Recruitment.Domain.Entities
{
    public class JobPosting : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string Title { get; set; }
        public string? Company { get; set; }
        public WorkplaceType? WorkplaceType { get; set; }
        public string? JobLocation { get; set; }
        public JobType? JobType { get; set; }
        public required string Description { get; set; }
        public JobPostingStatus Status { get; set; } = JobPostingStatus.Draft;
    }
}
