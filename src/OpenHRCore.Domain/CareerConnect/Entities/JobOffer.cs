namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class JobOffer : OpenHRCoreBaseEntity
    {
        public required Guid JobApplicationId { get; set; }
        public virtual JobApplication? JobApplication { get; set; }
        public decimal Salary { get; set; }
        public string? Position { get; set; }
    }
}
