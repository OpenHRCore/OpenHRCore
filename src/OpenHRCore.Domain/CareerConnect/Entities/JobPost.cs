namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class JobPost : OpenHRCoreBaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Location { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
