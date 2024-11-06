namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class JobOffer : OpenHRCoreBaseEntity
    {
        public required string JobApplicationId { get; set; }
        public required virtual JobApplication JobApplication { get; set; }
        public decimal Salary { get; set; }
        public string? Position { get; set; }
    }
   
}
