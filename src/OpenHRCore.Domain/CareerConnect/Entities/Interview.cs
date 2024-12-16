namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class Interview : OpenHRCoreBaseEntity
    {
        public required string JobApplicationId { get; set; }
        public required virtual JobApplication JobApplication { get; set; }
        public required DateTime InterviewDate { get; set; }
        public string? Feedback { get; set; }
        public string? Interviewer { get; set; }
        public int Step { get; set; } = 1;
    }
}
