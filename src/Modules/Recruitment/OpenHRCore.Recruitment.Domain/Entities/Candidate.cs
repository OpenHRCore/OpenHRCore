namespace OpenHRCore.Recruitment.Domain.Entities
{
    public class Candidate : OpenHRCoreBaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }

    }
}
