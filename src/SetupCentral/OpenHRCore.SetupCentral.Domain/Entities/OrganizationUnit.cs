using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.SetupCentral.Domain.Entities
{
    /// <summary>
    /// Represents an organizational unit (OU) within the organization.
    /// </summary>
    public class OrganizationUnit : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique code of the organizational unit (e.g., OU001, OU002).
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the organizational unit (e.g., HR Department, IT Department).
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the organizational unit, detailing its function and role.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the parent organizational unit.
        /// If null, this unit is at the top level.
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the parent organizational unit.
        /// </summary>
        public OrganizationUnit? Parent { get; set; }

        /// <summary>
        /// Gets or sets the collection of child organizational units.
        /// </summary>
        public ICollection<OrganizationUnit> Children { get; set; } = new List<OrganizationUnit>();

        /// <summary>
        /// Gets or sets a value indicating whether this is a root organizational unit.
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        /// Gets or sets any additional notes related to the organizational unit.
        /// </summary>
        public string? Notes { get; set; }
    }
}
