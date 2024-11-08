namespace OpenHRCore.Domain.Workforce.Entities
{
    public class OrganizationUnit : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public Guid? ParentOrganizationUnitId { get; set; }
        public virtual OrganizationUnit? ParentOrganizationUnit { get; set; } 
        public virtual ICollection<OrganizationUnit> SubOrganizationUnits { get; set; } = new List<OrganizationUnit>();
        public required int SortOrder { get; set; }
    }
}
