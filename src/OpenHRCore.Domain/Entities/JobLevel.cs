namespace OpenHRCore.Domain.Entities
{
    /// <summary>
    /// Represents a job level within the organizational hierarchy.
    /// </summary>
    /// <remarks>
    /// Job levels are used to define the structure and progression of positions within the company.
    /// They provide a standardized way to categorize roles based on responsibility, experience, and authority.
    /// </remarks>
    public class JobLevel : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique code identifying the job level.
        /// </summary>
        /// <remarks>
        /// The code should be a short, unique identifier (e.g., "L1", "L2").
        /// It is used for quick reference and should be consistent across the organization.
        /// </remarks>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the descriptive name of the job level.
        /// </summary>
        /// <remarks>
        /// The name should be clear and indicative of the level's position in the hierarchy
        /// (e.g., "Entry Level", "Senior Management").
        /// </remarks>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of the job level.
        /// </summary>
        /// <remarks>
        /// This field provides comprehensive information about the job level, including
        /// typical responsibilities, expected experience, and any other relevant details.
        /// It can be null if a detailed description is not necessary.
        /// </remarks>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the numerical order of the job level in the organizational hierarchy.
        /// </summary>
        /// <remarks>
        /// Lower values indicate more junior positions, while higher values represent more senior roles.
        /// This field is useful for sorting and displaying job levels in a structured order.
        /// </remarks>
        public required int SortOrder { get; set; }
    }
}
