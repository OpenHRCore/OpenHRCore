namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class Applicant : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public required string ContactInfo { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
