namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class Interview : OpenHRCoreBaseEntity
    {
        public required string JobApplicationId { get; set; }
        public required virtual JobApplication JobApplication { get; set; }
        public DateTime InterviewDate { get; set; }
        public required string Feedback { get; set; }
        public required string Interviewer { get; set; }
        public int Step { get; set; } // Step number to indicate if it's the first, second, etc. interview
    }
}
