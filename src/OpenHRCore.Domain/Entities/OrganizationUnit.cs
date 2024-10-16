namespace OpenHRCore.Domain.Entities
{
    /// <summary>
    /// Represents an organizational unit (OU) within the company structure.
    /// </summary>
    /// <remarks>
    /// An organizational unit is a division or department within the company hierarchy.
    /// It can have a parent unit and multiple child units, forming a tree-like structure.
    /// This class inherits from OpenHRCoreBaseEntity, which likely provides common properties
    /// such as Id, CreatedAt, UpdatedAt, etc.
    /// </remarks>
    public class OrganizationUnit : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique code identifying the organizational unit.
        /// </summary>
        /// <remarks>
        /// This code should be short, unique, and follow a consistent format (e.g., "OU001", "OU002").
        /// It is required and should be used for quick reference across the organization.
        /// </remarks>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the organizational unit.
        /// </summary>
        /// <remarks>
        /// This should be a clear, descriptive name (e.g., "Human Resources", "Finance Department").
        /// It is required and should accurately reflect the unit's role within the organization.
        /// </remarks>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of the organizational unit.
        /// </summary>
        /// <remarks>
        /// This field can contain information about the unit's responsibilities, goals, or any other relevant details.
        /// It is optional and can be used to provide additional context about the unit's purpose and function.
        /// </remarks>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the parent organizational unit, if any.
        /// </summary>
        /// <remarks>
        /// This property is nullable to accommodate root-level units that don't have a parent.
        /// It establishes the hierarchical relationship between units.
        /// </remarks>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the parent organizational unit.
        /// </summary>
        /// <remarks>
        /// This navigation property allows for easy traversal up the organizational hierarchy.
        /// It is virtual to support lazy loading in Entity Framework Core.
        /// </remarks>
        public virtual OrganizationUnit? Parent { get; set; }

        /// <summary>
        /// Gets or sets the collection of child organizational units.
        /// </summary>
        /// <remarks>
        /// This collection represents the direct subordinates of the current organizational unit.
        /// It is initialized as an empty list to prevent null reference exceptions.
        /// The virtual keyword supports lazy loading in Entity Framework Core.
        /// </remarks>
        public virtual ICollection<OrganizationUnit> Children { get; set; } = new List<OrganizationUnit>();

        /// <summary>
        /// Gets or sets additional notes or comments about the organizational unit.
        /// </summary>
        /// <remarks>
        /// This field can be used for any supplementary information that doesn't fit into other properties.
        /// It is optional and can be left null if not needed.
        /// </remarks>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the sort order of the organizational unit.
        /// </summary>
        /// <remarks>
        /// This property is used to define a custom ordering of units at the same hierarchical level.
        /// Lower values indicate higher precedence in sorting.
        /// It can be useful for displaying units in a specific order in user interfaces or reports.
        /// </remarks>
        public required int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this organizational unit is the root of the hierarchy.
        /// </summary>
        /// <remarks>
        /// Only one organizational unit should have this property set to true.
        /// It is used to identify the top-level unit in the organizational structure.
        /// Default value is false to ensure that most units are not considered root by default.
        /// </remarks>
        public bool IsRoot { get; set; } = false;
    }
}
