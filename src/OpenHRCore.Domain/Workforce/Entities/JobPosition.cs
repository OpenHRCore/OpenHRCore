namespace OpenHRCore.Domain.Workforce.Entities
{
    public class JobPosition : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string JobTitle { get; set; }
        public string? Description { get; set; }
        public required Guid OrganizationUnitId { get; set; }
        public virtual OrganizationUnit? OrganizationUnit { get; set; }
    }
}
