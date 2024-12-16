namespace OpenHRCore.Domain.CareerConnect.Entities
{
    public class Interview : OpenHRCoreBaseEntity
    {
        public required Guid JobApplicationId { get; set; }
        public virtual JobApplication? JobApplication { get; set; }
        public required DateTime InterviewDate { get; set; }
        public string? Feedback { get; set; }
        public string? Interviewer { get; set; }
        public int Step { get; set; } = 1;
    }
}
