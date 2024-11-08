namespace OpenHRCore.Domain.Workforce.Entities
{
    public class JobPosition : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required Guid JobLevelId { get; set; }
        public virtual JobLevel? JobLevel { get; set; }
        public required Guid JobGradeId { get; set; }
        public virtual JobGrade? JobGrade { get; set; }
        public required Guid OrganizationUnitId { get; set; }
        public virtual OrganizationUnit? OrganizationUnit { get; set; }
    }
}
