namespace OpenHRCore.Domain.Workforce.Entities
{
    public class Employee : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public required Gender Gender { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public string? Address { get; set; }
        public required Guid JobPositionId { get; set; }
        public virtual JobPosition? JobPosition { get; set; }
        public required Guid OrganizationUnitId { get; set; }
        public virtual OrganizationUnit? OrganizationUnit { get; set; }
    }
}
