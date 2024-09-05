using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents an organizational unit (OU) within the organization.
    /// </summary>
    public class OrganizationUnit : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
        public virtual OrganizationUnit? Parent { get; set; }
        public ICollection<OrganizationUnit> Children { get; set; } = new List<OrganizationUnit>();
        public string? Notes { get; set; }
        public int SortOrder { get; set; }
        public bool IsRoot { get; set; }
    }
}
