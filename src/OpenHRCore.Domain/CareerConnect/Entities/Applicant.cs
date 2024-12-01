namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class Applicant : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
